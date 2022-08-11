
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
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;
using Path = System.IO.Path;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WpfApp1.TilePalette.Views
{
    /// <summary>
    /// Interaction logic for TilePalette.xaml
    /// </summary>
    public partial class PaletteWindow : Window
    {
        public PaletteWindow()
        {
            InitializeComponent();
            ((TilePaletteViewModel)DataContext).OnError += ShowError;
            ((TilePaletteViewModel)DataContext).OnTileRequested += OpenCreator;
            ((TilePaletteViewModel)DataContext).OnLoadPathRequested += OpenLoadDialog;
            ((TilePaletteViewModel)DataContext).OnSavePathRequested += SaveFileDialog;
            ((TilePaletteViewModel)DataContext).OnFailed += ShowFail;
        }

        private string OpenLoadDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog() { 
                CheckFileExists = true, 
                CheckPathExists = true, 
                Filter = "Geralds Tile Palette|*.gtp", 
                Multiselect = false, 
                Title = "Open Tile Palette" 
            };
            if (dialog.ShowDialog() != true) return String.Empty;

            return dialog.FileName;
        }

        private string SaveFileDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                CheckPathExists = true,
                OverwritePrompt = true,
                Filter = "Geralds Tile Palette|*.gtp",
                Title = "Save Tile Palette",
                FileName = "Test Name"
            };

            if (dialog.ShowDialog() != true) return String.Empty;

            return dialog.FileName;
        }

        private Tile OpenCreator()
        {
            TileCreationView tileCreator = new TileCreationView();
            tileCreator.Owner = this;

            if (tileCreator.ShowDialog() == true)
            {
                return tileCreator.Result;
            }

            return null;
        }

        private void ShowError(object? sender, EventArgs e)
        {
            MessageBox.Show("Please enter a name.", "Missing Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void ShowFail(string message)
        {
            MessageBox.Show(message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected override void OnInitialized(EventArgs e)
        {
            LoadWindow();
            base.OnInitialized(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveWindow();
            base.OnClosing(e);
        }

        private void SaveWindow()
        {
            FileInfo targetFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "window-settings.json"));
            using(var fileStream = targetFile.Open(FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                JObject root = new JObject();
                JObject windowSettings = new()
                {
                    ["top"] = Top,
                    ["left"] = Left,
                    ["height"] = Height,
                    ["width"] = Width,
                    ["maximized"] = WindowState == WindowState.Maximized
                };
                root["windowSettings"] = windowSettings;
                using StreamWriter writer = new StreamWriter(fileStream);
                using JsonTextWriter jsonWriter = new JsonTextWriter(writer) { Formatting = Formatting.Indented };
                root.WriteTo(jsonWriter);
            }
        }

        private void LoadWindow()
        {
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "window-settings.json")))
            {
                return;
            }
            FileInfo targetFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "window-settings.json"));
            using(var fileStream = targetFile.Open(FileMode.Open, FileAccess.Read))
            {
                using StreamReader reader = new StreamReader(fileStream);
                using JsonTextReader jsonReader = new JsonTextReader(reader);
                JObject root = JObject.Load(jsonReader);

                JObject settings = root.Value<JObject>("windowSettings");
                Top = settings.Value<double>("top");
                Left = settings.Value<double>("left");
                Height = settings.Value<double>("height");
                Width = settings.Value<double>("width");
                WindowState = settings.Value<bool>("maximized") ? WindowState.Maximized : WindowState.Normal;
            }
        }
    }
}
