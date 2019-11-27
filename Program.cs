using System;
using System.Windows.Forms;

namespace Морской_Бой
{
    static public class Program
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
            Application.Run(new FormGame());
        }
    }
}
