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
using Abs4CL;
using LibraryTest;

namespace Abs4
{
    /// <summary>
    /// Interaction logic for SubWindow1.xaml
    /// </summary>
    public partial class SubWindow1 : Window
    {
        public SubWindow1()
        {
            InitializeComponent();
            ListboxIN.ItemsSource = numbersIns;
            ListboxARR.ItemsSource = ValueArray;
        }

        private List<double> numbersIns = new List<double>();
        private double[] ValueArray = new double[5];
        private int PrimeCount = 0;
        int arrayCount = 0;

        private void ExecuteEx(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(ValueIn.Text, out double Value) || Value <= 0)
            {
                MessageBox.Show("Please insert in POSITIVE value", "Double", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Tools.PrimeChecker(Value) && PrimeCount >= 2)
            {
                MessageBox.Show("Already have 2 prime numbers in please no more", "F", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (RepetitionChecker(Value))
            {
                MessageBox.Show("That number was repeated 3 times", "F", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                if (arrayCount != 5)
                    ArrayGo(Value);

                ADDandREFRESH(Value);

                if (Tools.PrimeChecker(Value))
                {
                    PrimeCount++;
                    PrimeCounterDisplay.Content = $"There are currently {PrimeCount} Prime Numbers OUT OF 2";
                } 
            }


            //ListboxIN.Items.Add(Value);
            ValueIn.Focus();
        }

        private void ADDandREFRESH(double Value)
        {
            numbersIns.Add(Value);
            ListboxIN.Items.Refresh();
            ListboxARR.Items.Refresh();
        }

        private bool RepetitionChecker(double Val)
        {
            if (numbersIns.FindAll((double x) => x == Val).Count == 3)
                return true;

            //if (Array.FindAll<double>(ValueArray, (x) => x == Val).Count() == 3)
            //    return true;

            return false;
        }

  


        private void ArrayGo(double Value)
        {
            ValueArray[arrayCount] = Value;
            arrayCount++;


            foreach (var item in ValueArray)
                Tools.PrimeChecker(item);
            //add counter and check

            //Less readable and probably more impactful perfomance wise
            if (ArrayPrimeChecker()) { };
                //There are more than 2 prime numbers
        }

        private bool ArrayPrimeChecker()
        {
            int check = Array.FindAll(ValueArray, (arrVal) =>
            {
                for (int i = 0; i <= arrVal / 2; i++)
                {
                    if (arrVal % i == 0)
                        return true;
                }
                return false;

            }).Count();

            if (check >= 2)
                return true;

            return false;
        }


        private void ClearFunct(object sender, RoutedEventArgs e)
        {
            PrimeCount = 0;
            arrayCount = 0;
            numbersIns.Clear();
            ValueArray = new double[5];
            ListboxIN.Items.Refresh();
            ValueIn.Clear();
        }
    }
}
