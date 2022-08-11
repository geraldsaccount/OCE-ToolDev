using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TilePaletteModel;

namespace WpfApp1.TilePalette.ViewModels
{
    public class TilePaletteViewModel : NotifiableObjectBase, IBinarySerializable
    {
        private ProjectModel project;
        private Tile newTile;
        private ObservableCollection<Tile> tiles = new ObservableCollection<Tile>();
        public event EventHandler OnError;
        public Func<Tile> OnTileRequested;
        public Func<string> OnLoadPathRequested;
        public Func<string> OnSavePathRequested;
        public Action<string> OnFailed;

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

            LoadPalette = new RelayCommand((obj) => Load(OnLoadPathRequested.Invoke()));
            SavePaletteAs = new RelayCommand((obj) => Save(OnSavePathRequested.Invoke()));
            NewPalette = new RelayCommand((obj) => ResetPalette());
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
        public RelayCommand NewPalette { get; set; }
        public RelayCommand LoadPalette { get; set; }
        public RelayCommand SavePaletteAs { get; set; }

        private void ResetPalette()
        {
            Tiles = new ObservableCollection<Tile>();
            NewTile = new Tile();
        }

        private void DeleteTile(Tile tile)
        {
            if (tiles.Contains(tile))
            {
                Tiles.Remove(tile);
                NewTile = new Tile();
            }
        }

        public void Load(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            FileInfo targetFile = new FileInfo(filePath);
            using var fileStream = targetFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader reader = new BinaryReader(fileStream);
            try
            {
                Deserialize(reader);

            }
            catch (Exception)
            {
                OnFailed.Invoke("Could not load the file.");
                ResetPalette();
                return;
            }
        }

        private void Save(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            FileInfo targetFile = new FileInfo(filePath);
            using var fileStream = targetFile.Open(FileMode.Create, FileAccess.Write, FileShare.Read);
            BinaryWriter writer = new BinaryWriter(fileStream);
            try
            {
                Serialize(ref writer);
            }
            catch (Exception)
            {
                OnFailed.Invoke("Could not save the file.");
                ResetPalette();
                return;
            }
        }

        public void Serialize(ref BinaryWriter writer)
        {
            writer.Write(Tiles.Count);
            foreach (var tile in tiles) tile.Serialize(ref writer);
            
        }

        public void Deserialize(BinaryReader reader)
        {
            int tilesCount = reader.ReadInt32();
            Tiles = new ObservableCollection<Tile>();
            for (int i = 0; i < tilesCount; i++) Tiles.Add(new Tile(reader));
        }
    }
}
