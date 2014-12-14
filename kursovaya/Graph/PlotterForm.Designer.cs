namespace OptLab.Graph
{
    partial class PlotterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlotterForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.contourPlotter1 = new OptLab.Graph.ContourPlot();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.showPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGridToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAreaHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnResize = new System.Windows.Forms.ToolStripButton();
            this.tOfX = new System.Windows.Forms.ToolStripTextBox();
            this.tOfY = new System.Windows.Forms.ToolStripTextBox();
            this.tOfW = new System.Windows.Forms.ToolStripTextBox();
            this.tOfH = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.contourPlotter1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(484, 461);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(484, 486);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.TopToolStripPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            // 
            // contourPlotter1
            // 
            this.contourPlotter1.Area = new System.Drawing.Rectangle(-10, -10, 20, 20);
            this.contourPlotter1.AutoSize = true;
            this.contourPlotter1.ColorOn = true;
            this.contourPlotter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contourPlotter1.DraeAreaH = false;
            this.contourPlotter1.DrawAreaL = false;
            this.contourPlotter1.DrawSteps = false;
            this.contourPlotter1.GridOn = false;
            this.contourPlotter1.H = null;
            this.contourPlotter1.L = null;
            this.contourPlotter1.Levels = 25;
            this.contourPlotter1.LineOn = true;
            this.contourPlotter1.ListOfPoints = null;
            this.contourPlotter1.Location = new System.Drawing.Point(0, 0);
            this.contourPlotter1.Name = "contourPlotter1";
            this.contourPlotter1.Offset = 20;
            this.contourPlotter1.Size = new System.Drawing.Size(484, 461);
            this.contourPlotter1.StepSize = 5;
            this.contourPlotter1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton2,
            this.toolStripSeparator1,
            this.btnResize,
            this.tOfX,
            this.tOfY,
            this.tOfW,
            this.tOfH,
            this.toolStripSeparator3,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(102, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPointsToolStripMenuItem,
            this.showGridToolStripMenuItem1,
            this.showAreaToolStripMenuItem,
            this.showAreaHToolStripMenuItem});
            this.toolStripSplitButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton2.Image")));
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton2.Text = "toolStripSplitButton2";
            this.toolStripSplitButton2.ButtonClick += new System.EventHandler(this.toolStripSplitButton2_ButtonClick);
            // 
            // showPointsToolStripMenuItem
            // 
            this.showPointsToolStripMenuItem.CheckOnClick = true;
            this.showPointsToolStripMenuItem.Name = "showPointsToolStripMenuItem";
            this.showPointsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.showPointsToolStripMenuItem.Text = "Show points";
            this.showPointsToolStripMenuItem.Click += new System.EventHandler(this.showPointsToolStripMenuItem_Click);
            // 
            // showGridToolStripMenuItem1
            // 
            this.showGridToolStripMenuItem1.CheckOnClick = true;
            this.showGridToolStripMenuItem1.Name = "showGridToolStripMenuItem1";
            this.showGridToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.showGridToolStripMenuItem1.Text = "Show grid";
            this.showGridToolStripMenuItem1.Click += new System.EventHandler(this.showGridToolStripMenuItem1_Click);
            // 
            // showAreaToolStripMenuItem
            // 
            this.showAreaToolStripMenuItem.CheckOnClick = true;
            this.showAreaToolStripMenuItem.Name = "showAreaToolStripMenuItem";
            this.showAreaToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.showAreaToolStripMenuItem.Text = "Show area L";
            this.showAreaToolStripMenuItem.Click += new System.EventHandler(this.showAreaToolStripMenuItem_Click);
            // 
            // showAreaHToolStripMenuItem
            // 
            this.showAreaHToolStripMenuItem.CheckOnClick = true;
            this.showAreaHToolStripMenuItem.Name = "showAreaHToolStripMenuItem";
            this.showAreaHToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.showAreaHToolStripMenuItem.Text = "Show area H";
            this.showAreaHToolStripMenuItem.Click += new System.EventHandler(this.showAreaHToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnResize
            // 
            this.btnResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResize.Image = global::OptLab.Properties.Resources.stock_position_size_1807;
            this.btnResize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResize.Name = "btnResize";
            this.btnResize.Size = new System.Drawing.Size(23, 22);
            this.btnResize.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // tOfX
            // 
            this.tOfX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tOfX.Name = "tOfX";
            this.tOfX.Size = new System.Drawing.Size(45, 25);
            this.tOfX.Visible = false;
            // 
            // tOfY
            // 
            this.tOfY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tOfY.Name = "tOfY";
            this.tOfY.Size = new System.Drawing.Size(45, 25);
            this.tOfY.Visible = false;
            // 
            // tOfW
            // 
            this.tOfW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tOfW.Name = "tOfW";
            this.tOfW.Size = new System.Drawing.Size(45, 25);
            this.tOfW.Visible = false;
            // 
            // tOfH
            // 
            this.tOfH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tOfH.Name = "tOfH";
            this.tOfH.Size = new System.Drawing.Size(45, 25);
            this.tOfH.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::OptLab.Properties.Resources.picture_save_4436;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Сохранить картинку";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // PlotterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 486);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "PlotterForm";
            this.Text = "Контурный график функции";
            this.Load += new System.EventHandler(this.PlotterForm_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private ContourPlot contourPlotter1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
        private System.Windows.Forms.ToolStripTextBox tOfX;
        private System.Windows.Forms.ToolStripTextBox tOfY;
        private System.Windows.Forms.ToolStripTextBox tOfW;
        private System.Windows.Forms.ToolStripTextBox tOfH;
        private System.Windows.Forms.ToolStripButton btnResize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem showPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGridToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAreaHToolStripMenuItem;




    }
}