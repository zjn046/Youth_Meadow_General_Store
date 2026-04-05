using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouthMeadowGeneralStore.Configuration;
using YouthMeadowGeneralStore.Models;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private Task EndBusinessDayAsync()
        {
            if (_bankruptcyTriggered || IsDisposed || Disposing)
            {
                return Task.CompletedTask;
            }

            if (_currentDate == "6月30日")
            {
                var finalReport = SimulateSales();
                UpdateMoneyLabel();
                UpdateDisplay(finalReport);
                AutoSave();
                FinishGame();
                return Task.CompletedTask;
            }

            var salesReport = SimulateSales();
            _dailyPurchase.Clear();
            IncrementDate();
            UpdateMoneyLabel();
            UpdateDisplay(salesReport);
            AutoSave();
            CheckBankruptcy();
            if (_bankruptcyTriggered || IsDisposed || Disposing)
            {
                return Task.CompletedTask;
            }

            ScheduleEnableStartBusinessButton();
            return Task.CompletedTask;
        }

        private void ScheduleEnableStartBusinessButton()
        {
            Task.Run(async () =>
            {
                await Task.Delay(3000);
                if (IsDisposed || Disposing)
                {
                    return;
                }

                try
                {
                    BeginInvoke((Action)(() =>
                    {
                        if (!IsDisposed && !Disposing)
                        {
                            btnStartBusiness.Enabled = true;
                        }
                    }));
                }
                catch
                {
                }
            });
        }

        private List<string> SimulateSales()
        {
            decimal totalIncome = 0m;
            var salesSummary = new Dictionary<string, (int Quantity, decimal Income)>();

            foreach (var slot in _shelfSlots.Where(item => item.Unlocked && item.Product != null && item.Quantity > 0))
            {
                var available = slot.Quantity;
                int soldCount;
                if (available == 1)
                {
                    soldCount = _random.NextDouble() < (double)(_salesModifier * 0.7m) ? 1 : 0;
                }
                else
                {
                    var baseSold = _random.Next(1, available + 1);
                    soldCount = Math.Min(available, (int)Math.Floor(baseSold * _salesModifier));
                }

                if (soldCount <= 0)
                {
                    continue;
                }

                var income = soldCount * slot.Product.Sell;
                totalIncome += income;
                if (salesSummary.ContainsKey(slot.Product.Name))
                {
                    var existing = salesSummary[slot.Product.Name];
                    salesSummary[slot.Product.Name] = (existing.Quantity + soldCount, existing.Income + income);
                }
                else
                {
                    salesSummary[slot.Product.Name] = (soldCount, income);
                }

                slot.Quantity -= soldCount;
                if (slot.Quantity == 0)
                {
                    slot.Product = null;
                }
            }

            var report = new List<string> { "今天的营业结束，销售情况如下：" };
            if (salesSummary.Count == 0)
            {
                report.Add("今天没有任何销售。");
            }
            else
            {
                foreach (var item in salesSummary)
                {
                    report.Add($"{item.Key} | 售出 {item.Value.Quantity} 件 | 收入 {FormatMoney(item.Value.Income)} 元");
                }

                report.Add($"今日总收入：{FormatMoney(totalIncome)} 元");
            }

            _money += totalIncome;
            _salesModifier = 1m;
            return report;
        }

        private void IncrementDate()
        {
            if (!MonthDay.TryParse(_currentDate, out var currentDate))
            {
                ShowError($"日期解析错误：{_currentDate}");
                currentDate = new MonthDay(7, 1);
            }

            _currentDate = currentDate.NextDay(GameAppConfig.MonthDays).ToDisplayString();
            CheckFestivals();
            AutoSave();
        }

        private bool TryParseCurrentDate(out int month, out int day)
        {
            if (MonthDay.TryParse(_currentDate, out var currentDate))
            {
                month = currentDate.Month;
                day = currentDate.Day;
                return true;
            }

            month = 7;
            day = 1;
            return false;
        }

        private bool ShouldTriggerTobaccoEvent()
        {
            var cigaretteNames = new HashSet<string> { "红塔山", "黄鹤楼", "软中华" };
            var total = _warehouse.Count(item => cigaretteNames.Contains(item.Name));
            total += _shelfSlots.Where(item => item.Product != null && cigaretteNames.Contains(item.Product.Name)).Sum(item => item.Quantity);
            return total >= 500;
        }

        private void ConfiscateAllCigarettes()
        {
            var cigaretteNames = new HashSet<string> { "红塔山", "黄鹤楼", "软中华" };
            _warehouse = _warehouse.Where(item => !cigaretteNames.Contains(item.Name)).ToList();
            foreach (var slot in _shelfSlots.Where(item => item.Product != null && cigaretteNames.Contains(item.Product.Name)))
            {
                slot.Product = null;
                slot.Quantity = 0;
            }
        }

        private void FinishGame()
        {
            _soundManager.PlayBackground(GameAppConfig.DefaultBackgroundTrack);
            ShowInfo(
                "这一年，在村里经营小卖部的日子，忙碌而充实。无数个清晨，赶在阳光洒满村落之前，我便开始整理货物、擦拭货架；\n" +
                "无数个夜晚，伴着月色与星光，我才结束一天的营业，盘点着一天的收支。每一次与村民的交流，每一笔小小的交易，都饱含着辛勤的汗水。\n" +
                "如今，终于靠着这份坚持与努力，赚到了人生的第一桶金，心中满是感慨与欣慰。闲暇时刷短视频，一句“生活不止眼前的苟且，还有诗和远方”如同一束光，照亮了我疲惫却又渴望自由的心。\n" +
                "是时候给自己放个假了。我轻轻关上了小卖部的门，将一张写有“休息中”的告示贴在门上，仿佛与这段忙碌时光短暂告别。背上早已准备好的书包，带着对未知的期待，我迈出家门，开启一段充满快乐与惊喜的旅程。\n" +
                "此刻，风轻云淡，前路充满未知与可能，而我，正走在追逐诗与远方的路上。");
            ShowInfo(
                "亲爱的玩家，衷心感谢您抽出时间试玩我们的游戏。在这段时光里，我们共同创造了许多难忘的瞬间。虽然此次体验暂告一段落，但我们期待着未来的再次相逢，携手探索更多精彩。\n" +
                "本游戏目前仍处于测试阶段。您的反馈对游戏的成长至关重要，如果您有任何宝贵建议或想法，欢迎添加 QQ 2653138927 与我们交流。");
            _persistenceService.DeleteSaveIfExists();
            Close();
        }

        private void TryPlayBackground(string fileName)
        {
            SetBackgroundTrack(fileName, true);
        }

        private void SetBackgroundTrack(string fileName, bool playNow)
        {
            if (!_soundManager.BackgroundExists(fileName))
            {
                return;
            }

            _currentBackground = fileName;
            if (playNow)
            {
                _soundManager.PlayBackground(fileName);
            }
        }
    }
}
