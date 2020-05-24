using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ProjectClient.View
{
    public partial class ModifyTable : AlterTable
    {
        #region fields and contructor
        public DataTable table { get => tabResult; }
        public ModifyTable(DataTable data)
        {
            InitializeComponent();
            tabResult = data;
            DelColCombobox.ItemsSource = data.Columns.Cast<DataColumn>().Select(x=>x.ColumnName).ToList();
        }

        #endregion

        #region events
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (NameAddColumnTxtbox.Text != string.Empty || !string.IsNullOrWhiteSpace(NameAddColumnTxtbox.Text))
            {
                tabResult.Columns.Add(NameAddColumnTxtbox.Text);
            }
            if (DelColCombobox.SelectedIndex >= 0)
            {
                tabResult.Columns.Remove(DelColCombobox.SelectedItem.ToString());
            }
            DialogResult dialog=MessageBox.Show("Zmodyfikowano tabele, czy wrócić do okienka głównego?",string.Empty,MessageBoxButtons.YesNo);
            if (dialog == System.Windows.Forms.DialogResult.Yes)
                this.Close();
            else
            {
                DelColCombobox.ItemsSource = tabResult.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
                DelColCombobox.SelectedIndex = -1;
                NameAddColumnTxtbox.Text = string.Empty;
            }
        }
        #endregion
    }
}
