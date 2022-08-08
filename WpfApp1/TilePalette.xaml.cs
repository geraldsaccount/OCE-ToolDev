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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for TilePalette.xaml
    /// </summary>
    public partial class TilePalette : Window
    {
        public TilePalette()
        {
            InitializeComponent();
            ((TilePaletteViewModel)DataContext).OnError += ShowError;
            ((TilePaletteViewModel)DataContext).OnTileRequested += OpenCreator;
        }

        private Tile OpenCreator()
        {
            TileCreationView tileCreator = new TileCreationView();

            if (tileCreator.ShowDialog() == true)
            {
                return tileCreator.Result;
            }

            return null;
        }

        private void ShowError(object? sender, EventArgs e)
        {
            MessageBox.Show("Please enter a name.");
        }
    }
}
