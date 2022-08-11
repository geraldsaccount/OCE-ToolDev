using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.TilePalette.ViewModels;

namespace WpfApp1.TilePalette.Views
{
    /// <summary>
    /// Interaction logic for TileCreationView.xaml
    /// </summary>
    public partial class TileCreationView : Window
    {
        public Tile Result;

        public TileCreationView()
        {
            InitializeComponent();
            ((TileCreationViewModel)DataContext).Create += OnCreate;
        }

        private void OnCreate(Tile result)
        {
            Result = result;
            DialogResult = true;
            Close();
        }
    }
}
