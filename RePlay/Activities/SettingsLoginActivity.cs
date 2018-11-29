using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Graphics;
using Android.Text;
using Android.Widget;

namespace RePlay.Activities
{
    [Activity(Label = "SettingsLoginActivity")]
    public class SettingsLoginActivity : Activity
    {
        const string PASSWORD = "replay";
        const string PASSWORD_ERR = "Incorrect password";
        const string PASSWORD_EMPTY = "Please enter your password";
        const string PASSWORD_CLEAN = "Enter your password";
        const string GREEN = "#69BE28";
        const string RED = "#D61926";
        ImageButton NextButton;
        ImageButton BackButton;
        EditText PasswordText;
        TextView PasswordPrompt;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SettingsLogin);

            NextButton = FindViewById<ImageButton>(Resource.Id.settings_next);
            NextButton.Click += Settings_Next_Click;

            BackButton = FindViewById<ImageButton>(Resource.Id.settings_back);
            BackButton.Click += Settings_Back_Click;

            PasswordText = FindViewById<EditText>(Resource.Id.password);
            PasswordText.TextChanged += User_Input_Changed;

            PasswordPrompt = FindViewById<TextView>(Resource.Id.enter_password);
        }

        void Settings_Back_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        void User_Input_Changed(object sender, TextChangedEventArgs e)
        {
            if (PasswordPrompt.Text.Equals(PASSWORD_ERR))
            {
                PasswordText.SetBackgroundResource(Resource.Drawable.EditTextBorder);
                PasswordPrompt.Text = PASSWORD_CLEAN;
                PasswordPrompt.SetTextColor(Color.ParseColor(GREEN));
            }
        }

        void Settings_Next_Click(object sender, EventArgs e)
        {
            string EnteredString = PasswordText.Text;

            if (string.IsNullOrEmpty(EnteredString))
            {
                PasswordPrompt.Text = PASSWORD_EMPTY;
            }
            else if (EnteredString.Equals(PASSWORD))
            {
                Intent intent = new Intent(this, typeof(SettingsActivity));
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
