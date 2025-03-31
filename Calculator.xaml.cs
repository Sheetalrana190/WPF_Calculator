using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        private string input = "";
        private string currentOperator = "";
        private decimal firstNumber, secondNumber;
        private bool isNewCalculation = false; // Detect new calculation

        public Calculator()
        {
            InitializeComponent();
            this.KeyDown += Calculator_KeyDown; // Handle key events
        }

        // Handles number button clicks
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (isNewCalculation) // Start fresh after previous calculation
            {
                input = "";
                isNewCalculation = false;
            }

            input += button.Content.ToString();
            txtDisplay.Text = input;
        }
        // Handles operator button clicks
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (input == "") return;

            firstNumber = Convert.ToDecimal(input);
            Button button = (Button)sender;
            currentOperator = button.Content.ToString();
            input = "";  // Reset input for the second number
            txtDisplay.Text = firstNumber + " " + currentOperator;
            isNewCalculation = false;
        }

        // Handles the equals button
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (input == "" || currentOperator == "") return;

            secondNumber = Convert.ToDecimal(input);
            decimal result = 0;

            switch (currentOperator)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    if (secondNumber != 0)
                        result = firstNumber / secondNumber;
                    else
                    {
                        MessageBox.Show("Cannot divide by zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    break;
                case "%":
                    result = firstNumber % secondNumber;
                    break;
            }

            txtDisplay.Text = result.ToString();
            input = result.ToString();
            currentOperator = "";
            isNewCalculation = true;
        }

        // Handles the clear button
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            input = "";
            firstNumber = 0;
            secondNumber = 0;
            currentOperator = "";
            txtDisplay.Text = "";
            isNewCalculation = false;
        }

        // Handles the backspace button
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);
                txtDisplay.Text = input;
            }
        }
        // Handles keyboard input
        private void Calculator_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle numeric keys (0-9)
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                HandleNumberInput(e.Key.ToString());
            }

            // Handle operator keys (+, -, *, /, %, etc.)
            if (e.Key == Key.Add || e.Key == Key.Subtract || e.Key == Key.Multiply || e.Key == Key.Divide)
            {
                HandleOperatorInput(e.Key.ToString());
            }

            // Handle percent key (Shift+5 for '%')
            if (e.Key == Key.D5 && Keyboard.Modifiers == ModifierKeys.Shift)
            {
                HandleOperatorInput("%");
            }

            // Handle the decimal point
            if (e.Key == Key.OemPeriod || e.Key == Key.Decimal)
            {
                HandleDecimalPoint();
            }

            // Handle backspace
            if (e.Key == Key.Back)
            {
                Backspace_Click(sender, e);
            }

            // Handle the equals key (Enter or NumPad Enter)
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {

                Equals_Click(sender, e);
            }

            // Handle clear (Escape)
            if (e.Key == Key.Escape)
            {
                Clear_Click(sender, e);
            }
        }

        // Handle number input from keyboard (0-9)
        private void HandleNumberInput(string key)
        {
            if (isNewCalculation) // Start fresh after a calculation
            {
                input = "";
                isNewCalculation = false;
            }

            input += key.Replace("D", "").Replace("NumPad", "");
            txtDisplay.Text = input;
        }


    }
}