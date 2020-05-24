using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ProjectClient.View
{
    public partial class DefineFields : AlterTable
    {
        #region fields and ctor
        public DataTable getTabResult
        {
            get { return tabResult; }
        }

        public DefineFields()
        {
            InitializeComponent();
            for (int i = 2; i <= 10; i++)
                ListOption.Items.Add(i);
        }
        #endregion

        #region events
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            tabResult = new DataTable();
            foreach (var item in FieldsArea.Children)
            {
                if (item is TextBox)
                {
                    TextBox textBox = (TextBox)item;
                    tabResult.Columns.Add(textBox.Text);
                }
            }
            MessageBox.Show("Utworzono kolumny");
            this.Close();
        }

        private void ShowList_Change(object sender, EventArgs args)
        {
            if(FieldsArea.ColumnDefinitions.Count<2)
                NewColumnDefinition(2);
            if (ListOption.SelectedValue != null)
            {
                FieldsArea.Children.Clear();
                FieldsArea.RowDefinitions.Clear();                             
                NewRowDefinition((int)ListOption.SelectedItem);              
            }
        }
        #endregion

        #region private methods
        private void NewColumnDefinition(int numberOfColumns)
        {
            if (FieldsArea.ColumnDefinitions.Count < 1)
            {
                for (int i = 1; i <= numberOfColumns; i++)
                {
                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    columnDefinition.Width = new GridLength(1, GridUnitType.Star);
                    FieldsArea.ColumnDefinitions.Add(columnDefinition);
                }
            }
        }

        private void NewRowDefinition(int numberOfRows)
        {
            for (int i = 1; i <= numberOfRows; i++)
            {

                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1, GridUnitType.Star);
                FieldsArea.RowDefinitions.Add(rowDefinition);
                FieldsArea.Children.Add(AddLabel(i));
                try
                {
                    FieldsArea.Children.Add(AddTxtbox(i));
                }
                catch (Exception)
                {
                    MessageBox.Show("Kolumna już istnieje");
                    break;
                }
            }
        }

        #endregion
    }
}
