//Создется объек мира
using Rogulike_way;
using System.Threading;
Console.CursorVisible = false;    //Отключение курсора

//Вызываю меню и создаю мир(комнаты, героя и тд)
Menu menu = new();
menu.Show();
World world = StartGame.CreateWorld(menu);

//Обработка нажатий
ConsoleKeyInfo keyInfo;
while (true)
{
    keyInfo = Console.ReadKey(true);
    switch (keyInfo.KeyChar)
    {
        case 'w' or 'ц':
            MovePlayer.Move("Up", ref world.currentRoom, world);
            break;

        case 's' or 'ы':
            MovePlayer.Move("Down", ref world.currentRoom, world);
            break;

        case 'd' or 'в':
            MovePlayer.Move("Right", ref world.currentRoom, world);
            break;

        case 'a' or 'ф':
            MovePlayer.Move("Left", ref world.currentRoom, world);
            break;

        case 'e' or 'у':
            //Invenary invenary = new Invenary();
            // invenary.ChooseAmmunition();
            break;

        case 'q' or 'й':
            Menu menu_ = new();
            menu_.Show();
            world = StartGame.CreateWorld(menu_);
            break;
    }
}

//Создание игры и начальное выставление и отображение героя
class StartGame
{
    static public World CreateWorld(Menu menu)
    {
        World world = new World();
        world.map = world.CreateMiniMap(); //Создаю мини карту(и одновремено расположение комнат относительно друг друга)
        world.roomsMini = world.AppArrMiniRooms(world.map); //Заполняю коллекцию мини комнат
        world.roomsReal = world.CreateArrRealRooms(world.roomsMini); //Создание коллекции реальных комнат 

        //Создание героя (создается в зависимости от выбора в меню)
        if (menu.hero_class == "wizard")
        {
            world.hero = new Wizard();
        }
        else if (menu.hero_class == "barbarian")
        {
            world.hero = new Barbarian();
        }
        else if (menu.hero_class == "prowler")
        {
            world.hero = new Prowler();
        }

        //Выбор начальной комнаты для отрисовки из мира
        Random rand = new Random();
        int numRoom = rand.Next(0, 9);
        world.currentRoom = world.roomsReal[numRoom];
        Monsters.CreateMonsters(world.currentRoom, world.hero);

        //Задаю герою координаты
        world.hero.coordinates = new int[2] { world.currentRoom.map.GetLength(1) / 2, world.currentRoom.map.GetLength(0) / 2 };  //Делаю так, чтобы он был посередине комнаты


        //Отрисовываю карту без героя
        DraftGame.DraftPlane(world.currentRoom, world);

        //Задежка
        System.Threading.Thread.Sleep(1000);

        //Указываю героя в центре координат
        world.currentRoom.map[world.hero.coordinates[1], world.hero.coordinates[0]] = World.charHero;
        DraftGame.DraftPlane(world.currentRoom, world);                     //Отрисовываю карту уже с героем

        return world;
    }
}

//Отрисовка игры(необходимо добавить отрисовку статистики персонажа и игровых событий)
class DraftGame
{
    static public int StatX = 25, StatY = 5;  //Смещение карты

    //Добавление символа в необходимой координате с дефолтным смещением
    static public void PutCurs(char ch, int y, int x)
    {
        Console.SetCursorPosition(StatX + x, StatY + y);
        PutColor(ch);

        Console.Write(ch);
        Console.ResetColor();

    }

    //Отрисовка цветов
    static public void PutColor(char ch)
    {
        World wr = new World();
        if (ch == World.charDoors)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }

