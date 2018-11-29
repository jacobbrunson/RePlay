using System;
using System.Collections.Generic;
using System.IO;
using RePlay.Entity;

namespace RePlay.Manager
{
    // Provides a singleton instance of the patient's current prescription
    public class PrescriptionManager : List<Prescription>
    {
        static PrescriptionManager instance;
        const string fileName = "prescription.dat";

        // private constructor
        PrescriptionManager() 
        {

        }

        // return the singleton instance
        public static PrescriptionManager Instance {
            get {
                if (instance == null) {
                    instance = new PrescriptionManager();
                }
                return instance;
            }
        }

        // load, parse, and add each prescribed exercise to the list
        public void LoadPrescription() {
            Clear();

            if (!File.Exists(filePath)) {
                SavePrescription();
            }

            using (var reader = new StreamReader(filePath))
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
        public void SavePrescription() {
            using (var writer = new StreamWriter(filePath)) {
                foreach (Prescription p in this) {
                    writer.WriteLine(String.Format("{0},{1},{2},{3}", p.Exercise, p.Game.AssetNamespace, p.Device, p.Duration));
                }
            }
        }

        // return the path of the prescription file
        string filePath {
            get
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, fileName);
            }
        }
    }
}
