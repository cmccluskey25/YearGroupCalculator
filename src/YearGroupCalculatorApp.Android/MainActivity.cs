using System;
using System.Globalization;
using Android.App;
using Android.OS;
using Android.Widget;
using SecondXamarinApp.Core;
using YearGroupCalculator.Android.Adapters;
using YearGroupCalculator.Android.Fragments;

namespace YearGroupCalculator.Android
{
    [Activity(Label = "Pupil Year Group Calculator", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private EditText _pupilDateOfBirthView;
        private EditText _calculateDateView;
        private Button _calculateButton;
        private ListView _resultsListView;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CalculatorView);

            GetViews();

            _calculateDateView.Text = DateTime.Now.ToString("dd/MM/yy");

            HandleEvents();
        }

        private void GetViews()
        {
            _pupilDateOfBirthView = FindViewById<EditText>(Resource.Id.pupilDob);
            _calculateDateView = FindViewById<EditText>(Resource.Id.calculationDate);
            _calculateButton = FindViewById<Button>(Resource.Id.calculate);
            _resultsListView = FindViewById<ListView>(Resource.Id.ResultsView);

        }
        private void HandleEvents()
        {
            _calculateButton.Click += CalculateButton_Click;
            _pupilDateOfBirthView.Click += PupilDob_Click;
            _calculateDateView.Click += CalculateDateView_Click;
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            DateTime dob;
            DateTime targetDate;

            if (IsValidInput(out dob, out targetDate))
            {

                var pupil = new Pupil(dob);

                var expectedYearGroup = pupil.CalculateExpectedYearGroup(targetDate);

                if (expectedYearGroup == null || !expectedYearGroup.IsASchoolYear)
                {
                    DisplayError(_pupilDateOfBirthView, "This pupil is not of school age");
                }
                else
                {
                    var result = pupil.CalculateYearGroupLikelihoods(targetDate);

                    _resultsListView.Adapter = new ResultItemRowAdapter(this, result);
                }
            }

        }

        private bool IsValidInput(out DateTime dob,out DateTime targetDate)
        {
            dob=DateTime.MinValue;
            targetDate = DateTime.MinValue;
            
            if (string.IsNullOrEmpty(_pupilDateOfBirthView.Text))
            {
                DisplayError(_pupilDateOfBirthView, "Please enter a date of birth");
                return false;
            }
            else
            {
                DisplayError(_pupilDateOfBirthView,null);
            }

            var isDob = DateTime.TryParseExact(_pupilDateOfBirthView.Text, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob );

            if (!isDob)
            {
                DisplayError(_pupilDateOfBirthView, "Is not a date");
                return false;
            }
            else
            {
                DisplayError(_pupilDateOfBirthView, null);
            }
            var isTargetDate = DateTime.TryParseExact(_calculateDateView.Text, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out targetDate);

            if (!isTargetDate)
            {
                DisplayError(_calculateDateView, "Is not a date");
                return false;
            }
            else
            {
                DisplayError(_calculateDateView, null);
            }

            return true;

        }

        private void DisplayError(EditText editText, string error)
        {
            editText.SetError(error, null);
        }

        private void PupilDob_Click(object sender, EventArgs e)
        {
            var frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                SetDate(_pupilDateOfBirthView,time.ToString("dd/MM/yy"));
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }

        private void SetDate(EditText editText, string value)
        {
            editText.Text = value;
        }

        private void CalculateDateView_Click(object sender, EventArgs e)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                SetDate(_calculateDateView, time.ToString("dd/MM/yy"));
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}