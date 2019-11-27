namespace Морской_Бой
{
    public delegate void deShowShip(Program.Точка place, int nr);
    public delegate void deShowFight(Program.Точка place, Program.Статус status);

    class Море
    {
        public static Program.Точка Размер_моря = new Program.Точка(10, 10);
        public static int Всего_кораблей = 10;
        public deShowShip ShowShip;
        public deShowFight ShowFight;
        protected int[,] Карта_кораблей;
        public Program.Статус[,] Карта_попаданий;
        public int Расставлено { get; protected set; }
        public int Убито { get; protected set; }
        protected Корабль[] корабль;
        public int создано { get; protected set; }
        public Море()
        {
            Карта_кораблей = new int[Размер_моря.x, Размер_моря.y];
            Карта_попаданий = new Program.Статус[Размер_моря.x, Размер_моря.y];
            корабль = new Корабль[Всего_кораблей];
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
                return Program.Статус.Неизвестно;
            if(Карта_попаданий[t.x, t.y] != Program.Статус.Неизвестно)
                return Карта_попаданий[t.x, t.y];
            Program.Статус статус;
            if(Карта_кораблей[t.x, t.y] == -1)
            {
                Карта_попаданий[t.x, t.y] = Program.Статус.Мимо;
                статус = Program.Статус.Мимо;
            } 
            else
                статус = корабль[Карта_кораблей[t.x, t.y]].Выстрел(t);
            Карта_попаданий[t.x, t.y] = статус;
            if(статус == Program.Статус.Убил)
            {
                Убито++;
                if(Убито >= Расставлено)
                   статус = Program.Статус.Победил;
                
            }
            ShowFight(t, статус);
            return статус;
        }
    }
}
