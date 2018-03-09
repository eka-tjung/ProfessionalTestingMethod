using Day5Example;
using System;
using System.Windows;
using System.Windows.Input;
namespace SampleUI
{
    public partial class MainWindow : Window
    {
        Circle testCircle;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtRadius_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                uint radius = Convert.ToUInt16(txtRadius.Text);
                testCircle = new Circle(radius);

                lblArea.Content = testCircle.Area;
                lblCircumference.Content = testCircle.Circumference;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        /// <summary>
        /// Displays message in the appropriate UI element
        /// </summary>
        /// <param name="msg">message to display</param>
        private void DisplayMessage(String msg)
        {
            tbMessage.Text = msg + "\r\n" + tbMessage.Text;
        }
    }
}
