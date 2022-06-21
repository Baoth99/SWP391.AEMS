using System;
using System.Threading;

namespace AEMS.Simulator.Core
{
    public class ConsoleAppConfig
    {
        public static void KeepConsoleAppRunning(Action onShutdown)
        {
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            Console.WriteLine("Press CTRL + C or CTRL + Break to exit...");

            Console.CancelKeyPress += (sender, e) =>
            {
                onShutdown();
                e.Cancel = true;
                manualResetEvent.Set();
            };

            manualResetEvent.WaitOne();
        }
    }
}
