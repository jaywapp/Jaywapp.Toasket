using Jaywapp.Toasket.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Toasket.Service
{
    public class Crawler
    {
        public const string URL = "https://www.wisetoto.com/";

        private ChromeOptions Options { get; }

        public Crawler()
        {
            Options = new ChromeOptions();
            Options.AddArgument("headless");
        }

        public IEnumerable<Match> CrawlMatches(int year, int protoNo)
        {
            var xPath = By.XPath("//*[@id=\"gameinfo_pt1\"]/div[2]");
            var content = string.Empty;

            using (var driver = new ChromeDriver(Options))
            {
                driver.Url = URL;
                content = driver.FindElement(xPath).Text;
            }

            var contentSets = Split(content, year).ToList();

            foreach (var contentSet in contentSets)
            {
                yield return Create(contentSet, year, protoNo);
            }
        }

        private static Match Create(List<string> contentSet, int year, int protoNo)
        {
            var text = contentSet.ElementAtOrDefault(3);

            if (string.IsNullOrEmpty(text))
                return null;

            if (text.StartsWith("H"))
                return CreateHandicapMatch(contentSet, year, protoNo);
            else if (text.StartsWith("U"))
                return CreateUnderOverMatch(contentSet, year, protoNo);
            else
                return CreateMatch(contentSet, year, protoNo);
        }

        private static Match CreateMatch(List<string> contentSet, int year, int protoNo)
        {
            if (!int.TryParse(contentSet[0], out int no)
                || !ContentConverter.TryConvertDateTime(contentSet[1], year, out DateTime date))
                return null;

            /*
            var contest = contentSet[2];
            var category = GetCategory(contest);
            */

            (var home, var homeScore) = SplitHomeStr(contentSet[3]);
            (var away, var awayScore) = SplitAwayStr(contentSet[5]);
            var win = GetAllocationPoint(contentSet[6]);
            var draw = GetAllocationPoint(contentSet[7]);
            var lose = GetAllocationPoint(contentSet[8]);

            return new Match(no, date, home, away, win, draw, lose);
        }

        private static Match CreateHandicapMatch(List<string> contentSet, int year, int protoNo)
        {
            if (!int.TryParse(contentSet[0], out int no)
                  || !ContentConverter.TryConvertDateTime(contentSet[1], year, out DateTime date))
                return null;

            /*
            var contest = contentSet[2];
            var category = GetCategory(contest);

            var handicap = GetNumber(contentSet[3]);
            */
            var content = contentSet[3];
            (var home, var homeScore) = SplitHomeStr(contentSet[4]);
            (var away, var awayScore) = SplitAwayStr(contentSet[6]);

            var win = GetAllocationPoint(contentSet[7]);
            var draw = GetAllocationPoint(contentSet[8]);
            var lose = GetAllocationPoint(contentSet[9]);

            return new Match(no, date, home, away, win, draw, lose, content);
        }

        private static Match CreateUnderOverMatch(List<string> contentSet, int year, int protoNo)
        {
            if (!int.TryParse(contentSet[0], out int no)
                    || !ContentConverter.TryConvertDateTime(contentSet[1], year, out DateTime date))
                return null;

            /*
            var contest = contentSet[2];
            var category = GetCategory(contest);

            var underOverScore = GetNumber(contentSet[3]);
            */

            var content = contentSet[3];
            var win = GetAllocationPoint(contentSet[7]);
            var lose = GetAllocationPoint(contentSet[9]);

            var home = contentSet[4];
            var away = contentSet[6];

            return new Match(no, date, home, away, win, 1, lose, content);
        }

        private static double GetAllocationPoint(string str)
        {
            var text = str.Replace("↑", "").Replace("↓", "");
            return double.TryParse(text, out double pt) ? pt : 1;
        }

        private static (string, double) SplitHomeStr(string str)
        {
            var splited = str.Split(' ');

            if (splited.Count() != 2)
                return (str, 0);

            var name = splited[0];
            var score = double.TryParse(splited[1], out double value) ? value : 0;

            return (name, score);
        }

        private static (string, double) SplitAwayStr(string str)
        {
            var splited = str.Split(' ');

            if (splited.Count() != 2)
                return (str, 0);

            var name = splited[1];
            var score = double.TryParse(splited[0], out double value) ? value : 0;

            return (name, score);
        }

        private static IEnumerable<List<string>> Split(string content, int year)
        {
            var splited = content.Split('\n')
                .Select(c => c.Trim(' ', '\r'))
                .ToList();

            var indexes = new List<int>();
            var pivot = 0;

            foreach (var str in splited)
            {
                if (ContentConverter.TryConvertDateTime(str, year, out DateTime date))
                    indexes.Add(pivot);

                pivot++;
            }

            indexes = indexes.Select(i => i -= 1).ToList();

            var contentSet = new List<string>();

            for (int i = 0; i < splited.Count(); i++)
            {
                if (indexes.Contains(i))
                {
                    if (contentSet.Any())
                    {
                        yield return contentSet;
                        contentSet = new List<string>();
                    }
                }

                contentSet.Add(splited[i]);
            }
        }
    }
}
