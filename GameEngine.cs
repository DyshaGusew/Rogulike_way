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
            MovePlayer.Move("Up", ref world.currentRoom, ref world);
            break;

        case 's' or 'ы':
            MovePlayer.Move("Down", ref world.currentRoom, ref world);
            break;

        case 'd' or 'в':
            MovePlayer.Move("Right", ref world.currentRoom, ref world);
            break;

        case 'a' or 'ф':
            MovePlayer.Move("Left", ref world.currentRoom, ref world);
            break;

        case 'e' or 'у':
            Inventory inventory = new Inventory();
             inventory.ChooseAmmunition();
            DraftGame.DraftPlane(world.currentRoom, world);
            break;

        case 'q' or 'й':
            menu.Show();
            world = new();
            world = StartGame.CreateWorld(menu);
            break;
    }
    //Условие появления босса
    if(world.hero.countDeadMonsters == 10)
    {
        world.hero.NowHealht = world.hero.StaticHealht;
        world.hero.NowStamina = world.hero.StaticStamina;
        Bosses boss = new Bosses(100, 50, "БАЛРОГ, демон тьмы");
        Console.Clear();
        Console.SetCursorPosition(60, 15);
        Console.Write($"Кажется, вы слышите шаги...");
        System.Threading.Thread.Sleep(2500);

        Console.Clear();
        Console.SetCursorPosition(50, 15);
        Console.Write($"Похоже своими действиями вы кое-кого очень сильно разозлили...");
        System.Threading.Thread.Sleep(2500);

        Console.Clear();
        Console.SetCursorPosition(60, 15);
        Console.Write($"Да начнется же великий бой!");
        System.Threading.Thread.Sleep(2500);

        Fight bossFight = new();
        
        if(bossFight.Start(world.hero, boss) == 0)
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(70, 15); Console.Write("Увы, вы проиграли");
            if (world.hero.level > 1)
            {
                Console.SetCursorPosition(70, 16); Console.Write($" Ваш счет: {world.hero.experience + (world.hero.level * 100)}");
            }
            else
            {
                Console.SetCursorPosition(70, 16); Console.Write($"  Ваш счет: {world.hero.experience}");
            }

            Thread.Sleep(2500);
            Menu menu_ = new();
            menu_.Show();
            world = new World();
            world = StartGame.CreateWorld(menu_);
        }
        else
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(40, 24); Console.Write("Поздравляем, вы уничтожили великое зло! Вы - настящий воин!");
            FileStream fileStream = new FileStream("Win.txt", FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            int i = 0; ;
            // считываем строки из файла
            string line;
            // закрываем StreamReader и файловый поток
            while ((line = streamReader.ReadLine()) != null)
            {
                Console.SetCursorPosition(2, 5+i);
                Console.Write(line);
                i++;
            }
            streamReader.Close();
            fileStream.Close();
            if (world.hero.level > 1)
            {
                Console.SetCursorPosition(60, 25); Console.Write($" Ваш счет: {world.hero.experience + (world.hero.level * 100)}");
            }
            else
            {
                Console.SetCursorPosition(60, 25); Console.Write($"  Ваш счет: {world.hero.experience}");
            }
            Console.SetCursorPosition(55, 25);
            return;

        }
        
    }
}


