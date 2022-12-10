using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Unity;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PDFEditor.Models;

namespace PDFEditor.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;
        public SheetInformation SheetInformation => Settings.PrintInformation;

        public DelegateCommand LoadCommand { get; }
        public DelegateCommand PrintCommand { get; }
        public DelegateCommand ShrinkCommand { get; }
        public DelegateCommand MagnifyCommand { get; }

        public MainWindowViewModel()
        {
            LoadCommand = new DelegateCommand(() =>
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "PDFファイル(.pdf)|*.pdf"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    var input = openFileDialog.FileName;
                    var output = MyAppSettings.WorkDirectory + "temp_%06d.jpg";
                    var resolution = 72 * 4; // default 72
                    var converter = new PDFConverter()
                    {
                        ResolutionX = resolution,
                        ResolutionY = resolution,
                        OutputFormat = "jpeg"
                    };

                    converter.Convert(input, output);
                    SheetInformation.ImageSource = MyAppSettings.WorkDirectory + "temp_000001.jpg";
                }
            });

            PrintCommand = new DelegateCommand(() =>
            {
                var printDialog = new PrintDialog();

                if (printDialog.ShowDialog() == true)
                {
                    var sheet = SheetInformation.Sheet;
                    var queue = printDialog.PrintQueue;
                    var ticket = queue.DefaultPrintTicket;
                    var writer = PrintQueue.CreateXpsDocumentWriter(queue);

                    writer.Write(sheet, ticket);
                }
            });

            ShrinkCommand = new DelegateCommand(() => SheetInformation.ChangeScale(-0.2));
            MagnifyCommand = new DelegateCommand(() => SheetInformation.ChangeScale(0.2));
        }
    }
}
