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
        #region Const Field
        public const string URL = "https://www.wisetoto.com/";
        private readonly ChromeOptions _options;
        #endregion

        #region Constructor
        public Crawler()
        {
            _options = new ChromeOptions();
            _options.AddArgument("headless");
        }
        #endregion

        #region Functions
        public List<Match> CrawlMatches()
        {
            var xPath = By.XPath("//*[@id=\"gameinfo_pt1\"]/div[2]");
            var content = string.Empty;

            using (var driver = new ChromeDriver(_options))
            {
                driver.Url = URL;
                content = driver.FindElement(xPath).Text;
            }

            var result = new List<Match>();
            var contentSets = Split(content).ToList();

            foreach (var contentSet in contentSets)
                result.Add(Create(contentSet));

            return result;
        }

        private static Match Create(List<string> contentSet)
        {
            var text = contentSet.ElementAtOrDefault(3);

            if (string.IsNullOrEmpty(text))
                return null;

            if (text.StartsWith("H"))
                return CreateHandicapMatch(contentSet);
            else if (text.StartsWith("U"))
                return CreateUnderOverMatch(contentSet);
            else
                return CreateMatch(contentSet);
        }

        private static Match CreateMatch(List<string> contentSet)
        {
            if (!int.TryParse(contentSet[0], out int no)
                || !TryConvertDateTime(contentSet[1], DateTime.Now.Year, out DateTime date))
                return null;

            var category = contentSet[2];
            (var home, var homeScore) = SplitHomeStr(contentSet[3]);
            (var away, var awayScore) = SplitAwayStr(contentSet[5]);
            var win = GetAllocationPoint(contentSet[6]);
            var draw = GetAllocationPoint(contentSet[7]);
            var lose = GetAllocationPoint(contentSet[8]);
            var result = GetResult(contentSet.ElementAtOrDefault(9));

            return new Match(no, date, category, home, away, win, draw, lose, "", result);
        }

        private static Match CreateHandicapMatch(List<string> contentSet)
        {
            if (!int.TryParse(contentSet[0], out int no)
                  || !TryConvertDateTime(contentSet[1], DateTime.Now.Year, out DateTime date))
                return null;

            var category = contentSet[2];
            var content = contentSet[3];
            (var home, var homeScore) = SplitHomeStr(contentSet[4]);
            (var away, var awayScore) = SplitAwayStr(contentSet[6]);
            var win = GetAllocationPoint(contentSet[7]);
            var draw = GetAllocationPoint(contentSet[8]);
            var lose = GetAllocationPoint(contentSet[9]);
            var result = GetResult(contentSet.ElementAtOrDefault(10));

            return new Match(no, date, category, home, away, win, draw, lose, content, result);
        }

        private static Match CreateUnderOverMatch(List<string> contentSet)
        {
            if (!int.TryParse(contentSet[0], out int no)
                    || !TryConvertDateTime(contentSet[1], DateTime.Now.Year, out DateTime date))
                return null;

            var category = contentSet[2];
            var content = contentSet[3];
            var win = GetAllocationPoint(contentSet[7]);
            var lose = GetAllocationPoint(contentSet[9]);
            var home = contentSet[4];
            var away = contentSet[6];
            var result = GetResult(contentSet.ElementAtOrDefault(10));

            return new Match(no, date, category, home, away, win, 1, lose, content, result);
        }


        private static eMatchResult GetResult(string content)
        {
            switch (content)
            {
                case "홈승":
                case "핸디승":
                case "언더":
                    return eMatchResult.Win;

                case "홈패":
                case "핸디패":
                case "오버":
                    return eMatchResult.Lose;

                case "무승부":
                    return eMatchResult.Draw;

                default:
                    return eMatchResult.None;
            }
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

        private static IEnumerable<List<string>> Split(string content)
        {
            var splited = content.Split('\n')
                .Select(c => c.Trim(' ', '\r'))
                .ToList();

            var indexes = new List<int>();
            var pivot = 0;

            foreach (var str in splited)
            {
                if (TryConvertDateTime(str, DateTime.Now.Year, out DateTime date))
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

        private static bool TryConvertDateTime(string content, int year, out DateTime date)
        {
            date = default;

            var splited = content.Split(new char[] { '.', ' ', '(', ')', ':', }, StringSplitOptions.RemoveEmptyEntries);

            if (splited.Count() != 5
                || !int.TryParse(splited[0], out int month)
                || !int.TryParse(splited[1], out int day)
                || !int.TryParse(splited[3], out int hour)
                || !int.TryParse(splited[4], out int minute))
                return false;

            date = new DateTime(year, month, day, hour, minute, 0);
            return true;
        }
        #endregion
    }
}
