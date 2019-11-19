using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Морской_Бой
{
    class Редактор: Море
    {
        static int[] длина_кораблей = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
        static Random rand = new Random();
        public Редактор() : base()
        {

        }
        public bool ПоставитьРовно()
        {
            ПоставитьКорабль(0,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(1,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(2,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(3,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(4,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(5,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(6,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(7,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(8,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            ПоставитьКорабль(9,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(1, 2),
                  new Program.Точка(1, 3),
                  new Program.Точка(1, 4)}
                );
            return true;
        }
    }
}
