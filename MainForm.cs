using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouthMeadowGeneralStore.Dialogs;
using YouthMeadowGeneralStore.Models;
using YouthMeadowGeneralStore.Services;
using YouthMeadowGeneralStore.Utilities;

namespace YouthMeadowGeneralStore
{
    public partial class MainForm : Form
    {
        private readonly SoundManager _soundManager;
        private readonly GamePersistenceService _persistenceService;
        private readonly Random _random = new Random();
        private bool _bankruptcyTriggered;
        private bool _initialBackgroundStarted;
        private decimal _money = 1000m;
        private string _currentBackground = "背景0.mp3";
        private bool _isNewGame = true;
        private string _currentDate = "7月1日";
        private readonly Dictionary<string, int> _dailyPurchase = new Dictionary<string, int>();
        private decimal _salesModifier = 1m;
        private bool _relationshipEstablished;
        private bool _matchmakingDone;
        private bool _event520Done;
        private int _tobaccoOffenseCount;
        private List<Product> _warehouse;
        private List<Product> _productList;
        private List<ShelfSlot> _shelfSlots;

        public MainForm(SoundManager soundManager)
        {
            _soundManager = soundManager;
            _persistenceService = new GamePersistenceService(AppDomain.CurrentDomain.BaseDirectory);
            _warehouse = new List<Product>();
            _productList = CreateBaseProducts();
            _shelfSlots = CreateInitialShelfSlots();

            InitializeComponent();
            WireEvents();

            LoadOrCreateSave();
            CheckFestivals(true);
            StartGame();
        }

        public IReadOnlyList<Product> ProductList => _productList;
        public string InitialBackgroundMusic => _currentBackground;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            displayList.FocusFirstItem();
            if (_initialBackgroundStarted)
            {
                return;
            }

            _initialBackgroundStarted = true;
            _soundManager.PlayBackground(_currentBackground);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F1:
                    OpenPurchaseDialog();
                    return true;
                case Keys.F2:
                    ShowWarehouse();
                    return true;
                case Keys.F3:
                    ShowShelfManager();
                    return true;
                case Keys.F4:
                    StartBusiness();
                    return true;
                case Keys.F8:
                    _soundManager.AdjustVolume(-10);
                    return true;
                case Keys.F9:
                    _soundManager.AdjustVolume(10);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public string FormatMoney(decimal amount)
        {
            return amount == decimal.Truncate(amount)
                ? amount.ToString("0", CultureInfo.InvariantCulture)
                : amount.ToString("0.##", CultureInfo.InvariantCulture);
        }

        public void PurchaseSelectedProduct(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex >= _productList.Count)
            {
                return;
            }

            var product = _productList[selectedIndex];
            var input = InputDialog.Prompt(this, "进货数量", $"请输入进货数量（{product.Name} 单价：{FormatMoney(product.Buy)}元）：", "1");
            if (input == null)
            {
                return;
            }

            if (!int.TryParse(input.Trim(), out var quantity) || quantity <= 0)
            {
                ShowError("请输入有效的正整数！");
                PurchaseSelectedProduct(selectedIndex);
                return;
            }

            var totalCost = product.Buy * quantity;
            if (_money < totalCost)
            {
                ShowError($"金钱不足，需要{FormatMoney(totalCost)}元，当前只有{FormatMoney(_money)}元");
                PurchaseSelectedProduct(selectedIndex);
                return;
            }

            _money -= totalCost;
            _dailyPurchase[product.Name] = GetDailyPurchase(product.Name) + quantity;
            CheckBankruptcy();
            for (var i = 0; i < quantity; i++)
            {
                _warehouse.Add(product.Clone());
            }

            UpdateMoneyLabel();
            AutoSave();
            _soundManager.PlayEffect("purchase");
        }

        private void WireEvents()
        {
            btnPurchase.Click += (sender, args) => OpenPurchaseDialog();
            btnWarehouse.Click += (sender, args) => ShowWarehouse();
            btnShelf.Click += (sender, args) => ShowShelfManager();
            btnStartBusiness.Click += async (sender, args) => await StartBusinessAsync();
        }

