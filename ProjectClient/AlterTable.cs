using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProjectClient
{
    public class AlterTable: Window
    {
        protected DataTable tabResult = null;

        protected Label AddLabel(int i)
        {
            Label label = new Label()
            {
                Content = "Nazwa kolumny " + i,
                Name = "LabelCol",
            };
                
            Grid.SetRow(label, i-1);
            Grid.SetColumn(label, 0);
            return label;
        }

        protected TextBox AddTxtbox(int i,string value=null)
        {
            object resource = Application.Current.FindResource("TxtboxResize");
            TextBox textBox = new TextBox()
            {
                Name = "column_" + i,
                Style = (Style)resource
            };
            if (value != null)
                textBox.Text = value;
            Grid.SetRow(textBox, i-1);
            Grid.SetColumn(textBox, 1);
            return textBox;
        }
    }
}
