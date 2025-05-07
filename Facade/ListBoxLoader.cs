using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPlayer.Facade
{
    
    public static class ListBoxLoader
    {
        public static void LoadFromDataTable(ListBox listBox, DataTable dataTable, string columnName)
        {
            listBox.Items.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                listBox.Items.Add(row[columnName].ToString());
            }
        }
    }

}
