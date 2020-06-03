using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Spreadsheet.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gridex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        // init the theme manager so the app can be customized!
        ThemeManagerLite themeManager = new ThemeManagerLite();

        public MainWindow(string[] args)
        {
            InitializeComponent();

            // set handlers
            // load saved user settings
            themeManager.ChangeTheme(Properties.Settings.Default.Theme);

            this.Height = Properties.Settings.Default.MainWindowHeight;
            this.Width = Properties.Settings.Default.MainWindowWidth;

            if (args.Length > 0)
            {
                try
                {
                    spreadsheet.Open(args[0]);
                }
                catch (Exception ex)
                {
                    // ShowMessage("Error", ex.Message);
                }
            }

            spreadsheet.Loaded += spreadsheet_Loaded;
        }

        void spreadsheet_Loaded(object sender, EventArgs args)
        {

            //Access the Active SpreadsheetGrid and hook the events associated with it.
            var grid = spreadsheet.ActiveGrid;
            grid.SelectionChanged += Grid_SelectionChanged;
        }

        private async void Grid_SelectionChanged(object sender, Syncfusion.UI.Xaml.CellGrid.Helpers.SelectionChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    var rangeList = spreadsheet.ActiveGrid.SelectedRanges;
                    var excelRange = GridExcelHelper.ConvertGridRangeToExcelRange(rangeList.First(), spreadsheet.ActiveGrid);
                    var cells = spreadsheet.ActiveSheet.Range[excelRange];

                    double sum = 0;
                    double avg = 0;
                    double count = 0;

                    foreach (var cell in cells)
                    {
                        double value = 0;
                        double.TryParse(cell.Value, out value);

                        if (value > 0)
                        {
                            sum += value;
                            count++;
                        }
                    }

                    avg = sum / count;

                    lblsum.Invoke(() => { lblsum.Content = $"{sum}"; });
                    lblaverage.Invoke(() => { lblaverage.Content = $"{avg}"; });
                    lblcount.Invoke(() => { lblcount.Content = $"{count}"; }); 
                }
                catch (Exception ex)
                {

                }
            });
        }

        // sets the theme on click from the theme radio button list
        private void SetTheme_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            string theme = radioButton.Content.ToString();
            themeManager.ChangeTheme(theme);
        }

        // various use of the Metro popup
        private async void Message_ClickAsync(object sender, RoutedEventArgs e)
        {
            MessageDialogResult res = await this.ShowMessageAsync("Hey, Listen!", "These are just some controls to take up space :)",
                MessageDialogStyle.AffirmativeAndNegative);

            if (res == MessageDialogResult.Affirmative)
            {
                await this.ShowMessageAsync("You clicked OK! ", res.ToString());
            }
        }

        // launches the SubWindow form via dialog
        private void menuinformation_Click(object sender, RoutedEventArgs e)
        {
            SubWindow subWindow = new SubWindow();
            subWindow.ShowDialog();
        }

        // this allows the user to drag the app by clicking and holding anywhere on the window!
        private void Mainwindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        // persist user resize because its nice to have and easy to do
        private async void Mainwindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    this.Invoke(() => { Properties.Settings.Default.MainWindowHeight = (int)Height; });
                    this.Invoke(() => { Properties.Settings.Default.MainWindowWidth = (int)Width; });
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    ShowMessage("Hey, listen!", ex.Message);
                }
            });
        }

        private async void menusave_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    Loading(true);
                    var saveDialog = new SaveFileDialog();
                    saveDialog.Title = "Save as...";
                    saveDialog.Filter = "Excel (*.xlsx)|*.xlsx";

                    if (saveDialog.ShowDialog() == true)
                    {
                        string file = saveDialog.FileName;
                        spreadsheet.Invoke(() => { spreadsheet.SaveAs($"{file}"); });
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Hey, listen!", ex.Message);
                }
                Loading(false);
            });
        }

        private async void menuopen_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Loading(true);

                try
                {
                    var openDialog = new OpenFileDialog();
                    openDialog.Title = "Select a file...";
                    openDialog.Filter = "Excel (*.xlsx)|*.xlsx";

                    if (openDialog.ShowDialog() == true)
                    {
                        string file = openDialog.FileName;
                        spreadsheet.Invoke(() => { spreadsheet.Open(file); });
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Hey, listen!", ex.Message);
                }
                Loading(false);
            });
        }

        private async void menusaveas_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    Loading(true);
                    var saveDialog = new SaveFileDialog();
                    saveDialog.Title = "Save as...";
                    saveDialog.Filter = "Excel (*.xlsx)|*.xlsx";

                    if (saveDialog.ShowDialog() == true)
                    {
                        string file = saveDialog.FileName;
                        spreadsheet.Invoke(() => { spreadsheet.SaveAs($"{file}"); });
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage("Hey, listen!", ex.Message);
                }
                Loading(false);
            });
        }

        private async void menunew_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() =>
            {
                Loading(true);
                try
                {
                    spreadsheet.Invoke(() => { spreadsheet.Create(1); });
                }
                catch (Exception ex)
                {
                    ShowMessage("Hey, listen!", ex.Message);
                }
                Loading(false);
            });
        }

        private async void ShowMessage(string title, string message)
        {
            await this.Invoke(async () => { await this.ShowMessageAsync(title, message); });
        }

        private void Loading(bool load)
        {
            loader.Invoke(() => { loader.IsIndeterminate = load; });
        }
    }
}
