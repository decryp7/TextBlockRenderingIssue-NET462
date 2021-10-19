using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CustomTextBlock
{
    /// <summary>
    /// Interaction logic for DataTextBlock.xaml
    /// </summary>
    public partial class DataTextBlock : UserControl
    {
        public static readonly DependencyProperty ValueProperty;

        static DataTextBlock()
        {
            FrameworkPropertyMetadata valuePropertyMetadata =
                new FrameworkPropertyMetadata(55,
                    FrameworkPropertyMetadataOptions.AffectsRender
                    | FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(ValueChangedHandler));
            ValueProperty = DependencyProperty.Register("Value",
                typeof(object),
                typeof(DataTextBlock),
                valuePropertyMetadata);
        }

        private static void ValueChangedHandler(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                return;
            }

            ((DataTextBlock)d).ValueTextBlock.Text = e.NewValue.ToString();
        }

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public DataTextBlock()
        {
            InitializeComponent();
        }
    }
}
