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
            Console.WriteLine("Loading prescription");
            if (!File.Exists(filePath)) {
                SavePrescription();
            }

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(' ');

                    RePlayGame game = GameManager.Instance.FindByNamespace(data[0]);
                    int time = int.Parse(data[1]);

                    Add(new Prescription(game, time));
                }
            }
        }

        public void SavePrescription() {
            using (var writer = new StreamWriter(filePath)) {
                foreach (Prescription p in this) {
                    writer.WriteLine(String.Format("{0} {1}", p.Game.AssetNamespace, p.Time));
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

        public RePlayGame Game;
        public int Time;

        public Prescription(RePlayGame game, int time) {
            Game = game;
            Time = time;
        }
    }
}
