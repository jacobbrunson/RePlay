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
    public class FitMiExercise_Rolling : FitMiExerciseBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExercise_Rolling(HIDPuckDongle p)
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


        }

        #endregion
    }
}