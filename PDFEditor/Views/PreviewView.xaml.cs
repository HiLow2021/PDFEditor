using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PDFEditor.ViewModels;

namespace PDFEditor.Views
{
    /// <summary>
    /// PreviewView.xaml の相互作用ロジック
    /// </summary>
    public partial class PreviewView : UserControl
    {
        public PreviewView()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
            {
                var context = DataContext as PreviewViewModel;
                var printInformation = context.SheetInformation;

                printInformation.Sheet = sheetCanvas;
            };
        }
    }
}
