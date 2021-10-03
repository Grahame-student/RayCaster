using System;
using System.Drawing;
using RayCaster.FrontEnd;

namespace RayCaster
{
    internal static class BrushFactory
    {
        private static readonly SolidBrush BrushPlayer = new(Color.Red);
        private static readonly SolidBrush BrushFloor = new(Color.White);
        private static readonly SolidBrush BrushWall = new(Color.Gray);
        private static readonly SolidBrush BrushCeiling= new(Color.Black);
        private static readonly SolidBrush BrushBoundary = new(Color.DarkGray);

        internal static Brush GetBrush(MapObjectType objectType)
        {
            return objectType switch
            {
                MapObjectType.Player => BrushPlayer,
                MapObjectType.Floor => BrushFloor,
                MapObjectType.Wall => BrushWall,
                MapObjectType.Ceiling => BrushCeiling,
                MapObjectType.Boundary => BrushBoundary,
                _ => throw new ArgumentOutOfRangeException(nameof(objectType), objectType, @"Invalid brush requested, nothing associated with requested MapObjectType")
            };
        }
    }
}