//Создание игры и начальное выставление и отображение героя
class StartGame
{
    static public World CreateWorld(Menu menu)
    {
        World world = new World();
        world.map = world.CreateMiniMap(); //Создаю мини карту(и одновремено расположение комнат относительно друг друга)
        world.roomsMini = world.CreateArrMiniRooms(world.map); //Заполняю коллекцию мини комнат
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
        world.hero.name = menu.hero_name;

        //Выбор начальной комнаты для отрисовки из мира
        Random rand = new Random();
        int numRoom = rand.Next(0, 9);
        world.currentRoom = world.roomsReal[numRoom];
        Monsters.CreateMonsters(world.currentRoom, world);

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
            {
                PutCurs(room.map[y, x], y, x);

                //Проверка для сундука(тк такой же символ)
                if (room.chest != null)
                {
                    if (room.chest.coordinates[0] == x && room.chest.coordinates[1] == y)
                    {
                        Console.SetCursorPosition(StatX + x, StatY + y);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                        Console.Write(World.charChest);
                        Console.ResetColor();
                    }
                }
            }

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

    //Отображение предмета, который подобран
    static public void DraftChoisItem(Items item, Inventory inventory)
    {
       
        ConsoleKeyInfo keyInfo;
        while (true)
        {
            Console.Clear();
            Console.SetCursorPosition(50, 15);
            Console.Write($"Вам достался предмет: {item.name} Уровень: {item.level}");
            Console.SetCursorPosition(68, 16);
            Console.Write("Взять - 1");
            Console.SetCursorPosition(68, 17);
            Console.Write("Выбросить - 0");
            keyInfo = Console.ReadKey(true);
            switch (keyInfo.KeyChar)
            {
                case '1':
                    inventory.AcceptItem(item);
                    return;

                case '0':
                    return;
            }
        }

    }

}

//Передвижение героя(необходимо добавить проверку на наличие чего-то кроме стен и дверей)
class MovePlayer
{
    static public void Move(string trend, ref RealRoom roomCurrent, ref World world)
    {
        if (trend == "Left")
        {
            //Указываю координаты смещения и сохраняю объект, который в них находится
            int[] moveCoordinates = { world.hero.coordinates[0] - 1, world.hero.coordinates[1] };   //Указываю каково смещение
            Object? obj = world.DefiningArea(moveCoordinates, roomCurrent);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

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
                ModsterDef((Monsters)obj, roomCurrent, ref world, trend);
            }

            else if (obj is Chest)   //Проверяю принадлежит ли объект классу сундуков
            {
                ChestDef((Chest)obj, roomCurrent, ref world, trend);
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
            Object? obj = world.DefiningArea(move_coordinates, roomCurrent);

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
                ModsterDef((Monsters)obj, roomCurrent, ref world, trend);
            }

            else if (obj is Chest)   //Проверяю принадлежит ли объект классу сундуков
            {
                ChestDef((Chest)obj, roomCurrent, ref world, trend);
            }


            else
            {
                EmptyDef(roomCurrent, world, trend);
            }

        }

        if (trend == "Up")
        {
            int[] move_coordinates = { world.hero.coordinates[0], world.hero.coordinates[1] - 1 };
            Object? obj = world.DefiningArea(move_coordinates, roomCurrent);

            if (obj is Borders)
                return;


            else if (obj is Doors)
            {
                DoorDef((Doors)obj, ref roomCurrent, world, trend);
            }

            else if (obj is Monsters)
            {
                ModsterDef((Monsters)obj, roomCurrent, ref world, trend);
            }

            else if (obj is Chest)   //Проверяю принадлежит ли объект классу сундуков
            {
                ChestDef((Chest)obj, roomCurrent, ref world, trend);
            }


            else
            {
                EmptyDef(roomCurrent, world, trend);
            }
        }

        if (trend == "Down")
        {
            int[] move_coordinates = { world.hero.coordinates[0], world.hero.coordinates[1] + 1 };
            Object? obj = world.DefiningArea(move_coordinates, roomCurrent);

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
                ModsterDef((Monsters)obj, roomCurrent, ref world, trend);
            }

            else if (obj is Chest)   //Проверяю принадлежит ли объект классу сундуков
            {
                ChestDef((Chest)obj, roomCurrent, ref world, trend);
            }


