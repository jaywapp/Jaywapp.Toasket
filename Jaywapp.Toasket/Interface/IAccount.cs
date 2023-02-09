namespace Jaywapp.Toasket.Interface
{
    public interface IAccount
    {
        /// <summary>
        /// 수익
        /// </summary>
        int Income { get; }
        /// <summary>
        /// 지출
        /// </summary>
        int Expenditure { get; }
        /// <summary>
        /// 순이익
        /// </summary>
        int Profit { get; }
    }
}
