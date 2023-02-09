using Jaywapp.Toasket.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Toasket.Model
{
    public class AnalysisResult : IAccount
    {
        #region Properties
        /// <inheritdoc/>
        public int Income { get; }
        /// <inheritdoc/>
        public int Expenditure { get; }
        /// <inheritdoc/>
        public int Profit { get; }

        /// <summary>
        /// 월별 Result
        /// </summary>
        public Dictionary<DateTime, AnalysisMonthlyResult> Monthlys { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="income"></param>
        /// <param name="expenditure"></param>
        public AnalysisResult(int income, int expenditure, Dictionary<DateTime, AnalysisMonthlyResult> monthlys)
        {
            Income = income;
            Expenditure = expenditure;
            Profit = income - expenditure;
            Monthlys = monthlys;
        }
        #endregion
    }
}
