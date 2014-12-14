using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OptLab.Func;
using OptLab.Graph;

namespace OptLab.Gradient
{
    public partial class GradientSearchForm : Form
    {
        private LagrangeSearchTask _done = null;
        private List<LagrangeSearchTask> _list = null;

        public GradientSearchForm()
        {
            InitializeComponent();
        }

        public List<LagrangeSearchTask> List
        {
            get { return _list; }
            set { _list = value; }
        }

        private void GradientSearchForm_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            listBox1.Items.Clear();
            if (_list != null)
            {
                foreach (LagrangeSearchTask task in _list)
                {
                    listBox1.Items.Add(new ListBoxItem() { Text = task.Name, Tag = task });
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            backgroundWorker1.RunWorkerAsync(((ListBoxItem) listBox1.SelectedItem).Tag);
            Cursor.Current = Cursors.WaitCursor;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (_done==null) return;
            var graph = new PlotterForm(new Function(_done.GetoriginFunc()), _done) { MdiParent = this.MdiParent };
            graph.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var sd = new SaveFileDialog();
            sd.Filter = @"Text document | *.txt";
            if (sd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(sd.FileName);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            richTextBox1.Text = "";
            _done = null;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            richTextBox1.Text = (string) ((ListBoxItem) listBox2.SelectedItem).Tag;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var task = (LagrangeSearchTask) e.Argument;
            task.CreateLagrange();
            task.StartSearchLagrange();
            e.Result = task;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var task = (LagrangeSearchTask) e.Result;
            for (int i = 0; i < task.Steps.Count; i++)
                listBox2.Items.Add(new ListBoxItem() { Text = task.StepsName[i], Tag = task.StepsStr[i] });
            richTextBox1.Text = task.OutInfo();
            richTextBox1.AppendText(task.GetSearchInfo());
            _done = task;
            Cursor.Current = Cursors.Default;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (_done == null) return;
            richTextBox1.Text = _done.GetSearchInfo();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            richTextBox1.Text = ((LagrangeSearchTask)((ListBoxItem)listBox1.SelectedItem).Tag).OutInfo();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var ts = new NewTaskDialog() {MdiParent = this.MdiParent, ListTask = _list };
            if (ts.ShowDialog() == DialogResult.OK ) LoadList();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            _list.Remove((LagrangeSearchTask) ((ListBoxItem) listBox1.SelectedItem).Tag);
            LoadList();
        }
    }

    public class ListBoxItem
    {
        public object Tag;
        public string Text;
        public override string ToString() { return Text; }
    }
}
