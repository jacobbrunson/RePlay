using System;
namespace RePlay
{
    public class Prescription
    {
        public RePlayGame Game;
        public int Time; //Seconds

        public Prescription(RePlayGame game, int time) 
        {
            Game = game;
            Time = time;
        }
    }
}