            else
            {
                EmptyDef(roomCurrent, world, trend);
            }
        }
    }

    //При обнаружении монстра
    static public void ModsterDef(Monsters monster, RealRoom roomCurrent, ref World world, string move)
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
        Fight normalMonter = new();
        if (normalMonter.Start(world.hero, monster) == 1)
        {
            world.hero.countDeadMonsters++;
            roomCurrent.monsters_list.Remove(monster);
            world.hero.experience += monster.experience;
            //Проверка уровня
            world.hero.CheckAndLevelUp(world);

            //Проверка на предмет в монстре
            if(monster.item != null)
            {
                if(monster.item is StaminaPotion)
                {   
                    StaminaPotion staminaPotion = (StaminaPotion)monster.item;

                    Console.Clear();
                    Console.SetCursorPosition(50, 15);
                    Console.Write($"Вам достался предмет: {staminaPotion.name} Уровень: {staminaPotion.level}");
                    Console.SetCursorPosition(50, 16);
                    Console.Write($"Ваша выносливость увеличелась на {staminaPotion.stamina}");
                    Thread.Sleep(3000);

                    world.hero.HealSp(staminaPotion.stamina);
                }
                else if (monster.item is HealingPotion)
                {
                    HealingPotion healingPotion = (HealingPotion)monster.item;

                    Console.Clear();
                    Console.SetCursorPosition(50, 15);
                    Console.Write($"Вам достался предмет: {healingPotion.name} Уровень: {healingPotion.level}");
                    Console.SetCursorPosition(50, 16);
                    Console.Write($"Ваш уровень жизней увеличелся на {healingPotion.heal}");
                    Thread.Sleep(3000);

                    world.hero.HealHp(healingPotion.heal);

                }
                else
                {
                    DraftGame.DraftChoisItem(monster.item, world.hero.inventory);
                }
                
            }
            

            DraftGame.DraftPlane(roomCurrent, world);
        }
        else
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetCursorPosition(70, 15); Console.Write("Увы, вы проиграли");
            if (world.hero.level > 1)
            {
                Console.SetCursorPosition(70, 16); Console.Write($" Ваш счет: {world.hero.experience + (world.hero.level * 100)}");
            }
            else
            {
                Console.SetCursorPosition(70, 16); Console.Write($"  Ваш счет: {world.hero.experience}");
            }
           
            Thread.Sleep(2500);
            Menu menu = new();
            menu.Show();
            world = new World();
            world = StartGame.CreateWorld(menu);
        }

    }

    static public void ChestDef(Chest chest, RealRoom roomCurrent, ref World world, string move)
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

        Thread.Sleep(100);
        roomCurrent.map[world.hero.coordinates[1], world.hero.coordinates[0]] = World.charHero;
        DraftGame.PutCurs(World.charHero, world.hero.coordinates[1], world.hero.coordinates[0]);

        if (chest.item is StaminaPotion)
        {
            StaminaPotion staminaPotion = (StaminaPotion)chest.item;

            Console.Clear();
            Console.SetCursorPosition(50, 15);
            Console.Write($"Вам достался предмет: {staminaPotion.name} Уровень: {staminaPotion.level}");
            Console.SetCursorPosition(50, 16);
            Console.Write($"Ваша выносливость увеличелась на {staminaPotion.stamina}");
            Thread.Sleep(3000);


            world.hero.HealSp(staminaPotion.stamina);
        }
        else if (chest.item is HealingPotion)
        {
            HealingPotion healingPotion = (HealingPotion)chest.item;

            Console.Clear();
            Console.SetCursorPosition(50, 15);
            Console.Write($"Вам достался предмет: {healingPotion.name} Уровень: {healingPotion.level}");
            Console.SetCursorPosition(50, 16);
            Console.Write($"Ваш уровень жизней увеличелся на {healingPotion.heal}");
            Thread.Sleep(3000);


            world.hero.HealHp(healingPotion.heal);
        }
        else
        {
            DraftGame.DraftChoisItem(chest.item, world.hero.inventory);
        }

        roomCurrent.chest = new Chest();

        DraftGame.DraftPlane(roomCurrent, world);

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
                    Monsters.CreateMonsters(room1, world);
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


