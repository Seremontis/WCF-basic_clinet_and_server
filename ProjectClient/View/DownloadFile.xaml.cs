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
        public SettingsDownload GetSettings { get => settings; }
        public bool Flaga { get; private set; }
        public DownloadFile(ChannelProjectServer server)
        {
            InitializeComponent();
            Flaga = false;
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
            if (AvailableFileCombobox.SelectedItem == null)
            {
                MessageBox.Show("Proszę wybrać tabelę do pobrania", "Uwaga");
                Flaga = false;
            }
            else
            {
                settings.FileNameServer = AvailableFileCombobox.SelectedItem.ToString();
                Flaga = true;
            }
            settings.TypeFile = (FileType)TypeFileCombobox.SelectedIndex;
            if (!string.IsNullOrEmpty(PathTxtbox.Text))
                settings.PathLocal = PathTxtbox.Text + "." + settings.TypeFile.ToString().ToLower();
            else
            {
                Flaga = false;
                MessageBox.Show("Podaj ścieżkę zapisu");
            }


            if (Flaga)
                this.Close();
        }
        #endregion
    }
}
