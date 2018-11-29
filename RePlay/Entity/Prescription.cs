namespace RePlay.Entity
{
    // holds the data associated with a prescribed exercise
    public class Prescription
    {
        // data to know about a prescribed exercise
        public string Exercise;
        public RePlayGame Game;
        public string Device;
        public int Duration;

        // basic constructor
        public Prescription(string exercise, RePlayGame game, string device, int duration)
        {
            Exercise = exercise;
            Game = game;
            Device = device;
            Duration = duration;
        }
    }
}