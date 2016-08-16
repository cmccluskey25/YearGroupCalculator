using System;
using NUnit.Framework;
using SecondXamarinApp.Core;

namespace YearGroupCalculator.Core.Tests
{
    [TestFixture]
    public class PupilTests
    {
        private const string NotStartedSchool = "Not started school";
        private const string P1 = "P1";
        private const string S6 = "S6";
        private const string LeftSchool = "Left school";

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_Normal_ThreeYearOld_ReturnNull(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(2010, 8, 1);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: null);
        }

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_Normal_FourYearOld_returnNotStartedSchool(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(2009, 8, 1);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: NotStartedSchool);
        }

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_Normal_FiveYearOld_returnP1(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(2008, 8, 1);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: P1);

        }

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_CanDefer_FourAndHalf_returnP1(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(2009, 2, 28);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: P1);
        }

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_CanDefer_SixteenAndAHalf_returnS6(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(1997, 2, 28);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: S6);
        }

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_Normal_Seventeen_returnS6(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(1996, 8, 1);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: S6);
        }

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_CanDefer_SeventeenAndAHalf_returnLeftSchool(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(1996, 2, 28);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: LeftSchool);
        }

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_Normal_Eighteen_returnLeftSchool(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(1995, 8, 1);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: LeftSchool);
        }

        [Test]
        [TestCase("01/8/2013", Description = "Beginning of Term")]
        [TestCase("31/7/2014", Description = "End of term")]
        public void CalculateExpectedYearGroup_Normal_Nineteen_returnNull(string targetDateString)
        {
            var targetDate = DateTime.Parse(targetDateString);

            var dob = new DateTime(1994, 8, 1);

            TestCalculateExpectedYearGroup(dob, targetDate, expectedYearGroupName: null);
        }


        [Test]
        [TestCase("01/08/1994", Description = "Nineteen")]
        [TestCase("01/08/1995", Description = "Eighteen")]
        [TestCase("28/02/1995", Description = "Seventeen and a half")]
        [TestCase("01/8/2009", Description = "Four")]
        [TestCase("01/8/2010", Description = "Three")]
        public void CalculateYearGroupLikelihoods_NotOfSchoolAgeCases_returnInvalidOperation(string dobString)
        {
            var dob = DateTime.Parse(dobString);

            var targetDate = new DateTime(2013, 8, 1);

            var pupil = new Pupil(dob);

            Assert.Throws<InvalidOperationException>(() => pupil.CalculateYearGroupLikelihoods(targetDate));


        }

        [Test]
        [TestCase("01/08/1996", Description = "Seventeen")]
        [TestCase("28/02/1997", Description = "Sixteen and a half")]
        [TestCase("01/08/1997", Description = "Sixteen")]
        [TestCase("01/8/2008", Description = "Five")]
        [TestCase("28/2/2009", Description = "Four and a half")]
        public void CalculateYearGroupLikelihoods_SchoolAgeCases_returnLikelihoods(string dobString)
        {
            var dob = DateTime.Parse(dobString);

            var targetDate = new DateTime(2013, 8, 1);

            var pupil = new Pupil(dob);

            var likelihoods = pupil.CalculateYearGroupLikelihoods(targetDate);

            Assert.AreEqual(2, likelihoods.Count);
            Assert.AreEqual(100, likelihoods[0].Likelihood + likelihoods[1].Likelihood);


        }
        private static Pupil CreatePupil(DateTime dob)
        {
            return new Pupil(dob);
        }

        private static void TestCalculateExpectedYearGroup(DateTime dob, DateTime targetDate, string expectedYearGroupName)
        {
            var pupil = CreatePupil(dob);

            var actualYearGroup = pupil.CalculateExpectedYearGroup(targetDate);

            if (expectedYearGroupName == null)
            {
                Assert.AreEqual(null, actualYearGroup);
            }
            else
            {
                Assert.AreEqual(expectedYearGroupName, actualYearGroup.Name);
            }

        }
    }
}
