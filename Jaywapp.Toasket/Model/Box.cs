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
        /// <summary>
        /// 생성시간
        /// </summary>
        public DateTime Created { get; set; }
        
        /// <summary>
        /// 선택 매치 목록
        /// </summary>
        public List<Match> Picks { get; set; } = new List<Match>();
        
        /// <inheritdoc/>
        public int Expenditure { get; set; } = 0;
        
        /// <inheritdoc/>
        public int Income => GetIncome();
        
        /// <inheritdoc/>
        public int Profit => GetProfit();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="picks"></param>
        /// <param name="money"></param>
        public Box(IEnumerable<Match> picks, int money)
        {
            Created = DateTime.Now;
            Picks = picks.ToList();
            Expenditure = money;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Box() : this(Enumerable.Empty<Match>(), 0)
        {
        }
        #endregion

        #region Functions
        /// <summary>
        /// 배당률을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public double GetRatio() => Calculator.Multiply(Picks, p => p.GetRatio());

        /// <summary>
        /// 총 수익을 반환합니다.
        /// </summary>
        /// <returns></returns>
        private int GetIncome() => (int)(GetRatio() * Expenditure);

        /// <summary>
        /// 총 순수익을 반환합니다.
        /// </summary>
        /// <returns></returns>
        private int GetProfit() => Income - Expenditure;

        /// <summary>
        /// 적중 결과를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public bool IsHitted()
        {
            return Picks != null && Picks.Any() 
                && Picks.All(p => p.IsHitted());
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
