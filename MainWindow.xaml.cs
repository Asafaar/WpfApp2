using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private double num1 = 0;
        private double num2 = 0;
        private string currentOp = "";
        private bool operationPressed = false;
        private bool newCalculation = true;

        public MainWindow()
        {
            InitializeComponent();
        }
        // <summary>
        /// Handles the click event for number buttons.
        
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string number = btn.Content.ToString();

            if (DisplayTextBox.Text == "0" || operationPressed || newCalculation)
            {
                DisplayTextBox.Text = number;
                operationPressed = false;
                newCalculation = false;
            }
            else
            {
                DisplayTextBox.Text += number;
            }
        }
        // when the decimal button is clicked, it checks if the current display text is "0" or
        // if an operation was pressed or a new calculation is being started.
        // If so, it sets the display to "0." Otherwise,
        // it appends a decimal point if it doesn't already exist in the text.
    
        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (operationPressed || newCalculation)
            {
                DisplayTextBox.Text = "0.";
                operationPressed = false;
                newCalculation = false;
            }
            else if (!DisplayTextBox.Text.Contains("."))
            {
                DisplayTextBox.Text += ".";
            }
        }
        // Handles the click event for operation buttons (+, -, *, /).

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            if (!operationPressed)
            {
                if (currentOp != "" && !newCalculation)
                {
                    DoCalculation();
                }

                num1 = double.Parse(DisplayTextBox.Text);
            }

            currentOp = btn.Tag?.ToString() ?? "";
            operationPressed = true;
            newCalculation = false;
        }
        // Handles the click event for the equals button.
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (currentOp != "" && !operationPressed)
            {
                DoCalculation();
                currentOp = "";
                newCalculation = true;
            }
        }
        // Method to perform the calculation based on the current operation.
        // It retrieves the second number from the display,
        // performs the calculation, and updates the display with the result.
        private void DoCalculation()
        {
            num2 = double.Parse(DisplayTextBox.Text);
            double result = 0;

            try
            {
                switch (currentOp)
                {
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "*":
                        result = num1 * num2;
                        break;
                    case "/":
                        if (num2 == 0)
                        {
                            DisplayTextBox.Text = "Error";
                            return;
                        }
                        result = num1 / num2;
                        break;
                }

                // Show result
                if (result == Math.Floor(result) && result < 999999999)
                {
                    DisplayTextBox.Text = result.ToString("0");
                }
                else
                {
                    DisplayTextBox.Text = result.ToString("G8");
                }

                num1 = result;
            }
            catch (Exception)
            {
                DisplayTextBox.Text = "Error";
            }
        }
        // Handles the click event for the clear button.
        // It resets the display and all internal variables to their initial state.
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            DisplayTextBox.Text = "0";
            num1 = 0;
            num2 = 0;
            currentOp = "";
            operationPressed = false;
            newCalculation = true;
        }
        // Handles the click event for the clear entry button.
        // It clears the current entry in the display but keeps the calculator's state intact.
        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            DisplayTextBox.Text = "0";
        }
        // Handles the click event for the backspace button.
        // It removes the last character from the display text.
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayTextBox.Text.Length > 1)
            {
                DisplayTextBox.Text = DisplayTextBox.Text.Substring(0, DisplayTextBox.Text.Length - 1);
            }
            else
            {
                DisplayTextBox.Text = "0";
            }
        }
    }
}