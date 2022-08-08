using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class TilePaletteViewModel : NotifiableObjectBase
    {
        private Tile newTile;
        private ObservableCollection<Tile> tiles = new ObservableCollection<Tile>();
        public event EventHandler OnError;
        public Func<Tile> OnTileRequested;

        public TilePaletteViewModel()
        {
            NewTile = new Tile();

            CreateTileCommand = new RelayCommand(
                //(obj) =>
                //{
                //    return !string.IsNullOrEmpty(newTile.Name) && !tiles.Contains(newTile);
                //},
                (obj) =>
                {
                    if (string.IsNullOrEmpty(NewTile.Name))
                    {
                        OnError?.Invoke(this, new EventArgs());
                        return;
                    }
                    Tiles.Add(newTile);
                    NewTile = new Tile();
                }
                );

            DeleteTileCommand = new RelayCommand(
                (obj) =>
                {
                    return !string.IsNullOrEmpty(newTile.Name) && tiles.Count > 0;
                },
                (obj) =>
                {
                    DeleteTile(NewTile);
                }
                );

            RequestTileCommand = new RelayCommand(
                (obj) =>
                {
                    var createdTile = OnTileRequested?.Invoke();
                    if (createdTile != null)
                    {
                        Tiles.Add(createdTile);
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
                    if (newTile != null)
                    {
                        newTile.PropertyChanged -= UpdateTile;
                    }
                    newTile = value;
                    if (newTile != null)
                    {
                        newTile.PropertyChanged += UpdateTile;
                        UpdateTile(this, null);
                    }
                    
                    RaisePropertyChanged();
                }
            }
        }

        private void UpdateTile(object? sender, PropertyChangedEventArgs e)
        {
            CreateTileCommand?.RaiseCanExecuteChange();
            DeleteTileCommand?.RaiseCanExecuteChange();
        }

        public ObservableCollection<Tile> Tiles
        {
            get { return tiles; }
            set
            {
                if (tiles != value)
                {
                    tiles = value;
                    RaisePropertyChanged();
                    DeleteTileCommand.RaiseCanExecuteChange();
                }
            }
        }

        public RelayCommand CreateTileCommand { get; set; }
        public RelayCommand DeleteTileCommand { get; set; }
        public RelayCommand RequestTileCommand { get; set; }

        private void DeleteTile(Tile tile)
        {
            if (tiles.Contains(tile))
            {
                Tiles.Remove(tile);
                NewTile = new Tile();
            }
        }
    }
}
