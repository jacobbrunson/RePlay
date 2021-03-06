﻿using System;
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

        public List<String> GetNames()
        {
            return new List<String>();
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
