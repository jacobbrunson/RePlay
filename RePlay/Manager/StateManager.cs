using System;
using System.Collections.Generic;
using System.IO;

namespace RePlay.Manager
{
    public class StateManager
    {
        static StateManager instance;
        const string fileName = "state.dat";

        private long timestamp = 0;
        private int index = 0;

        public int Index
        {
            get
            {
                LoadState();
                long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                if (milliseconds - timestamp > (8 * 60 * 60 * 1000))        // After 8 hours, go to beginning of prescription
                {
                    index = 0;
                }
                return index;
            }
        }

        StateManager()
        {

        }

        public static StateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StateManager();
                }
                return instance;
            }
        }

        public void LoadState()
        {
            if (!File.Exists(FilePath))
            {
                SaveState();
            }

            using (var reader = new StreamReader(FilePath))
            {
                string line = reader.ReadLine();

                if(line != null)
                {
                    timestamp = long.Parse(line);
                    index = int.Parse(reader.ReadLine());
                }
            }
        }

        public void SaveState()
        {
            using (var writer = new StreamWriter(FilePath))
            {
                if (timestamp != 0)
                {
                    writer.WriteLine(timestamp);
                    writer.WriteLine(index);
                }
            }
        }

        public void UpdateState(long t, int i)
        {
            timestamp = t;
            index = i;
            SaveState();
        }

        string FilePath
        {
            get
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                return System.IO.Path.Combine(path, fileName);
            }
        }
    }
}
