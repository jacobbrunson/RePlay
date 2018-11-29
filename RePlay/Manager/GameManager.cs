using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Android.Content.Res;
using RePlay.Entity

namespace RePlay.Manager
{
    // Provides a single instance of the list of available games
    public class GameManager : List<RePlayGame>
    {
        static GameManager instance;
        const string assetName = "games.txt";

        // private constructor
        GameManager()
        {
        }

        // return the singleton instance of this list
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        // return the game's data based on its namespace
        public RePlayGame FindByNamespace(string name)
        {
            foreach (RePlayGame game in this)
            {
                if (game.AssetNamespace.Equals(name))
                {
                    return game;
                }
            }
            return null;
        }

        // return the names of every game
        public List<String> GetNames()
        {
            return this.Select(game => game.Name());
        }

        // return a game's data based on its name
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

        // pull, parse, and append game data from its file format
        public void LoadGames(AssetManager assets) {
            Clear();
            using (var reader = new StreamReader(assets.Open("games.txt")))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    try
                    {
                        string assemblyQualifiedName = data[4].Replace(";", ",");
                        Add(new RePlayGame(data[1].Trim(), data[0].Trim(), data[2].Trim(), bool.Parse(data[3].Trim()), assemblyQualifiedName));
                    }
                    catch (Exception)
                    {
                        //empty
                    }
                }
            }
        }
    }

    
}
