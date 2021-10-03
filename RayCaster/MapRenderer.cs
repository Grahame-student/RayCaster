using System;
using System.Diagnostics;
using System.Drawing;
using RayCaster.LibRay;

namespace RayCaster.FrontEnd
{
    internal class MapRenderer
    {
        private const Single RADIANS_MAX = MathF.PI * 2;
        private const Single RADIANS_TOP = (MathF.PI * 2 * 0.25f);
        private const Single RADIANS_BOTTOM = (MathF.PI * 2 * 0.75f);
        private const Single RADIANS_LEFT = MathF.PI;
        private const Single RADIANS_RIGHT = 0;

        private const Single DEG_TO_RAD = MathF.PI / 180;

        private readonly Int32 _width;
        private readonly Int32 _height;

        // The number of degrees in the player's field of view
        private readonly Single _fieldOfView = 60;

        internal Int32 CellWidth { get; }
        internal Int32 CellHeight { get; }

        internal MapRenderer(Int32 width, Int32 height, Int32 cols, Int32 rows)
        {
            _width = width;
            _height = height;
            CellWidth = _width / cols;
            CellHeight = _height / rows;
        }

        internal Int32 GetCol(Int32 x)
        {
            return x / CellWidth;
        }

        internal Int32 GetCol(Single x)
        {
            return (Int32)(x / CellWidth);
        }

        internal Int32 GetRow(Int32 y)
        {
            return y / CellHeight;
        }

        internal Int32 GetRow(Single y)
        {
            return (Int32)(y / CellHeight);
        }

        internal void Update(Map gameMap, Graphics graphics, Boolean drawRays)
        {
            graphics.Clear(Color.White);
            DrawPlayerDirection(gameMap, graphics);

            if (drawRays)
            {
                DrawMapRays(gameMap, graphics);
            }
            DrawMapCells(gameMap, graphics);
            DrawMapGrid(gameMap, graphics);
        }

        private void DrawMapCells(Map gameMap, Graphics graphics)
        {
            for (var col = 0; col < gameMap.Columns; col++)
            {
                for (var row = 0; row < gameMap.Rows; row++)
                {
                    if (gameMap[col, row].Type != MapObjectType.Floor)
                    {
                        DrawCell(graphics, col, row, gameMap[col, row].Type);
                    }
                }
            }
        }

        private void DrawCell(Graphics graphics, Int32 col, Int32 row, MapObjectType type)
        {
            graphics.FillRectangle(BrushFactory.GetBrush(type), col * CellWidth, row * CellWidth, CellWidth, CellHeight);
        }

        private void DrawMapGrid(Map gameMap, Graphics graphics)
        {
            var pen = new Pen(Color.DeepSkyBlue);
            for (var col = 0; col <= gameMap.Columns; col++)
            {
                graphics.DrawLine(pen, col * CellWidth, 0, col * CellWidth, _height);
            }

            for (var row = 0; row <= gameMap.Rows; row++)
            {
                graphics.DrawLine(pen, 0, row * CellHeight, _width, row * CellHeight);
            }
        }

        private void DrawPlayerDirection(Map gameMap, Graphics graphics)
        {
            Player player = gameMap.Player;
            Int32 eyesX = (player.X * CellWidth) + (CellWidth / 2);
            Int32 eyesY = player.Y * CellHeight;
            graphics.FillRectangle(BrushFactory.GetBrush(MapObjectType.Ceiling), eyesX - 2, eyesY - 2, 4, 4);
        }

        private void DrawMapRays(Map gameMap, Graphics graphics)
        {
            // Get point to start all rays from, aka the player's eyes
            Player player = gameMap.Player;

            // First ray is from the right of the field of view and in the direction the player is looking
            Single theta = player.Angle - (_fieldOfView / 2f);

            // the amount to increment theta by for each ray
            // generate 1 ray for each pixel column
            Single dTheta = _fieldOfView / _width;
            for (var rays = 0; rays < _width; rays++)
            {
                theta += dTheta;
                CastRay(gameMap, graphics, player, theta);
            }
        }

        public void CastRay(Map gameMap, Graphics graphics, Player player, Single theta)
        {
            // Player position in pixels
            Single pX = (player.X * CellWidth) + (CellWidth / 2);
            Single pY = (player.Y * CellHeight);

            var ray = new Ray(theta);
            //Debug.WriteLine($"Theta: {ray.Theta}");
            Target hTarget = GetHorizontalWall(gameMap, ray, pX, pY);
            //Target vTarget = GetVerticalWall(gameMap, theta, pX, pY);

            graphics.DrawLine(new Pen(Color.Gold), pX, pY, hTarget.X, hTarget.Y);

            /*
                Steps of finding intersections with vertical grid lines:

                1. Find coordinate of the first intersection (point B in this example).
                   The ray is facing right in the picture, so B.x = rounded_down(Px/64) * (64) + 64.
                   If the ray had been facing left B.x = rounded_down(Px/64) * (64) – 1.
                   A.y = Py + (Px-A.x)*tan(ALPHA);

                2. Find Xa. 
                   Note: Xa is just the width of the grid;
                         if the ray is facing right, Xa will be positive.
                         if the ray is facing left,  Ya will be negative.
                
                3. Find Ya using the equation given above.

                4. Check the grid at the intersection point. If there is a wall on the grid, stop and calculate the distance.

                5. If there is no wall, extend the to the next intersection point. Notice that the coordinate of the next intersection point -call it (Xnew,Ynew) is just Xnew=Xold+Xa, and Ynew=YOld+Ya.
             */

        }

