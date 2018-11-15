namespace RePlay {
    public class Prescription
    {

        public string Exercise;
        public RePlayGame Game;
        public string Device;
        public int Duration;

        public Prescription(string exercise, RePlayGame game, string device, int duration)
        {
            Exercise = exercise;
            Game = game;
            Device = device;
            Duration = duration;
        }
    }
}