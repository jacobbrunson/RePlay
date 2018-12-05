using System;
using Android.Graphics;

namespace RePlay.Entity
{
    public class Patient
    {
        public string First;
        public string Last;
        public Bitmap Photo;


        public string FullName {
            get {
                return First + " " + Last;
            }
        }
    }
}
