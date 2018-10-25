using System;
using System.Collections.Generic;
using System.IO;

namespace RePlay
{
    public class PrescriptionManager : List<Prescription>
    {
        private static PrescriptionManager instance;
        private const string fileName = "prescription.dat";

        private PrescriptionManager() 
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
                    string[] data = line.Split(' ');

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
                    writer.WriteLine(String.Format("{0} {1}", p.Game.AssetNamespace, p.Duration));
                }
            }
        }

        private string filePath {
            get
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, fileName);
            }
        }
    }

    public class Prescription {

        public string Exercise;
        public RePlayGame Game;
        public string Device;
        public int Duration;

        public Prescription(string exercise, RePlayGame game, string device, int duration) {
            Exercise = exercise;
            Game = game;
            Device = device;
            Duration = duration;
        }
    }
}
