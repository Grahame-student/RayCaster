using System;

namespace RayCaster.FrontEnd
{
    internal class Map
    {
        private readonly MapCell[,] _cells;

        internal Int32 Columns { get; }
        internal Int32 Rows { get; }
        internal Boolean Changed { get; private set; }
        internal Player Player { get; }

        internal MapCell this[Int32 col, Int32 row] => _cells[col, row];

        internal Map(Int32 cols, Int32 rows)
        {
            Player = new Player(0, 0);
            Columns = cols;
            Rows = rows;
            _cells = new MapCell[cols, rows];
            for (var col = 0; col < cols; col++)
            {
                for (var row = 0; row < rows; row++)
                {
                    _cells[col, row] = new MapCell(MapObjectType.Floor);
                }
            }
        }

        internal void SetCell(Int32 col, Int32 row, MapObjectType type)
        {
            Changed = false;
            if (_cells[col, row].Type == type) return;

            _cells[col, row].Type = type;
            if (type == MapObjectType.Player)
            {
                Player.SetPosition(col, row);
            }
            Changed = true;
        }
    }
}
