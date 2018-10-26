using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FitMiAndroid;

namespace Exercises
{
    public abstract class FitMiExerciseGyroBase : FitMiExerciseBase
    {
        #region Private data members

        private int max_elements = 50;
        protected List<double> theta = new List<double>();
        protected List<double> delta_theta = new List<double>();
        protected List<double> theta_4evr = new List<double>();

        protected double latest_theta_4evr = 0.0;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FitMiExerciseGyroBase(HIDPuckDongle p)
            : base(p)
        {
            //empty
        }

        #endregion

        #region Methods
        public void Update(int axis_x, int axis_y)
        {
            base.Update();

            double new_x = FitMi_Controller.PuckPack0.Gyrometer[axis_x];
            double new_y = FitMi_Controller.PuckPack0.Gyrometer[axis_y];
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
        }

        #endregion
    }
}