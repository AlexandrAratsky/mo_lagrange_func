using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using OptLab.Gradient;
using OptLab.Graph;
using OptLab.Func;
using Mathos.Parser;

namespace OptLab
{
    public partial class MainForm : Form
    {
        private List<LagrangeSearchTask> _taskList = new List<LagrangeSearchTask>(); 

        public MainForm()
        {
            InitializeComponent();
        }

        private void newTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newMdiChild = new NewTaskDialog() { MdiParent = this, ListTask = _taskList };
            newMdiChild.Show();
        }

        private void taskListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newMdiChild = new GradientSearchForm() { MdiParent = this, List = _taskList };
            newMdiChild.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var t1 = new LagrangeSearchTask("x^2+(1/3)*y^2-x*y", "2*x-y", "(2/3)*y-x", new Vector(8d, 8d)) { Name = "Example_1" };
            t1.AddLFunc(1, new Function("2-x^2-y^2"), new VectorFunction("-2*x", "-2*y"), 1d);
            _taskList.Add(t1);

            var t2 = new LagrangeSearchTask("y", "0", "1", new Vector(0, 0.5d)) { Name = "Example_2" };
            t2.AddLFunc(1, new Function("x^2+4*y^2-4"), new VectorFunction("2*x", "8*y"), 1d);
            t2.AddLFunc(2, new Function("x^3-y"), new VectorFunction("3*x^2", "-1"), 1d);
            _taskList.Add(t2);

            var t3 = new LagrangeSearchTask("x^2+y^2", "2*x", "2*y", new Vector(-1, 2)) { Name = "Example_3" };
            t3.AddHFunc(1, new Function("1-x-y"), new VectorFunction("-1", "-1"), 0);
            _taskList.Add(t3);

            var t4 = new LagrangeSearchTask("x^2+y^2", "2*x", "2*y", new Vector(2, 1)) { Name = "Example_4" };
            t4.AddLFunc(1, new Function("1-x+y^2"), new VectorFunction("-1", "2*y"), 1);
            t4.AddLFunc(2, new Function("(x-1)^2-y"), new VectorFunction("2*x-2", "-1"), 1);
            _taskList.Add(t4);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newMdiChild = new PlotterForm() { MdiParent = this };
            newMdiChild.Show();
        }
    }
}
