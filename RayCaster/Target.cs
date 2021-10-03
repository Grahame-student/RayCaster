using System;

namespace RayCaster.FrontEnd
{
    internal class Target
    {
        internal Single X { get; }
        internal Single Y { get; }
        internal Boolean Valid { get; }
        internal Single Dist { get; }
        internal MapObjectType Type { get; }

        internal Target(Single originX, Single originY, Single destX, Single destY, MapObjectType type)
        {
            X = destX;
            Y = destY;
            Single dTx = X - originX;
            Single dTy = Y - originY;
            Dist = MathF.Sqrt((dTx * dTx) + (dTy * dTy));
            Valid = true;
            Type = type;
        }

        public Target()
        {
            X = 0;
            Y = 0;
            Valid = false;
        }
    }
}