        private void LoadOrCreateSave()
        {
            if (!_persistenceService.SaveExists())
            {
                _isNewGame = true;
                SaveGame();
                return;
            }

            try
            {
                var saveData = _persistenceService.Load();
                _money = saveData.Money;
                _warehouse = saveData.Warehouse ?? new List<Product>();
                _shelfSlots = (saveData.ShelfSlots ?? CreateInitialShelfSlots()).Select(slot => slot.Clone()).ToList();
                _currentDate = string.IsNullOrWhiteSpace(saveData.CurrentDate) ? "7月1日" : saveData.CurrentDate;
                _currentBackground = string.IsNullOrWhiteSpace(saveData.BackgroundMusic) ? "背景0.mp3" : saveData.BackgroundMusic;
                _matchmakingDone = saveData.MatchmakingDone;
                _relationshipEstablished = saveData.RelationshipEstablished;
                _event520Done = saveData.Event520Done;
                _isNewGame = false;
            }
            catch
            {
                _money = 1000m;
                _warehouse = new List<Product>();
                _shelfSlots = CreateInitialShelfSlots();
                _currentDate = "7月1日";
                _currentBackground = "背景0.mp3";
                _isNewGame = true;
            }
        }

        private void SaveGame()
        {
            var saveData = new GameSaveData
            {
                Money = _money,
                Warehouse = _warehouse.Select(item => item.Clone()).ToList(),
                ShelfSlots = _shelfSlots.Select(slot => slot.Clone()).ToList(),
                CurrentDate = _currentDate,
                BackgroundMusic = _currentBackground,
                MatchmakingDone = _matchmakingDone,
                RelationshipEstablished = _relationshipEstablished,
                Event520Done = _event520Done
            };

            try
            {
                _persistenceService.Save(saveData);
            }
            catch (Exception ex)
            {
                ShowError("存档失败：" + ex.Message);
            }
        }

        private void AutoSave()
        {
            Task.Run((Action)SaveGame);
        }

        private void StartGame()
        {
            UpdateMoneyLabel();
            if (_isNewGame)
            {
                UpdateDisplay(new[]
                {
                    "又到了一年毕业季，你和全国几千万大学生一同踏入社会。然而，迎接你们的是全国范围内爆发的大学生就业困境，你也无奈地加入了毕业即失业的行列。",
                    "在学校的那些年，你逐渐看清了社会的运行规律。资本家总是利用各种规则钻劳动法的空子，最大限度压榨劳动者的价值。",
                    "你的父母从小就给你灌输各种积极向上的观念，希望你日后能拥有自己的店铺，成为行业翘楚，在族谱上留下光辉的一笔。而小时候，你为了一根辣条都要精打细算，小心翼翼。看着身为小卖部老板的大爷，一边收着你的钱，一边在背后向你父母打小报告，你早已受够了这种憋屈的生活。于是，你下定决心要开一家属于自己的小卖部，把曾经受过的气都还回去。",
                    "你多次去找村书记，却屡屡碰壁。但在不懈努力下，你终于敲开了村书记的家门。你满怀信心地向村书记保证，自己一定能做出一番大事业，带领村子走向小康，大幅提升村民的生活质量。你描绘的宏伟蓝图不仅打动了自己，也赢得了村书记的信任。村书记便在离你大爷小卖部不远处，为你安排了一间小平房，还拍着你的肩膀说：“小伙子，我看好你，父老乡亲的美好生活就指望你了。”",
                    "就这样，你拿着大学期间剩下的 1000 元启动资金，进驻了村书记安排的小平房。所幸这里以前也是小卖部，货架、柜台等设施一应俱全。你心想：只要进点货，就可以大展宏图了，老天一定是眷顾我的。接下来，你就要开始进货了。"
                });
            }
            else
            {
                UpdateDisplay(new[]
                {
                    "欢迎回来，继续您的小卖部创业之旅吧！",
                    $"现在是：{_currentDate}，您的资产：{FormatMoney(_money)}元。"
                });
            }
        }

        private void OpenPurchaseDialog()
        {
            var purchaseDialog = new PurchaseDialog(this);
            purchaseDialog.Show(this);
        }

        private void ShowWarehouse()
        {
            var lines = new List<string>
            {
                $"今天是：{_currentDate}",
                $"当前金钱：{FormatMoney(_money)}元",
                string.Empty
            };

            if (_warehouse.Count == 0)
            {
                lines.Add("仓库当前没有库存商品");
                UpdateDisplay(lines);
                return;
            }

            lines.Add("仓库库存：");
            foreach (var group in _warehouse.GroupBy(item => item.Name))
            {
                var product = group.First();
                lines.Add($"├ {group.Key} ×{group.Count()}  进价:{FormatMoney(product.Buy)}元  售价:{FormatMoney(product.Sell)}元");
            }

            UpdateDisplay(lines);
        }

