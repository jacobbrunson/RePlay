using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            if (!(obj is Prescription prescription)) return false;
            return Exercise == prescription.Exercise && Device == prescription.Device
                                           && Duration == prescription.Duration && Game.Equals(prescription.Game);
        }

        public override int GetHashCode()
        {
            var hashCode = -1748797614;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Exercise);
            hashCode = hashCode * -1521134295 + EqualityComparer<RePlayGame>.Default.GetHashCode(Game);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Device);
            hashCode = hashCode * -1521134295 + Duration.GetHashCode();
            return hashCode;
        }
    }
}