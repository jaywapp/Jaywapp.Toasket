using Jaywapp.Toasket.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaywapp.Toasket.Service
{
    public static class MatchRepositoryLoader
    {
        public static IEnumerable<Match> Load(string path)
        {
            var lines = ReadLines(path);

            foreach(var line in lines.Skip(1))
            {
                var splited = line.Split('\t');

                var noStr = splited[0];
                var category = splited[1];
                var timeStr = splited[2];
                var typeStr = splited[3];
                (var home, var away) = Split(splited[4]);

                if (!int.TryParse(noStr, out int no)
                    || !DateTime.TryParse(timeStr, out DateTime time))
                    continue;
            }

            yield break;
        }

        private static (string, string) Split(string content)
        {
            var splited = content.Split(new string[] { "vs" }, StringSplitOptions.RemoveEmptyEntries);
            return (splited[0].Trim(), splited[1].Trim());
        }

        private static IEnumerable<string> ReadLines(string path)
        {
            var result = new List<string>();

            using (var reader = new StreamReader(path))
            {
                var line = "";

                while((line = reader.ReadLine()) != null)
                    result.Add(line);
            }

            return result;
        }
    }
}
