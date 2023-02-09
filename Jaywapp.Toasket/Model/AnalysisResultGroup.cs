using Jaywapp.Toasket.Interface;
using System;
using System.Collections.Generic;

namespace Jaywapp.Toasket.Model
{
    public class AnalysisResultGroup : IAccount
    {
        #region Properties
        /// <inheritdoc/>
        public int Income { get; }
        /// <inheritdoc/>
        public int Expenditure { get; }
        /// <inheritdoc/>
        public int Profit { get; }

        /// <summary>
        /// 하위 Result
        /// </summary>
        public Dictionary<DateTime, AnalysisResult> Children { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="income"></param>
        /// <param name="expenditure"></param>
        public AnalysisResultGroup(int income, int expenditure, Dictionary<DateTime, AnalysisResult> children)
        {
            Income = income;
            Expenditure = expenditure;
            Profit = income - expenditure;
            Children = children;
        }
        #endregion
    }
}
