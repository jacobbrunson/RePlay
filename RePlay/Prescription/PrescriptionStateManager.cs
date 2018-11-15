using System;
using System.Collections.Generic;
using System.IO;

namespace RePlay
{
    public class PrescriptionStateManager
    {
        static PrescriptionStateManager instance;
        const string fileName = "state.dat";

        public int CurrentGameIndex = 0;

        private PrescriptionStateManager()
        {

        }

        public static PrescriptionStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrescriptionStateManager();
                }
                return instance;
            }
        }

        public void LoadState()
        {
            if (!File.Exists(filePath))
            {
                SaveState();
            }

            using (var reader = new StreamReader(filePath))
            {
                string line = reader.ReadLine();

                if (line == null) {
                    CurrentGameIndex = 0;
                    return;
                }

                CurrentGameIndex = int.Parse(line);
            }
        }

        public void SaveState()
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(CurrentGameIndex);
            }
        }

        string filePath
        {
            get
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, fileName);
            }
        }
    }
}
