using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Морской_Бой
{
    static class Program
    {
        public enum Статус
        {
            Неизвестно,
            Мимо,
            Ранил,
            Убил,
            Победил
        }
        public struct Точка
        {
            public int x;
            public int y;
            public Точка(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
