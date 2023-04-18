//Класс который хранит все о мире


//Класс Мини комнат с разными характеристиками

using Rogulike_way;
using System;
using System.Threading;

public class Chest{
    public Items item;
    public int[] coordinates = {100, 100};
    public char designation = World.charChest;
}

public class MiniRoom
{
    public int x;
    public int y;
    public int number;
    public bool visitings;

    public bool upDoor;
    public bool downDoor;
    public bool rightDoor;
    public bool leftDoor;
    public MiniRoom(int i) { 
    
    number = i;

    visitings = false;
    upDoor = false;
    downDoor = false;
    rightDoor = false;
    leftDoor = false;
    }
}

//Класс обычной комнатф
public class RealRoom
{

    public char[,] map;
    public int number;
    public bool visitings;
    public List<Borders> borders_list = new();
    public List<Doors> doors_list = new();
    

    public List<Monsters> monsters_list = new();  //Пустая коллекция монстров
    public Chest chest;          //Пустая коллекция сундуков с объектами

    public RealRoom(int number_)
    {
        number = number_;
    }
}

//Класс стенок
public class Borders
{
    public int[] coordinates;
    public Borders()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public Borders(int x, int y) : this()
    {
        coordinates = new int[2] { x, y };
    }
}

//Класс дверей
public class Doors
{
    public int[] coordinates;
    public int room_num;   //Указывает в какую комнату ведет
    public Doors()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public Doors(int x, int y) : this()
    {
        coordinates = new int[2] { x, y };
    }
}


//Класс, где создается игровой мир
public class World
{
    static public char charRoomBordVert = '║';
    static public char charRoomBordHor = '═';
    static public char charDoors = '╬';

    static public char charMiniRoomBordVert = '│';
    static public char charMiniRoomBordHor = '─';
    static public char charMiniRoom = '#';
    static public char charMiniEmpty = ' ';

    static public char charChest = '#';

    static public char charHero = '@';

    static public char ork = 'O';
    static public char ghost = 'G';
    static public char knight = 'K';
    static public char rat = 'R';
    static public char skeleton = 'S';

    public Hero hero = new Hero();

    //Коллекции комнат и карты
    public char[,]? map;
    public RealRoom? currentRoom;
    public List<MiniRoom> roomsMini = new();
    public List<RealRoom> roomsReal = new();
    public List<Chest> roomsChests = new();

    public List<Items> Items = new();


    //Заполняю/удаляю, если должна быть дверь
    public void DelBorders(int[] coordDoor, ref RealRoom room)  
    {
        foreach (Borders borders in room.borders_list)
        {
            if (borders.coordinates[0] == coordDoor[0] && borders.coordinates[1] == coordDoor[1])
            {
                room.borders_list.Remove(borders);
                room.borders_list.Remove(borders);
                room.borders_list.Remove(borders);
                return;
            }
        }
        
    }

    ///Просто создание char[,] массива в виде карты
    //Создание рандомной мини карты
    public char[,] CreateMiniMap()
    {
        //Массив миникарты
        int xLen = 12;
        int yLen = 12;
        char[,] map = new char[yLen, xLen];

        //Начальная комната
        int x_pos = 4; int y_pos = 4;   //Позиция первой комнаты
        map[y_pos, x_pos] = charMiniRoom;

        //Количесвто комнат колеблится от 8 до 10
        Random random = new Random();
        int c_room = random.Next(9, 11); 

        //Создаем окружение у комнаты с указанными координатами
        map = CreateOkrujMini(map, x_pos, y_pos);



        //Выбирают ту комнату, которой не было и создаю ее окружение, пока количество комнат не сравняется с указанным
        while (CounterRoomsMini(map) <= c_room)
        {
            for (int x = 0; x < xLen; x++)
            {
                for (int y = 0; y < yLen; y++)
                {
                    if (map[x, y] == charMiniRoom)
                    {
                        x_pos = x; y_pos = y;

                    }
                }
            }
            map = CreateOkrujMini(map, x_pos, y_pos);
        }

        //Заполняет мини карту пустотой (по сути не нужно, но если вдруг вид пустоты изменится, то поменять его будет проще)
        for (int x = 0; x < xLen-1; x++)
        {
            for (int y = 0; y < yLen-1; y++)
            {
                if(map[x,y] != charMiniRoom)
                {
                    map[x, y] = charMiniEmpty;
                }
            }
        }



        //Границы

        map[0, 0] = '┌';
        map[0, xLen - 1] = '┐';

        map[yLen - 1, 0] = '└';
        map[yLen - 1, xLen - 1] = '┘';
        //Первая строка
        for (int x = 1; x < xLen-1; x++)
        {
            map[0, x] = charMiniRoomBordHor;
        }

        //Остальные строки
        for (int y = 1; y < yLen-1; y++)
        {
            map[y, 0] = charMiniRoomBordVert;

            for (int x = 1; x < xLen; x++)
            {
                
            }
            map[y, xLen - 1] = charMiniRoomBordVert;
        }
        //Последняя строка
        for (int x = 1; x < xLen-1; x++)
        {
            map[yLen - 1, x] = charMiniRoomBordHor;
        }

        
        return map;
    }

