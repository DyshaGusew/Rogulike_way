//Создется объек мира
World world = new World();

world.map = world.CreateMiniMap(); //Создаю мини карту(и одновремено расположение комнат относительно друг друга)
world.roomsMini = world.AppArrMiniRooms(world.map); //Заполняю коллекцию мини комнат
world.roomsReal = world.CreateArrRealRooms(world.roomsMini); //Создание коллекции реальных комнат 

Console.CursorVisible = false;    //Отключение курсора
// Создается меню, идет ожидание выбора персонажа
Menu menu = new Menu();
menu.Show();

//Создание героя (создается в зависимости от выбора в меню)
Hero hero = new Hero();
if (menu.hero_class == "wizard")
{
    hero = new Wizard();
} 
else if (menu.hero_class == "barbarian")
{
    hero = new Barbarian();
} 
else if (menu.hero_class == "prowler")
{
    hero = new Prowler();
}


//Выбор начальной комнаты для отрисовки из мира
Random rand = new Random();
int numRoom = rand.Next(0, 9);
RealRoom currentRoom = world.roomsReal[numRoom];

//Задаю герою координаты
hero.coordinates = new int[2] { currentRoom.map.GetLength(1) / 2, currentRoom.map.GetLength(0) / 2 };  //Делаю так, чтобы он был посередине комнаты


//Отрисовываю карту без героя
DraftGame.DraftPlane(currentRoom, world.roomsMini, world.map);

//Задежка
System.Threading.Thread.Sleep(500);

//Указываю героя в центре координат
currentRoom.map[hero.coordinates[1], hero.coordinates[0]] = '@';
DraftGame.DraftPlane(currentRoom, world.roomsMini, world.map);                     //Отрисовываю карту уже с героем



//Обработка нажатий
ConsoleKeyInfo keyInfo;
do
{
    keyInfo = Console.ReadKey(true);
    if(keyInfo.KeyChar == 'w' || keyInfo.KeyChar == 'ц')
        MovePlayer.Move("Up", ref currentRoom, world, hero);
   
    else if(keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'ы')
        MovePlayer.Move("Down", ref currentRoom, world, hero);

    else if (keyInfo.KeyChar == 'd' || keyInfo.KeyChar == 'в')
        MovePlayer.Move("Right", ref currentRoom, world, hero);

    else if (keyInfo.KeyChar == 'a' || keyInfo.KeyChar == 'ф')
        MovePlayer.Move("Left", ref currentRoom, world, hero);

} while (keyInfo.KeyChar != 'q' && keyInfo.KeyChar != 'й');


//Отрисовка игры(необходимо добавить отрисовку статистики персонажа и игровых событий)
class DraftGame
{
    static public int StatX = 20, StatY = 5;  //Смещение карты

    
    //Добавление символа в необходимой координате с дефолтным смещением
    static public void PutCurs(char ch, int y, int x)
    {
        Console.SetCursorPosition(StatX + x, StatY + y);
        Console.Write(ch);
    }

    //Добавление символа в необходимой координате с указанным смещением
    static public void PutCursRange(char ch, int y, int x, int statX, int statY)
    {
        Console.SetCursorPosition(statX + x, statY + y);
        Console.Write(ch);
    }


    //Отрисовка Мини карты и комнаты
    static public void DraftPlane(RealRoom room, List<MiniRoom> roomsMini, char[,] map)
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
        DraftMinyMap(room, roomsMini, map);    
    }

    //Отрисовка миникарты с текущим положением героя
    static public void DraftMinyMap(RealRoom realRoom, List<MiniRoom> roomsMini, char[,] map)
    //Функция миникарты принимает 3 аргумента т.к ей нужно знать(где герой, текущую миникомнату, миникарту)
    {
        //Определение размерности
        int x_len = map.GetLength(1);
        int y_len = map.GetLength(0);

        //Проверка наличия посещения в комнатае для отобрадения или убирания комнаты, в которой не было игрока
        foreach (MiniRoom roomMini in roomsMini)
        {
            
        }

        //Когда номер миникомнаты совпадает с текущей реальной, то миникарта отображает в текущей миникомнате @
        foreach (MiniRoom roomMini in roomsMini)
        {
            if (roomMini.visitings == true)
            {
                map[roomMini.y, roomMini.x] = '#';
            }
            else
            {
                map[roomMini.y, roomMini.x] = '.';
            }

            if (roomMini.number == realRoom.number) 
            {
                for (int y = 0; y < y_len; y++)
                    for (int x = 0; x < x_len; x++)
                    {
                        if (map[y, x] == '@')
                        {
                            map[y, x] = '#';
                        }

                    }
                roomMini.visitings = true;
                map[roomMini.y, roomMini.x] = '@'; 
            }
        }


        //Вывожу миникарту в указанных координатах
        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)
            {
                PutCursRange(map[y, x], y, x, 80, 0);
            }
    }
}


