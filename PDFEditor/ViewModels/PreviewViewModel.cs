using System;
using System.Collections.Generic;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Unity;
using PDFEditor.Models;
using PdfSharp.Drawing;

namespace PDFEditor.ViewModels
{
    class PreviewViewModel : BindableBase
    {
        [Dependency]
        public MyAppSettings Settings { get; set; }
        public Configuration Configuration => Settings.Configuration;
        public SheetInformation SheetInformation => Settings.PrintInformation;

        public DelegateCommand MouseLeftButtonDownCommand { get; }
        public DelegateCommand MouseLeftButtonUpCommand { get; }
        public DelegateCommand MouseMoveCommand { get; }

        public PreviewViewModel()
        {
            MouseLeftButtonDownCommand = new DelegateCommand(() => SheetInformation.IsEditing = true);
            MouseLeftButtonUpCommand = new DelegateCommand(() => SheetInformation.IsEditing = false);
            MouseMoveCommand = new DelegateCommand(() =>
            {
                if (!SheetInformation.IsEditing)
                {
                    return;
                }

                var canvas = SheetInformation.Sheet as Canvas;
                var position = Mouse.GetPosition(canvas);

                using (var xGraphics = XGraphics.FromCanvas(canvas, new XSize(canvas.Width, canvas.Height), XGraphicsUnit.Presentation))
                {
                    xGraphics.DrawEllipse(XBrushes.Black, position.X, position.Y, 10, 10);
                }
            });
        }
    }
}
