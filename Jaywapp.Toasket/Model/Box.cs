using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Jaywapp.Toasket.Model
{
    public class Box : IXmlFileSerializable
    {
        #region Properties
        public DateTime Created { get; set; }
        public List<Match> Picks { get; set; } = new List<Match>();
        public int Money { get; set; }
        #endregion

        #region Constructor
        public Box(IEnumerable<Match> picks, int money)
        {
            Created = DateTime.Now;
            Picks = picks.ToList();
            Money = money;
        }

        public Box() : this(Enumerable.Empty<Match>(), 0)
        {
        }
        #endregion

        #region Functions
        public double GetRatio() => Calculater.Multiply(Picks, p => p.GetRatio());

        public int GetReturnMoney() => (int)(GetRatio() * Money);

        public XElement Serialize()
        {
            var element = new XElement(nameof(Box));

            element.Add(
                new XAttribute(nameof(Created), Created),
                new XAttribute(nameof(Money), Money));

            foreach (var pick in Picks)
                element.Add(pick.Serialize());

            return element;
        }

        public void Serialize(XElement element)
        {
            if (element.Name != nameof(Box))
                return;

            if (element.TryGetAttributeDateTime(nameof(Created), out DateTime created))
                Created = created;
            if (element.TryGetAttributeInteger(nameof(Money), out int money))
                Money = money;

            var picks = element
                .Elements(nameof(Match))
                .Select(e=> e.Load<Match>())
                .ToList();

            Picks = picks;
        }
        #endregion
    }
}
