using Jaywapp.Toasket.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaywapp.Toasket.Model
{
    public class AnalysisMonthlyResult : IAccount
    {
        #region Properties
        /// <inheritdoc/>
        public int Income { get; }
        /// <inheritdoc/>
        public int Expenditure { get; }
        /// <inheritdoc/>
        public int Profit { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="income"></param>
        /// <param name="expenditure"></param>
        public AnalysisMonthlyResult(int income, int expenditure)
        {
            Income = income;
            Expenditure = expenditure;
            Profit = income - expenditure;
        }
        #endregion
    }
}
