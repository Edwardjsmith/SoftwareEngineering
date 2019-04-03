using System.Windows.Forms;
using System.Drawing;

namespace GUI_for_Software_Engineering_Project.Notification
{
    class Notification
    {
        private static Notification Instance = null;
        private static readonly object padlock = new object();

        private NotifyIcon notifyIcon;
        private int notificationTime = 10000;

        private Notification()
        {
            notifyIcon = new NotifyIcon()
            {
                Visible = true,
                Icon = new Icon("popup.ico")
            };
        }
        ~Notification()
        {
            notifyIcon.Dispose();
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

        public void showNotification(string title, string text)
        {
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = text;
            notifyIcon.ShowBalloonTip(notificationTime);
        }

        public void showNotification(string title)
        {
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = null;
            notifyIcon.ShowBalloonTip(notificationTime);
        }

    }
}
