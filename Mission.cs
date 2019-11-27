using System;

namespace Морской_Бой
{
    public class Mission
    {
        Море sea;
        Random rand;
        int[,] shape = {
            {1, 2, 1, 3, 1, 2, 1, 3, 1, 2 },
            {2, 1, 3, 1, 2, 1, 3, 1, 2, 1 },
            {1, 3, 1, 2, 1, 3, 1, 2, 1, 3 },
            {3, 1, 2, 1, 3, 1, 2, 1, 3, 1 },
            {1, 2, 1, 3, 1, 2, 1, 3, 1, 2 },
            {2, 1, 3, 1, 2, 1, 3, 1, 2, 1 },
            {1, 3, 1, 2, 1, 3, 1, 2, 1, 3 },
            {3, 1, 2, 1, 3, 1, 2, 1, 3, 1 },
            {1, 2, 1, 3, 1, 2, 1, 3, 1, 2 },
            {2, 1, 3, 1, 2, 1, 3, 1, 2, 1 }};
        //int[,] shape = {
        //    {2, 1, 1, 3, 1, 2, 1, 3, 1, 2 },
        //    {1, 2, 3, 1, 1, 1, 3, 1, 2, 1 },
        //    {3, 1, 2, 1, 3, 2, 1, 2, 3, 1 },
        //    {1, 3, 1, 2, 1, 3, 2, 1, 1, 3 },
        //    {2, 1, 2, 3, 2, 2, 1, 3, 2, 1 },
        //    {1, 2, 3, 1, 2, 2, 3, 1, 1, 2 },
        //    {3, 1, 1, 2, 3, 1, 2, 1, 3, 1 },
        //    {1, 3, 2, 1, 1, 3, 1, 2, 1, 3 },
        //    {1, 2, 1, 3, 2, 2, 1, 3, 2, 1 },
        //    {2, 1, 3, 1, 2, 1, 3, 1, 1, 2 }};
        bool modeDanger;
        int[] shipLenght = new int[5];
        int[,] map;
        int[,] put;

