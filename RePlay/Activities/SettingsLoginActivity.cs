using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Graphics;
using Android.Text;
using Android.Widget;

// SettingsLoginActivity: Authenticate user to get to settings page
namespace RePlay.Activities
{
    [Activity(Label = "SettingsLoginActivity")]
    public class SettingsLoginActivity : Activity
    {
        // Display strings used on prompt
        const string PASSWORD = "replay";
        const string PASSWORD_ERR = "Incorrect password";
        const string PASSWORD_EMPTY = "Please enter your password";
        const string PASSWORD_CLEAN = "Enter your password";

        // Colors
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

            // Add click handlers
            NextButton = FindViewById<ImageButton>(Resource.Id.settings_next);
            NextButton.Click += Settings_Next_Click;

            BackButton = FindViewById<ImageButton>(Resource.Id.settings_back);
            BackButton.Click += Settings_Back_Click;

            // Add input changed handler
            PasswordText = FindViewById<EditText>(Resource.Id.password);
            PasswordText.TextChanged += User_Input_Changed;

            PasswordPrompt = FindViewById<TextView>(Resource.Id.enter_password);
        }

        // Handle the back image button click
        void Settings_Back_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        // Handle the text change in edit text
        void User_Input_Changed(object sender, TextChangedEventArgs e)
        {
            if (PasswordPrompt.Text.Equals(PASSWORD_ERR))
            {
                PasswordText.SetBackgroundResource(Resource.Drawable.EditTextBorder);
                PasswordPrompt.Text = PASSWORD_CLEAN;
                PasswordPrompt.SetTextColor(Color.ParseColor(GREEN));
            }
        }

        // Handle the next button click
        void Settings_Next_Click(object sender, EventArgs e)
        {
            string EnteredString = PasswordText.Text;

            // Check if the password is correct and handle appropriately
            if (string.IsNullOrEmpty(EnteredString))
            {
                PasswordPrompt.Text = PASSWORD_EMPTY;
            }
            else if (EnteredString.Equals(PASSWORD))
            {
                // Password is correct; launch the settings activity
                Intent intent = new Intent(this, typeof(SettingsActivity));
                StartActivity(intent);
            }
            else
            {
                // Incorrect password; change edit text UI to reflect incorrect password
                PasswordPrompt.Text = PASSWORD_ERR;
                PasswordText.SetBackgroundResource(Resource.Drawable.EditTextBorderRed);
                PasswordPrompt.SetTextColor(Color.ParseColor(RED));
            }
        }
    }
}
