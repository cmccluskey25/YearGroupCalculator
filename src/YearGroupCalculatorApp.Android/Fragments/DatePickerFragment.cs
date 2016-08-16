using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace YearGroupCalculator.Android.Fragments
{
    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        // TAG can be any string that you desire.
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();
        private Action<DateTime> _dateSelectedHandler = delegate { };

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
           
            _dateSelectedHandler(selectedDate);
        }

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment frag = new DatePickerFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month,
                                                           currently.Day);
            return dialog;
        }
    }
}
