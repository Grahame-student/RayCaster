using System;

namespace RayCaster.LibRay
{ 
    public class Ray
    {
        private const Single DEGREES_RIGHT = 0;
        private const Single DEGREES_TOP = 90;
        private const Single DEGREES_LEFT = 180;
        private const Single DEGREES_BOTTOM = 270;
        private const Single DEGREES_IN_CIRCLE = 360;

        private const Single DEG_TO_RAD = MathF.PI / 180;

        private const Single MAX_UNITS = 999999;

        /// <summary>
        /// Angle of ray in degrees
        /// </summary>
        /// <remarks>
        /// 0 degrees faces right
        /// As degrees increase the ray rotates counter-clockwise
        /// </remarks>
        public Single Theta { get; }
        /// <summary>
        /// Angle of ray in radians
        /// </summary>
        /// <remarks>
        /// This value is normalised to a value from 0 to 2 * Pi
        /// </remarks>
        public Single ThetaRad { get; }
        /// <summary>
        /// Indicates if the ray is pointing in an upwards direction
        /// </summary>
        public Boolean Up { get; }
        /// <summary>
        /// Indicates if the ray is pointing towards the right
        /// </summary>
        public Boolean Right { get; }
        /// <summary>
        /// Distance to travel on Y axis when looking for horizontal edges
        /// </summary>
        public Single Hdy { get; }
        /// <summary>
        /// Distance to travel on X axis when looking for horizontal edges
        /// </summary>
        public Single Hdx { get; }
        /// <summary>
        /// Distance to travel on Y axis when looking for vertical edges
        /// </summary>
        public Single Vdy { get; }
        /// <summary>
        /// Distance to travel on X axis when looking for vertical edges
        /// </summary>
        public Single Vdx { get; }

        public Ray(Single theta)
        {
            Theta = GetNormalisedTheta(theta);
            ThetaRad = theta * DEG_TO_RAD;
            Up = Theta is > DEGREES_RIGHT and < DEGREES_LEFT;
            Right = Theta is < DEGREES_TOP or > DEGREES_BOTTOM;
            Single tanTheta = MathF.Tan(ThetaRad);
            Hdy = Up ? -1 : 1;
            Hdx = Theta == 0 ? MAX_UNITS : 1 / tanTheta;
            Vdy = tanTheta;
            Vdx = Right ? 1 : -1;
        }


        private static Single GetNormalisedTheta(Single theta)
        {
            Single result = theta % DEGREES_IN_CIRCLE;
            return result > 0 ? result : result + DEGREES_IN_CIRCLE;
        }
    }
}