//Передвижение героя(необходимо добавить проверку на наличие чего-то кроме стен и дверей)
class MovePlayer
{
    static public void Move(string trend, ref RealRoom roomCurrent, World world, Hero hero)
    {
        if (trend == "Left")
        {
            //Указываю координаты смещения и сохраняю объект, который в них находится
            int[] moveCoordinates = { hero.coordinates[0] - 1, hero.coordinates[1]};   //Указываю каково смещение
            Object obj = world.DefiningArea(moveCoordinates, roomCurrent);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо
            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            else if (obj is Doors doors)   //Проверяю принадлежит ли объект классу дверей
            {
                //Координаты игрока меняются
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = doors;
                foreach(RealRoom room1 in world.roomsReal)
                {
                    //Если комната из коллекции равна указываемой у текущей двери(то есть дверь ведет в нужную комнату, то текущая меняется на указываемую)
                    if(room1.number == door.room_num)
                    {
                        roomCurrent = room1;                  
                    }
                }

                //Координаты в середине и рядом с дверью
                hero.coordinates[0] = roomCurrent.map.GetLength(1)-2; hero.coordinates[1] = roomCurrent.map.GetLength(0) / 2;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = '@';

                //Отрисовываем
                DraftGame.DraftPlane(roomCurrent, world.roomsMini, world.map);
                return;
            }

            //Если простанство пустое
            else
            {
                //Меняю карту и соответсвенно меняю координаты героя
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] -= 1; hero.coordinates[1] -= 0;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = '@';
                DraftGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
            }


            
        }

        //Остальное работает подобно верхнему
        if(trend == "Right")
        {
            int[] move_coordinates = { hero.coordinates[0] +1, hero.coordinates[1] };   
            Object obj = world.DefiningArea(move_coordinates, roomCurrent); 
           
            if (obj is Borders)  
            {
                return;
            }

            
            else if (obj is Doors doors)   
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = doors;
                foreach (RealRoom room1 in world.roomsReal)
                {
                    if (room1.number == door.room_num)
                    {
                        roomCurrent = room1;
                    }
                }
                hero.coordinates[0] = 1; hero.coordinates[1] = roomCurrent.map.GetLength(0) /2;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = '@';

                DraftGame.DraftPlane(roomCurrent, world.roomsMini, world.map);
                return;
            }

            else
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 1; hero.coordinates[1] -= 0;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = '@';
                DraftGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
            }
            
        }

        if(trend == "Up")
        {
            int[] move_coordinates = { hero.coordinates[0], hero.coordinates[1]-1};   
            Object obj = world.DefiningArea(move_coordinates, roomCurrent); 

            if (obj is Borders)  
            {
                return;
            }

            else if (obj is Doors doors)  
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = doors;
                foreach (RealRoom room1 in world.roomsReal)
                {
                    if (room1.number == door.room_num)
                    {
                        roomCurrent = room1;
                    }
                }
                hero.coordinates[0] = roomCurrent.map.GetLength(1) / 2; hero.coordinates[1] = roomCurrent.map.GetLength(0) - 2;              
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = '@';

                DraftGame.DraftPlane(roomCurrent, world.roomsMini, world.map);
                return;
            }

            else
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 0; hero.coordinates[1] -= 1;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = '@';
                DraftGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
            }
        }

        if(trend == "Down")
        {
            int[] move_coordinates = { hero.coordinates[0], hero.coordinates[1]+1};   
            Object obj = world.DefiningArea(move_coordinates, roomCurrent);  

            if (obj is Borders)   
            {
                return;
            }
            else if (obj is Doors)   
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = (Doors)obj;
                foreach (RealRoom room1 in world.roomsReal)
                {
                    if (room1.number == door.room_num)
                    {
                        roomCurrent = room1;
                    }
                }
                hero.coordinates[0] = (roomCurrent.map.GetLength(1) / 2); hero.coordinates[1] = 1;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = '@';

                DraftGame.DraftPlane(roomCurrent, world.roomsMini, world.map);
                return;
            }

            else
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 0; hero.coordinates[1] += 1;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = '@';
                DraftGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
            }
        }
    }
}