        else if (ch == World.charHero)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        else if (ch == World.charRoomBordHor || ch == World.charRoomBordVert || ch == '╔' || ch == '╗' || ch == '╚' || ch == '╝')
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        else if (ch == World.charMiniRoom)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        else if (ch == World.charMiniRoomBordVert || ch == World.charMiniRoomBordHor || ch == '┌' || ch == '┐' || ch == '└' || ch == '┘')
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        else if (ch == World.skeleton)
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        else if (ch == World.rat)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        else if (ch == World.ork)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        else if (ch == World.knight)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
        }
        else if (ch == World.ghost)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
    }


    //Добавление символа в необходимой координате с указанным смещением и цветом
    static public void PutCursRange(char ch, int y, int x, int statX, int statY)
    {
        Console.SetCursorPosition(statX + x, statY + y);
        PutColor(ch);
        Console.Write(ch);
        Console.ResetColor();
    }


    //Отрисовка Мини карты и комнаты
    static public void DraftPlane(RealRoom room, World world)
    {
        //Отрисовка комнаты
        //Длина комнаты
        int x_len = room.map.GetLength(1);
        int y_len = room.map.GetLength(0);

        //Рисую указанные символы
        Console.Clear();
        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)
                PutCurs(room.map[y, x], y, x);

        //Отрисовка миникарты
        DraftMinyMap(room, world);
        DraftHeroParametrs(room, world);
    }

    //Отрисовка состояния героя
    static public void DraftHeroParametrs(RealRoom room, World world)
    {
        Console.SetCursorPosition(0, StatY);
        Console.Write($"Герой {world.hero.name}\nЗдоровье: {world.hero.NowHealht.ToString("F1")}\nВыносливость: {world.hero.NowStamina.ToString("F1")}\nУровень: {world.hero.level}\nОпыт: {world.hero.experience}");
    }

    //Отрисовка миникарты с текущим положением героя
    static public void DraftMinyMap(RealRoom realRoom, World world)
    //Функция миникарты принимает 3 аргумента т.к ей нужно знать(где герой, текущую миникомнату, миникарту)
    {
        //Определение размерности
        int x_len = world.map.GetLength(1);
        int y_len = world.map.GetLength(0);

        //Когда номер миникомнаты совпадает с текущей реальной, то миникарта отображает в текущей миникомнате @
        foreach (MiniRoom roomMini in world.roomsMini)
        {
            //Проверка на наличие посещения комнаты
            if (roomMini.visitings == true)
            {
                world.map[roomMini.y, roomMini.x] = World.charMiniRoom;
            }
            else
            {
                world.map[roomMini.y, roomMini.x] = World.charMiniEmpty;
            }

            if (roomMini.number == realRoom.number)
            {
                for (int y = 0; y < y_len; y++)
                    for (int x = 0; x < x_len; x++)
                    {
                        if (world.map[y, x] == World.charHero)
                        {
                            world.map[y, x] = World.charMiniRoom;
                        }

                    }
                roomMini.visitings = true;
                realRoom.visitings = true;
                world.map[roomMini.y, roomMini.x] = World.charHero;
            }
        }


        //Вывожу миникарту в указанных координатах
        Console.SetCursorPosition(82 + x_len / 2 - 5, 0);
        Console.WriteLine("Миникарта");
        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)
            {
                PutCursRange(world.map[y, x], y, x, 82, 1);
            }
    }
}

//Передвижение героя(необходимо добавить проверку на наличие чего-то кроме стен и дверей)
class MovePlayer
{
    static public void Move(string trend, ref RealRoom roomCurrent, World world)
    {
        if (trend == "Left")
        {
            //Указываю координаты смещения и сохраняю объект, который в них находится
            int[] moveCoordinates = { world.hero.coordinates[0] - 1, world.hero.coordinates[1] };   //Указываю каково смещение
            Object obj = world.DefiningArea(moveCoordinates, roomCurrent);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо
            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            else if (obj is Doors)   //Проверяю принадлежит ли объект классу дверей
            {
                DoorDef((Doors)obj, ref roomCurrent, world, trend);
            }

            else if (obj is Monsters)   //Проверяю принадлежит ли объект классу монстров
            {
                ModsterDef((Monsters)obj, roomCurrent, world, trend);
            }

            //Если простанство пустое
            else
            {
                //Меняю карту и соответсвенно меняю координаты героя
                EmptyDef(roomCurrent, world, trend);
            }
        }

        //Остальное работает подобно верхнему
        if (trend == "Right")
        {
            int[] move_coordinates = { world.hero.coordinates[0] + 1, world.hero.coordinates[1] };
            Object obj = world.DefiningArea(move_coordinates, roomCurrent);

            if (obj is Borders)
            {
                return;
            }


            else if (obj is Doors)
            {
                DoorDef((Doors)obj, ref roomCurrent, world, trend);
            }

            else if (obj is Monsters)
            {
                ModsterDef((Monsters)obj, roomCurrent, world, trend);
            }

            else
            {
                EmptyDef(roomCurrent, world, trend);
            }

        }

        if (trend == "Up")
        {
            int[] move_coordinates = { world.hero.coordinates[0], world.hero.coordinates[1] - 1 };
            Object obj = world.DefiningArea(move_coordinates, roomCurrent);

            if (obj is Borders)
                return;


            else if (obj is Doors)
            {
                DoorDef((Doors)obj, ref roomCurrent, world, trend);
            }

            else if (obj is Monsters)
            {
                ModsterDef((Monsters)obj, roomCurrent, world, trend);
            }

            else
            {
                EmptyDef(roomCurrent, world, trend);
            }
        }

        if (trend == "Down")
        {
            int[] move_coordinates = { world.hero.coordinates[0], world.hero.coordinates[1] + 1 };
            Object obj = world.DefiningArea(move_coordinates, roomCurrent);

            if (obj is Borders)
            {
                return;
            }

            else if (obj is Doors)
            {
                DoorDef((Doors)obj, ref roomCurrent, world, trend);
            }

            else if (obj is Monsters)
            {
                ModsterDef((Monsters)obj, roomCurrent, world, trend);
            }

            else
            {
                EmptyDef(roomCurrent, world, trend);
            }
        }
    }

