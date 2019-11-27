using System;
using System.Windows.Forms;

namespace Морской_Бой
{
    public partial class FormGame : Form
    {
        Редактор sea_user;
        Редактор sea_comp;
        SeaGrid GridUser;
        SeaGrid GridComp;

        enum Mode
        {
            EditShips,
            PlayUser,
            PlayComp,
            Finish
        };
        Mode mode;
        public FormGame()
        {
            InitializeComponent();
            sea_user = new Редактор();
            sea_user.ShowShip = ShowUserShip;
            sea_user.ShowFight = ShowUserFight;
            sea_comp = new Редактор();
            sea_comp.ShowShip = ShowCompShip;
            sea_comp.ShowFight = ShowCompFight;
            GridUser = new SeaGrid(grid_user);
            GridComp = new SeaGrid(grid_comp);
            ReStart();
        }
        private void ReStart()
        {
            mode = Mode.EditShips;
            sea_user.Сброс();
            sea_comp.Сброс();
            sea_comp.ПоставитьРовно();
            buttonRandom.Visible = true;
            buttonClear.Visible = true;
            buttonStart.Visible = true;
            ShowUnPlacedShips();
        }
        private void ShowUserShip(Program.Точка place, int nr)
        {
            GridUser.ShowShip(place, nr);
        }
        private void ShowCompShip(Program.Точка place, int nr)
        {
            GridComp.ShowShip(place, nr);
        }
        private void ShowUserFight(Program.Точка place, Program.Статус status)
        {
            GridUser.ShowFight(place, status);
        }
        private void ShowCompFight(Program.Точка place, Program.Статус status)
        {
            GridComp.ShowFight(place, status);
        }
        private void grid_user_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                PlaceShip();
        }
        private void PlaceShip()
        {
            Program.Точка[] ship = GridUser.GetSelectedCells();
            if (ship == null)
                return;
            if (ship.Length == 1)
                sea_user.ОчиститьТочку(ship[0]);
            sea_user.ПоставитьПоТочкам(ship);
            ShowUnPlacedShips();
        }
        private void ShowUnPlacedShips()
        {
            sea_comp.ПоставитьРовно();
            for (int j = 0; j < Море.Всего_кораблей; j++)
            {
                if (!sea_user.НетКорабля(j))
                    sea_comp.УбратьКорабль(j);
                buttonStart.Enabled = (sea_user.создано == Море.Всего_кораблей);
            }
        }
        private void grid_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PlaceShip();
        }
        private void FormGame_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           // if(e.Button == System.Windows.Forms.MouseButtons.Right)
        }
        private void buttonRandom_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
            sea_user.ПоставитьСлучайно();
            ShowUnPlacedShips();
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
            sea_user.Сброс();
            ShowUnPlacedShips();
        }

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            ReStart();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (mode != Mode.EditShips) return;
            if(sea_user.создано == Море.Всего_кораблей)
            {
                sea_comp.ПоставитьСлучайно();
                mode = Mode.PlayUser;
                buttonRandom.Visible = false;
                buttonClear.Visible = false;
                buttonStart.Visible = false;
            }
        }
    }
}