        public Mission(Море sea)
        {
            this.sea = sea;
            rand = new Random();
            map = new int[Море.Размер_моря.x, Море.Размер_моря.y];
            put = new int[Море.Размер_моря.x, Море.Размер_моря.y];
            Reset();
        }
        private void Reset()
        {
            shipLenght[1] = 4;
            shipLenght[2] = 3;
            shipLenght[3] = 2;
            shipLenght[4] = 1;
            for (int x = 0; x < Море.Размер_моря.x; x++)
                for (int y = 0; y < Море.Размер_моря.y; y++)
                    map[x, y] = 0;
            modeDanger = false;
        }
        public Program.Статус Fight(out Program.Точка target)
        {
            if (modeDanger)
                target = FightDanger();
            else
                target = FightShapes();
            Program.Статус status = sea.Выстрел(target);
            switch (status)
            {
                case Program.Статус.Мимо: map[target.x, target.y] = 1; break;
                case Program.Статус.Ранил: map[target.x, target.y] = 2; modeDanger = true; break;
                case Program.Статус.Убил:
                case Program.Статус.Победил: 
                    map[target.x, target.y] = 2;
                    int len = MarkKilledShip(target);
                    shipLenght[len]--;
                    modeDanger = false;
                    break;
            }
            return status;
        }
        private Program.Точка FightShapes()
        {
            InitPut();
            for (int x = 0; x < Море.Размер_моря.x; x++)
                for (int y = 0; y < Море.Размер_моря.y; y++)
                    if (map[x, y] == 0)
                        put[x, y] = shape[x, y];
         return RandomPut();
        }
        private Program.Точка FightDanger()
        {
            InitPut();
            for (int x = 0; x < Море.Размер_моря.x; x++)
                for (int y = 0; y < Море.Размер_моря.y; y++)
                    if(map[x, y] == 2)
                    {
                        bool longer = false;
                        Program.Точка ship = new Program.Точка(x, y);
                       for (int lenght = shipLenght.Length - 1; lenght >= 2; lenght --)
                            if (longer || shipLenght[lenght] > 0)
                            {
                                CheckShipDirection(ship, -1,  0, lenght);
                                CheckShipDirection(ship,  1,  0, lenght);
                                CheckShipDirection(ship,  0, -1, lenght);
                                CheckShipDirection(ship,  0,  1, lenght);
                                longer = true;
                            }
                    }
                    return RandomPut();
        }
        private void CheckShipDirection(Program.Точка ship, int sx, int sy, int lenght)
        {
            if (Map(ship.x, ship.y) != 2) return;
            if (Map(ship.x - sx, ship.y - sy) == 2) return;
            if(sx == 0)
            {
                if (Map(ship.x - 1, ship.y) == 2) return;
                if (Map(ship.x + 1, ship.y) == 2) return;
            }
            if (sy == 0)
            {
                if (Map(ship.x, ship.y - 1) == 2) return;
                if (Map(ship.x, ship.y + 1) == 2) return;
            }
            int unknown = 0;
            int unknown_j = 0;
            for (int j = 1; j < lenght; j++)
            {
                int p = Map(ship.x + j * sx, ship.y + j * sy);
                if (p == 1) return;
                if (p == -1) return;
                if (p == 0)
                {
                    unknown++;
                    if (unknown == 1)
                        unknown_j = j;
                }
            }
            if(unknown >= 1)
                put[ship.x + unknown_j * sx, ship.y + unknown_j * sy]++;
        }
        private int Map(int x, int y)
        {
            if (sea.НаМоре(new Program.Точка(x, y)))
                return map[x, y];
            return -1;
        }
        private void InitPut()
        {
            for (int x = 0; x < Море.Размер_моря.x; x++)
                for (int y = 0; y < Море.Размер_моря.y; y++)
                    put[x, y] = 0;
        }
        private Program.Точка RandomPut()
        {
            ShowPutArray();
            int max = -1;
            int qty = 0;
            for (int x = 0; x < Море.Размер_моря.x; x++)
                for (int y = 0; y < Море.Размер_моря.y; y++)
                    if (put[x, y] > max)
                    {
                        max = put[x, y];
                        qty = 1;
                    }
                    else
                    if (put[x, y] == max)
                        qty++;
            int nr = rand.Next(0, qty);
            for (int x = 0; x < Море.Размер_моря.x; x++)
                for (int y = 0; y < Море.Размер_моря.y; y++)
                    if (put[x, y] == max)
                        if (nr-- == 0)
                            return new Program.Точка(x, y);
            return new Program.Точка(0, 0);
        }
        private int MarkKilledShip(Program.Точка place)
        {
            if (!sea.НаМоре(place))
                return 0;
            if(map[place.x, place.y] == 2)
            {
                map[place.x, place.y] = 3;
                int x, y;
                for (x = place.x - 1; x <= place.x + 1; x++)
                    for (y = place.y - 1; y <= place.y + 1; y++)
                        if (Map(x, y) == 0)
                            map[x, y] = 1;
                int lenght = 1; 
                lenght += MarkKilledShip(new Program.Точка(place.x - 1, place.y));
                lenght += MarkKilledShip(new Program.Точка(place.x + 1, place.y));
                lenght += MarkKilledShip(new Program.Точка(place.x, place.y - 1));
                lenght += MarkKilledShip(new Program.Точка(place.x, place.y + 1));
                return lenght;
            }

            return 0;
        }
        private void ShowPutArray()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            for (int x = 0; x < Море.Размер_моря.x; x++)
                for (int y = 0; y < Море.Размер_моря.y; y++)
                {
                    Console.SetCursorPosition(x + 1, y + 12);
                    Console.Write(put[x, y] > 0 ? put[x, y].ToString() : " " );
                }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadKey();
        }
    }
}