    //При обнаружении монстра
    static public void ModsterDef(Monsters monster, RealRoom roomCurrent, World world, string move)
    {
        roomCurrent.map[world.hero.coordinates[1], world.hero.coordinates[0]] = ' ';
        DraftGame.PutCurs(' ', world.hero.coordinates[1], world.hero.coordinates[0]);
        if (move == "Up")
        {
            world.hero.coordinates[0] += 0; world.hero.coordinates[1] -= 1;
        }
        else if (move == "Down")
        {
            world.hero.coordinates[0] += 0; world.hero.coordinates[1] += 1;
        }
        else if (move == "Left")
        {
            world.hero.coordinates[0] -= 1; world.hero.coordinates[1] -= 0;
        }
        else if (move == "Right")
        {
            world.hero.coordinates[0] += 1; world.hero.coordinates[1] -= 0;
        }


        roomCurrent.map[world.hero.coordinates[1], world.hero.coordinates[0]] = World.charHero;
        DraftGame.PutCurs(World.charHero, world.hero.coordinates[1], world.hero.coordinates[0]);
        Fight gg = new();
        if (gg.Start(world.hero, monster) == 1)
        {
            roomCurrent.monsters_list.Remove(monster);
            world.hero.experience += monster.experience;
            //Проверка уровня
            world.hero.CheckAndLevelUp();
            DraftGame.DraftPlane(roomCurrent, world);
        }
        else
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(70, 15); Console.Write("Увы, вы проиграли");
            if (world.hero.level > 1)
            {
                Console.SetCursorPosition(70, 16); Console.Write($"  Ваш счет: {world.hero.experience + world.hero.level * world.hero.experience}");
            }
            Console.SetCursorPosition(70, 16); Console.Write($"  Ваш счет: {world.hero.experience}");
            Environment.Exit(0);
        }

    }

    static public void DoorDef(Doors door, ref RealRoom roomCurrent, World world, string move)
    {
        //Координаты игрока меняются
        roomCurrent.map[world.hero.coordinates[1], world.hero.coordinates[0]] = ' ';

        foreach (RealRoom room1 in world.roomsReal)
        {
            //Если комната из коллекции равна указываемой у текущей двери(то есть дверь ведет в нужную комнату, то текущая меняется на указываемую)
            if (room1.number == door.room_num)
            {
                if (room1.visitings == false)
                {
                    Monsters.CreateMonsters(room1, world.hero);
                }
                roomCurrent = room1;

            }
        }

        //Координаты в середине и рядом с дверью
        if (move == "Up")
        {
            world.hero.coordinates[0] = roomCurrent.map.GetLength(1) / 2; world.hero.coordinates[1] = roomCurrent.map.GetLength(0) - 2;
        }
        else if (move == "Down")
        {
            world.hero.coordinates[0] = (roomCurrent.map.GetLength(1) / 2); world.hero.coordinates[1] = 1;
        }
        else if (move == "Left")
        {
            world.hero.coordinates[0] = roomCurrent.map.GetLength(1) - 2; world.hero.coordinates[1] = roomCurrent.map.GetLength(0) / 2;
        }
        else if (move == "Right")
        {
            world.hero.coordinates[0] = 1; world.hero.coordinates[1] = roomCurrent.map.GetLength(0) / 2;
        }

        roomCurrent.map[world.hero.coordinates[1], world.hero.coordinates[0]] = World.charHero;

        //Отрисовываем
        DraftGame.DraftPlane(roomCurrent, world);
    }

    static public void EmptyDef(RealRoom roomCurrent, World world, string move)
    {
        roomCurrent.map[world.hero.coordinates[1], world.hero.coordinates[0]] = ' ';
        DraftGame.PutCurs(' ', world.hero.coordinates[1], world.hero.coordinates[0]);

        //Координаты в середине и рядом с дверью
        if (move == "Up")
        {
            world.hero.coordinates[0] += 0; world.hero.coordinates[1] -= 1;
        }
        else if (move == "Down")
        {
            world.hero.coordinates[0] += 0; world.hero.coordinates[1] += 1;
        }
        else if (move == "Left")
        {
            world.hero.coordinates[0] -= 1; world.hero.coordinates[1] -= 0;
        }
        else if (move == "Right")
        {
            world.hero.coordinates[0] += 1; world.hero.coordinates[1] -= 0;
        }

        roomCurrent.map[world.hero.coordinates[1], world.hero.coordinates[0]] = World.charHero;
        DraftGame.PutCurs(World.charHero, world.hero.coordinates[1], world.hero.coordinates[0]);
    }
}