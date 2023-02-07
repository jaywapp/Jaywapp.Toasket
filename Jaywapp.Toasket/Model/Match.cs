﻿using Jaywapp.Toasket.Interface;
using System;
using System.Xml.Linq;

namespace Jaywapp.Toasket.Model
{
    public class Match : IXmlFileSerializable
    {
        #region Properties
        public int No { get; set; }
        public DateTime Time { get; set; }
        public string Category { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public double Win { get; set; }
        public double Draw { get; set; }
        public double Lose { get; set; }
        public string Comment { get; set; }
        public eMatchResult Pick { get; set; }
        public eMatchResult Result { get; set; }
        #endregion

        #region Constructor
        public Match(
            int no, DateTime time,
            string category, string home, string away, 
            double win, double draw, double lose,
            string comment,
            eMatchResult result = eMatchResult.None)
        {
            No = no;
            Time = time;
            Home = home;
            Away = away;
            Category = category;
            Win = win;
            Draw = draw;
            Lose = lose;
            Result = result;

            Comment = comment ?? string.Empty;
        }

        public Match() : this(0, default, "", "", "", 1, 1, 1, "") { }
        #endregion

        #region Functions
        public double GetRatio()
        {
            switch (Pick)
            {
                case eMatchResult.Win: return Win;
                case eMatchResult.Draw: return Draw;
                case eMatchResult.Lose: return Lose;
                case eMatchResult.None:
                default:
                    return 1;
            }
        }

        public Match Copy()
        {
            var match = new Match(No, Time, Category, Home, Away, Win, Draw, Lose, Comment, Result);
            match.Pick = Pick;

            return match;
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(Match));

            element.Add(
                new XAttribute(nameof(No), No),
                new XAttribute(nameof(Time), Time),
                new XAttribute(nameof(Category), Category),
                new XAttribute(nameof(Home), Home),
                new XAttribute(nameof(Away), Away),
                new XAttribute(nameof(Win), Win),
                new XAttribute(nameof(Draw), Draw),
                new XAttribute(nameof(Lose), Lose),
                new XAttribute(nameof(Comment), Comment),
                new XAttribute(nameof(Pick), Pick));

            if (Result != eMatchResult.None)
                element.Add(new XAttribute(nameof(Result), Result));

            return element;
        }

        public void Serialize(XElement element)
        {
            if (element.Name != nameof(Match))
                return;

            if (element.TryGetAttributeInteger(nameof(No), out int no))
                No = no;
            if (element.TryGetAttributeDateTime(nameof(Time), out DateTime time))
                Time = time;
            if (element.TryGetAttributeValue(nameof(Category), out string category))
                Category = category;
            if (element.TryGetAttributeValue(nameof(Home), out string home))
                Home = home;
            if (element.TryGetAttributeValue(nameof(Away), out string away))
                Away = away;
            if (element.TryGetAttributeDouble(nameof(Win), out double win))
                Win = win;
            if (element.TryGetAttributeDouble(nameof(Draw), out double draw))
                Draw = draw;
            if (element.TryGetAttributeDouble(nameof(Lose), out double lose))
                Lose = lose;
            if (element.TryGetAttributeValue(nameof(Comment), out string comment))
                Comment = comment;
            if (element.TryGetAttributeEnum(nameof(Pick), out eMatchResult pick))
                Pick = pick;
            if (element.TryGetAttributeEnum(nameof(Result), out eMatchResult result))
                Result = result;
        }

        public bool IsHitted()
        {
            if (Pick == eMatchResult.None || Result == eMatchResult.None)
                return false;

            return Pick == Result;
        }
        #endregion
    }
}
