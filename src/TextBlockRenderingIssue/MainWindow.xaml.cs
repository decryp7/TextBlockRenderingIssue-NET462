using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace TextBlockRenderingIssue
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
                FormattableString.Invariant(
                    $"Viewer Size: Width: {Viewer.ActualWidth}, Height: {Viewer.ActualHeight}. Canvas Size: Width: {Canvas.ActualWidth}, Height: {Canvas.ActualHeight}. {runtimeVersion} from {runtimeDirectory}. ");
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
                DataTextBlock.Dispatcher.InvokeAsync(() =>
                {
                    DataTextBlock.Value = randomValue;
                });
            }
        }
    }
}
