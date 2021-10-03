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

        private const Single SMALL_DELTA = 0.0001f;

        // Value to use when the result of the Tan function is infinite
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
        public Single Hdy { get; private set; }

        /// <summary>
        /// Distance to travel on X axis when looking for horizontal edges
        /// </summary>
        public Single Hdx { get; private set; }

        /// <summary>
        /// Distance to travel on Y axis when looking for vertical edges
        /// </summary>
        public Single Vdy { get; private set; }

        /// <summary>
        /// Distance to travel on X axis when looking for vertical edges
        /// </summary>
        public Single Vdx { get; private set; }

        public Ray(Single theta)
        {
            Theta = GetNormalisedTheta(theta);
            ThetaRad = theta * DEG_TO_RAD;
            Up = Theta is > DEGREES_RIGHT and < DEGREES_LEFT;
            Right = Theta is < DEGREES_TOP or > DEGREES_BOTTOM;
            Single tanTheta = MathF.Tan(ThetaRad);

            SetHorizontalSearchParams(tanTheta);
            SetVerticalSearchParams(tanTheta);
        }

        private static Single GetNormalisedTheta(Single theta)
        {
            Single result = theta % DEGREES_IN_CIRCLE;
            return result < 0 ? result + DEGREES_IN_CIRCLE : result;
        }

        private void SetHorizontalSearchParams(Single tanTheta)
        {
            Hdy = Up ? -1 : 1;
            if (IsHorizontal())
            {
                Hdx = Right ? MAX_UNITS : -MAX_UNITS;
            }
            else
            {
                Single result = MathF.Abs(1 / tanTheta);
                Hdx = Right ? result : -result;
            }
        }

        private void SetVerticalSearchParams(Single tanTheta)
        {
            if (IsVertical())
            {
                Vdy = Up ? -MAX_UNITS : MAX_UNITS;
            }
            else
            {
                Vdy = tanTheta;
            }

            Vdx = Right ? 1 : -1;
        }

        private Boolean IsHorizontal()
        {
            return Math.Abs(Theta - DEGREES_RIGHT) < SMALL_DELTA || 
                   Math.Abs(Theta - DEGREES_LEFT) < SMALL_DELTA;
        }

        private Boolean IsVertical()
        {
            return Math.Abs(Theta - DEGREES_TOP) < SMALL_DELTA || 
                   Math.Abs(Theta - DEGREES_BOTTOM) < SMALL_DELTA;
        }
    }
}
