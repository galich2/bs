using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Морской_Бой
{
    class Море
    {
        public static Program.Точка Размер_моря = new Program.Точка(10, 10);
        public static int Всего_кораблей = 10;
        protected int[,] Карта_кораблей;
        Program.Статус[,] Карта_попаданий;
        public int Расставлено { get; protected set; }
        public int Убито { get; protected set; }
        protected Корабль[] корабль;

        public Море()
        {
            Карта_кораблей = new int[Размер_моря.x, Размер_моря.y];
            Карта_попаданий = new Program.Статус[Размер_моря.x, Размер_моря.y];
            корабль = new Корабль[Всего_кораблей];
            Сброс();
        }
        public void Сброс()
        {
            for (int x = 0; x < Размер_моря.x; x++)
             { 
                for (int y = 0; y < Размер_моря.y; y++)
                {
                    Карта_кораблей[x, y] = -1;
                    Карта_попаданий[x, y] = Program.Статус.Неизвестно;
                }
            }
            for (int i = 0; i < Всего_кораблей; i++)
            {
                корабль[i] = null;
                Расставлено = 0;
                Убито = 0;
            }
        }
        public void ПоставитьКорабль(int Номер, Program.Точка[] Палуба)
        {
            if (корабль[Номер]!= null)
                УбратьКорабль(Номер);
            корабль[Номер] = new Корабль(Палуба);
            foreach (Program.Точка t in Палуба)
            {
                Карта_кораблей[t.x, t.y] = Номер;
                Расставлено++;
            }
            
        }
        public void УбратьКорабль(int Номер)
        {
            foreach (Program.Точка t in корабль[Номер].Палуба)
            {
                Карта_кораблей[t.x, t.y] = -1;
                корабль[Номер] = null;
                Расставлено--;
            }
        }
        public bool НетКорабля(int Номер)
        {
            return корабль[Номер] == null;
        }
        public int КартаКораблей(Program.Точка t)
        {
            if (НаМоре(t))
            {
                return Карта_кораблей[t.x, t.y];
            }
            return -1;    
        }
        public Program.Статус КартаПопаданий(Program.Точка t)
        {
            if (НаМоре(t))
            {
                return Карта_попаданий[t.x, t.y];
            }
           return Program.Статус.Неизвестно;
        }
        public bool НаМоре(Program.Точка t)
        {
            return (t.x >= 0 && t.x < Размер_моря.x && t.y >= 0 && t.y < Размер_моря.y);
        }
        public Program.Статус Выстрел(Program.Точка t)
        {
            if (!НаМоре(t))
            {
                return Program.Статус.Неизвестно;
            }
            if(Карта_попаданий[t.x, t.y] != Program.Статус.Неизвестно)
            {
                return Карта_попаданий[t.x, t.y];
            }
            if(Карта_кораблей[t.x, t.y] == -1)
            {
                Карта_попаданий[t.x, t.y] = Program.Статус.Мимо;
                return Program.Статус.Мимо;
            }
            Program.Статус статус = корабль[Карта_кораблей[t.x, t.y]].Выстрел(t);
            Карта_попаданий[t.x, t.y] = статус;
            if(статус == Program.Статус.Убил)
            {
                Убито++;
                if(Убито > Расставлено)
                {
                    return Program.Статус.Победил;
                }
            }
            return статус;
        }
    }
}
