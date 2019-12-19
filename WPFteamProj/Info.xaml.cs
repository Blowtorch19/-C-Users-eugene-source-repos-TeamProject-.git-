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
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        readonly AlcoholManager alcoholManager = new AlcoholManager();
        public Info()
        {
            InitializeComponent();
            InfoGrid.ItemsSource =
                        (from uC in alcoholManager.conditions
                         select new
                         {
                             NameOfPhase = uC.Name,
                             uC.Description
                         }) ;
        }
    }
}
