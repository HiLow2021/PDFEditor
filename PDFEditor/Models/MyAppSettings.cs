using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFEditor.Models
{
    public class MyAppSettings
    {
        public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string WorkDirectory = BaseDirectory + "temp" + Path.DirectorySeparatorChar;
        public static readonly string ConfigPath = BaseDirectory + "config.dat";
        public static readonly double DefaultWindowWidth = 1200;
        public static readonly double DefaultWindowHeight = 650;

        public Configuration Configuration { get; set; } = new Configuration();
        public SheetInformation PrintInformation { get; set; } = new SheetInformation();
    }
}
