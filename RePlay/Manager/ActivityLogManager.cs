using System;
using System.Collections.Generic;
using System.IO;
using RePlay.Entity;
using Android.App;
using Android.OS;

namespace RePlay.Manager
{
    // Provides a singleton instance of the patient's completed exercises
    public class ActivityLogManager
    {
        static ActivityLogManager instance;
        const string fileName = "activity_log.dat";

        // private constructor
        ActivityLogManager()
        {

        }

        // return the singleton instance
        public static ActivityLogManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ActivityLogManager();
                }
                return instance;
            }
        }

        public void ClearLogFile()
        {
            if(IsExternalStorageWritable())
            {
                System.IO.File.WriteAllText(FilePath, string.Empty);
            }
        }

        public List<String> LoadActivity()
        {
            if (IsExternalStorageWritable() && !File.Exists(FilePath))
            {
                using (var writer = File.AppendText(FilePath));
            }

            List<String> activities = new List<String>();

            if (IsExternalStorageWritable())
            { 
                using (var reader = new StreamReader(FilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        activities.Add(line);
                        Console.WriteLine(line);
                    }
                }
            }

            return activities;
        }

        // append the exercise log list to a file for persistence
        public void SaveActivity(String exercise, String game, String type)
        {
            if(IsExternalStorageWritable())
            {
                String timestamp = DateTimeOffset.Now.ToString();
                using (var writer = File.AppendText(FilePath))
                {
                    writer.WriteLine(String.Format("{0},{1},{2},{3}", exercise, game, type, timestamp));
                }
            }
        }

        public bool IsExternalStorageWritable()
        {
            String state = Android.OS.Environment.GetExternalStorageState(
                Application.Context.GetExternalFilesDir(
                    Android.OS.Environment.DirectoryDocuments));
            if (Android.OS.Environment.MediaMounted.Equals(state))
            {
                return true;
            }
            return false;
        }

        // return the path of the log file
        string FilePath
        {
            get
            {
                Java.IO.File directory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
                String path = directory.ToString();
                return Path.Combine(path, fileName);
            }
        }
    }
}
