
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace RePlay
{
    public class NavigationFragment : Fragment
    {
        private void homeClicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            Intent intent = new Intent(button.Context, typeof(MainActivity));
            button.Context.StartActivity(intent);
        }

        private void gamesClicked(object sender, EventArgs e)
        {
            Console.WriteLine("navigate to games launcher");
        }

        private void connectionClicked(object sender, EventArgs e)
        {
            IsConnected = !IsConnected;
        }

        private void settingsClicked(object sender, EventArgs e)
        {
            Console.WriteLine("navigate to settings screen");
        }

        private bool isConnected = true;
        public bool IsConnected {
            get { return isConnected; }
            set
            {
                isConnected = value;
                if (isConnected) {
                    connectionButton.SetImageResource(Resource.Drawable.pcmConnected);
                } else {
                    connectionButton.SetImageResource(Resource.Drawable.pcmDisconnected);
                }
            }
        }

        private ImageButton connectionButton;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Navigation, container, false);

            view.FindViewById<ImageButton>(Resource.Id.homeButton).Click += homeClicked;
            view.FindViewById<ImageButton>(Resource.Id.gamesButton).Click += gamesClicked;

            connectionButton = view.FindViewById<ImageButton>(Resource.Id.connectionButton);
            connectionButton.Click += connectionClicked;

            view.FindViewById<ImageButton>(Resource.Id.settingsButton).Click += settingsClicked;

            return view;
        }
    }
}
