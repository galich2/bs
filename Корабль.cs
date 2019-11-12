using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Морской_Бой
{
    class Корабль
    {
        int Попаданий;
        public Program.Точка[] Палуба { get; private set;}
        public Корабль (Program.Точка[] Палуба)
        {
            this.Палуба = Палуба;
            Попаданий = 0;
        }
        public Program.Статус Выстрел(Program.Точка t)
        {
            for (int i = 0; i < Палуба.Length; i++)
                if (Палуба[i].x == t.x && Палуба[i].y == t.y)
                {
                    Попаданий++;
                    if (Попаданий == Палуба.Length)
                    {
                        return Program.Статус.Убил;
                    }
                    else
                    {
                        return Program.Статус.Ранил;
                    }
                }
            return Program.Статус.Мимо;
        }
    }
}
