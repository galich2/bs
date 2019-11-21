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
        Редактор sea_user;
        Редактор sea_comp;
        static string abc = "АБВГДЕЖЗИК";
        
        Color color_back = Color.DarkSeaGreen;
        
        Color[] color_ship = {
            Color.Purple, 
            Color.Purple, Color.Purple,
            Color.Purple, Color.Purple, Color.Purple,
            Color.Purple, Color.Purple, Color.Purple, Color.Purple};
        
        Color[] color_fight = {
            Color.DarkSeaGreen,
            Color.SeaGreen,
            Color.Orange,
            Color.OrangeRed,
            Color.OrangeRed};

        public MainForm()
        {
            InitializeComponent();
            sea_user = new Редактор();
            sea_user.ShowShip = ShowUserShip;
            sea_user.ShowFight = ShowUserFight;
            sea_comp = new Редактор();
            sea_comp.ShowShip = ShowCompShip;
            sea_comp.ShowFight = ShowCompFight;
            InitGrid(grid_user);
            InitGrid(grid_comp);

        }
      private void InitGrid(DataGridView grid)
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            grid.DefaultCellStyle.BackColor = color_back;
            for (int x = 0; x < Море.Размер_моря.x; x++)  
                grid.Columns.Add("col_" + x.ToString(), abc.Substring(x, 1));
            for (int y = 0; y < Море.Размер_моря.y; y++)
            {
                grid.Rows.Add();
                grid.Rows[y].HeaderCell.Value = (y + 1).ToString();
            }
            grid.Height = Море.Размер_моря.y * grid.Rows[0].Height + grid.ColumnHeadersHeight + 2;
            grid.ClearSelection();
        }
        //private void ShowShips(DataGridView grid, Редактор sea)
        //{
        //    for (int x = 0; x < Море.Размер_моря.x; x++)
        //        for (int y = 0; y < Море.Размер_моря.y; y++)
        //        {
        //            int nr = sea.КартаКораблей(new Program.Точка(x, y));
        //            if (nr < 0)
        //                grid[x, y].Style.BackColor = color_back;
        //            else
        //                grid[x, y].Style.BackColor = color_ship[nr];
        //        }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //ShowShip(grid_user, new Program.Точка(2, 5), 5);
            //ShowFight(grid_comp, new Program.Точка(2, 5), Program.Статус.Ранил);
            //ShowFight(grid_comp, new Program.Точка(3, 5), Program.Статус.Победил);
            sea_user.Выстрел(new Program.Точка(0, 0));
            return;
            sea_user.Сброс();
            sea_user.ПоставитьРовно();
            //ShowShips(grid_user, sea_user);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sea_user.Сброс();
            int loop = 100;
            while (--loop > 0 && sea_user.создано < Море.Всего_кораблей)
            {
                for (int i = 0; i < Море.Всего_кораблей; i++)
                    if (sea_user.НетКорабля(i))
                        sea_user.ПоставитьСлучайно(i);
            loop--;
            }
            //if (sea_user.создано < Море.Всего_кораблей)
            //    sea_user.Сброс();
            //ShowShips(grid_user, sea_user);
        }
        private void ShowShip(DataGridView grid, Program.Точка place, int nr)
        {
            if(nr < 0)
                grid[place.x, place.y].Style.BackColor = color_back;
            else
                grid[place.x, place.y].Style.BackColor = color_ship[nr];
        }

        private void ShowFight(DataGridView grid, Program.Точка place, Program.Статус status)
        {
          
            grid[place.x, place.y].Style.BackColor = color_fight[(int)status];
        }
        private void ShowUserShip(Program.Точка place, int nr)
        {
            ShowShip(grid_user, place, nr);
        }
        private void ShowCompShip(Program.Точка place, int nr)
        {
            ShowShip(grid_comp, place, nr);
        }
        private void ShowUserFight(Program.Точка place, Program.Статус status)
        {
            ShowFight(grid_user, place, status);
        }
        private void ShowCompFight(Program.Точка place, Program.Статус status)
        {
            ShowFight(grid_comp, place, status);
        }
    }
}
