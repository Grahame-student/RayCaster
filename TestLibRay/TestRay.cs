using System;
using NUnit.Framework;
using RayCaster.LibRay;

namespace RayCaster.Test.TestLibRay
{
    [TestFixture]
    public class TestRay
    {
        private const Int32 SOME_THETA = 60;
        private const Int32 SOME_UP_RIGHT_THETA = 45;    // Up-Right
        private const Int32 SOME_DOWN_LEFT_THETA = 225; // Down-Left
        private const Int32 FULL_CIRCLE = 360;

        // Single.Epsilon is too small a difference to reliably detect
        // 0.0001 is a smaller delta than we'd expect to see in normal use
        private const Single SMALL_DELTA = 0.0001f;

        private const Single MAX_DELTA = 999999;

        private Ray _ray;

        [Test]
        public void Constructor_SetsTheta_ToPassedInAngle()
        {
            _ray = new Ray(SOME_THETA);

            Assert.That(_ray.Theta, Is.EqualTo(SOME_THETA));
        }

        [Test]
        public void Constructor_NormalisesThetaToSingleCircle_WhenThetaGreaterThanFullCircle()
        {
            _ray = new Ray(SOME_THETA + FULL_CIRCLE);

            Assert.That(_ray.Theta, Is.EqualTo(SOME_THETA));
        }

        [Test]
        public void Constructor_NormalisesThetaToSingleCircle_WhenThetaNegative()
        {
            _ray = new Ray(0 - SMALL_DELTA);

            Assert.That(_ray.Theta, Is.EqualTo(FULL_CIRCLE - SMALL_DELTA));
        }

        [Test]
        public void ThetaRad_ReturnsTheta_InRadians()
        {
            _ray = new Ray(SOME_THETA);

            Assert.That(_ray.ThetaRad, Is.EqualTo(SOME_THETA * (MathF.PI / 180)));
        }

        [Test]
        public void Up_ReturnsTrue_WhenThetaGreaterThan0()
        {
            _ray = new Ray(0 + SMALL_DELTA);

            Assert.That(_ray.Up, Is.True);
        }

        [Test]
        public void Up_ReturnsFalse_WhenThetaGreaterThan180()
        {
            _ray = new Ray(180 + SMALL_DELTA);

            Assert.That(_ray.Up, Is.False);
        }

        [Test]
        public void Right_ReturnsTrue_WhenThetaLessThan90()
        {
            _ray = new Ray(90 - SMALL_DELTA);

            Assert.That(_ray.Right, Is.True);
        }

        [Test]
        public void Right_ReturnsTrue_WhenThetaGreaterThan270()
        {
            _ray = new Ray(270 + SMALL_DELTA);

            Assert.That(_ray.Right, Is.True);
        }

        [Test]
        public void Hdy_ReturnsMinus1_WhenRayInDownwardDirection()
        {
            _ray = new Ray(SOME_DOWN_LEFT_THETA);

            Assert.That(_ray.Hdy, Is.EqualTo(1));
        }

        [Test]
        public void Hdy_Returns1_WhenRayInUpwardDirection()
        {
            _ray = new Ray(SOME_UP_RIGHT_THETA);
            Assert.That(_ray.Up, Is.True);

            Assert.That(_ray.Hdy, Is.EqualTo(-1));
        }

        [Test]
        public void Hdy_SanityCheck_OverFullCircle()
        {
            for (Single theta = 0; theta < FULL_CIRCLE; theta += 0.01f)
            {
                _ray = new Ray(theta);
                Assert.That(_ray.Hdy, _ray.Up ? Is.EqualTo(-1) : Is.EqualTo(1));
            }
        }

        [Test]
        public void Hdx_Returns_1OverTanTheta()
        {
            _ray = new Ray(SOME_THETA);

            Single expectedResult = 1 / MathF.Tan(_ray.ThetaRad);
            Assert.That(_ray.Hdx, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Hdx_Returns_1OverTanThet()
        {
            // Ray is horizontal and facing right
            // If the ray travels along Y axis by 1 unit (hint, it can't!)
            // It would need to travel an infinite distance along the X axis
            _ray = new Ray(0);

            Assert.That(_ray.Hdx, Is.EqualTo(MAX_DELTA));
        }


        [Test]
        public void Hdx_Returns_1OverTanThe()
        {
            // Horizontal and facing left
            _ray = new Ray(180);

            Single expectedResult = 1 / MathF.Tan(_ray.ThetaRad);
            Assert.That(_ray.Hdx, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Vdy_Returns_TanTheta()
        {
            _ray = new Ray(SOME_THETA);

            Single expectedResult = MathF.Tan(_ray.ThetaRad);
            Assert.That(_ray.Vdy, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Vdx_Returns1_WhenRayInRightDirection()
        {
            _ray = new Ray(SOME_UP_RIGHT_THETA);

            Assert.That(_ray.Vdx, Is.EqualTo(1));
        }

        [Test]
        public void Vdx_ReturnsMinus1_WhenRayInLeftDirection()
        {
            _ray = new Ray(SOME_DOWN_LEFT_THETA);

            Assert.That(_ray.Vdx, Is.EqualTo(-1));
        }

        [Test]
        public void Vdx_SanityCheck_OverFullCircle()
        {
            for (Single theta = 0; theta < FULL_CIRCLE; theta += 0.01f)
            {
                _ray = new Ray(theta);
                Assert.That(_ray.Vdx, _ray.Right ? Is.EqualTo(1) : Is.EqualTo(-1));
            }
        }
    }
}
