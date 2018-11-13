﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FitMiAndroid;

namespace Exercises
{
    public class FitMiExercise_Unknown : FitMiExerciseBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExercise_Unknown(HIDPuckDongle p)
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
        }

        #endregion
    }
}