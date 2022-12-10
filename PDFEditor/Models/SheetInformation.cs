using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Prism.Mvvm;

namespace PDFEditor.Models
{
    public class SheetInformation : BindableBase
    {
        public ObservableCollection<string> Sheets { get; } = new ObservableCollection<string>();
        public FrameworkElement Sheet { get; set; }
        public FrameworkElement SheetParent => Sheet.Parent as FrameworkElement;

        private string _ImageSource;
        public string ImageSource
        {
            get { return _ImageSource; }
            set { SetProperty(ref _ImageSource, value); }
        }

        private double _Scale = 1;
        public double Scale
        {
            get { return _Scale; }
            set
            {
                SetProperty(ref _Scale, value);
                RaisePropertyChanged(nameof(ScalePercentage));
            }
        }

        public double ScalePercentage => Scale * 100;

        private bool _IsEditing;
        public bool IsEditing
        {
            get { return _IsEditing; }
            set { SetProperty(ref _IsEditing, value); }
        }

        private bool _IsPrinting;
        public bool IsPrinting
        {
            get { return _IsPrinting; }
            set { SetProperty(ref _IsPrinting, value); }
        }

        public void ChangeScale(double changeAmount)
        {
            var min = 0.2;
            var max = 5.0;

            Scale = Clamp(Scale + changeAmount, min, max);
            SheetParent.LayoutTransform = new ScaleTransform(Scale, Scale);
        }

        private double Clamp(double value, double min, double max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }

    }
}
