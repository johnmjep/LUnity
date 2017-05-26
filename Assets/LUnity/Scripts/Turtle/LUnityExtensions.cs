using System.Collections;
using System.Collections.Generic;

namespace LUnity
{
    public static class LUnityExtensions
    {
        /// <summary>
        /// Extension method for float to convert degrees to radians
        /// </summary>
        /// <param name="value">Angle in degrees</param>
        /// <returns>Angle in radians</returns>
        public static float ToRadians(this float value)
        {
            return value * 0.0174533f;
        }

        /// <summary>
        /// Extension method for float to convert radians to degrees
        /// </summary>
        /// <param name="value">Angle in radians</param>
        /// <returns>Angle in degrees</returns>
        public static float ToDegrees(this float value)
        {
            return value * 57.2958f;
        }
    } 
}
