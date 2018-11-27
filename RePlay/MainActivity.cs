using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.IO;
using System;

namespace RePlay
{
    [Activity(Label = "RePlay", MainLauncher = true, Icon = "@mipmap/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Load games from Assets/games.txt and load prescriptions from internal storage
            GameManager.Instance.LoadGames(Assets);
            PrescriptionManager.Instance.LoadPrescription();
            PrescriptionStateManager.Instance.LoadState();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            ImageButton button = FindViewById<ImageButton>(Resource.Id.playButton);

            button.Click += delegate
            {
                LaunchPrescription();
            };
        }

        private void LaunchPrescription() {

            int gameIndex = PrescriptionStateManager.Instance.CurrentGameIndex;
            Prescription prescription = PrescriptionManager.Instance[gameIndex];
            RePlayGame game = prescription.Game;
            Type t = Type.GetType(game.AssemblyQualifiedName); //This is what gets the correct name

            Intent intent = new Intent(this, t);
            intent.PutExtra("CONTENT_DIR", game.AssetNamespace); //Correct asset namespace
            //We need to also pass in prescription time
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            System.Console.WriteLine("game finished!!!");

            if (PrescriptionStateManager.Instance.CurrentGameIndex == PrescriptionManager.Instance.Count-1) {
                System.Console.WriteLine("prescription complete!");
                PrescriptionStateManager.Instance.CurrentGameIndex = 0;
                PrescriptionStateManager.Instance.SaveState();
                return;
            }

            System.Console.WriteLine("starting next game");
            PrescriptionStateManager.Instance.CurrentGameIndex += 1;
            PrescriptionStateManager.Instance.SaveState();

            //LaunchPrescription();
        }
    }
}