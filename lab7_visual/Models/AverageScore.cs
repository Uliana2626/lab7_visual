using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace lab7_visual.Models
{
    public class AverageScore : INotifyPropertyChanged
    {
        string score;
        string color;
        public event PropertyChangedEventHandler PropertyChanged;

        public AverageScore()
        {
            score = "0";
            color = "Red";
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Score
        {
            get => score;
            set
            {
                score = value;
                try
                {
                    if (Convert.ToDouble(score) < 1)
                        Color = "Red";
                    else if (Convert.ToDouble(score) < 1.5)
                        Color = "Yellow";
                    else
                        Color = "Green";
                    NotifyPropertyChanged();
                }
                catch
                {

                }
            }
        }

        public string Color
        {
            get => color;
            set
            {
                color = value;
                NotifyPropertyChanged();
            }
        }
    }
}
