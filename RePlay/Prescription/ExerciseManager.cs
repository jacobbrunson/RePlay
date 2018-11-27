using System;
using System.Collections.Generic;
using System.IO;
using Android.Content.Res;

namespace RePlay
{
    public class ExerciseManager : Dictionary<String, String>
    {
        static ExerciseManager instance;
        const string exerciseFile = "exercises.txt";

        public ExerciseManager()
        {

        }

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

        public void LoadExercises(AssetManager assets)
        {
            Clear();
            using (var reader = new StreamReader(assets.Open("exercises.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    Add(data[0],data[1]);
                }
            }
        }
    }
}
