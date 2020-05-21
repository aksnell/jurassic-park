using System;
using System.IO;
using System.Collections.Generic;

namespace JurassicPark
{
    public class Logger
    {
        private List<string> Logs;
        private bool Debug;

        public Logger(bool debug)
        {
            Logs = new List<string>();
            Debug = debug;
        }

        public void Write(string log)
        {
            string newLog = $"{DateTime.Now}: {log};

            if (Debug)
            {
                Console.WriteLine(newLog);
            }

            Logs.Add(newLog);
        }

        public void Save()
        {
            using (TextWriter tw = new StreamWriter("log.txt"))
            {
                foreach (String log in Logs)
                {
                    tw.WriteLine(log);
                }
            }
        }
    }
}
