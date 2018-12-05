using System;
using System.Collections.Generic;
using System.IO;
using RePlay.Entity;

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
            System.IO.File.WriteAllText(FilePath, string.Empty);
        }

        public List<String> LoadActivity()
        {
            if (!File.Exists(FilePath))
            {
                using (var writer = File.AppendText(FilePath));
            }

            List<String> activities = new List<String>();
            using (var reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                { 
                    activities.Add(line);
                    Console.WriteLine(line);
                }
            }

            return activities;
        }

        // append the exercise log list to a file for persistence
        public void SaveActivity(String exercise, String game, String type)
        {
            String timestamp = DateTimeOffset.Now.ToString();
            using (var writer = File.AppendText(FilePath))
            {
                writer.WriteLine(String.Format("{0},{1},{2},{3}", exercise, game, type, timestamp));
            }
        }

        // return the path of the log file
        string FilePath
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(path, fileName);
            }
        }
    }
}
