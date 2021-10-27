using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TextBlockRenderingIssue;

namespace TextBlockRendering.NET3._5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Timer timer;
        private readonly int[] dummyValues = new[] { 1, 11, 111, 1111, 11111, 5, 55, 555, 5555, 55555 };
        private readonly RandomGenerator r = new RandomGenerator();
        private readonly string runtimeVersion = System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion();
        private readonly string runtimeDirectory =
            System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();

        public MainWindow()
        {
            InitializeComponent();
            this.SizeChanged += OnSizeChanged;
            timer = new Timer(Refresh, null, 1000, 1000);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Title =
                string.Format(CultureInfo.InvariantCulture, 
                    "Viewer Size: Width: {0}, Height: {1}. Canvas Size: Width: {2}, Height: {3}. {4} from {5}.",
                    Viewer.ActualWidth,
                    Viewer.ActualHeight,
                    Canvas.ActualWidth,
                    Canvas.ActualHeight, 
                    runtimeVersion,
                    runtimeDirectory);
        }

        private void Refresh(object state)
        {
            int randomValue = dummyValues[r.Next(0, 10000) % dummyValues.Length];
            //int randomValue = r.Next(0, 100);

            if (DataTextBlock.CheckAccess())
            {
                DataTextBlock.Value = randomValue;
            }
            else
            {
                DataTextBlock.Dispatcher.BeginInvoke(new Action(() => { DataTextBlock.Value = randomValue; }));
            }
        }
    }
}
