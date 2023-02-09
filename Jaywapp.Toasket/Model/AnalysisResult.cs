﻿using Jaywapp.Toasket.Interface;
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
        public List<AnalysisMonthlyResult> Monthlys { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="income"></param>
        /// <param name="expenditure"></param>
        public AnalysisResult(int income, int expenditure, IEnumerable<AnalysisMonthlyResult> monthlys)
        {
            Income = income;
            Expenditure = expenditure;
            Profit = income - expenditure;
            Monthlys = monthlys.ToList();
        }
        #endregion
    }
}
