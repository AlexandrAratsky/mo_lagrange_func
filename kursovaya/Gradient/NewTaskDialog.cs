using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OptLab.Func;

namespace OptLab.Gradient
{
    public partial class NewTaskDialog : Form
    {
        private List<LagrangeSearchTask> _list = null;
        private LagrangeSearchTask _task = new LagrangeSearchTask("x^2+(1/3)*y^2-x*y", "2*x-y", "(2/3)*y-x", new Vector(5.1d, 5d)) 
            { Name = "Example", Sigma = 0.0001d};

        private FuncStr _func = new FuncStr() { _f = "x^2+(1/3)*y^2-x*y", _dxf = "2*x-y", _dyf = "(2/3)*y-x" };


        public NewTaskDialog()
        {
            _task.AddHFunc(1,new Function("x^2-y^2-1"),new VectorFunction("2*x","2*y"), 1f);
            _task.AddLFunc(1,new Function("1-x-y^2"),new VectorFunction("-1","2*y"),1f);
            InitializeComponent();
            LoadValues();
            LoatHFunc();
            LoatLFunc();
            ListFSelected();
            funCombo.SelectedIndex = 0;
         }

        public List<LagrangeSearchTask> ListTask
        {
            get { return _list; }
            set { _list = value; }
        }

        private void ListFSelected()
        {
            if (listF.SelectedItem == null) return;
            var tmp = (Tuple<Tuple<bool, int>,FuncStr>) ((ListBoxItem)listF.SelectedItem).Tag;
            switch (funCombo.SelectedIndex)
            {
                case 0:
                    richF.Text = tmp.Item2._f;
                    break;
                case 1:
                    richF.Text = tmp.Item2._dxf;
                    break;
                case 2:
                    richF.Text = tmp.Item2._dyf;
                    break;
            }
            txtCoef.Text = tmp.Item2._coef.ToString();
        }
        private void GetMethod()
        {
            if (_task.IsGradient)
            {
                comboBox1.SelectedIndex = 0;
                var tmp = new GradientTask("", "", "", new Vector(0, 0));
                richTextBox1.Text = "Params:\n\t∆ = " + tmp.Delta + "\n\tη = " + tmp.Eta + "\n\tμ = " + tmp.Mu;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
                var tmp = new HookeJeevesTask("", new Vector(0, 0));
                richTextBox1.Text = "Params:\n\t∆ = " + tmp.H;
            }
        }
        private void GetPenalty()
        {
            switch (_task.Penalty.PenaltyType)
            {
                case Penalty.Type.Constant:
                    comboBox2.SelectedIndex = 0;
                    break;
                case Penalty.Type.Adaptive:
                    comboBox2.SelectedIndex = 1;
                    break;
                case Penalty.Type.Linear:
                    comboBox2.SelectedIndex = 2;
                    break;
                case Penalty.Type.Square:
                    comboBox2.SelectedIndex = 3;
                    break;
            }
        }
        private void LoatHFunc()
        {
            foreach (var h in _task.H.Keys)
            {
                listF.Items.Add(new ListBoxItem()
                {
                    Tag = new Tuple<Tuple<bool, int>, FuncStr>(new Tuple<bool, int>(true, h), 
                        new FuncStr() { _f = _task.H[h].ToString(), _dxf = _task.Dh[h].FuncX, _dyf = _task.Dh[h].FuncY, _coef = _task.CoefH[h] }),
                    Text = "h" + h + "(x,y)"
                });
                listF.SelectedIndex = 0;
            }
        }
        private void LoatLFunc()
        {
            foreach (var l in _task.L.Keys)
            {
                listF.Items.Add(new ListBoxItem()
                {
                    Tag = new Tuple<Tuple<bool, int>, FuncStr>(new Tuple<bool, int>(false, l),
                        new FuncStr() { _f = _task.L[l].ToString(), _dxf = _task.Dl[l].FuncX, _dyf = _task.Dl[l].FuncY, _coef = _task.CoefL[l] }),
                    Text = "g" + l + "(x,y)"
                });
            }
            listF.SelectedIndex = 0;
        }
        private void LoadValues()
        {
            richBox.Text = _func._f;
            GetMethod();
            txtGammaZ.Text = _task.Penalty.Start.ToString();
            txtGammaF.Text = _task.Penalty.Multiplier.ToString();
            GetPenalty();
            txtSigma.Text = _task.Sigma.ToString();
            txtName.Text = _task.Name;
            txtX.Text = _task.StartPoint.X.ToString();
            txtY.Text = _task.StartPoint.Y.ToString();
            
        }

        private void NewTaskDialog_Load(object sender, EventArgs e)
        {

        }

        private void funCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            ListFSelected();
        }

        private void listF_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListFSelected();
        }

        private void richF_TextChanged(object sender, EventArgs e)
        {
            if (listF.SelectedItem == null) return;
            var tmp = (Tuple<Tuple<bool, int>, FuncStr>)((ListBoxItem)listF.SelectedItem).Tag;
            switch (funCombo.SelectedIndex)
            {
                case 0:
                    tmp.Item2._f = richF.Text;
                    break;
                case 1:
                    tmp.Item2._dxf = richF.Text;
                    break;
                case 2:
                    tmp.Item2._dyf = richF.Text;
                    break;
            }
        }

        private void txtCoef_TextChanged(object sender, EventArgs e)
        {
            if (listF.SelectedItem == null) return;
            var tmp = (Tuple<Tuple<bool, int>, FuncStr>)((ListBoxItem)listF.SelectedItem).Tag;
            tmp.Item2._coef = Double.Parse(txtCoef.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    _task.IsGradient = true;
                    break;
                case 1:
                    _task.IsGradient = false;
                    break;
            }
            GetMethod();
        }

        private void addH_Click(object sender, EventArgs e)
        {
            int count = listF.Items.Cast<object>().Count(item => ((Tuple<Tuple<bool, int>, FuncStr>) ((ListBoxItem) item).Tag).Item1.Item1);
            listF.Items.Add(new ListBoxItem()
            {
                Tag = new Tuple<Tuple<bool, int>, FuncStr>(new Tuple<bool, int>(true, count+1),
                    new FuncStr() { _f = "y-x", _dxf = "-1", _dyf = "1", _coef = 1d }),
                Text = "h" + (count + 1) + "(x,y)"
            });
            listF.SelectedIndex = 0;
        }

        private void addG_Click(object sender, EventArgs e)
        {
            int count = listF.Items.Cast<object>().Count(item => !((Tuple<Tuple<bool, int>, FuncStr>) ((ListBoxItem) item).Tag).Item1.Item1);
            listF.Items.Add(new ListBoxItem()
            {
                Tag = new Tuple<Tuple<bool, int>, FuncStr>(new Tuple<bool, int>(false, count + 1),
                    new FuncStr() { _f = "-x", _dxf = "1", _dyf = "0", _coef = 1d }),
                Text = "g" + (count + 1) + "(x,y)"
            });
            listF.SelectedIndex = 0;
        }

        private void del_Click(object sender, EventArgs e)
        {
            if (listF.SelectedItem == null) return;
            listF.Items.Remove(listF.SelectedItem);
            if (listF.Items.Count > 0) listF.SelectedIndex = 0;
            else
            {
                richF.Text = "";
                txtCoef.Text = "";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }

    public class FuncStr
    {
        public double _coef;
        public string _f;
        public string _dxf;
        public string _dyf;
    }
}
