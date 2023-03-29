//Игровой движок в котором все и происходит

//Создется объек мира
World world = new World();

//Console.SetWindowSize(500, 500);
// Создается меню, идет ожидание выбора персонажа
Menu menu = new Menu();
menu.Show();

//Отображение наччала игры
Console.WriteLine("GameStart");

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

//Пример создания монстра
Monsters Goblin = new Ghost(1);
//Fight fight = new Fight(Hero, Goblin);


//Создаю мини карту(и одновремено при пмощи нее делаю взаимодействия между комнатами)
char[,] miniMap = world.CreateMiniMap();

//Заполняю коллекцию мини комнат
List<MiniRoom> roomsMini = world.AppArrMiniRooms(miniMap);



//Создание всех комнат

List<RealRoom> roomsReal = world.CreateArrRealRooms(roomsMini);

RealRoom Room = roomsReal[0];

//Только для первой отрисовки
char[,] RoomMap = roomsReal[0].map;

//задаю герою координаты
int[] coordinates_hero = { RoomMap.GetLength(1) / 2, RoomMap.GetLength(0) / 2 };   //Делаю так, чтобы он был посередине
hero.coordinates = coordinates_hero;


//Отрисовываю карту без героя
PaintGame.DraftCart(RoomMap);
PaintGame.DraftMinyMap(Room, roomsMini, miniMap, 80, 0);
         //Задежка


//Указываю героя в центре координат
RoomMap[hero.coordinates[1], hero.coordinates[0]] = '@';
PaintGame.DraftCart(RoomMap);                     //Отрисовываю карту уже с героем
PaintGame.DraftMinyMap(Room, roomsMini, miniMap, 80, 0);


Console.CursorVisible = false;    //Отключение курсора
ConsoleKeyInfo keyInfo;
do
{
    keyInfo = Console.ReadKey(true);
    if(keyInfo.KeyChar == 'w' || keyInfo.KeyChar == 'ц')
        MovePlayer.Move("Up", ref Room, world, hero, roomsReal, miniMap, roomsMini);
   
    else if(keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'ы')
        MovePlayer.Move("Down", ref Room, world, hero, roomsReal, miniMap, roomsMini);

    else if (keyInfo.KeyChar == 'd' || keyInfo.KeyChar == 'в')
        MovePlayer.Move("Right", ref Room, world, hero, roomsReal, miniMap, roomsMini);

    else if (keyInfo.KeyChar == 'a' || keyInfo.KeyChar == 'ф')
        MovePlayer.Move("Left", ref Room, world, hero, roomsReal, miniMap, roomsMini);

} while (keyInfo.KeyChar != 'q' && keyInfo.KeyChar != 'й');


//Отрисовка игры(необходимо добавить отрисовку статистики персонажа и игровых событий)
class PaintGame
{
    static public int StatX = 20, StatY = 5;  //Смещение карты

    //Отрисовка указанной карты
    static public void DraftCart(char[,] map) //Рисует первую комнату по заготовке
    {
        int x_len = map.GetLength(1);
        int y_len = map.GetLength(0);
       
        Console.Clear();
        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)         
                PutCurs(map[y, x], y, x);
        
    }

    //Добавление символа в необходимой координате
    static public void PutCurs(char ch, int y, int x)
    {
        Console.SetCursorPosition(StatX + x, StatY + y);
        Console.Write(ch);
    }

    static public void PutCursRange(char ch, int y, int x, int statX, int statY)
    {
        Console.SetCursorPosition(statX + x, statY + y);
        Console.Write(ch);
    }

    //Для миникарты
    static public void DraftMinyMap(RealRoom realRoom, List<MiniRoom> roomsMini, char[,] map, int statX, int statY) //Рисует первую комнату по заготовке
    {
        int x_save = 0, y_save = 0;
        int x_len = map.GetLength(1);
        int y_len = map.GetLength(0);
        foreach (MiniRoom roomMini in roomsMini)
        {
            if(roomMini.number == realRoom.number) 
            {
                for (int y = 0; y < y_len; y++)
                    for (int x = 0; x < x_len; x++)
                    {
                        if (map[y, x] == '@')
                        {
                            map[y, x] = '#';
                        }

                    }
                map[roomMini.y, roomMini.x] = '@'; 
            }
        }


        

        
        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)
            {
                PutCursRange(map[y, x], y, x, statX, statY);

            }
        


    }
}


