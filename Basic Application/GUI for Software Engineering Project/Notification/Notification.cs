using System.Windows.Forms;
using System.Drawing;

namespace GUI_for_Software_Engineering_Project.Notification
{
    class Notification
    {
        private static Notification Instance = null;
        private static readonly object padlock = new object();


        private Notification()
        {

        }

        public static Notification instance
        {
            get
            {
                lock (padlock)
                {
                    if (Instance == null)
                    {
                        Instance = new Notification();
                    }

                    return Instance;
                }
            }
        }

        public void showNotification(string title, string text, int time)
        {
            NotifyIcon notifyIcon = new NotifyIcon()
            {
                Visible = true,
                Icon = new Icon("popup.ico"),
                BalloonTipTitle = title,
                BalloonTipText = text
            };

            notifyIcon.ShowBalloonTip(time);
            notifyIcon.Dispose();
        }
    }
}
