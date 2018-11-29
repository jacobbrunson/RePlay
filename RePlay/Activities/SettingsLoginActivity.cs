
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Graphics;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;

namespace RePlay.Activities
{
    [Activity(Label = "SettingsLoginActivity")]
    public class SettingsLoginActivity : Activity
    {
        private const string PASSWORD = "replay";
        private const string PASSWORD_ERR = "Incorrect password";
        private const string PASSWORD_EMPTY = "Please enter your password";
        private const string PASSWORD_CLEAN = "Enter your password";
        private const string GREEN = "#69BE28";
        private const string RED = "#D61926";
        private ImageButton NextButton;
        private ImageButton BackButton;
        private EditText PasswordText;
        private TextView PasswordPrompt;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SettingsLogin);

            NextButton = this.FindViewById<ImageButton>(Resource.Id.settings_next);
            NextButton.Click += Settings_Next_Click;

            BackButton = this.FindViewById<ImageButton>(Resource.Id.settings_back);
            BackButton.Click += Settings_Back_Click;

            PasswordText = this.FindViewById<EditText>(Resource.Id.password);
            PasswordText.TextChanged += User_Input_Changed;

            PasswordPrompt = this.FindViewById<TextView>(Resource.Id.enter_password);
        }

        private void Settings_Back_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void User_Input_Changed(object sender, TextChangedEventArgs e)
        {
            if(PasswordPrompt.Text.Equals(PASSWORD_ERR))
            {
                PasswordText.SetBackgroundResource(Resource.Drawable.EditTextBorder);
                PasswordPrompt.Text = PASSWORD_CLEAN;
                PasswordPrompt.SetTextColor(Color.ParseColor(GREEN));
            }
        }

        private void Settings_Next_Click(object sender, EventArgs e)
        {
            string EnteredString = PasswordText.Text;

            if(string.IsNullOrEmpty(EnteredString))
            {
                PasswordPrompt.Text = PASSWORD_EMPTY;
            }
            else if(EnteredString.Equals(PASSWORD))
            {
                Intent intent = new Intent(this, typeof(WrapperActivities.SettingsActivity));
                StartActivity(intent);
            }
            else
            {
                PasswordPrompt.Text = PASSWORD_ERR;
                PasswordText.SetBackgroundResource(Resource.Drawable.EditTextBorderRed);
                PasswordPrompt.SetTextColor(Color.ParseColor(RED));
            }
        }
    }
}
