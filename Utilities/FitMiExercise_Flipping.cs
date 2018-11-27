using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FitMiAndroid;

namespace Exercises
{
    public class FitMiExercise_Flipping : FitMiExerciseGyroBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExercise_Flipping(HIDPuckDongle p)
            : base(p)
        {
            //empty
        }

        #endregion

        #region Overrides

        public override void Update()
        {
            base.Update(1, 0);
            Dictionary<FitMiSensitivity, double> sensitivity_mapping =
                base.mapSensitivity(new double[] { 180.0, 150.0, 120.0, 90.0, 60.0, 30.0, 15.0 });
            CurrentNormalizedValue = latest_theta_4evr / sensitivity_mapping[Sensitivity];
        }

        #endregion
    }
}
