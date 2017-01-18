using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BastardFat.ColoredTextBlock.Test
{
    class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ColoredRepresentation = new Controls.ColoredTextString($"`{rand.Next(255).ToString("X2")}{rand.Next(255).ToString("X2")}{rand.Next(255).ToString("X2")}`Some awesome color!");
            StringRepresentation = $"`FF00FF`Some `00FFFF`awesome `FFFF00`color!";
        }
        private Random rand = new Random();
        private System.Timers.Timer t = new System.Timers.Timer(1000);


        public event PropertyChangedEventHandler PropertyChanged;

        private string stringRepresentation;

        public string StringRepresentation
        {
            get { return stringRepresentation; }
            set
            {
                stringRepresentation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StringRepresentation)));
            }
        }

        private Controls.ColoredTextString coloredRepresentation;

        public Controls.ColoredTextString ColoredRepresentation
        {
            get { return coloredRepresentation; }
            set
            {
                coloredRepresentation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColoredRepresentation)));
            }
        }
    }
}
