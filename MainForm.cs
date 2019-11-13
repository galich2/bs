using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Морской_Бой
{
    public partial class MainForm : Form
    {
        Море sea_user = new Море();
        Море sea_comp = new Море();
        static string abc = "АБВГДЕЖЗИК";
        Color Color_back = Color.DarkSeaGreen;
        public MainForm()
        {
            InitializeComponent();
            sea_user = new Море();
            sea_comp = new Море();
            InitGrid(grid_user);
            InitGrid(grid_comp);

        }
      private void InitGrid(DataGridView grid)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            grid.DefaultCellStyle.BackColor = Color_back;
            for (int x = 0; x < Море.Размер_моря.x; x++)  
                grid.Columns.Add("col_" + x.ToString(), abc.Substring(x, 1));
            for (int y = 0; y < Море.Размер_моря.x; y++)
            {
                grid.Rows.Add();
                grid.Rows[y].HeaderCell.Value = (y + 1).ToString();
            }
            grid.Height = Море.Размер_моря.y * grid.Rows[0].Height + grid.ColumnHeadersHeight + 2;
        }
       
    }
}
