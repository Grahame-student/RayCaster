
namespace RayCaster.FrontEnd
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picRender = new System.Windows.Forms.PictureBox();
            this.pic2DMap = new System.Windows.Forms.PictureBox();
            this.btnRender = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radWall = new System.Windows.Forms.RadioButton();
            this.radFloor = new System.Windows.Forms.RadioButton();
            this.chkDrawMapRays = new System.Windows.Forms.CheckBox();
            this.slideAngle = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2DMap)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slideAngle)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.picRender, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pic2DMap, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1298, 487);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // picRender
            // 
            this.picRender.BackColor = System.Drawing.Color.White;
            this.picRender.Location = new System.Drawing.Point(655, 3);
            this.picRender.Name = "picRender";
            this.picRender.Size = new System.Drawing.Size(640, 480);
            this.picRender.TabIndex = 1;
            this.picRender.TabStop = false;
            this.picRender.Paint += new System.Windows.Forms.PaintEventHandler(this.picRender_Paint);
            // 
            // pic2DMap
            // 
            this.pic2DMap.BackColor = System.Drawing.Color.White;
            this.pic2DMap.Location = new System.Drawing.Point(3, 3);
            this.pic2DMap.Name = "pic2DMap";
            this.pic2DMap.Size = new System.Drawing.Size(641, 481);
            this.pic2DMap.TabIndex = 2;
            this.pic2DMap.TabStop = false;
            this.pic2DMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pic2DMap_Paint);
            this.pic2DMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic2DMap_MouseUp);
            // 
            // btnRender
            // 
            this.btnRender.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRender.Location = new System.Drawing.Point(0, 644);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(1298, 27);
            this.btnRender.TabIndex = 1;
            this.btnRender.Text = "Render";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radWall);
            this.panel1.Controls.Add(this.radFloor);
            this.panel1.Location = new System.Drawing.Point(3, 493);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 145);
            this.panel1.TabIndex = 2;
            // 
            // radWall
            // 
            this.radWall.AutoSize = true;
            this.radWall.Checked = true;
            this.radWall.Location = new System.Drawing.Point(9, 28);
            this.radWall.Name = "radWall";
            this.radWall.Size = new System.Drawing.Size(48, 19);
            this.radWall.TabIndex = 1;
            this.radWall.TabStop = true;
            this.radWall.Text = "Wall";
            this.radWall.UseVisualStyleBackColor = true;
            this.radWall.Click += new System.EventHandler(this.TileType_Click);
            // 
            // radFloor
            // 
            this.radFloor.AutoSize = true;
            this.radFloor.Location = new System.Drawing.Point(9, 3);
            this.radFloor.Name = "radFloor";
            this.radFloor.Size = new System.Drawing.Size(52, 19);
            this.radFloor.TabIndex = 0;
            this.radFloor.Text = "Floor";
            this.radFloor.UseVisualStyleBackColor = true;
            this.radFloor.Click += new System.EventHandler(this.TileType_Click);
            // 
            // chkDrawMapRays
            // 
            this.chkDrawMapRays.AutoSize = true;
            this.chkDrawMapRays.Checked = true;
            this.chkDrawMapRays.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawMapRays.Location = new System.Drawing.Point(1203, 493);
            this.chkDrawMapRays.Name = "chkDrawMapRays";
            this.chkDrawMapRays.Size = new System.Drawing.Size(83, 19);
            this.chkDrawMapRays.TabIndex = 3;
            this.chkDrawMapRays.Text = "checkBox1";
            this.chkDrawMapRays.UseVisualStyleBackColor = true;
            this.chkDrawMapRays.CheckedChanged += new System.EventHandler(this.chkDrawMapRays_CheckedChanged);
            // 
            // slideAngle
            // 
            this.slideAngle.Location = new System.Drawing.Point(209, 493);
            this.slideAngle.Maximum = 0;
            this.slideAngle.Minimum = -360;
            this.slideAngle.Name = "slideAngle";
            this.slideAngle.Size = new System.Drawing.Size(988, 45);
            this.slideAngle.TabIndex = 6;
            this.slideAngle.TickFrequency = 30;
            this.slideAngle.Value = -90;
            this.slideAngle.Scroll += new System.EventHandler(this.slideAngle_Scroll);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 671);
            this.Controls.Add(this.slideAngle);
            this.Controls.Add(this.chkDrawMapRays);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRender);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmMain";
            this.Text = "Ray Caster Demo";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2DMap)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slideAngle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox picRender;
        private System.Windows.Forms.PictureBox pic2DMap;
        private System.Windows.Forms.Button btnRender;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radWall;
        private System.Windows.Forms.RadioButton radFloor;
        private System.Windows.Forms.CheckBox chkDrawMapRays;
        private System.Windows.Forms.TrackBar slideAngle;
    }
}

