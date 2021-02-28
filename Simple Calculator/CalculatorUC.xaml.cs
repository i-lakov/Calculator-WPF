using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCalculator
{
    /// <summary>
    /// Interaction logic for Simple_Calculator.xaml
    /// </summary>
    public partial class CalculatorUC : UserControl
    {
        private enum Operation { PLUS, MINUS, MULTIPLY, DIVIDE, NO_OP }
        private Operation operation;

        private double numberA;
        private double numberB;
        private double result;
        private double numberM;

        public CalculatorUC()
        {
            InitializeComponent();
        }

        #region Properties

        public double Result
        {
            get
            {
                double buf;
                if (double.TryParse(txtOutputLine.Text, out buf))
                {
                    return buf;
                }
                else return 0;
            }
        }

        public double NumberA
        {
            set { numberA = value; }
        }

        public double NumberB
        {
            set { numberB = value; }
        }
        #endregion

        #region Memory Function Methods
        private void BtnM_Click(object sender, RoutedEventArgs e)
        {
            numberM = Result;
        }

        private void BtnMPlus_Click(object sender, RoutedEventArgs e)
        {
            numberM += Result;
            txtOutputLine.Text = $"{numberM}";
        }

        private void BtnMMinus_Click(object sender, RoutedEventArgs e)
        {
            numberM -= Result;
            txtOutputLine.Text = $"{numberM}";
        }

        private void BtnMC_Click(object sender, RoutedEventArgs e)
        {
            numberM = 0;
        }
        #endregion

        #region Other Functionality Methods
        private void BtnCE_Click(object sender, RoutedEventArgs e)
        {
            txtOutputLine.Text = "0";
            txtOutputLine.Text = " ";
        }

        private void BtnC_Click(object sender, RoutedEventArgs e)
        {
            string entry = (string)((Button)sender).Content;
            if (txtOutputLine.Text != "0")
            {
                txtOutputLine.Text = txtOutputLine.Text.Remove(txtOutputLine.Text.Length - 1);
            }
        }

        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtOutputLine.Text, out numberA))
            {
                txtOutputLine.Text = "0";
                switch ((string)((Button)sender).Content)
                {
                    case "+":
                        operation = Operation.PLUS;
                        txtOutputLine.Text = " ";
                        break;
                    case "-":
                        operation = Operation.MINUS;
                        txtOutputLine.Text = " ";
                        break;
                    case "x":
                        operation = Operation.MULTIPLY;
                        txtOutputLine.Text = " ";
                        break;
                    case "/":
                        operation = Operation.DIVIDE;
                        txtOutputLine.Text = " ";
                        break;
                    default:
                        operation = Operation.NO_OP;
                        break;
                }
            }
            else
            {
                MessageBox.Show("Invalid input!");
            }
        }

        private void BtnPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (txtOutputLine.Text[0] == '-')
            {
                txtOutputLine.Text = txtOutputLine.Text.Remove(0, 1);
            }
            else
            {
                txtOutputLine.Text = txtOutputLine.Text.Insert(0, "-");
            }
        }

        private void BtnEqual_Click(object sender, RoutedEventArgs e)
        {
            bool failCheck = false;

            if (double.TryParse(txtOutputLine.Text, out numberB))
            {
                switch (operation)
                {
                    case Operation.PLUS:
                        result = numberA + numberB;
                        break;
                    case Operation.MINUS:
                        result = numberA - numberB;
                        break;
                    case Operation.MULTIPLY:
                        result = numberA * numberB;
                        break;
                    case Operation.DIVIDE:
                        if (numberB == 0)
                        {
                            MessageBox.Show("Dividing by 0 is undefined");
                            failCheck = true;
                            break;
                        }
                        else
                        {
                            result = numberA / numberB;
                            break;
                        }
                    case Operation.NO_OP:
                        break;
                    default:
                        break;
                }
                if (failCheck)
                {
                    txtOutputLine.Text = "";
                    operation = Operation.NO_OP;
                }
                else
                {
                    txtOutputLine.Text = "" + result;
                    operation = Operation.NO_OP;
                }
            }
            else
            {
                MessageBox.Show("Invalid input!");
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string entry = (string)((Button)sender).Content;
            if (txtOutputLine.Text == "0")
            {
                txtOutputLine.Text = entry;
            }
            else
            {
                if (txtOutputLine.Text.Contains(",") && entry == ",") return;
                txtOutputLine.Text += entry;
            }
        }
        #endregion
    }
}