    //Создание комнат вокруг комнаты мини
    public char[,] CreateOkrujMini(char[,] miniMap, int y_pos, int x_pos)
    {
        Random random = new Random();
        int c_door = random.Next(1, 4);

        int[] storons = new int[4] { 2, 1, 3, 4 }; //1-up 2 right 3 down 4 left

        //Путаю очередность создания сторон
        for (int i = storons.Length - 1; i >= 0; i--)
        {
            int j = random.Next(i + 1);
            var temp = storons[j];
            storons[j] = storons[i];
            storons[i] = temp;
        }

        //В зависимости от стороны указываю направление
        for (int i = 0; i < c_door; i++)
        {
            if (storons[i] == 1)
                miniMap[y_pos - 1, x_pos] = charMiniRoom;

            if (storons[i] == 2)
                miniMap[y_pos, x_pos + 1] = charMiniRoom;


            if (storons[i] == 3)
                miniMap[y_pos + 1, x_pos] = charMiniRoom;

            if (storons[i] == 4)
                miniMap[y_pos, x_pos - 1] = charMiniRoom;
        }
        return miniMap;
    }

    //Счетчик комнат мини
    public int CounterRoomsMini(char[,] miniMap)
    {
        int n = 0;
        for (int x = 0; x < 12; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                if (miniMap[x, y] == charMiniRoom)
                {
                    n++;
                }
            }
        }
        return n;

    }

 
    ///Создание коллекции мини комнат
    //Заполняю коллекцию мини комнат объектами мини комнат
    public List<MiniRoom> CreateArrMiniRooms(char[,] miniMap)       
    {
           List<MiniRoom> roomsMini = new List<MiniRoom>();
       int i = 0;
        for (int x = 0; x < 12; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                if (miniMap[x, y] == charMiniRoom)
                {
                    //Создаю объек мини комнаты и ставлю для него значения дверей и номер
                    MiniRoom miniRoom = new MiniRoom(i);
                    if (miniMap[x, y + 1] == charMiniRoom)
                    {
                        miniRoom.rightDoor = true;
                    }
                    if (miniMap[x, y - 1] == charMiniRoom)
                    {
                        miniRoom.leftDoor = true;
                    }
                    if (miniMap[x - 1, y] == charMiniRoom)
                    {
                        miniRoom.upDoor = true;
                    }
                    if (miniMap[x + 1, y] == charMiniRoom)
                    {
                        miniRoom.downDoor = true;
                    }

                    i++;
                    miniRoom.x = y; miniRoom.y = x;

                    roomsMini.Add(miniRoom);
                }

            }
            
        }
        return roomsMini;





    }


    //Создание рандомной комнаты в реальном размере
    public RealRoom CreateRoomReal(int count_room, MiniRoom miniRoom, List<MiniRoom> roomsMini) {
        //Объект комнаты с номером идентичной маленькой комнаты
        RealRoom roomReal = new RealRoom(miniRoom.number);

        Random random = new Random();
        int x_len = random.Next(30, 50);
        int y_len = random.Next(10, 22);




        char[,] map = new char[y_len, x_len];

        //Угловые элементы
        map[0, 0] = '╔';
        map[0, x_len-1] = '╗';

        map[y_len-1, 0] = '╚';
        map[y_len-1, x_len - 1] = '╝';

        //Добовляю угловые элементы
        Borders border = new Borders(0, 0);
        roomReal.borders_list.Add(border);
        border = new Borders(0, x_len - 1);
        roomReal.borders_list.Add(border);
        border = new Borders(y_len - 1, 0);
        roomReal.borders_list.Add(border);
        border = new Borders(y_len - 1, x_len - 1);
        roomReal.borders_list.Add(border);

        //Создание границ
        //Первая строка
        for (int x = 1; x < x_len-1; x++)
        {
            map[0, x] = charRoomBordHor;
            border = new Borders(x, 0);
            roomReal.borders_list.Add(border);
        }
          
        //Остальные строки
        for (int y = 1; y < y_len-1; y++)
        {
            map[y, 0] = charRoomBordVert;
            border = new Borders(0, y);
            roomReal.borders_list.Add(border);
            for (int x = 1; x < x_len - 2; x++)
            {
                map[y, x] = ' ';
            }
            map[y, x_len-1] = charRoomBordVert;

            border = new Borders(x_len - 1, y);
            roomReal.borders_list.Add(border);

        }

        //Последняя строка
        for (int x = 1; x < x_len-1; x++)
        {
            map[y_len-1, x] = charRoomBordHor;
            border = new Borders(x, y_len - 1);
            roomReal.borders_list.Add(border);
        }

        
        //Добавляю двери
        if(miniRoom.rightDoor == true)
        {

            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { x_len - 1, y_len / 2 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor, ref roomReal);

            map[y_len / 2, x_len - 1] = charDoors;
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
            foreach(MiniRoom roomMin in roomsMini)
            {
                if(roomMin.x == miniRoom.x + 1 && roomMin.y == miniRoom.y)
                {
                    door.room_num = roomMin.number;
                    roomReal.doors_list.Add(door);
                }
            }
            
          
        }
          
        if (miniRoom.upDoor == true)
        {
           // Console.WriteLine("ww");

            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { ((x_len) / 2), 0 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor, ref roomReal);

            map[0, (x_len) / 2] = charDoors;
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
            roomReal.doors_list.Add(door);
            DelBorders(coordDoor, ref roomReal);
            foreach (MiniRoom roomMin in roomsMini)
            {
               
                if (roomMin.x == miniRoom.x && roomMin.y == miniRoom.y-1)
                {
                    door.room_num = roomMin.number;
                    roomReal.doors_list.Add(door);
                }
            }
           
        }

        if (miniRoom.downDoor == true)
        {
            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { ((x_len) / 2), y_len - 1 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor, ref roomReal);

            map[coordDoor[1], coordDoor[0]] = charDoors;
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
            foreach (MiniRoom roomMin in roomsMini)
            {
                if (roomMin.x == miniRoom.x && roomMin.y == miniRoom.y +1)
                {
                    door.room_num = roomMin.number;
                    roomReal.doors_list.Add(door);
                }
            }
            
        }

        if (miniRoom.leftDoor == true)
        {

            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { 0 , y_len / 2 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor, ref roomReal);

            map[y_len / 2, 0] = charDoors;
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
            foreach (MiniRoom roomMin in roomsMini)
            {
                if (roomMin.x == miniRoom.x -1 && roomMin.y == miniRoom.y)
                {
                    door.room_num = roomMin.number;
                    roomReal.doors_list.Add(door);
                }
            }
            
        }


        roomReal.map = map;
        return roomReal;
        
    }

    //Добавляю в сундуки предметы взависимости от уровня героя
    public void AppChest(Hero hero, RealRoom room)
    {
        List<Items> correctItems = new List<Items>();
        Items items = new();
        //Создаю подходящий
         if(hero.level == 1)
         {
             foreach(Items items1 in items.itemsArr2)
             {
                 correctItems.Add(items1);
             }
             foreach (Items items1 in items.itemsArr3)
             {
                 correctItems.Add(items1);
             }
         }

         else if (hero.level == 2)
         {
             foreach (Items items1 in items.itemsArr3)
             {
                 correctItems.Add(items1);
             }
             foreach (Items items1 in items.itemsArr4)
             {
                 correctItems.Add(items1);
             }
         }

          if (hero.level >= 3)
         {
             foreach (Items items1 in items.itemsArr5)
             {
                 correctItems.Add(items1);
             }
             foreach (Items items1 in items.itemsArr4)
             {
                 correctItems.Add(items1);
             }
         }

         //Рандомно кладу в сундук нужный
         Random random = new Random();
         room.chest.item = null;
         room.chest.item = correctItems[random.Next(0, 12)];
         
    }
    

    //Добавление предметов в монстров
    public void AppItemMonster(Hero hero, Monsters monster)
    {
        List<Items> correctItems = new List<Items>();
        Items items = new();
        //Создаю подходящий
        if (hero.level == 1)
        {

            foreach (Items items1 in items.itemsArr1)
            {
                correctItems.Add(items1);
            }
            foreach (Items items1 in items.itemsArr2)
            {
                correctItems.Add(items1);
            }
        }

        else if (hero.level == 2)
        {
            foreach (Items items1 in items.itemsArr2)
            {
                correctItems.Add(items1);
            }
            foreach (Items items1 in items.itemsArr3)
            {
                correctItems.Add(items1);
            }
        }

        else if(hero.level == 3)
        {
            foreach (Items items1 in items.itemsArr3)
            {
                correctItems.Add(items1);
            }
            foreach (Items items1 in items.itemsArr4)
            {
                correctItems.Add(items1);
            }
        }

        else if(hero.level == 4)
        {
            foreach (Items items1 in items.itemsArr4)
            {
                correctItems.Add(items1);
            }
            foreach (Items items1 in items.itemsArr5)
            {
                correctItems.Add(items1);
            }
        }

        //Рандомно кладу в сундук нужный
        Random random = new Random();

        //Проверка, чтобы 
        if(random.Next(1, 3) == 2)
        {
            if (hero.level == 1)
            {
                monster.item = correctItems[random.Next(0, 10)];
            }
        }
        if(monster.item != null && hero.level != 1)
                {
            monster.item = correctItems[random.Next(0, 10)];
        }
    }

    //Создание сундука в комнате
    public void RoomsAppendChest(List<RealRoom> roomsReal)
    {
        Random random = new Random();
        int chestCount = random.Next(4, 7); 
        while(roomsChests.Count != chestCount)
        {
            int ChanceChest = random.Next(0, roomsReal.Count);
            foreach (RealRoom room in roomsReal)
            {
                if (room.number == ChanceChest && room.chest == null)
                {
                    Chest chest = new Chest();
                    chest.coordinates = new int[] { random.Next(1, room.map.GetLength(1) - 2), random.Next(2, room.map.GetLength(0) - 2) };

                    //Проверка на наличие чего-либо
                    while (DefiningArea(chest.coordinates, room) != null)
                    {
                        chest.coordinates = new int[] { random.Next(1, room.map.GetLength(1) - 2), random.Next(2, room.map.GetLength(0) - 2) };
                    }

                    room.map[chest.coordinates[1], chest.coordinates[0]] = chest.designation;
                    room.chest = chest;
                    AppChest(hero, room);
                    roomsChests.Add(chest);
                }
            }
        }


    }

    //Заполнение коллекции комнат
    public List<RealRoom> CreateArrRealRooms(List<MiniRoom> roomsMini_)
    {
        //Коллекция обычных комнат со своими координатами
        List<RealRoom> roomsReal = new List<RealRoom>();

        //Перебирает коллекцию и создает просто карты с таким же номером
        foreach(MiniRoom miniRoom in roomsMini_)
        {
            RealRoom room = CreateRoomReal(miniRoom.number, miniRoom, roomsMini_);

            roomsReal.Add(room);
        }
        RoomsAppendChest(roomsReal);

        return roomsReal;
    }


    //Проверка объекта в координатах(изучение области) куда надо пойти
    public Object? DefiningArea(int[] coordinates_move, RealRoom room)      
   {
 
        if (hero.coordinates.SequenceEqual(coordinates_move)) //Штучка для сравнения массивов
        {
            return hero;                 //Возыращает героя
        }

        if(room.chest != null)
        {
            if (room.chest.coordinates.SequenceEqual(coordinates_move)) //Штучка для сравнения массивов
            {
                return room.chest;                 //Возыращает героя
            }
        }



        foreach (Monsters monster in room.monsters_list)
        {
           
            if (monster.coordinates.SequenceEqual(coordinates_move))
            {
                return monster;                 //Возыращает монстра на указанных координатах
            }

        }

        foreach (Borders border in room.borders_list)
        {
            if (border.coordinates.SequenceEqual(coordinates_move))
            {
                return border;                 //Возыращает монстра на указанных координатах
            }

        }

        foreach (Doors door in room.doors_list)
        {
            if (door.coordinates.SequenceEqual(coordinates_move))
            {
                return door;                 //Возыращает монстра на указанных координатах
            }
            
        }
        return null;

        
    }       
}

