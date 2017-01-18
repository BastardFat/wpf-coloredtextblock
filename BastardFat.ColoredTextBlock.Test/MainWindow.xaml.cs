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

namespace BastardFat.ColoredTextBlock.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            ColoredTextBlock.Dispatcher.Invoke(() =>
            ColoredTextBlock.RichText = new Controls.ColoredTextString($"`{rand.Next(255).ToString("X2")}{rand.Next(255).ToString("X2")}{rand.Next(255).ToString("X2")}`Some awesome color!"));
        }


        private Random rand = new Random();
        private System.Timers.Timer t = new System.Timers.Timer(1000);


    }
}
