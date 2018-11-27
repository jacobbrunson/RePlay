using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FitMiAndroid;

namespace Exercises
{
    public class FitMiExercise_ReachAcross : FitMiExerciseBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExercise_ReachAcross(HIDPuckDongle p)
            : base(p)
        {
            //empty
        }

        #endregion

        #region Overrides

        public override void Update()
        {
            base.Update();
        }

        #endregion
    }
}