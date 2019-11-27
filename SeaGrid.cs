using System.Drawing;
using System.Windows.Forms;

namespace Морской_Бой
{
    class SeaGrid
    {
        DataGridView grid;
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

        static string abc = "АБВГДЕЖЗИК";

        public SeaGrid(DataGridView grid)
        {
            this.grid = grid;
            InitGrid();
        }
        private void InitGrid()
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
        public void ShowShip(Program.Точка place, int nr)
        {
            if (nr < 0)
                grid[place.x, place.y].Style.BackColor = color_back;
            else
                grid[place.x, place.y].Style.BackColor = color_ship[nr];
        }
        public void ShowFight(Program.Точка place, Program.Статус status)
        {

            grid[place.x, place.y].Style.BackColor = color_fight[(int)status];
        }
        public Program.Точка[] GetSelectedCells()
        {
            if (grid.SelectedCells.Count == 0)
                return null;
            Program.Точка[] ship = new Program.Точка[grid.SelectedCells.Count];
            int j = 0;
            foreach(DataGridViewCell cell in grid.SelectedCells)
                ship[j++] = new Program.Точка(cell.ColumnIndex, cell.RowIndex);
            grid.ClearSelection();
            return ship;
        }
    }
}
