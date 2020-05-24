using System.Windows;

namespace ProjectClient.View
{

    public partial class Loader : BaseWindow
    {
        #region ctor and fields
        private string _NameTable = string.Empty;

        public string NameTable { get => _NameTable; }
        public Loader()
        {
            InitializeComponent();
            NameTableCombox.ItemsSource = connector.GetNamesList();
        }
        #endregion

        #region events
        private void ConfirmBut_Click(object sender, RoutedEventArgs e)
        {
            if (NameTableCombox.SelectedIndex > -1)
            {
                _NameTable = (string)NameTableCombox.SelectedValue;
                this.Close();
            }
            else
                MessageBox.Show("Wybierz tabelę");
        }

        #endregion
    }
}
