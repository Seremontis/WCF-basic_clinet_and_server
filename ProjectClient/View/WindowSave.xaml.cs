using System.Windows;

namespace ProjectClient.View
{
    public partial class WindowSave : Window
    {
        #region ctor and fields
        public string NameFile { get => NameDataTxtbox.Text; }
        public WindowSave()
        {
            InitializeComponent();
        }

        #endregion

        #region events
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (NameDataTxtbox.Text == string.Empty)
                MessageBox.Show("Proszę nadać nazwę tabeli", "Uzupełnij pole");
            else
                this.Close();
        }

        #endregion
    }
}
