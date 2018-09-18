using System;
using System.Collections.Generic;

namespace Utilities
{
    public class Axes
    {
        private List<int> measurementDevice;

        private int index;

        public Axes(List<int> md, int i)
        {
            measurementDevice = md;
            index = i;
        }

        public double GetValue()
        {
            return measurementDevice[index];
        }
    }
}
