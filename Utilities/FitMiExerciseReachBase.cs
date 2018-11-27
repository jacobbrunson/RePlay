using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FitMiAndroid;

namespace Exercises
{
    public class FitMiExerciseReachBase : FitMiExerciseBase
    {
        const bool LEFT = false;
        const bool RIGHT = true;

        #region private members
            private bool current_puck { get; set; } = LEFT;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExerciseReachBase(HIDPuckDongle p)
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
            var current_force_value = 0;

            if (current_puck == LEFT)
            {
                current_force_value = FitMi_Controller.PuckPack0.Loadcell;
            }
            else
            {
                current_force_value = FitMi_Controller.PuckPack0.Loadcell;
            }
            SwitchPuck();
            
            Dictionary<FitMiSensitivity, double> sensitivity_mapping =
                base.mapSensitivity(new double[] { 1000.0, 900.0, 800.0, 700.0, 600.0, 500.0, 450.0 });
            CurrentNormalizedValue = current_force_value / sensitivity_mapping[Sensitivity];
        }

        #endregion

        #region Methods

        private void SwitchPuck()
        {
            current_puck = !current_puck;
        }

        #endregion
    }
}