﻿using System;
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
        Color[] color_ship = {Color.DarkOrange, 
            Color.DarkMagenta, Color.DarkMagenta,
            Color.Black, Color.Black, Color.Black,
            Color.Red, Color.Red, Color.Red, Color.Red};
        public MainForm()
        {
            InitializeComponent();
            sea_user = new Редактор();
            sea_comp = new Редактор();
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
        private void ShowShips(DataGridView grid, Море sea)
        {
            for (int x = 0; x < Море.Размер_моря.x; x++)
                for (int y = 0; y < Море.Размер_моря.y; y++)
                {
                    int nr = sea.КартаКораблей(new Program.Точка(x, y));
                    if (nr < 0)
                        grid[x, y].Style.BackColor = color_back;
                    else
                        grid[x, y].Style.BackColor = color_ship[nr];
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sea_user.ПоставитьРовно();
            sea_user.ПоставитьРовно();
            ShowShips(grid_user, sea_user);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Море.Всего_кораблей; i++)
            sea_user.ПоставитьСлучайно(i);
            ShowShips(grid_user, sea_user);
        }

    }
}
