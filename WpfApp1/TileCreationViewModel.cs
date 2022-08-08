using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class TileCreationViewModel : NotifiableObjectBase
    {
        private Tile newTile;
        public event EventHandler OnError;
        public Action<Tile> Create;


        public TileCreationViewModel()
        {
            NewTile = new Tile();
            CreateTileCommand = new RelayCommand(
                (obj) =>
                {
                    if (string.IsNullOrEmpty(NewTile.Name))
                    {
                        OnError?.Invoke(this, new EventArgs());
                    }
                    else
                    {
                        Create?.Invoke(NewTile);
                    }
                }
                );
        }

        public Tile NewTile
        {
            get { return newTile; }
            set
            {
                if (newTile != value)
                {
                    newTile = value;

                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand CreateTileCommand { get; set; }

    }
}
