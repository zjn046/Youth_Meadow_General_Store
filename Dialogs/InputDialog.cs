using System.Drawing;
using System.Windows.Forms;

namespace YouthMeadowGeneralStore.Dialogs
{
    public static class InputDialog
    {
        public static string Prompt(IWin32Window owner, string title, string message, string defaultValue)
        {
            using (var dialog = new Form())
            using (var label = new Label())
            using (var textBox = new TextBox())
            using (var okButton = new Button())
            using (var cancelButton = new Button())
            {
                dialog.Text = title;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;
                dialog.ClientSize = new Size(380, 150);

                label.AutoSize = false;
                label.Text = message;
                label.Location = new Point(12, 12);
                label.Size = new Size(356, 48);

                textBox.Text = defaultValue ?? string.Empty;
                textBox.Location = new Point(12, 65);
                textBox.Size = new Size(356, 23);

                okButton.Text = "确定";
                okButton.DialogResult = DialogResult.OK;
                okButton.Location = new Point(212, 105);

                cancelButton.Text = "取消";
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Location = new Point(293, 105);

                dialog.Controls.Add(label);
                dialog.Controls.Add(textBox);
                dialog.Controls.Add(okButton);
                dialog.Controls.Add(cancelButton);
                dialog.AcceptButton = okButton;
                dialog.CancelButton = cancelButton;

                return dialog.ShowDialog(owner) == DialogResult.OK ? textBox.Text : null;
            }
        }
    }
}
