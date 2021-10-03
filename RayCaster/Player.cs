using System;

namespace RayCaster.FrontEnd
{
    internal class Player
    {
        private const Int32 DIR_NORTH = 90;

        internal Int32 X { get; private set; }
        internal Int32 Y { get; private set; }
        internal Int32 Angle { get; set; }

        internal Player(Int32 col, Int32 row)
        {
            Angle = DIR_NORTH;
            SetPosition(col, row);
        }

        internal void SetPosition(Int32 col, Int32 row)
        {
            X = col;
            Y = row;
        }
    }
}