//Передвижение героя(необходимо добавить проверку на наличие чего-то кроме стен и дверей)
class MovePlayer
{
    static public void Move(string trend, ref RealRoom room, World world, Hero hero, List<RealRoom> roomsReal, char[,] miniMap, List<MiniRoom> roomsMini)
    {

        if (trend == "Left")
        {
            int[] move_coordinates = { hero.coordinates[0] - 1, hero.coordinates[1]};   //Указываю каково смещение
            Object obj = world.DefiningArea(move_coordinates, room);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо(пока только стены)
            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            else if (obj is Doors)   //Проверяю принадлежит ли объект классу стен
            {
                room.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = (Doors) obj;
                foreach(RealRoom room1 in roomsReal)
                {
                    //Если комната из коллекции равна указываемой у текущей двери
                    if(room1.number == door.room_num)
                    {
                        room = room1;
                    }
                }

                hero.coordinates[0] = room.map.GetLength(1)-2; hero.coordinates[1] = room.map.GetLength(0) / 2;
                room.map[hero.coordinates[1], hero.coordinates[0]] = '@';

                PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
                PaintGame.DraftCart(room.map);
                PaintGame.DraftMinyMap(room, roomsMini, miniMap, 80, 0);

                return;
            }

            else
            {
                //Меняю карту и соответсвенно меняю координаты героя
                room.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                PaintGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] -= 1; hero.coordinates[1] -= 0;
                room.map[hero.coordinates[1], hero.coordinates[0]] = '@';
                PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
            }


            
        }

        if(trend == "Right")
        {
            int[] move_coordinates = { hero.coordinates[0] +1, hero.coordinates[1] };   //Указываю каково смещение
            Object obj = world.DefiningArea(move_coordinates, room);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо(пока только стены)
            //Если стена, то не двигаюсь

           
            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            
            else if (obj is Doors)   //Проверяю принадлежит ли объект классу стен
            {
                room.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = (Doors)obj;
                foreach (RealRoom room1 in roomsReal)
                {
                    //Если комната из коллекции равна указываемой у текущей двери
                    if (room1.number == door.room_num)
                    {
                        room = room1;
                    }
                }
                hero.coordinates[0] = 1; hero.coordinates[1] = room.map.GetLength(0) /2;
                room.map[hero.coordinates[1], hero.coordinates[0]] = '@';


                PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
                PaintGame.DraftCart(room.map);
                PaintGame.DraftMinyMap(room, roomsMini, miniMap, 80, 0);


                return;
            }

            else
            {
                room.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                PaintGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 1; hero.coordinates[1] -= 0;
                room.map[hero.coordinates[1], hero.coordinates[0]] = '@';
                PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
            }
            
        }

        if(trend == "Up")
        {
            int[] move_coordinates = { hero.coordinates[0], hero.coordinates[1]-1};   //Указываю каково смещение
            Object obj = world.DefiningArea(move_coordinates, room);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо(пока только стены)
            //Если стена, то не двигаюсь


            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            else if (obj is Doors)   //Проверяю принадлежит ли объект классу стен
            {
                room.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = (Doors)obj;
                foreach (RealRoom room1 in roomsReal)
                {
                    //Если комната из коллекции равна указываемой у текущей двери
                    if (room1.number == door.room_num)
                    {
                        room = room1;
                    }
                }
                hero.coordinates[0] = room.map.GetLength(1) / 2; hero.coordinates[1] = room.map.GetLength(0) - 2;
               
                room.map[hero.coordinates[1], hero.coordinates[0]] = '@';

 
                PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
                PaintGame.DraftCart(room.map);
                PaintGame.DraftMinyMap(room, roomsMini, miniMap, 80, 0);

                return;
            }

            else
            {
                room.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                PaintGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 0; hero.coordinates[1] -= 1;
                room.map[hero.coordinates[1], hero.coordinates[0]] = '@';
                PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
            }
        }
        if(trend == "Down")
        {
            int[] move_coordinates = { hero.coordinates[0], hero.coordinates[1]+1};   //Указываю каково смещение
            Object obj = world.DefiningArea(move_coordinates, room);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо(пока только стены)
            //Если стена, то не двигаюсь


            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }
            else if (obj is Doors)   //Проверяю принадлежит ли объект классу стен
            {
                room.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                Doors door = (Doors)obj;
                foreach (RealRoom room1 in roomsReal)
                {
                    //Если комната из коллекции равна указываемой у текущей двери
                    if (room1.number == door.room_num)
                    {
                        room = room1;
                    }
                }
                hero.coordinates[0] = (room.map.GetLength(1) / 2); hero.coordinates[1] = 1;
                room.map[hero.coordinates[1], hero.coordinates[0]] = '@';

                PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
                PaintGame.DraftCart(room.map);
                PaintGame.DraftMinyMap(room, roomsMini, miniMap, 80, 0);


                return;
            }

            else
            {
                room.map[hero.coordinates[1], hero.coordinates[0]] = ' ';
                PaintGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
                hero.coordinates[0] += 0; hero.coordinates[1] += 1;
                room.map[hero.coordinates[1], hero.coordinates[0]] = '@';
                PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
            }
        }
    }
}


