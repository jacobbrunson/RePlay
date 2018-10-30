using System;
using System.Collections.Generic;
using System.IO;
using Android.Content.Res;

namespace RePlay
{
    public class GameManager : List<RePlayGame>
    {
        static GameManager instance;
        const string assetName = "games.txt";

        GameManager()
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

        public RePlayGame FindByNamespace(string name) {
            foreach (RePlayGame game in this) {
                if (game.AssetNamespace.Equals(name)) {
                    return game;
                }
            }
            return null;
        }

        public RePlayGame FindByName(string name)
        {
            foreach (RePlayGame game in this)
            {
                if (game.Name.Equals(name))
                {
                    return game;
                }
            }
            return null;
        }

        public void LoadGames(AssetManager assets) {
            using (var reader = new StreamReader(assets.Open("games.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    Add(new RePlayGame(data[1], data[0]));
                }
            }
        }
    }

    public class RePlayGame
    {
        public readonly string Name;
        public readonly string AssetNamespace;

        public RePlayGame(string name, string assetNamespace)
        {
            Name = name;
            AssetNamespace = assetNamespace;
        }
    }
}
