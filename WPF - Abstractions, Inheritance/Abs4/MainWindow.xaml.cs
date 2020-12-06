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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SubWindow1 newWind = new SubWindow1();
            newWind.Show();
        }


        private List<string> actors = new List<string>();
        private void Actor_Addition(object sender, RoutedEventArgs e)
        {
            string ActorInput = Actores.Text.Trim();
            actors.Add(ActorInput);
            Finisher.Content = "Finalize";
            Finisher.IsEnabled = true;
        }

        private void Finalize(object sender, RoutedEventArgs e)
        {
            if (!float.TryParse(Duracao.Text, out _) || !int.TryParse(Ano.Text, out _))
            {
                MessageBox.Show("Insert Valid numbers into year(no decimals) and duration(can take in decimals)", "Title", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (Tools.EmptyCheck(NomeFilme.Text) || Tools.EmptyCheck(Diretor.Text) || Tools.EmptyCheck(Actores.Text))
            {
                MessageBox.Show("Please fill out all forms", "Title", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

     
            IComedia com = ClassFactory.ComediaFactory(Diretor.Text, NomeFilme.Text, int.Parse(Ano.Text), float.Parse(Duracao.Text), actors);
            Displayer.Text = $"{com}";
        }
    }
}
