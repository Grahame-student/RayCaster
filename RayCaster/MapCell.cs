namespace RayCaster.FrontEnd
{
    internal class MapCell
    {
        internal MapObjectType Type { get; set; }

        internal MapCell(MapObjectType type)
        {
            Type = type;
        }
    }
}
