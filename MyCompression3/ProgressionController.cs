using System.Threading;
using System.Windows.Forms;

namespace MyCompression
{
    public static class ProgressionController
    {
        private static Thread _progressionThread;

        public static void Show()
        {
            _progressionThread = new Thread(Run);
            _progressionThread.Start();
        }

        public static void Abort()
        {
            _progressionThread.Abort();
        }

        private static void Run()
        {
            Application.Run(new ProgessBox());
        }
    }
}
