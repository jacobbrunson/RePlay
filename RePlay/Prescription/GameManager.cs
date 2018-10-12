using System;
using System.Collections.Generic;
using System.IO;
using Android.Content.Res;

namespace RePlay
{
    public class GameManager : List<RePlayGame>
    {
        private static GameManager instance;
        private const string assetName = "games.txt";

        private GameManager()
        {
        }

        public static GameManager Instance {
            get {
                if (instance == null) {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void LoadGames(AssetManager assets) {
            var reader = new StreamReader(assets.Open("games.txt"));
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('\t');
                Add(new RePlayGame(data[0], data[1]));
            }
        }
    }
}
