using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using SecondXamarinApp.Core;

namespace YearGroupCalculator.Android.Adapters
{
    public class ResultItemRowAdapter : BaseAdapter<YearGroupResult>
    {
        readonly Activity _context;
        readonly List<YearGroupResult> _list;

        public ResultItemRowAdapter(Activity context, List<YearGroupResult> list)
        {
            _context = context;
            _list = list;
        }

        public override int Count => _list.Count;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override YearGroupResult this[int index] => _list[index];

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // re-use an existing view, if one is available
            // otherwise create a new one
            var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.ResultsItemRow, parent, false);

            var item = this[position];
            view.FindViewById<TextView>(Resource.Id.ResultsItemRowType).Text = item.ResultType.ToString();
            view.FindViewById<TextView>(Resource.Id.ResultsItemRowYearGroup).Text = item.YearGroup.Name;
            view.FindViewById<TextView>(Resource.Id.ResultsItemRowLikelihood).Text = item.Likelihood + "%";
           
            return view;
        }
    }
}