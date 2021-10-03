using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace RayCaster.FrontEnd
{
    public partial class FrmMain : Form
    {
        private const Int32 MAP_WIDTH = 640;
        private const Int32 MAP_HEIGHT = 480;

        private readonly Map _map;
        private readonly MapRenderer _render;
        private MapObjectType _tile = MapObjectType.Wall;


        internal FrmMain()
        {
            InitializeComponent();

            radFloor.Tag = MapObjectType.Floor;
            radWall.Tag = MapObjectType.Wall;

            _map = new Map(16, 12);
            _render = new MapRenderer(MAP_WIDTH, MAP_HEIGHT, _map.Columns, _map.Rows);

            _map.SetCell(8, 6, MapObjectType.Player);
        }

        private void FrmMain_Load(Object sender, EventArgs e)
        {
        }

        private void pic2DMap_Paint(Object sender, PaintEventArgs e)
        {
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                _render.Update(_map, e.Graphics, chkDrawMapRays.Checked);
                watch.Stop();

                Debug.WriteLine($"Time to render map: {watch.Elapsed}");
            }
            catch
            {
                Debug.WriteLine("Render error");
            }
        }

        private void pic2DMap_MouseUp(Object sender, MouseEventArgs e)
        {
            Int32 col = _render.GetCol(e.X);
            Int32 row = _render.GetRow(e.Y);

            if (_map[col, row].Type == MapObjectType.Player) return;
            _map.SetCell(col, row, _tile);

            if (_map.Changed)
            {
                pic2DMap.Invalidate();

            }
        }

        private void TileType_Click(Object sender, EventArgs e)
        {
            var button = sender as RadioButton;

            if (button?.Checked == true)
            {
                _tile = (MapObjectType)button.Tag;
            }
        }

        private void btnRender_Click(Object sender, EventArgs e)
        {
            picRender.Invalidate();
        }

        private void picRender_Paint(object sender, PaintEventArgs e)
        {
            _render.RayCast(_map, e.Graphics);
        }

        private void chkDrawMapRays_CheckedChanged(object sender, EventArgs e)
        {
            pic2DMap.Invalidate();
        }

        private void slideAngle_Scroll(object sender, EventArgs e)
        {
            _map.Player.Angle = -slideAngle.Value;
            Debug.WriteLine($"Player Angle: {-slideAngle.Value}");
            pic2DMap.Invalidate();

        }
    }
}
