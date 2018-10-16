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
    public class FitMiExercise_PuckRoll : FitMiExerciseBase
    {
        #region Private data members

        private int max_elements = 50;
        private List<double> theta = new List<double>();
        private List<double> delta_theta = new List<double>();
        private List<double> theta_4evr = new List<double>();

        private double latest_theta_4evr = 0.0;

        private Dictionary<FitMiSensitivity, double> sensitivity_mapping = new Dictionary<FitMiSensitivity, double>()
        {
            { FitMiSensitivity.ExtremelyLow, 360.0 },
            { FitMiSensitivity.Low, 180.0 },
            { FitMiSensitivity.MediumLow, 90.0 },
            { FitMiSensitivity.Medium, 60.0 },
            { FitMiSensitivity.MediumHigh, 45.0 },
            { FitMiSensitivity.High, 30.0 },
            { FitMiSensitivity.ExtremelyHigh, 15.0 }
        };

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExercise_PuckRoll (HIDPuckDongle p)
            : base(p)
        {
            //empty
        }

        #endregion

        #region Methods

        public override void Update()
        {
            base.Update();

            double new_x = FitMi_Controller.PuckPack0.Gyrometer[1];
            double new_y = FitMi_Controller.PuckPack0.Gyrometer[0];
            double new_t = CartestianToPolar(new_x, new_y);
            
            if (theta.Count > 0)
            {
                double dt = new_t - theta.Last();
                if (Math.Abs(dt) >= 300)
                {
                    if (delta_theta.Count > 0)
                    {
                        dt = delta_theta.Last();
                    }
                    else
                    {
                        dt = 0;
                    }
                }
                else if (double.IsNaN(dt))
                {
                    dt = 0;
                }

                delta_theta.Add(dt);
                latest_theta_4evr += dt;
            }

            theta.Add(new_t);
            theta_4evr.Add(latest_theta_4evr);

            if (theta.Count >= max_elements)
            {
                int num_elem_to_remove = theta.Count - max_elements;
                theta.RemoveRange(0, num_elem_to_remove);
            }

            if (delta_theta.Count >= max_elements)
            {
                int num_elem_to_remove = delta_theta.Count - max_elements;
                delta_theta.RemoveRange(0, num_elem_to_remove);
            }

            if (theta_4evr.Count >= max_elements)
            {
                int num_elem_to_remove = theta_4evr.Count - max_elements;
                theta_4evr.RemoveRange(0, num_elem_to_remove);
            }

            CurrentActualValue = latest_theta_4evr;
            CurrentNormalizedValue = latest_theta_4evr / sensitivity_mapping[Sensitivity];
        }

        #endregion
    }
}