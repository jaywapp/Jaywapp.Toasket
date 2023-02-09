﻿using Jaywapp.Toasket.Model;
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
        public Analyst(IEnumerable<Box> boxes)
        {
            _boxes = boxes.ToList();
        }
        #endregion

        #region Functions
        public AnalysisResultGroup Analysis(DateTime start, DateTime end)
        {
            var filtered = Filtering(_boxes, start, end).ToList();
            var hitted = filtered.Where(i => i.IsHitted()).ToList();

            // 총 지출
            var expenditure = filtered.Sum(i => i.Expenditure);
            // 총 수익
            var income = hitted.Sum(i => i.Income);

            return new AnalysisResultGroup(income, expenditure, AnalysisMonthly(filtered));
        }

        private Dictionary<DateTime, AnalysisResult> AnalysisMonthly(IEnumerable<Box> boxes)
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

        public AnalysisResultGroup Analysis()
        {
            var start = _boxes.Min(b => b.Created);
            var end = _boxes.Max(b => b.Created);

            return Analysis(start, end);
        }

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
