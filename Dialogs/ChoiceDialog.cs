using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace YouthMeadowGeneralStore.Dialogs
{
    public sealed class ChoiceDialog : Form
    {
        private readonly ListBox _listBox;

        private ChoiceDialog(string title, string message, IEnumerable<string> options)
        {
            Text = title;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ClientSize = new Size(520, 380);

            var label = new Label
            {
                AutoSize = false,
                Text = message,
                Location = new Point(12, 12),
                Size = new Size(496, 36)
            };

            _listBox = new ListBox
            {
                Location = new Point(12, 56),
                Size = new Size(496, 260)
            };
            _listBox.Items.AddRange(options.Cast<object>().ToArray());
            _listBox.DoubleClick += OnConfirm;

            var okButton = new Button
            {
                Text = "确定",
                DialogResult = DialogResult.OK,
                Location = new Point(352, 332)
            };
            okButton.Click += OnConfirm;

            var cancelButton = new Button
            {
                Text = "取消",
                DialogResult = DialogResult.Cancel,
                Location = new Point(433, 332)
            };

            Controls.Add(label);
            Controls.Add(_listBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            AcceptButton = okButton;
            CancelButton = cancelButton;
            Shown += (sender, args) =>
            {
                if (_listBox.Items.Count > 0 && _listBox.SelectedIndex < 0)
                {
                    _listBox.SelectedIndex = 0;
                }

                _listBox.Focus();
            };
        }

        public static int? Prompt(IWin32Window owner, string title, string message, IEnumerable<string> options)
        {
            using (var dialog = new ChoiceDialog(title, message, options))
            {
                return dialog.ShowDialog(owner) == DialogResult.OK && dialog._listBox.SelectedIndex >= 0
                    ? (int?)dialog._listBox.SelectedIndex
                    : null;
            }
        }

        private void OnConfirm(object sender, EventArgs e)
        {
            if (_listBox.SelectedIndex >= 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
