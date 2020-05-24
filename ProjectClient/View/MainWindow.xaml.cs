using ProjectClient.ServerProject;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using ProjectClient.Model;
using System.IO;

namespace ProjectClient.View
{
    public partial class MainWindow : BaseWindow
    {

        #region fields and ctor
        private DataTable data = null;
        private string NameFile = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Event controls

        #region Menu events
        private void NewTable_Click(object sender, RoutedEventArgs e)
        {
            DefineFields window = new DefineFields();
            window.ShowDialog();      
            if (window.getTabResult != null)
            {
                data = window.getTabResult;
                ResultGrid.ItemsSource = data.DefaultView;
                AddRowBut.IsEnabled = true;
                FilterGroup.IsEnabled = false;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (data != null)
            {
                SaveFile();
                LoadData(false);
            }        
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            Loader window = new Loader();
            window.ShowDialog();
            NameFile = window.NameTable;
            if(!string.IsNullOrEmpty(window.NameTable))
            {
                AddRowBut.IsEnabled = true;
                RefreshTabButton.IsEnabled = true;
                DelRowBut.IsEnabled = true;
                LoadData();
            }
            
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            if (data != null)
            {
                DownloadFile window = new DownloadFile(connector);
                window.ShowDialog();
                if (window.Flaga)
                {
                    if (NameFile == window.GetSettings.FileNameServer)
                    {
                        DialogResult dialogResult = MessageBox.Show("Czy chcesz najpierw zapisać postępy?", string.Empty, MessageBoxButtons.YesNo);
                        if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                            SaveFile();
                    }
                    File.WriteAllBytes(window.GetSettings.PathLocal, connector.GetFile(window.GetSettings.FileNameServer, window.GetSettings.TypeFile));
                    MessageBox.Show("Plik zostal pobrany");
                }               
            }
            else
                MessageBox.Show("Proszę najpierw utworzyć bądź zaimportować z serwera tabelę");
        }

        private void ModifyTable_Click(object sender, RoutedEventArgs e)
        {
            if (data != null)
            {
                ModifyTable window = new ModifyTable(data);
                window.ShowDialog();
                data = window.table;
                ResultGrid.ItemsSource = null;
                ResultGrid.ItemsSource = data.DefaultView;
                ResultGrid.Items.Refresh();
                FilterGroup.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Brak tabeli do modyfikacji");
            }

        }

        private void LoadLocalFile_Click(object sender, RoutedEventArgs e)
        {
            FilterGroup.IsEnabled = false;
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "CSV(*.csv)| *.csv",
                RestoreDirectory = true
            };
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DialogResult resultMessage = MessageBox.Show("Kliknij \"Tak\" jeśli dane są oddzielone średnikiem, \"Nie\" jeśli są przecinkiem.", "Wybór separatora", MessageBoxButtons.YesNoCancel);
                switch (resultMessage)
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        ReadLocalFile(dialog.FileName, ';');
                        AddRowBut.IsEnabled = true;
                        RefreshTabButton.IsEnabled = false;
                        DelRowBut.IsEnabled = true;
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        ReadLocalFile(dialog.FileName, ',');
                        AddRowBut.IsEnabled = true;
                        RefreshTabButton.IsEnabled = false;
                        DelRowBut.IsEnabled = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Author_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aplikacja wykonana przez Karola Ścigała", "Autor", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        #endregion

        #region events on table
        private void AddRowBut_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = data.NewRow();
            data.Rows.Add(row);
            DelRowBut.IsEnabled = true;
        }

        private void RefreshTabButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void DelRowBut_Click(object sender, RoutedEventArgs e)
        {
            if (ResultGrid.SelectedIndex != -1)
            {
                data.Rows.RemoveAt(ResultGrid.SelectedIndex);
                //ResultGrid.Items.Refresh();
            }
            else if (ResultGrid.SelectedIndex != data.Rows.Count - 1)
            {
                data.Rows.RemoveAt(data.Rows.Count - 1);
                //ResultGrid.Items.Refresh();
            }

            if (ResultGrid.Items.Count < 1)
                DelRowBut.IsEnabled = false;
        }

        private void OperationTypCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((OperationOnTable)OperationTypCombo.SelectedIndex == OperationOnTable.COUNT)
                ColCombo.IsEnabled=false;
            else
                ColCombo.IsEnabled=true;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (OperationTypCombo.SelectedIndex < (int)OperationOnTable.COUNT && ColCombo.SelectedIndex > 0 && GroupColCombo.SelectedIndex == 0)
                MessageBox.Show("Proszę wybrać kolumnę do pogrupowania");
            else
            {
                var result = connector.GetStats(NameFile, ColCombo.SelectedItem.ToString(), GroupColCombo.SelectedItem.ToString(), (OperationOnTable)OperationTypCombo.SelectedIndex);
                if (result != null)
                {
                    if (result.Item2 == null)
                        MessageBox.Show("Uzyskany wynik to: {0}", result.Item1.ToString());
                    else
                        ResultGrid.ItemsSource = result.Item2;
                }
                else
                    MessageBox.Show(connector.ErroMessage, "Wystąpił błąd");
            }
        }

        #endregion

        #endregion

        #region additional methods
        private void LoadData(bool flaga=true)
        {
            if(flaga)
                data = connector.LoadFromServer(NameFile);
            ResultGrid.ItemsSource = data.DefaultView;
            ColCombo.Items.Clear();
            OperationTypCombo.Items.Clear();
            ColCombo.Items.Add("*");
            foreach (DataColumn item in data.Columns)
                ColCombo.Items.Add(item.ColumnName);
            GroupColCombo.ItemsSource = ColCombo.Items;
            foreach (var item in Enum.GetValues(typeof(OperationOnData)))
                OperationTypCombo.Items.Add(item);
            FilterGroup.IsEnabled = true;
            ColCombo.SelectedIndex = GroupColCombo.SelectedIndex = 0;
        }

        private void ReadLocalFile(string fullpath, char separator)
        {
            data = new DataTable();
            using (StreamReader sr = new StreamReader(fullpath, Encoding.Default))
            {
                string[] headers = sr.ReadLine().Split(separator);
                for (int i = 0; i < headers.Count(); i++)
                {
                    data.Columns.Add(headers[i]);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(separator);
                    DataRow dr = data.NewRow();
                    for (int i = 0; i < rows.Count(); i++)
                    {
                        dr[i] = rows[i];
                    }
                    data.Rows.Add(dr);
                }
            }
            ResultGrid.ItemsSource = data.DefaultView;
        }

        private void SaveFile()
        {
            if (data != null)
            {
                if (data.TableName == string.Empty)
                {
                    WindowSave window = new WindowSave();
                    window.ShowDialog();
                    NameFile = window.NameFile;
                }
                else
                    NameFile = data.TableName;

                if (!string.IsNullOrEmpty(NameFile))
                {
                    if (connector.SaveToServer(data, NameFile))
                    {
                        MessageBox.Show("Zapisano");
                        FilterGroup.IsEnabled = true;
                    }
                    else if (connector.ErroMessage != string.Empty)
                        MessageBox.Show(connector.ErroMessage);
                }         
            }
        }

        #endregion
    }
}
