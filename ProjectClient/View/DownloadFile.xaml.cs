using ProjectClient.Model;
using System;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using RadioButton = System.Windows.Controls.RadioButton;
using ProjectClient.ServerProject;

namespace ProjectClient.View
{

    public partial class DownloadFile : BaseWindow
    {
        #region fields and contructor
        private SettingsDownload settings;
        private bool _flaga;
        public SettingsDownload GetSettings { get => settings; }
        public bool Flaga { get => _flaga; }
        public DownloadFile(ChannelProjectServer server)
        {
            InitializeComponent();
            connector = server;
            AvailableFileCombobox.ItemsSource = connector.GetNamesList();
            foreach (var item in Enum.GetValues(typeof(FileType)))
                TypeFileCombobox.Items.Add(item);            
        }
        #endregion


        #region events
        private void SelectPathBut_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog() {
                FileName = AvailableFileCombobox.SelectedItem != null ? AvailableFileCombobox.SelectedItem.ToString() : "Plik"
                };
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if(openFileDialog.FileName.LastIndexOf('.')!=-1)
                    PathTxtbox.Text = openFileDialog.FileName.Substring(0, openFileDialog.FileName.LastIndexOf('.'));
                else
                    PathTxtbox.Text = openFileDialog.FileName;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            settings.Storage = radioButton.Content.ToString();
        }

        private void ConfirmBut_Click(object sender, RoutedEventArgs e)
        {
            
            if (settings.Storage == string.Empty)
            {
                MessageBox.Show("Proszę wybrać który plik pobrać (lokalny czy sewera)", "Uwaga");
                _flaga = false;
            }
            else
                _flaga = true;

            if (AvailableFileCombobox.IsEnabled)
            {
                if (AvailableFileCombobox.SelectedItem == null)
                {
                    MessageBox.Show("Proszę wybrać tabelę do pobrania", "Uwaga");
                    _flaga = false;
                }
                else
                {
                    settings.FileNameServer = AvailableFileCombobox.SelectedItem.ToString();
                    _flaga = true;
                }
            }
            settings.TypeFile = (FileType)TypeFileCombobox.SelectedIndex;
            settings.PathLocal = PathTxtbox.Text+"."+settings.TypeFile.ToString().ToLower();
            

            if (_flaga)
                this.Close();
        }

        private void BaseWindow_Closed(object sender, EventArgs e)
        {
            _flaga = false;
            this.Close();
        }
        #endregion
    }
}
