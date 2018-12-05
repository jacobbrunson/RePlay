using System;
using System.IO;
using Android.Content.Res;
using Android.Graphics;
using RePlay.Entity;

namespace RePlay.Manager
{
	// provide methods to save and load patient data
    public class PatientLoader
    {
        const string fileName = "patient.dat";
        const string photoName = "profile.jpg";

        const string defaultFirstName = "John";
        const string defaultLastName = "Doe";
        const string defaultPhotoAssetName = "defaultProfile.jpg";

        // loads the current state from the state file
        // this is distinct from other "manager" classes in that it returns
        // the loaded data rather than updating the state of the singleton
        public static Patient Load(AssetManager assets)
        {
            if (!File.Exists(FilePath))
            {
                Patient defaultPatient = new Patient
                {
                    First = defaultFirstName,
                    Last = defaultLastName,
                    Photo = BitmapFactory.DecodeStream(assets.Open(defaultPhotoAssetName))
                };

                Save(defaultPatient);

                return defaultPatient;
            }

            Patient patient = new Patient();

            using (var reader = new StreamReader(FilePath))
            {
                string first = reader.ReadLine();
                string last = reader.ReadLine();

                patient.First = first;
                patient.Last = last;
            }

            using (var reader = new StreamReader(PhotoPath)) {
                patient.Photo = BitmapFactory.DecodeStream(reader.BaseStream);
            }

            return patient;
        }

        // writes patient out to the patient file and photo file
        // note that this is distinct from other "manager" classes in that it
        // saves the Patient passed in rather than saving the singleton state
        public static void Save(Patient patient)
        {
            using (var writer = new StreamWriter(FilePath))
            {
                writer.WriteLine(patient.First);
                writer.WriteLine(patient.Last);
            }

            var stream = new FileStream(PhotoPath, FileMode.Create);
            patient.Photo.Compress(Bitmap.CompressFormat.Jpeg, 80, stream);
            stream.Close();
        }

        // returns the file path of the patient file
        static string FilePath
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, fileName);
            }
        }

        // returns the file path of the patient photo file
        static string PhotoPath
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, photoName);
            }
        }
    }
}
