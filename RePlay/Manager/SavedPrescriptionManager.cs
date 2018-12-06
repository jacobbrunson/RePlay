using System;
using System.Collections.Generic;
using System.IO;
using RePlay.Entity;

namespace RePlay.Manager
{
    public class SavedPrescriptionManager : List<Prescription>
    {
        static SavedPrescriptionManager instance;
        const string fileName = "savedprescriptions.dat";

        SavedPrescriptionManager()
        {
        }

        public static SavedPrescriptionManager Instance 
        {
            get
            {
                if(instance == null)
                {
                    instance = new SavedPrescriptionManager();
                }
                return instance;
            }
        }

        // load, parse, and add each prescribed exercise to the list
        public void LoadPrescription()
        {
            Clear();

            if (!File.Exists(FilePath))
            {
                SavePrescription();
            }

            using (var reader = new StreamReader(FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    string exercise = data[0];
                    RePlayGame game = GameManager.Instance.FindByNamespace(data[1]);
                    string device = data[2];
                    int duration = int.Parse(data[3]);


                    Add(new Prescription(exercise, game, device, duration));
                }
            }
        }

        // save the prescription list to a file for persistence
        public void SavePrescription()
        {
            using (var writer = new StreamWriter(FilePath))
            {
                foreach (Prescription p in this)
                {
                    writer.WriteLine(String.Format("{0},{1},{2},{3}", p.Exercise, p.Game.AssetNamespace, p.Device, p.Duration));
                }
            }
        }

        // return the path of the prescription file
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
