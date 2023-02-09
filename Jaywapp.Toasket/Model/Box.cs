using Jaywapp.Toasket.Helper;
using Jaywapp.Toasket.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Jaywapp.Toasket.Model
{
    public class Box : IXmlFileSerializable, IAccount
    {
        #region Properties
        public DateTime Created { get; set; }
        public List<Match> Picks { get; set; } = new List<Match>();
        public int Expenditure { get; set; } = 0;
        public int Income => GetIncome();
        public int Profit => GetProfit();
        #endregion

        #region Constructor
        public Box(IEnumerable<Match> picks, int money)
        {
            Created = DateTime.Now;
            Picks = picks.ToList();
            Expenditure = money;
        }

        public Box() : this(Enumerable.Empty<Match>(), 0)
        {
        }
        #endregion

        #region Functions
        public double GetRatio() => Calculater.Multiply(Picks, p => p.GetRatio());

        private int GetIncome() => (int)(GetRatio() * Expenditure);

        private int GetProfit() => Income - Expenditure;

        public bool IsHitted()
        {
            return Picks != null && Picks.Any() 
                && Picks.All(p => p.IsHitted());
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(Box));

            element.Add(
                new XAttribute(nameof(Created), Created),
                new XAttribute(nameof(Expenditure), Expenditure));

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
            if (element.TryGetAttributeInteger(nameof(Expenditure), out int money))
                Expenditure = money;

            var picks = element
                .Elements(nameof(Match))
                .Select(e=> e.Load<Match>())
                .ToList();

            Picks = picks;
        }
        #endregion
    }
}