        private void ShowShelfManager()
        {
            var options = new List<string> { "【全部上架】" };
            for (var i = 0; i < _shelfSlots.Count; i++)
            {
                var slot = _shelfSlots[i];
                if (!slot.Unlocked)
                {
                    options.Add($"{i + 1}. 已锁定，需花费{FormatMoney(slot.UnlockCost)}元解锁");
                }
                else if (slot.Product == null)
                {
                    options.Add($"{i + 1}. 暂时空闲：0");
                }
                else
                {
                    options.Add($"{i + 1}. {slot.Product.Name}：{slot.Quantity}");
                }
            }

            var selection = ChoiceDialog.Prompt(this, "货架管理", "请选择操作：", options);
            if (selection == null)
            {
                return;
            }

            if (selection.Value == 0)
            {
                var shelfUpdated = AutoShelveAll();
                if (shelfUpdated)
                {
                    _soundManager.PlayEffect("shelf");
                    AutoSave();
                }

                RefreshShelfDisplay();
                return;
            }

            HandleShelfSlot(selection.Value - 1);
            RefreshShelfDisplay();
        }

        private async Task StartBusinessAsync()
        {
            if (!btnStartBusiness.Enabled)
            {
                return;
            }

            StartBusiness();
            await Task.CompletedTask;
        }

        private void StartBusiness()
        {
            if (!btnStartBusiness.Enabled)
            {
                return;
            }

            if (!AskYesNo("开始营业确认", "你确认已经准备好，并正式开门营业吗？\n\n营业后将迎来新的一天，做好准备迎接顾客了吗？"))
            {
                return;
            }

            btnStartBusiness.Enabled = false;
            _soundManager.PlayEffect("business");
            if (_currentDate == "7月1日")
            {
                _currentBackground = "背景1.mp3";
                _soundManager.PlayBackground(_currentBackground);
            }

            UpdateDisplay(new[] { "新的一天开始了，营业中……顾客即将上门！" });
            _ = TriggerRandomEventAfterDelayAsync();
        }

        private async Task TriggerRandomEventAfterDelayAsync()
        {
            await Task.Delay(3000);
            TriggerRandomEvent();
        }

        private void CheckBankruptcy()
        {
            if (_money >= 0 || _bankruptcyTriggered)
            {
                return;
            }

            _bankruptcyTriggered = true;
            ShowError(
                "入不敷出的你只能关闭小店，黯然神伤地离开这片生你养你的故土，加入了去大城市打工的行列。\n" +
                "感谢试玩本游戏，祝您下次取得更好成绩，本游戏处于测试阶段，开发者：面壁者虫二，如有宝贵建议，请添加QQ2653138927或发送邮件至2653138927@qq.com",
                "很遗憾");
            _persistenceService.DeleteSaveIfExists();
            Close();
        }

        private void UpdateMoneyLabel()
        {
            lblMoney.Text = $"当前金钱：{FormatMoney(_money)}";
        }

        private void UpdateDisplay(IEnumerable<string> contents)
        {
            displayList.SetItems(contents);
            if (Visible)
            {
                displayList.FocusFirstItem();
            }
        }

        private void RefreshShelfDisplay()
        {
            var lines = new List<string> { "货架上目前存放以下商品 (每个格子只能上架一种商品):" };
            for (var i = 0; i < _shelfSlots.Count; i++)
            {
                var slot = _shelfSlots[i];
                if (!slot.Unlocked)
                {
                    lines.Add($"{i + 1}. 已锁定，需花费{FormatMoney(slot.UnlockCost)}元解锁");
                }
                else if (slot.Product == null)
                {
                    lines.Add($"{i + 1}. 暂时空闲：0");
                }
                else
                {
                    lines.Add($"{i + 1}. {slot.Product.Name}：{slot.Quantity}");
                }
            }

            UpdateDisplay(lines);
        }

