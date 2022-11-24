using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Gsof.Xaml.Theme;

namespace Gsof.Xaml.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        ThemeManager tm = new ThemeManager();

        private int _value;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string properyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properyName));
        }


        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            tm.ApplyDefaultResource(Application.Current.Resources, "system", new[] { "Themes/Dictionary1.xaml" });
            tm.ApplyResource(Application.Current.Resources, new ThemeGroup("system", new[] { new ResourceDictionary() { Source = new Uri("pack://application:,,,/Gsof.Xaml.Demo;component/Themes/Dictionary2.xaml") }, }));
        }

        private void Windows1OnClick(object sender, RoutedEventArgs e)
        {
            var windows = new Window1();
            windows.Show();
        }
    }
}
