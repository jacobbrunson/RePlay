using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercises
{
    /// <summary>
    /// Enumeration of all supported FitMi exercises
    /// </summary>
    public enum FitMiExerciseType
    {
        //Arm exercises
        Touches,
        ReachAcross,
        Clapping,
        ReachOut,
        ReachDiagonal,
        Supination,
        Curls,
        ShoulderExtension,
        ShoulderAbduction,
        Flyout,

        //Hand exercises
        WristFlexion,
        WristDeviation,
        Grip,
        Rotate,
        KeyPinch,
        FingerTap,
        ThumbOpposition,
        FingerTwists,
        Rolling,
        Flipping,

        //The unknown exercise
        Unknown
    }
}