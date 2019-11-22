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
            Сброс();
            ПоставитьКорабль(0,
                new Program.Точка[]
                { new Program.Точка(1, 1),
                  new Program.Точка(2, 1),
                  new Program.Точка(3, 1),
                  new Program.Точка(4, 1)}
                );
            ПоставитьКорабль(1,
                new Program.Точка[]
                { new Program.Точка(1, 3),
                  new Program.Точка(2, 3),
                  new Program.Точка(3, 3) }
                );
            ПоставитьКорабль(2,
                new Program.Точка[]
                { new Program.Точка(5, 3),
                  new Program.Точка(6, 3),
                  new Program.Точка(7, 3)}
                );
            ПоставитьКорабль(3,
                new Program.Точка[]
                { new Program.Точка(1, 5),
                  new Program.Точка(2, 5)}
                );
            ПоставитьКорабль(4,
                new Program.Точка[]
                { new Program.Точка(4, 5),
                  new Program.Точка(5, 5)}
                );
            ПоставитьКорабль(5,
                new Program.Точка[]
                { new Program.Точка(7, 5),
                  new Program.Точка(8, 5)}
                );
            for (int номер = 6; номер < Всего_кораблей; номер++)
            {
                ПоставитьКорабль(номер, new Program.Точка[] {new Program.Точка((номер - 5)*2 - 1, 7)});
            }
            return true;
        }
        public void Сброс()
        {
            for (int x = 0; x < Размер_моря.x; x++)
            {
                for (int y = 0; y < Размер_моря.y; y++)
                {
                    Карта_кораблей[x, y] = -1;
                    ShowShip(new Program.Точка(x, y), -1);
                    Карта_попаданий[x, y] = Program.Статус.Неизвестно;
                    ShowFight(new Program.Точка(x, y), Program.Статус.Неизвестно);
                }
            }
            for (int i = 0; i < Всего_кораблей; i++)
            {
                корабль[i] = null;
                Расставлено = 0;
                Убито = 0;
            }
        }
        public bool ПоставитьПоТочкам(Program.Точка[] палуба)
        {
            int длина = палуба.Length;
            int номер = НайтиНомер(длина);
            if (номер < 0)
                return false;
            Program.Точка лв = палуба[0];
            Program.Точка пн = палуба[0];
            for (int j = 0; j < длина; j++)
            {
                лв.x = Math.Min(лв.x, палуба[j].x);
                лв.y = Math.Min(лв.y, палуба[j].y);
                пн.x = Math.Max(пн.x, палуба[j].x);
                пн.y = Math.Max(пн.y, палуба[j].y);
            }
            if (лв.x == пн.x)
            {
                if (пн.y - лв.y + 1 != длина)
                    return false;
            }
            else
            if (лв.y == пн.y)
            {
                if (пн.x - лв.x + 1 != длина)
                    return false;
            }
            else
                return false;
            for (int j = 0; j < длина; j++)
                ОчиститьОбласть(палуба[j]);
            ПоставитьКорабль(номер, палуба);
            return true;
        }
        protected int НайтиНомер(int длина)
        {
            for (int j = 0; j < длина_кораблей.Length; j++)
                if (длина == длина_кораблей[j])
                    if (НетКорабля(j))
                        return j;

            return -1;
        }
        public void ПоставитьКорабль(int Номер, Program.Точка[] Палуба)
        {
            if (корабль[Номер] != null)
                УбратьКорабль(Номер);
            корабль[Номер] = new Корабль(Палуба);
            foreach (Program.Точка t in Палуба)
            {
                Карта_кораблей[t.x, t.y] = Номер;
                ShowShip(t, Номер);
            }
                Расставлено++;
        }
        public void УбратьКорабль(int Номер)
        {
            foreach (Program.Точка t in корабль[Номер].Палуба)
            {
                Карта_кораблей[t.x, t.y] = -1;
                ShowShip(t, -1);
            }
            корабль[Номер] = null;
                Расставлено--;
            
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
        protected void ОчиститьОбласть(Program.Точка t)
        {
            Program.Точка p;
            for (p.x = t.x - 1; p.x <= t.x + 1; p.x++)
                for (p.y = t.y - 1; p.y <= t.y + 1; p.y++)
                    ОчиститьТочку(p);
        }

        public void ОчиститьТочку(Program.Точка t)
        {
            if (!НаМоре(t)) return;
            if (Карта_кораблей[t.x, t.y] == -1)
                return;
            УбратьКорабль(Карта_кораблей[t.x, t.y]);
        }
        public bool ПоставитьСлучайно(int номер)
        {
            int длина = длина_кораблей[номер];
            Program.Точка нос;
            Program.Точка шаг;
            if (rand.Next(2) == 0)
            {
                нос = new Program.Точка(rand.Next(0, Размер_моря.x - длина + 1), rand.Next(0, Размер_моря.y));
                шаг = new Program.Точка(1, 0);
            }
            else
            {
                нос = new Program.Точка(rand.Next(0, Размер_моря.x), rand.Next(0, Размер_моря.y - длина + 1));
                шаг = new Program.Точка(0, 1);
            }
            Program.Точка[] палуба = new Program.Точка[длина];
            for (int j = 0; j < длина; j++)
            {
                палуба[j] = new Program.Точка(нос.x + j * шаг.x, нос.y + j * шаг.y);
                ОчиститьОбласть(палуба[j]);
            }
            ПоставитьКорабль(номер, палуба);
            
            return true;
        }
    }
}
