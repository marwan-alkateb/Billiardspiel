using System;

namespace Tracking.Server
{
    internal class Program
    {
        private static void PrintBanner()
        {
            Console.WriteLine(@"
  _____            _   _             ___                      
 |_   _| _ __ _ __| |_(_)_ _  __ _  / __| ___ _ ___ _____ _ _ 
   | || '_/ _` / _| / / | ' \/ _` | \__ \/ -_) '_\ V / -_) '_|
   |_||_| \__,_\__|_\_\_|_||_\__, | |___/\___|_|  \_/\___|_|  
                             |___/                            
");
            Console.WriteLine("Write 'calibrate' to calibrate the tracking engine.");
            Console.WriteLine("Write 'start' to start the tracking engine and sending data");
            Console.WriteLine("Write 'exit' to cloe the engine");
        }

        public static void Main(string[] args)
        {
            PrintBanner();
            new TrackingServer().Open();
        }
    }
}
