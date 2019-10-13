using System;
using System.IO;
using System.Linq;

namespace ResponsibleSystem.Common.CosmosDb.ImportTool.Helpers
{
    public class Prompter
    {
        private StreamWriter fileWriter;
        private string LogFileName;

        private Prompter()
        {
            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
            LogFileName = $"logs/importer_{DateTime.Now.ToString("yyyy_MM_dd-HH_mm")}.log";
            fileWriter = new StreamWriter(File.Open(LogFileName, FileMode.OpenOrCreate));
        }

        private static Prompter _default;
        public static Prompter Default
        {
            get
            {
                return _default ?? (_default = new Prompter());
            }
        }

        public void Info(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            fileWriter.Write(text);
            fileWriter.Write("\n");
            Console.ResetColor();
        }

        public void Info(string format, params object[] parameters)
        {
            Info(string.Format(format, parameters));
        }

        public void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            fileWriter.Write(text);
            fileWriter.Write("\n");
            Console.ResetColor();
        }

        internal bool ReadYN(string question, bool defaultValue)
        {
            string defaultAnswer = defaultValue ? "Y" : "N";

            question = question + $" Y/N [{defaultAnswer}]";

            do
            {
                Info(question);
                Write("> ");
                string answer = Console.ReadLine();

                fileWriter.Write(answer);
                fileWriter.Write("\n");

                bool saidNothing = string.IsNullOrWhiteSpace(answer);
                if (saidNothing)
                {
                    return defaultValue;
                }

                bool saidYes = answer.ToLower() == "y";
                bool saidNo = answer.ToLower() == "n";

                if (saidYes || saidNo)
                {
                    return saidYes;
                }

            } while (true);
        }

        public void Error(string format, params object[] parameters)
        {
            Error(string.Format(format, parameters));
        }

        public void WriteLine(string text = "")
        {
            Console.WriteLine(text);
            fileWriter.Write(text);
            fileWriter.Write("\n");
        }

        public void Write(string text)
        {
            Console.Write(text);
            fileWriter.Write(text);
        }

        public void WriteLine(string format, params object[] parameters)
        {
            WriteLine(string.Format(format, parameters));
        }

        public int ReadInt(string question, int min, int max)
        {
            int choosed;
            bool parsed;
            bool parsedAndInRange;

            do
            {
                Info(question);
                Write("> ");
                string answer = Console.ReadLine();

                fileWriter.Write(answer);
                fileWriter.Write("\n");

                parsed = int.TryParse(answer, out choosed);

                parsedAndInRange = parsed && choosed >= min && choosed <= max;
            } while (!parsedAndInRange);
            return choosed;
        }

        public int? ReadIntOrEmpty(params int[] allowedValues)
        {
            int choosedValue;
            bool parsed;
            do
            {
                Write("> ");
                string answer = Console.ReadLine();

                fileWriter.Write(answer);
                fileWriter.Write("\n");

                if (string.IsNullOrWhiteSpace(answer))
                {
                    return null;
                }

                parsed = int.TryParse(answer, out choosedValue);

                if (parsed && allowedValues.Contains(choosedValue))
                {
                    return choosedValue;
                }

            } while (true);
        }

        public string ReadLine()
        {
            Write("> ");
            string answer = Console.ReadLine();
            fileWriter.Write(answer);
            fileWriter.Write("\n");
            return answer;
        }

        public void Close()
        {
            fileWriter.Flush();
            fileWriter.Close();
        }
    }
}
