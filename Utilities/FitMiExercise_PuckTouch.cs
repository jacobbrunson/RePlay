using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FitMiAndroid;

namespace Exercises
{
    public class FitMiExercise_PuckTouch : FitMiExerciseBase
    {
        #region Private data members

        private Dictionary<FitMiSensitivity, double> sensitivity_mapping = new Dictionary<FitMiSensitivity, double>()
        {
            { FitMiSensitivity.ExtremelyLow, 1000.0 },
            { FitMiSensitivity.Low, 900.0 },
            { FitMiSensitivity.MediumLow, 825.0 },
            { FitMiSensitivity.Medium, 750.0 },
            { FitMiSensitivity.MediumHigh, 675.0 },
            { FitMiSensitivity.High, 600.0 },
            { FitMiSensitivity.ExtremelyHigh, 500.0 }
        };
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExercise_PuckTouch(HIDPuckDongle p)
            : base(p)
        {
            //empty
        }

        #endregion

        #region Overrides

        public override void Update()
        {
            base.Update();

            //Grab the current force value on the loadcell of the puck
            var current_force_value = FitMi_Controller.PuckPack0.Loadcell;

            CurrentActualValue = current_force_value;
            CurrentNormalizedValue = current_force_value / sensitivity_mapping[Sensitivity];
        }

        #endregion
    }
}