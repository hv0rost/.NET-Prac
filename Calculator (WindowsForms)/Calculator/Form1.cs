using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private readonly Calculator calc;
        public Form1()
        {
            InitializeComponent();
            calc = new Calculator();
            calc.DidUpdateValue += Calc_DidUpdate;
            calc.InputError += Calc_Error;
            calc.CalculationError += Calc_Error;
        }

        private void Calc_Error(object sender, string e)
        {
            MessageBox.Show(e, "Calculator Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Calc_DidUpdate(Calculator sender,double value, int precision)
        {
            if (precision > 0)
                label1.Text = String.Format("{0:F" + precision + "}", value);
            else
                label1.Text = $"{value}";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string text = (sender as Button).Text;
            string name = (sender as Button).Name;
            object tag = (sender as Button).Tag;
            //MessageBox.Show($"{name} : {text}, {tag}");

            if (int.TryParse(text, out int digit))
            {
                calc.AddDigit(digit);
            }
            else
            {
                switch (tag)
                {
                    case "decimal":
                        calc.AddDecimalPoint();
                        break;
                    case "evaluate":
                        calc.Compute();
                        break;
                    case "addition":
                        calc.AddOperation(Operation.Add);
                        break;
                    case "substraction":
                        calc.AddOperation(Operation.Sub);
                        break;
                    case "multiplication":
                        calc.AddOperation(Operation.Mul);
                        break;
                    case "division":
                        calc.AddOperation(Operation.Div);
                        break;
                    case "degree":
                        calc.AddOperation(Operation.Pow);
                        break;
                    case "reverse":
                        calc.AddOperation(Operation.Drob);
                        break;
                    case "radical":
                        calc.AddOperation(Operation.Sqrt);
                        break;
                    case "Cos":
                        calc.AddOperation(Operation.Cos);
                        break;
                    case "Sin":
                        calc.AddOperation(Operation.Sin);
                        break;
                    case "clear":
                        calc.Clear();
                        break;
                    case "reset":
                        calc.Reset();
                        break;
                    case "earse":
                        calc.Earse();
                        break;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //string code = e.KeyCode.ToString();
            //string data = e.KeyData.ToString();
            //string value = e.KeyValue.ToString();

            //MessageBox.Show($"{code} {data} {value}");

            switch (e.KeyCode)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    calc.AddDigit(1);
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    calc.AddDigit(2);
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    calc.AddDigit(3);
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    calc.AddDigit(4);
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    calc.AddDigit(5);
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    calc.AddDigit(6);
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    calc.AddDigit(7);
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    calc.AddDigit(8);
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    calc.AddDigit(9);
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    calc.AddDigit(0);
                    break;
                case Keys.Oemplus:
                case Keys.Add:
                    calc.AddOperation(Operation.Add);
                    break;
                case Keys.OemMinus:
                case Keys.Subtract:
                    calc.AddOperation(Operation.Sub);
                    break;
                case Keys.Multiply:
                    calc.AddOperation(Operation.Mul);
                    break;
                case Keys.Divide:
                    calc.AddOperation(Operation.Div);
                    break;
                default:
                    MessageBox.Show(Convert.ToString(e.KeyCode));
                    break;
                    /** и так далее**/
            }

        }

    }
}
