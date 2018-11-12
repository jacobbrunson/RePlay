using System;
using System.Collections.Generic;
using System.IO;

namespace RePlay
{
    public class PrescriptionManager : List<Prescription>
    {
        static PrescriptionManager instance;
        const string fileName = "prescription.dat";

        PrescriptionManager() 
        {

        }

        public static PrescriptionManager Instance {
            get {
                if (instance == null) {
                    instance = new PrescriptionManager();
                }
                return instance;
            }
        }

        public void LoadPrescription() {
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

        public void SavePrescription() {
            using (var writer = new StreamWriter(filePath)) {
                foreach (Prescription p in this) {
                    writer.WriteLine(String.Format("{0},{1},{2},{3}", p.Exercise, p.Game.AssetNamespace, p.Device, p.Duration));
                }
            }
        }

        string filePath {
            get
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, fileName);
            }
        }
    }
}