        private void HandleShelfSlot(int index)
        {
            if (index < 0 || index >= _shelfSlots.Count)
            {
                return;
            }

            var slot = _shelfSlots[index];
            if (!slot.Unlocked)
            {
                if (!AskYesNo("扩充货架确认", $"你确定要花费{FormatMoney(slot.UnlockCost)}元扩充该货架吗？"))
                {
                    return;
                }

                if (_money < slot.UnlockCost)
                {
                    ShowError($"金钱不足，扩充需要{FormatMoney(slot.UnlockCost)}元，当前只有{FormatMoney(_money)}元");
                    return;
                }

                _money -= slot.UnlockCost;
                slot.Unlocked = true;
                UpdateMoneyLabel();
                CheckBankruptcy();
                ShowInfo("扩充成功！");
                AutoSave();
                return;
            }

            if (slot.Product != null)
            {
                HandleExistingShelfProduct(slot);
            }
            else
            {
                HandleEmptyShelfSlot(slot);
            }
        }

        private void HandleExistingShelfProduct(ShelfSlot slot)
        {
            if (slot.Quantity >= 500)
            {
                RemoveShelfStock(slot, slot.Quantity);
                return;
            }

            var selection = ChoiceDialog.Prompt(this, "选择操作", $"该货架当前有 {slot.Quantity} 个 {slot.Product.Name}。\n请选择操作：", new[] { "上架商品", "下架商品" });
            if (selection == null)
            {
                return;
            }

            if (selection.Value == 0)
            {
                AddShelfStock(slot);
            }
            else
            {
                RemoveShelfStock(slot, slot.Quantity);
            }
        }

        private void AddShelfStock(ShelfSlot slot)
        {
            var available = _warehouse.Count(item => item.Name == slot.Product.Name);
            var maxAllowed = Math.Min(available, 500 - slot.Quantity);
            if (maxAllowed <= 0)
            {
                ShowInfo($"该货架已达到500个{slot.Product.Name}的上架上限。");
                return;
            }

            var input = InputDialog.Prompt(this, "上架数量", $"请输入补充上架数量（最多 {maxAllowed}）", maxAllowed.ToString(CultureInfo.InvariantCulture));
            if (input == null)
            {
                return;
            }

            if (!int.TryParse(input, out var quantity) || quantity <= 0 || quantity > maxAllowed)
            {
                ShowError("请输入有效的正整数！");
                return;
            }

            RemoveWarehouseProducts(slot.Product.Name, quantity);
            slot.Quantity += quantity;
            _soundManager.PlayEffect("Discard");
            AutoSave();
        }

        private void RemoveShelfStock(ShelfSlot slot, int defaultQuantity)
        {
            var input = InputDialog.Prompt(this, "下架数量", $"请输入下架数量（最多 {slot.Quantity}）", defaultQuantity.ToString(CultureInfo.InvariantCulture));
            if (input == null)
            {
                return;
            }

            if (!int.TryParse(input, out var quantity) || quantity <= 0 || quantity > slot.Quantity)
            {
                ShowError("请输入有效的正整数！");
                return;
            }

            for (var i = 0; i < quantity; i++)
            {
                _warehouse.Add(slot.Product.Clone());
            }

            slot.Quantity -= quantity;
            if (slot.Quantity == 0)
            {
                slot.Product = null;
            }

            _soundManager.PlayEffect("Discard");
            AutoSave();
        }

        private void HandleEmptyShelfSlot(ShelfSlot slot)
        {
            var grouped = _warehouse.GroupBy(item => item.Name).ToList();
            if (grouped.Count == 0)
            {
                ShowInfo("仓库内没有商品可上架。");
                return;
            }

            var options = grouped.Select(group => $"{group.Key} (数量：{group.Count()})").ToList();
            var selection = ChoiceDialog.Prompt(this, "货架上架", "请选择要上架的商品", options);
            if (selection == null)
            {
                return;
            }

            var groupSelection = grouped[selection.Value];
            var maxAllowed = Math.Min(groupSelection.Count(), 500);
            var input = InputDialog.Prompt(this, "上架数量", $"请输入上架数量（最多 {maxAllowed}）", maxAllowed.ToString(CultureInfo.InvariantCulture));
            if (input == null)
            {
                return;
            }

            if (!int.TryParse(input, out var quantity) || quantity <= 0 || quantity > maxAllowed)
            {
                ShowError("请输入有效的正整数！");
                return;
            }

            slot.Product = groupSelection.First().Clone();
            slot.Quantity = quantity;
            RemoveWarehouseProducts(slot.Product.Name, quantity);
            _soundManager.PlayEffect("shelf");
            AutoSave();
        }