        private Target GetHorizontalWall(Map gameMap, Ray ray, Single pX, Single pY)
        {
            // Find horizontal walls
            //
            // |    A|
            // +---/-+
            // |  /  |  ^ - player facing up
            // | ^   |  / - ray at 60 degrees
            // |     |  A - first pixel inside next tile in direction of ray
            // |     |
            // |     |
            // +-----+
            //
            // Locate nearest horizontal tile boundary in direction of ray
            // Tiles contain pixels from (y * CellHeight) to (y * CellHeight) - 1, e.g. 0 to 39 for 40 x 40 tiles

            Single aY = MathF.Floor(pY / CellHeight) * CellHeight - 1;
            // Move along the x-axis by a factor of the amount moved along the y-axis
            Single aX = pX + ((pY - aY) * ray.Hdx);

            Single nextY = aY;
            Single nextX = aX;

            while(IsWithinVisibleBounds(nextX, nextY))
            {
                if (gameMap[GetCol(nextX), GetRow(nextY)].Type == MapObjectType.Wall) break;
                nextY += ray.Hdy;
                nextX += ray.Hdx;
            }
            return new Target(pX, pY, nextX, nextY);
        }

        private Boolean IsWithinVisibleBounds(Single nextX, Single nextY)
        {
            return (nextX > 0 && nextX < _width && nextY > 0 && nextY < _height);
        }

        private static Boolean IsHorizontal(Single thetaRad)
        {
            return thetaRad != 0 || Math.Abs(thetaRad - MathF.PI) > 0.0001f;
        }

        private Target GetVerticalWall(Map gameMap, Single theta, Single pX, Single pY)
        {
            // Between 0 and 2 * Pi
            Single thetaRad = GetCorrectedTheta((theta) * DEG_TO_RAD);
            Single correctionY = IsUp(thetaRad) ? -1 : 1;
            Single tanTheta = MathF.Tan(thetaRad);

            // Find vertical walls
            // Locate nearest vertical tile boundary in direction of ray
            // Tiles contain pixels from (x * CellWidth) to (x * CellWidth) - 1, e.g. 0 to 39 for 40 x 40 tiles
            Single bX = IsRight(thetaRad) ? MathF.Floor(pX / CellWidth) * CellWidth + CellWidth : MathF.Floor(pX / CellWidth) * CellWidth - 1;
            // Move along the y-axis by a factor of the amount moved along the x-axis
            Single bY = pY + (pX - bX) / tanTheta;

            // Calculate constant steps to move by
            Single dX = CellWidth;
            Single dY = (CellHeight / tanTheta) * correctionY;

            Debug.WriteLine($"theta: {theta:F5}, right: {IsRight(theta)}, pX: {pX}, pY: {pY}, bX: {bX}, by: {bY}");

            Single nextX = bX;
            Single nextY = bY;

            while (nextX > 0 && nextX < _width && nextY > 0 && nextY < _height)
            {
                if (gameMap[GetCol(nextX), GetRow(nextY)].Type == MapObjectType.Wall) break;
                nextY += dY;
                nextX += dX;
            }

            return new Target(pX, pY, nextX, nextY);
        }

        private bool IsVertical(Single thetaRad)
        {
            return Math.Abs(thetaRad - (MathF.PI / 2)) < 0.0001f || Math.Abs(thetaRad - ((3 * MathF.PI) / 2)) < 0.0001f;
        }

        private static Boolean IsUp(Single thetaRad)
        {
            return thetaRad is > RADIANS_RIGHT and < RADIANS_LEFT;
        }

        private static Boolean IsRight(Single thetaRad)
        {
            return thetaRad is > RADIANS_BOTTOM or < RADIANS_TOP;
        }

        private static Single GetCorrectedTheta(Single theta)
        {
            theta %= RADIANS_MAX;
            if (theta < 0)
            {
                theta += RADIANS_MAX;
            }
            return theta;
        }

        #region draw 3d view
        internal void RayCast(Map map, Graphics graphics)
        {
            DrawBackground(graphics);
            RenderMap(map, graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            Int32 midHeight = _height / 2;
            graphics.FillRectangle(BrushFactory.GetBrush(MapObjectType.Ceiling), 0, 0, _width, midHeight);
            graphics.FillRectangle(BrushFactory.GetBrush(MapObjectType.Floor), 0, midHeight, _width, midHeight);
        }

        private void RenderMap(Map map, Graphics graphics)
        {
        }
        #endregion
    }
}
