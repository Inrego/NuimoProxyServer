using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
using NuimoHass.WPF.Properties;

namespace NuimoHass.WPF
{
    /// <summary>
    /// Interaction logic for LedMatrix.xaml
    /// </summary>
    public partial class LedMatrix : UserControl
    {
        public static readonly DependencyProperty MatrixProperty = DependencyProperty.Register("Matrix",
            typeof(Shared.LedMatrix), typeof(LedMatrix), new PropertyMetadata(new Shared.LedMatrix(true)));


        public LedMatrix()
        {
            InitializeComponent();
        }

        public Shared.LedMatrix Matrix
        {
            get { return (Shared.LedMatrix)GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var matrixKey in Matrix.Matrix.Keys)
            {
                Matrix.Matrix[matrixKey].Value = false;
            }
        }

        private void InvertButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var matrixKey in Matrix.Matrix.Keys)
            {
                Matrix.Matrix[matrixKey].Value = !Matrix.Matrix[matrixKey].Value;
            }
        }
    }
}
