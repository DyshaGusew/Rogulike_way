//Создется объек мира
using Rogulike_way;

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
Monsters Monster = new Ork(10);
Fight fight = new Fight();
int a = fight.Start(hero, Monster);
Thread.Sleep(3000);
Console.Clear();
Console.WriteLine(a);
/*
//Выбор начальной комнаты для отрисовки из мира
Random rand = new Random();
int numRoom = rand.Next(0, 9);
RealRoom currentRoom = world.roomsReal[numRoom];
Monsters.CreateMonsters(currentRoom, hero);

//Задаю герою координаты
hero.coordinates = new int[2] { currentRoom.map.GetLength(1) / 2, currentRoom.map.GetLength(0) / 2 };  //Делаю так, чтобы он был посередине комнаты


//Отрисовываю карту без героя
DraftGame.DraftPlane(currentRoom, world);

//Задежка
System.Threading.Thread.Sleep(1000);

//Указываю героя в центре координат
currentRoom.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
DraftGame.DraftPlane(currentRoom, world);                     //Отрисовываю карту уже с героем



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
        if (ch == '╬')
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;    
        }

        else if (ch == '@')
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        else if (ch == '═' || ch == '║' || ch == '╔' || ch == '╗' || ch == '╚' || ch == '╝')
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        Console.Write(ch);
        Console.ResetColor();

    }

    //Добавление символа в необходимой координате с указанным смещением
    static public void PutCursRange(char ch, int y, int x, int statX, int statY)
    {
        Console.SetCursorPosition(statX + x, statY + y);
        if (ch == '#')
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

        }

        else if (ch == '@')
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        else if (ch == '│' || ch == '─' || ch == '┌' || ch == '┐' || ch == '└' || ch == '┘')
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

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
                world.map[roomMini.y, roomMini.x] = world.charMiniRoom;
            }
            else
            {
                world.map[roomMini.y, roomMini.x] = world.charMiniEmpty;
            }

            if (roomMini.number == realRoom.number) 
            {
                for (int y = 0; y < y_len; y++)
                    for (int x = 0; x < x_len; x++)
                    {
                        if (world.map[y, x] == world.charHero)
                        {
                            world.map[y, x] = world.charMiniRoom;
                        }

                    }
                roomMini.visitings = true;
                realRoom.visitings = true;
                world.map[roomMini.y, roomMini.x] = world.charHero;
            }
        }


        //Вывожу миникарту в указанных координатах
        Console.SetCursorPosition(80 + x_len/2-5, 0);
        Console.WriteLine("Миникарта");
        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)
            {
                PutCursRange(world.map[y, x], y, x, 80, 1);
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

            else if (obj is Doors)   //Проверяю принадлежит ли объект классу дверей
            {
                //Координаты игрока меняются
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = (Doors)obj;
                foreach (RealRoom room1 in world.roomsReal)
                {
                    //Если комната из коллекции равна указываемой у текущей двери(то есть дверь ведет в нужную комнату, то текущая меняется на указываемую)
                    if (room1.number == door.room_num)
                    {
                        if (room1.visitings == false)
                        {
                            Monsters.CreateMonsters(room1, hero);
                        }
                        roomCurrent = room1;

                    }
                }

                //Координаты в середине и рядом с дверью
                hero.coordinates[0] = roomCurrent.map.GetLength(1) - 2; hero.coordinates[1] = roomCurrent.map.GetLength(0) / 2;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;

                //Отрисовываем
                DraftGame.DraftPlane(roomCurrent, world);
                return;
            }

            else if (obj is Monsters)   //Проверяю принадлежит ли объект классу дверей
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] -= 1; hero.coordinates[1] -= 0;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                DraftGame.PutCurs(world.charHero, hero.coordinates[1], hero.coordinates[0]);
                Fight gg = new Fight(hero, (Monsters)obj);

            }
            //Если простанство пустое
            else
            {
                //Меняю карту и соответсвенно меняю координаты героя
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] -= 1; hero.coordinates[1] -= 0;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                DraftGame.PutCurs(world.charHero, hero.coordinates[1], hero.coordinates[0]);
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

            
            else if (obj is Doors)   
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = (Doors)obj;
                foreach (RealRoom room1 in world.roomsReal)
                {
                    if (room1.number == door.room_num)
                    {
                        if (room1.visitings == false)
                        {
                            Monsters.CreateMonsters(room1, hero);
                        }
                        roomCurrent = room1;
                    }
                }
                hero.coordinates[0] = 1; hero.coordinates[1] = roomCurrent.map.GetLength(0) /2;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                    
                DraftGame.DraftPlane(roomCurrent, world);
                return;
            }
            else if (obj is Monsters)   //Проверяю принадлежит ли объект классу дверей
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 1; hero.coordinates[1] -= 0;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                DraftGame.PutCurs(world.charHero, hero.coordinates[1], hero.coordinates[0]);
                Fight gg = new Fight(hero, (Monsters)obj);

            }
            else
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 1; hero.coordinates[1] -= 0;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                DraftGame.PutCurs(world.charHero, hero.coordinates[1], hero.coordinates[0]);
            }
            
        }

        if(trend == "Up")
        {
            int[] move_coordinates = { hero.coordinates[0], hero.coordinates[1]-1};   
            Object obj = world.DefiningArea(move_coordinates, roomCurrent); 

            if (obj is Borders)  
                return;


            else if (obj is Doors)  
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = (Doors) obj;
                foreach (RealRoom room1 in world.roomsReal)
                {
                    if (room1.number == door.room_num)
                    {
                        if (room1.visitings == false)
                        {
                            Monsters.CreateMonsters(room1, hero);
                        }
                        roomCurrent = room1;
                    }
                }
                hero.coordinates[0] = roomCurrent.map.GetLength(1) / 2; hero.coordinates[1] = roomCurrent.map.GetLength(0) - 2;              
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;

                DraftGame.DraftPlane(roomCurrent, world);
                return;
            }
            else if (obj is Monsters)   //Проверяю принадлежит ли объект классу дверей
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 0; hero.coordinates[1] -= 1;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                DraftGame.PutCurs(world.charHero, hero.coordinates[1], hero.coordinates[0]);
                Fight gg = new Fight(hero, (Monsters)obj);

            }
            else
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 0; hero.coordinates[1] -= 1;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                DraftGame.PutCurs(world.charHero, hero.coordinates[1], hero.coordinates[0]);
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
                        if (room1.visitings == false)
                        {
                            Monsters.CreateMonsters(room1, hero);
                        }
                        roomCurrent = room1;
                    }
                }
                hero.coordinates[0] = (roomCurrent.map.GetLength(1) / 2); hero.coordinates[1] = 1;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;

                DraftGame.DraftPlane(roomCurrent, world);
                return;
            }
            else if (obj is Monsters)   //Проверяю принадлежит ли объект классу дверей
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 0; hero.coordinates[1] += 1;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                DraftGame.PutCurs(world.charHero, hero.coordinates[1], hero.coordinates[0]);
                Fight gg = new Fight(hero, (Monsters)obj);

            }
            else
            {
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                DraftGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 0; hero.coordinates[1] += 1;
                roomCurrent.map[hero.coordinates[1], hero.coordinates[0]] = world.charHero;
                DraftGame.PutCurs(world.charHero, hero.coordinates[1], hero.coordinates[0]);
            }
        }
    }
}

*/
