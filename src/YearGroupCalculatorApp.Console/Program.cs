using SecondXamarinApp.Core;
using System;
using System.Diagnostics;

namespace SecondXamarinApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var pupil = new Pupil(new DateTime(2008, 5, 13));
            var result = pupil.CalculateYearGroupLikelihoods(DateTime.Now);
            foreach(var resultType in result)
            {
                Debug.WriteLine("{0} : Year group: {1} : Likelihood :{2}", ((YearGroupResult)resultType).ResultType,
                                ((YearGroupResult)resultType).YearGroup.Name, ((YearGroupResult)resultType).Likelihood);
            }
        }
    }
}
