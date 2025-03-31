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

    }
}