using System;

namespace SecondXamarinApp.Core
{
    public class YearGroup
    {
        public YearGroup(string name, int year)
        {
            Name = name;
            Year = year;
        }
        public string Name { get; set; }

        public int Year { get; set; }

        public bool IsASchoolYear => Year > 0 && Year < 14;

        public DateTime CalculateMarchDateOfBirth(DateTime currentDate)
        {
            return new DateTime(currentDate.Year-(4+Year), 3, 1);
        }

    }
}