        private bool AutoShelveAll()
        {
            var changed = false;
            var groups = _warehouse.GroupBy(item => item.Name).ToList();
            foreach (var group in groups)
            {
                var slot = _shelfSlots.FirstOrDefault(item => item.Unlocked && item.Product != null && item.Product.Name == group.Key)
                    ?? _shelfSlots.FirstOrDefault(item => item.Unlocked && item.Product == null);

                if (slot == null)
                {
                    continue;
                }

                var quantity = group.Count();
                if (slot.Product == null)
                {
                    var add = Math.Min(quantity, 500);
                    if (add <= 0)
                    {
                        continue;
                    }

                    slot.Product = group.First().Clone();
                    slot.Quantity = add;
                    RemoveWarehouseProducts(group.Key, add);
                    changed = true;
                }
                else
                {
                    var add = Math.Min(quantity, 500 - slot.Quantity);
                    if (add <= 0)
                    {
                        continue;
                    }

                    slot.Quantity += add;
                    RemoveWarehouseProducts(group.Key, add);
                    changed = true;
                }
            }

            return changed;
        }

        private void RemoveWarehouseProducts(string productName, int quantity)
        {
            var removed = 0;
            _warehouse = _warehouse.Where(item =>
            {
                if (removed < quantity && item.Name == productName)
                {
                    removed++;
                    return false;
                }

                return true;
            }).ToList();
        }

        private int GetDailyPurchase(string productName)
        {
            return _dailyPurchase.TryGetValue(productName, out var quantity) ? quantity : 0;
        }

        private static List<Product> CreateBaseProducts()
        {
            return new List<Product>
            {
                new Product { Name = "雪碧", Buy = 1m, Sell = 3m },
                new Product { Name = "可乐", Buy = 1m, Sell = 3m },
                new Product { Name = "笔记本", Buy = 1m, Sell = 2m },
                new Product { Name = "钢笔", Buy = 1m, Sell = 2m },
                new Product { Name = "旺仔牛奶", Buy = 1m, Sell = 4m },
                new Product { Name = "苏打饼", Buy = 2m, Sell = 4m },
                new Product { Name = "乐视薯片", Buy = 2m, Sell = 3m },
                new Product { Name = "白象方便面", Buy = 2m, Sell = 4m },
                new Product { Name = "文件袋", Buy = 2m, Sell = 3m },
                new Product { Name = "王老吉", Buy = 2m, Sell = 4m },
                new Product { Name = "早餐面包", Buy = 2m, Sell = 5m },
                new Product { Name = "果粒橙", Buy = 2m, Sell = 5m },
                new Product { Name = "珠江啤酒", Buy = 5m, Sell = 8m },
                new Product { Name = "红塔山", Buy = 7m, Sell = 10m },
                new Product { Name = "二锅头", Buy = 8m, Sell = 10m },
                new Product { Name = "黄鹤楼", Buy = 15m, Sell = 20m },
                new Product { Name = "合金甩棍", Buy = 20m, Sell = 30m },
                new Product { Name = "菜刀", Buy = 20m, Sell = 35m },
                new Product { Name = "软中华", Buy = 60m, Sell = 75m }
            };
        }

        private static List<ShelfSlot> CreateInitialShelfSlots()
        {
            var unlockCosts = new Dictionary<int, decimal>
            {
                [3] = 1000m,
                [4] = 3000m,
                [5] = 4500m,
                [6] = 6000m,
                [7] = 7500m,
                [8] = 9000m,
                [9] = 10500m
            };

            var slots = new List<ShelfSlot>();
            for (var i = 0; i < 10; i++)
            {
                slots.Add(new ShelfSlot
                {
                    Unlocked = i < 3,
                    Product = null,
                    Quantity = 0,
                    UnlockCost = i < 3 ? 0m : unlockCosts[i]
                });
            }

            return slots;
        }

        private DialogResult ShowMessage(string text, string title, MessageBoxIcon icon, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return MessageBox.Show(this, text, title, buttons, icon);
        }

        private void ShowInfo(string text, string title = "提示")
        {
            ShowMessage(text, title, MessageBoxIcon.Information);
        }

        private void ShowWarning(string text, string title = "提示")
        {
            ShowMessage(text, title, MessageBoxIcon.Warning);
        }

        private void ShowError(string text, string title = "错误")
        {
            ShowMessage(text, title, MessageBoxIcon.Error);
        }

        private bool AskYesNo(string title, string text)
        {
            return ShowMessage(text, title, MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

    }
}
