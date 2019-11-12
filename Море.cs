using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Морской_Бой
{
    class Море
    {
        public static Program.Точка Размер_поля = new Program.Точка(10, 10);
        public static int Всего_кораблей = 10;
        protected int[,] Карта_кораблей;
        Program.Статус[,] Карта_попаданий;
    }
}
