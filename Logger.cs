using System;
using System.IO;
using System.Collections.Generic;

namespace JurassicPark
{
    public class Logger
    {
        private string User;
        private List<string> Logs;
        private bool Debug;

        public Logger(string user, bool debug)
        {

            User = user;
            Logs = new List<string>();

            Debug = debug;
            if (debug)
            {
                Info("enabled debug mode");
                Console.WriteLine("\tTo disable change logger(name, true) to logger(name, false) in Program.cs");
            }
        }

        public void Info(string log)
        {
            string newLog = $"DEBUG: User: {User} - {log}";

            if (Debug)
            {
                Console.WriteLine(newLog);
            }

            Logs.Add($"{DateTime.Now}: {newLog}");
        }

        public void Error(string err)
        {
            string newErr = $"ERROR: {err}";
            Console.WriteLine($"\t{newErr}");
            Logs.Add($"{DateTime.Now}: {newErr}");
        }

        public void Save()
        {
            Info("saving logs to log.txt");
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
