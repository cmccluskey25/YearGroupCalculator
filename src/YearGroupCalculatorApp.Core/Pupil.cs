using System;
using System.Collections.Generic;
using System.Linq;

namespace SecondXamarinApp.Core
{
    public class Pupil
    {
        readonly List<YearGroup> _yearGroups = new List<YearGroup>
            {
            new YearGroup("Not started school",0),
            new YearGroup ("P1",1 ),
            new YearGroup ("P2",2),
            new YearGroup ("P3",3 ),
            new YearGroup ("P4",4 ),
            new YearGroup ("P5",5 ),
            new YearGroup ("P6",6 ),
            new YearGroup ("P7",7 ),
            new YearGroup ("S1",8 ),
            new YearGroup ("S2",9 ),
            new YearGroup ("S3",10 ),
            new YearGroup ("S4",11 ),
            new YearGroup ("S5",12 ),
            new YearGroup ("S6",13 ),
            new YearGroup ("Left school",14 )
        };

        // From Growing Up in Scotland http://www.gov.scot/Resource/0039/00392709.pdf
        readonly Dictionary<int, int> _birthMonthDeferralPercentages = new Dictionary<int, int>
        {
            {1,41 },
            {2,48 },
            {3,1 },
            {4,0 },
            {5,0 },
            {6,0 },
            {7,2 },
            {8,2 },
            {9,7 },
            {10,14 },
            {11,16 },
            {12,22 }
        };

        public DateTime DateOfBirth { get; set; }

        public Pupil(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public List<YearGroupResult> CalculateYearGroupLikelihoods(DateTime targetDate)
        {

            var expectedYearGroup = CalculateExpectedYearGroup(targetDate);

            if (expectedYearGroup == null || !expectedYearGroup.IsASchoolYear)
                throw new InvalidOperationException("This pupil is not of school age");

            var thisDeferredYear = expectedYearGroup.Year-1;

            var deferredYearGroup = _yearGroups.First(x => x.Year == thisDeferredYear);

            var likelihoodDeferred = _birthMonthDeferralPercentages.First(x => x.Key == DateOfBirth.Month).Value;

            return new List<YearGroupResult>
            {
                {new YearGroupResult(ResultType.Expected, expectedYearGroup, 100 - likelihoodDeferred)},
                {new YearGroupResult(ResultType.Deferred, deferredYearGroup, likelihoodDeferred)}
            };
        }

        public YearGroup CalculateExpectedYearGroup(DateTime targetDate)
        {
            var thisYearsStartTermYear = targetDate.Month >= 8 && targetDate.Month <= 12 ? targetDate.Year : targetDate.Year - 1;

            var startTermDate = new DateTime(thisYearsStartTermYear, 8, 1);

            var expectedToStartAge = DateOfBirth.Month >= 3 && DateOfBirth.Month <= 12 ? 5 : 4;

            var expectedStartTerm = new DateTime(expectedToStartAge + DateOfBirth.Year, 8, 1);

            var yearsCompleted = startTermDate.Year - expectedStartTerm.Year;

            var thisExpectedYear = yearsCompleted + 1;

            return _yearGroups.FirstOrDefault(x => x.Year == thisExpectedYear);

        }
    }
}
