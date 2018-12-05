using System;
using System.Collections.Generic;
using System.IO;
using Android;
using Android.App;
using Android.Content.Res;

namespace RePlay.Manager
{
    // Exercise Manager - singleton class used to load exercises from file
    public class ExerciseManager : Dictionary<String, String>
    {
        static ExerciseManager instance;
        const string exerciseFile = "exercises.txt";

        // private constructor
        ExerciseManager()
        {

        }

        // returns the singleton instance of the exercise manager, creating one if needed
        public static ExerciseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExerciseManager();
                }
                return instance;
            }
        }

        // used to load exercises from a the text file exercises.txt
        public void LoadExercises(AssetManager assets)
        {
            Clear();
            using (var reader = new StreamReader(assets.Open("exercises.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    Add(data[0], data[1]);
                }
            }
        }

        // utility method to map the exercise name (as a string) into a resource drawable identifier
        public int MapNameToPic(string exercise, Activity a)
        {
            string picName = this[exercise] + "0";
            int resource = a.Resources.GetIdentifier(picName, "drawable", a.PackageName);

            return resource == 0 ? Resource.Drawable.curls0 : resource;
        }
    }
}
