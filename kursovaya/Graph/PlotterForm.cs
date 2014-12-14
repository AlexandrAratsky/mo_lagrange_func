using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;
using OptLab.Func;

namespace OptLab.Graph
{
    public partial class PlotterForm : Form
    {
        private bool _isColoring = false;

        public PlotterForm(Function func, LagrangeSearchTask task)
        {
            InitializeComponent();
            contourPlotter1.SetFunction(func.Eval,func.ToString());
            if (task.Points != null)
            {
                contourPlotter1.ListOfPoints = task.Points;
                contourPlotter1.DrawSteps = true;
                showPointsToolStripMenuItem.Checked = true;
            }
            if (task.L != null)
            {
                contourPlotter1.L = task.L;
                contourPlotter1.DrawAreaL = true;
                showAreaToolStripMenuItem.Checked = true;
            }
            if (task.H != null)
            {
                contourPlotter1.H = task.H;
                contourPlotter1.DraeAreaH = true;
                showAreaHToolStripMenuItem.Checked = true;
            }
        }

        public PlotterForm()
        {
            InitializeComponent();
        }

        private void PlotterForm_Load(object sender, EventArgs e)
        {
            _isColoring = contourPlotter1.ColorOn;
            toolStripSplitButton2.Image = !_isColoring ?
                global::OptLab.Properties.Resources.stock_draw_freeform_line_filled_3110 : global::OptLab.Properties.Resources.stock_draw_freeform_line_6979;
            tOfX.Text = contourPlotter1.Area.X.ToString("0");
            tOfY.Text = contourPlotter1.Area.Y.ToString("0");
            tOfW.Text = contourPlotter1.Area.Width.ToString("0");
            tOfH.Text = contourPlotter1.Area.Height.ToString("0");
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var bmp = new Bitmap(contourPlotter1.Width, contourPlotter1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            contourPlotter1.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            var sd = new SaveFileDialog();
            sd.Filter = @"Bitmap image | *.bmp";
            if (sd.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(sd.FileName+".bmp", ImageFormat.Bmp);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!btnResize.Checked)
            {
                btnResize.Checked = true;
                tOfX.Visible = true;
                tOfY.Visible = true;
                tOfW.Visible = true;
                tOfH.Visible = true;
            }
            else
            {
                btnResize.Checked = false;
                tOfX.Visible = false;
                tOfY.Visible = false;
                tOfW.Visible = false;
                tOfH.Visible = false;
                var rect = new Rectangle();
                try
                {
                    rect.X = Int32.Parse(tOfX.Text);
                    rect.Y = Int32.Parse(tOfY.Text);
                    rect.Width = Int32.Parse(tOfW.Text);
                    rect.Height = Int32.Parse(tOfH.Text);
                    if (rect.Width == 0 || rect.Height == 0) throw new Exception("Width or Height is zero!");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(@"Exception:\n" + exception.Message, @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                contourPlotter1.Area = rect;
            }

        }

        private void toolStripSplitButton2_ButtonClick(object sender, EventArgs e)
        {
            _isColoring = !_isColoring;
            toolStripSplitButton2.Image = !_isColoring ?
                global::OptLab.Properties.Resources.stock_draw_freeform_line_filled_3110 : global::OptLab.Properties.Resources.stock_draw_freeform_line_6979;
            Cursor.Current = Cursors.WaitCursor;
            contourPlotter1.ColorOn = _isColoring;
            Cursor.Current = Cursors.Default;
        }

        private void showPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            contourPlotter1.DrawSteps = showPointsToolStripMenuItem.Checked;
            Cursor.Current = Cursors.Default;
        }

        private void showGridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            contourPlotter1.GridOn = showGridToolStripMenuItem1.Checked;
            Cursor.Current = Cursors.Default;
        }

        private void showAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            contourPlotter1.DrawAreaL = showAreaToolStripMenuItem.Checked;
            Cursor.Current = Cursors.Default;
        }

        private void showAreaHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            contourPlotter1.DraeAreaH = showAreaHToolStripMenuItem.Checked;
            Cursor.Current = Cursors.Default;
        }
    }
}
