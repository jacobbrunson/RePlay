using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Dictionary<FitMiSensitivity, double> mapSensitivity(double[] sensitivity)
        {
            return new Dictionary<FitMiSensitivity, double>()
            {
                { FitMiSensitivity.ExtremelyLow, sensitivity[0] },
                { FitMiSensitivity.Low, sensitivity[1] },
                { FitMiSensitivity.MediumLow, sensitivity[2] },
                { FitMiSensitivity.Medium, sensitivity[3] },
                { FitMiSensitivity.MediumHigh, sensitivity[4] },
                { FitMiSensitivity.High, sensitivity[5] },
                { FitMiSensitivity.ExtremelyHigh, sensitivity[6] }
            };
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

        public static FitMiExerciseBase GetExerciseClass(HIDPuckDongle puck_dongle, FitMiExerciseType type)
        {
            switch(type)
            {

                case FitMiExerciseType.Touches:
                    return new FitMiExercise_PuckTouch(puck_dongle);
                case FitMiExerciseType.ReachAcross:
                    return new FitMiExercise_ReachAcross(puck_dongle);
                case FitMiExerciseType.Clapping:
                    return new FitMiExercise_Clapping(puck_dongle);
                case FitMiExerciseType.ReachOut:
                    return new FitMiExercise_ReachOut(puck_dongle);
                case FitMiExerciseType.ReachDiagonal:
                    return new FitMiExercise_ReachDiagonal(puck_dongle);
                case FitMiExerciseType.Supination:
                    return new FitMiExercise_Supination(puck_dongle);
                case FitMiExerciseType.Curls:
                    return new FitMiExercise_Curls(puck_dongle);
                case FitMiExerciseType.ShoulderExtension:
                    return new FitMiExercise_ShoulderExtension(puck_dongle);
                case FitMiExerciseType.ShoulderAbduction:
                    return new FitMiExercise_ShoulderAbduction(puck_dongle);
                case FitMiExerciseType.Flyout:
                    return new FitMiExercise_Flyout(puck_dongle);

                //Hand exercises
                case FitMiExerciseType.WristFlexion:
                    return new FitMiExercise_WristFlexion(puck_dongle);
                case FitMiExerciseType.WristDeviation:
                    return new FitMiExercise_WristDeviation(puck_dongle);
                case FitMiExerciseType.Grip:
                    return new FitMiExercise_Grip(puck_dongle);
                case FitMiExerciseType.Rotate:
                    return new FitMiExercise_Rotate(puck_dongle);
                case FitMiExerciseType.KeyPinch:
                    return new FitMiExercise_KeyPinch(puck_dongle);
                case FitMiExerciseType.FingerTap:
                    return new FitMiExercise_FingerTap(puck_dongle);
                case FitMiExerciseType.ThumbOpposition:
                    return new FitMiExercise_ThumbOpposition(puck_dongle);
                case FitMiExerciseType.FingerTwists:
                    return new FitMiExercise_FingerTwists(puck_dongle);
                case FitMiExerciseType.Flipping:
                    return new FitMiExercise_Flipping(puck_dongle);

                //The unknown exercise
                default:
                    return new FitMiExercise_Unknown(puck_dongle);
            }
        }

        #endregion
    }
}