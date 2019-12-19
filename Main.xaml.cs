using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TeamProject;

namespace WPFteamProj
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        readonly AlcoholManager alcoholManager = new AlcoholManager();
        readonly User user = new User();
        private const double WforFemale = 0.6;
        private const double WforMale = 0.7;
        public Main()
        {
            InitializeComponent();
            foreach (var item in alcoholManager.alcohols)
            {
                AlcoBox.Items.Add(item.Name);
            }
        }


        private void AlcoBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user.AlcoholType = AlcoBox.SelectedItem.ToString();
        }

        private bool NotNull()
        {
            if (user.Sex == null || ageBox == null || weightBox == null || user.AlcoholType == null)
            {
                ageBox.Text = null;
                weightBox.Text = null;
                MessageBox.Show("Fill all fields!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                user.Age = int.Parse(ageBox.Text);
                user.Weight = double.Parse(weightBox.Text);
                return true;
            }
        }

        private void Sex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            user.Sex = selectedItem.Content.ToString();
            if (user.Sex == "male")
            {
                user.WidmarK = WforMale;
            }
            else
            {
                if (user.Sex == "female")
                {
                    user.WidmarK = WforFemale;
                }
            }

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (NotNull())
            {
                Alcohol typeAlcohol = alcoholManager.alcohols.Find(a => a.Name == user.AlcoholType);
                alcoholManager.Volumes(typeAlcohol, user);
                if (user.Age >= 18)
                {
                    TotalGrid.ItemsSource =
                        (from uC in alcoholManager.userConditions
                         select new
                         {
                             NameOfPhase = uC.Name,
                             VolumeOfAlcohol_ml = uC.Volume,
                             YouCanDrivePast_min = uC.Hours
                         });
                }
                else
                {
                    MessageBox.Show(messageBoxText: "You are too young for drinking alcohol", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            
            
        }
    }
}