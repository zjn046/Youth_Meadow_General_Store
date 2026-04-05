using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace YouthMeadowGeneralStore.Dialogs
{
    public sealed class PurchaseDialog : Form
    {
        private readonly MainForm _owner;
        private readonly ListBox _listBox;

        public PurchaseDialog(MainForm owner)
        {
            _owner = owner;
            Text = "进货列表";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(400, 300);
            KeyPreview = true;

            _listBox = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 220
            };
            _listBox.DoubleClick += ConfirmSelection;
            _listBox.KeyDown += OnDialogKeyDown;

            var tipLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 36,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "双击列表内商品或按回车键进行购买，按 ESC 退出"
            };

            var closeButton = new Button
            {
                Text = "关闭",
                Dock = DockStyle.Bottom,
                Height = 36
            };
            closeButton.Click += (sender, args) => Close();

            Controls.Add(closeButton);
            Controls.Add(tipLabel);
            Controls.Add(_listBox);
            Load += (sender, args) => RefreshProducts();
            Shown += (sender, args) =>
            {
                if (_listBox.Items.Count > 0 && _listBox.SelectedIndex < 0)
                {
                    _listBox.SelectedIndex = 0;
                }

                _listBox.Focus();
            };
            KeyDown += OnDialogKeyDown;
        }

        public void RefreshProducts()
        {
            _listBox.Items.Clear();
            _listBox.Items.AddRange(_owner.ProductList
                .Select(product => $"{product.Name} 进货价{_owner.FormatMoney(product.Buy)}元, 售价{_owner.FormatMoney(product.Sell)}元")
                .Cast<object>()
                .ToArray());
        }

        private void OnDialogKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConfirmSelection(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }

        private void ConfirmSelection(object sender, EventArgs e)
        {
            if (_listBox.SelectedIndex >= 0)
            {
                var currentIndex = _listBox.SelectedIndex;
                _owner.PurchaseSelectedProduct(currentIndex);
                if (!IsDisposed && _listBox.Items.Count > 0)
                {
                    _listBox.SelectedIndex = Math.Min(currentIndex, _listBox.Items.Count - 1);
                    _listBox.Focus();
                }
            }
        }
    }
}
