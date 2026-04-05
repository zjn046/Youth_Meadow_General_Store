using System.Linq;
using System.Threading.Tasks;
using YouthMeadowGeneralStore.Configuration;
using YouthMeadowGeneralStore.Models;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm
    {
        private async Task<bool> HandleSpecialDateEventsAsync(MonthDay currentDate)
        {
            foreach (var definition in _specialDateEventDefinitions)
            {
                if (definition.ShouldExecute(currentDate) && await definition.ExecuteAsync())
                {
                    return true;
                }
            }

            return false;
        }

        private async Task<bool> HandleValentineDayAsync()
        {
            if (_relationshipEstablished)
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        ShowInfo(
                            "情人节的浪漫气息在空气中氤氲，你鼓足勇气邀约心仪女生共赴约会。" +
                            "漫步街头，温柔的晚风轻拂，彼此的话语中藏着甜蜜与心动。" +
                            "这场约会，像一根无形的红线，将两颗心悄然拉近，让你们的关系迎来全新的进展。",
                            "情人节到啦");
                        break;
                    case 2:
                        ShowInfo(
                            "情人节的甜蜜约会如期而至，你与心仪女生相约共进晚餐。餐后，" +
                            "她眉眼弯弯地提议买杯奶茶，随后又走进商场闲逛购物，夜幕降临时还一同走进影院。" +
                            "从美食到逛街，再到观影，这一路的消费如流水，最终让你的钱包瞬间“瘦身”，现金锐减1500元。",
                            "情人节到啦");
                        break;
                    default:
                        ShowInfo(
                            "在情人节这个充满诗意的日子，你和心仪女生踏上郊游之旅。" +
                            "然而旅途中，未知的状况让你逐渐失去耐心，情绪变得暴躁易怒，" +
                            "时不时的不耐烦尽数展现在她面前。女生看着这样的你，眼底满是失望，" +
                            "可心中对你尚存期待，决定再给彼此一些时间，盼望着能看到你更好的一面。",
                            "情人节到啦");
                        break;
                }
            }

            _money -= 100m;
            await EndBusinessDayAsync();
            return true;
        }

        private async Task<bool> HandleNewYearSaleAsync()
        {
            ShowInfo(
                "新年到了，为迎接新年的到来，批发市场举行大酬宾，你也趁机去捡了个漏，捡了不少的好东西回来。",
                "新年大酬宾");
            var goods = new[]
            {
                new Product { Name = "蛋散", Buy = 15m, Sell = 18m },
                new Product { Name = "九江煎堆", Buy = 2m, Sell = 3m },
                new Product { Name = "油角", Buy = 30m, Sell = 35m },
                new Product { Name = "糖莲子", Buy = 35m, Sell = 55m },
                new Product { Name = "糖冬瓜", Buy = 5m, Sell = 10m }
            };

            return await HandleRandomMarketAsync(goods);
        }

        private async Task<bool> HandleFlowerMarketAsync()
        {
            ShowInfo("好一朵迎春花，人人都爱它，迎春花市到了，快去进货吧。", "迎春花市");
            var flowers = new[]
            {
                new Product { Name = "四季橘", Buy = 80m, Sell = 100m },
                new Product { Name = "柑橘", Buy = 130m, Sell = 150m },
                new Product { Name = "富贵竹", Buy = 10m, Sell = 20m },
                new Product { Name = "百合花", Buy = 50m, Sell = 55m },
                new Product { Name = "桃花", Buy = 30m, Sell = 66m }
            };

            return await HandleRandomMarketAsync(flowers);
        }

        private async Task<bool> HandleMatchmakingAsync()
        {
            _matchmakingDone = true;
            if (AskYesNo("相亲市场", "新年伊始，相亲市场再次火爆。隔壁王寡妇为你介绍了一桩亲事，你是否愿意答应呢？"))
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        _relationshipEstablished = true;
                        ShowInfo("你与那位女孩子交谈甚欢，很快彼此便确立了关系，似乎命运注定你们相遇。", "相亲成功");
                        break;
                    case 2:
                        _money -= 500m;
                        ShowInfo("你与小姑娘共享晚餐，聊得非常投缘，现金减少500元。", "相亲结果");
                        break;
                    default:
                        _money -= 1000m;
                        ShowWarning("小姑娘对你完全不感兴趣，表现得冷淡疏离，最终这段相亲完全未能有任何进展，现金减少1000元。", "相亲失败");
                        break;
                }
            }
            else
            {
                ShowInfo("你错过了这次相亲，虽然遗憾，但也许这就是命运的安排。错过的，或许正是更好的。", "相亲未参与");
            }

            _money -= 100m;
            await EndBusinessDayAsync();
            return true;
        }

        private async Task<bool> Handle520Async()
        {
            _event520Done = true;
            TryPlayBackground("520.mp3");
            if (AskYesNo(
                "520浪漫时刻",
                "520浪漫时刻悄然降临，爱意正待诉说。若真心倾慕她，何不用一份专属定制的心意，传递心底炽热的情感？\n\n" +
                "现推出1314元定制马克杯，将你们的专属回忆、甜蜜昵称或浪漫誓言镌刻杯身，让每一次举杯都盛满爱意。此刻，你是否愿意为她开启这份独特的浪漫？"))
            {
                switch (_random.Next(1, 4))
                {
                    case 1:
                        ShowInfo("心动回响：她被这份独一无二的礼物深深打动，瞬间坠入爱河，与你甜蜜牵手，正式确定恋人关系。", "520结果");
                        break;
                    case 2:
                        ShowInfo("友达之上：新奇的礼物令她眼前一亮，但感情仍保持在朋友阶段，继续以独特的方式相处。", "520结果");
                        break;
                    default:
                        ShowWarning("遗憾擦肩：这份定制未能契合她的审美与期待，她认为彼此并不合适，最终你们分道扬镳。", "520结果");
                        break;
                }
            }
            else
            {
                if (_random.Next(1, 3) == 1)
                {
                    ShowWarning("错过缘分：没有收到礼物的她，满心失望与愤怒，误以为你只是虚情假意，这段感情就此画上遗憾的句号。", "520结果");
                }
                else
                {
                    ShowWarning("错失良机：她因你的“精打细算”错失惊喜，内心失落，原本可能发展的感情，也在无声中消逝 。", "520结果");
                }
            }

            _money -= 100m;
            await EndBusinessDayAsync();
            return true;
        }

        private async Task<bool> HandleRandomMarketAsync(Product[] products)
        {
            foreach (var item in products.OrderBy(_ => _random.Next()).Take(_random.Next(1, products.Length + 1)))
            {
                _warehouse.Add(item.Clone());
                _money -= 100m;
                await EndBusinessDayAsync();
                if (_bankruptcyTriggered || IsDisposed || Disposing)
                {
                    return true;
                }
            }

            return true;
        }
    }
}
