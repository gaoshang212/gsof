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
using System.Windows.Shapes;

namespace Gsof.Xaml.Demo
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();

            var model = new TestModel()
            {
                A = "AAAAAAAAAAAAA",
                B = "BBBBBBBBBBBB",
                C = "CCCCCCCCCCCCC"
            };

            this.DataContext = model;
        }
    }

    public class TestModel
    {
        private string _a;

        public string A
        {
            get { return _a; }
            set
            {
                _a = value;
                Console.WriteLine(_a);
            }
        }

        private string _b;

        public string B
        {
            get { return _b; }
            set
            {
                _b = value;
                Console.WriteLine(_b);
            }
        }

        private string _c;

        public string C
        {
            get { return _c; }
            set
            {
                _c = value;
                Console.WriteLine(_c);
            }
        }
    }
}
