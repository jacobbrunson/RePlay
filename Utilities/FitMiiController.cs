using System;
using FitMi_Research_Puck;
using HidSharp;

namespace Utilities
{
    public abstract class FitMiiController
    {
        protected const int FIRST = 0;
        protected const int SECOND = 1;

        protected Axes[] axes;
        protected static HIDPuckDongle PuckManager;
        protected static PuckPacket Puck;

        public enum ControlScheme
        {
            ROLLING,
            GYROMETER
        }

        protected FitMiiController(){
            Puck = PuckManager.PuckPack0;
        }

        protected FitMiiController(int numAxes, ControlScheme scheme)
        {
            Puck = PuckManager.PuckPack0;
            axes = new Axes[numAxes];
            switch (scheme)
            {
                case ControlScheme.ROLLING:
                    axes[FIRST] = new Axes(Puck.Gyrometer, 1);
                    break;
                case ControlScheme.GYROMETER:
                    axes[FIRST] = new Axes(Puck.Accelerometer, 0);
                    break;
            }
        }
    }

    public class OneAxisController : FitMiiController
    {
        OneAxisController(ControlScheme scheme) : base(1, scheme)
        {

        }

        public double GetAxisValue()
        {
            return axes[FIRST].GetValue();
        }
    }

    public class TwoAxesController : FitMiiController
    {
        TwoAxesController(ControlScheme scheme): base(2, scheme)
        {

        }

        private double GetAxisValue(int axis)
        {
            return axes[axis].GetValue();
        }

        public double GetFirstAxis()
        {
            return GetAxisValue(FIRST);
        }

        public double GetSecondAxis()
        {
            return GetAxisValue(SECOND);
        }
    }
}
