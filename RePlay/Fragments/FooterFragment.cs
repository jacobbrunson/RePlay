using Android.App;
using Android.OS;
using Android.Views;

namespace RePlay.Fragments
{
    public class FooterFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Footer, container, false);

            return view;
        }
    }
}
