using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using FitMiAndroid;

namespace Exercises
{
    /// <summary>
    /// Enumeration of sensitivity values
    /// </summary>
    public enum FitMiSensitivity
    {
        ExtremelyLow,
        Low,
        MediumLow,
        Medium,
        MediumHigh,
        High,
        ExtremelyHigh
    }

    /// <summary>
    /// Base class for FitMi exercises
    /// </summary>
    public abstract class FitMiExerciseBase
    {
        #region Protected members

        protected HIDPuckDongle FitMi_Controller = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExerciseBase(HIDPuckDongle puck_dongle)
        {
            FitMi_Controller = puck_dongle;
        }

        #endregion

        #region Properties

        public FitMiSensitivity Sensitivity { get; set; } = FitMiSensitivity.Medium;

        public double MinimumNormalizedRange { get; set; } = -1.0;

        public double MaximumNormalizedRange { get; set; } = 1.0;

        public double CurrentNormalizedValue { get; set; } = 0.0;

        public double CurrentActualValue { get; set; } = 0.0;

        #endregion

        #region Protected Methods

        protected const double RadiansToDegrees = (180.0 / Math.PI);

        protected double CartestianToPolar(double x, double y)
        {
            //Given a cartesian coordinate, this returns an angle from 0 to 360
            double result = Math.Atan(y / x) * RadiansToDegrees;
            if (x < 0)
            {
                result += 180;
            }
            else if (y < 0)
            {
                result += 360;
            }

            return result;
        }

        #endregion

        #region Methods

        public virtual void Update ()
        {
            if (FitMi_Controller != null)
            {
                if (FitMi_Controller.IsOpen)
                {
                    FitMi_Controller.CheckForNewPuckData();
                }
            }
        }

        #endregion
    }
}