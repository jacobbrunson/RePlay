using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FitMiAndroid;

namespace Exercises
{
    public class FitMiExercise_Clapping : FitMiExerciseBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExercise_Clapping(HIDPuckDongle p)
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
            Dictionary<FitMiSensitivity, double> sensitivity_mapping =
                base.mapSensitivity(new double[] { 1000.0, 900.0, 800.0, 700.0, 600.0, 500.0, 450.0 });
            CurrentNormalizedValue = current_force_value / sensitivity_mapping[Sensitivity];
        }

        #endregion
    }
}