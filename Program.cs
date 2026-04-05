using System;
using System.Windows.Forms;
using YouthMeadowGeneralStore.Services;

namespace YouthMeadowGeneralStore
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var soundManager = new SoundManager())
            using (var form = new MainForm(soundManager))
            {
                soundManager.PlayStartupSequenceBlocking();
                Application.Run(form);
            }
        }
    }
}
