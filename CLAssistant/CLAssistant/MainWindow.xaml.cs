using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;

namespace CLAssistant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string LOG_FILE_NAME = "test.txt";

        bool isCancelled = false;
        BackgroundWorker worker = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            // Initializes background worker (so as not to lock up the UI)
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
            isCancelled = true;
            ResultsTextbox.AppendText("Cancelled!");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValidQuery = true;
            String keyword = KeywordTextbox.Text.ToString().Trim();
            ProgressLabel.Content = string.Empty;

            // Validates keyword is valid
            if (!isValidKeyword(keyword))
            {
                ProgressLabel.Content = "Please enter a keyword to search on Craigslist";
                KeywordTextbox.Clear();
                isValidQuery = false;
            }

            // Validates maxPrice is valid
            String maxPrice = MaxPriceTextbox.Text.ToString().Trim();
            if (!isValidPrice(maxPrice))
            {
                ProgressLabel.Content = "Price must be between 0 to 100000";
                MaxPriceTextbox.Clear();
                isValidQuery = false;
            }

            if (isValidQuery)
            {
                if (string.Empty != maxPrice)
                {
                    GetCraigsListResult(keyword, maxPrice);
                }
                else
                {
                    GetCraigsListResult(keyword);
                }
            }
        }

        /// <summary>
        /// Checks to see if user enters a valid keyword
        /// </summary>
        /// <returns>True if keyword is valid</returns>
        private bool isValidKeyword(String keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to see if user enters a valid price
        /// </summary>
        /// <returns>True if price is valid</returns>
        private bool isValidPrice(String input)
        {
            int price = 0;
            bool isValid = true;

            if(input == null || String.IsNullOrWhiteSpace(input))
            {
                return isValid;
            }

            try
            {
                price = Convert.ToInt32(MaxPriceTextbox.Text.ToString().Trim());
            }
            catch (Exception)
            {
                isValid = false;
            }

            if (isValid)
            {
                if (price < 0 || price > 100000)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
