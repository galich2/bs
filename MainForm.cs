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
        public MainForm()
        {
            InitializeComponent();
        }
        Море sea = new Море();
        private void MainForm_Load(object sender, EventArgs e)
        {
            //Сброс();
            sea.ПоставитьКорабль(0, new Program.Точка[]
            {
                new Program.Точка(0,1),
                new Program.Точка(0,2),
                new Program.Точка(0,3),
                new Program.Точка(0,4),
            });
            ShowSea();
            sea.ПоставитьКорабль(1, new Program.Точка[]
            {
                new Program.Точка(2,1),
                new Program.Точка(2,2),
                new Program.Точка(2,3),
            });
            sea.Выстрел(new Program.Точка(0,1));
            ShowSea();
            ShowFight();
        }
        public void ShowSea()
        {
            string text = "";
            for (int y = 0; y < Море.Размер_моря.y; y++)
            {
                for (int x = 0; x < Море.Размер_моря.x; x++)
                    text += sea.КартаКораблей(new Program.Точка(x, y)) == -1 ? "." : "O";
                text += Environment.NewLine;
            }
            textBox1.Text = text;
        }
        public void ShowFight()
        {
            string text = "";
            for (int y = 0; y < Море.Размер_моря.y; y++)
            {
                for (int x = 0; x < Море.Размер_моря.x; x++)
                {
                    switch (sea.КартаПопаданий(new Program.Точка(x, y)))
                    {
                        case Program.Статус.Неизвестно: 
                            text += "."; break;
                        case Program.Статус.Мимо:
                            text += ","; break;
                        case Program.Статус.Ранил:
                            text += "x"; break;
                        case Program.Статус.Убил:
                            text += "X"; break;
                    }
                }
                text += Environment.NewLine;
            }
            textBox2.Text = text;
        }
    }
}
