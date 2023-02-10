using Jaywapp.Toasket.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Toasket.Service
{
    public class Analyst
    {
        #region Internal Field
        private IReadOnlyList<Box> _boxes;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boxes"></param>
        public Analyst(IEnumerable<Box> boxes)
        {
            _boxes = boxes.ToList();
        }
        #endregion

        #region Functions
        /// <summary>
        /// <paramref name="start"/>부터 <paramref name="end"/>까지의 결과를 분석합니다.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public AnalysisResultGroup Analysis(DateTime start, DateTime end)
        {
            var filtered = Filtering(_boxes, start, end).ToList();
            var hitted = filtered.Where(i => i.IsHitted()).ToList();

            // 총 지출
            var expenditure = filtered.Sum(i => i.Expenditure);
            // 총 수익
            var income = hitted.Sum(i => i.Income);

            return new AnalysisResultGroup(income, expenditure, AnalysisChild(filtered));
        }

        /// <summary>
        /// 월별 결과를 분석합니다.
        /// </summary>
        /// <param name="boxes"></param>
        /// <returns></returns>
        private Dictionary<DateTime, AnalysisResult> AnalysisChild(IEnumerable<Box> boxes)
        {
            var result = new Dictionary<DateTime, AnalysisResult>();

            var groups = boxes
                .GroupBy(b => b.Created.ToString("yyyy/MM"))
                .OrderBy(g=> g.Key)
                .ToList();

            foreach (var group in groups)
            {
                var hitted = group.Where(i => i.IsHitted()).ToList();

                // 지출
                var expenditure = group.Sum(i => i.Expenditure);
                // 수익
                var income = hitted.Sum(i => i.Income);

                result.Add(
                    DateTime.Parse(group.Key), 
                    new AnalysisResult(income, expenditure));
            }

            return result;
        }

        /// <summary>
        /// 전체 기간에 대해 분석합니다.
        /// </summary>
        /// <returns></returns>
        public AnalysisResultGroup Analysis()
        {
            var start = _boxes.Min(b => b.Created);
            var end = _boxes.Max(b => b.Created);

            return Analysis(start, end);
        }

        /// <summary>
        /// <paramref name="boxes"/>에서 <paramref name="start"/>, <paramref name="end"/>내의 항목을 추출합니다.
        /// </summary>
        /// <param name="boxes"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static IEnumerable<Box> Filtering(IEnumerable<Box> boxes, DateTime start, DateTime end)
        {
            foreach(var box in boxes.ToList())
            {
                var date = box.Created.Date;
                if(start.Date <= date && date <= end.Date)
                    yield return box;
            }
        }
        #endregion
    }
}
