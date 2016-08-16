namespace SecondXamarinApp.Core
{
    public class YearGroupResult
    {
        public YearGroupResult(ResultType resultType, YearGroup yearGroup, int likelihood)
        {
            ResultType = resultType;
            YearGroup = yearGroup;
            Likelihood = likelihood;
        }
        

        public YearGroup YearGroup { get; set; }

        public int Likelihood { get; set; }

        public ResultType ResultType { get; set; }

    }
}
