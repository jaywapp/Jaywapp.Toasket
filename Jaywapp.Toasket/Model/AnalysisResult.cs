using Jaywapp.Toasket.Interface;

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
        #endregion

        #region Constructor
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="income"></param>
        /// <param name="expenditure"></param>
        public AnalysisResult(int income, int expenditure)
        {
            Income = income;
            Expenditure = expenditure;
            Profit = income - expenditure;
        }
        #endregion
    }
}
