//Класс который хранит все о мире
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

//Класс Мини комнат с разными характеристиками
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
    public List<Borders> borders_list = new List<Borders>();
    public List<Doors> doors_list = new List<Doors>();

    public RealRoom()
    {
        number = 1;
    }
    public RealRoom(int number_)
    {
        number = number_;
    }

    public RealRoom(char[,] map_)
    {
        map = map_;
    }

    public RealRoom(int number_, char[,] map_)
    {
        number = number_;
        map = map_;
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

    //Работа с монстрами и объектами, добавление их в коллекции
    public Monsters monster;
    public Items item;

    public List<Monsters> monsters_list = new List<Monsters>();  //Пустая коллекция монстров
    public List<Items> items_list = new List<Items>();                  //Пустая коллекция объектов
    




    public void AppMonsters(Monsters monster, bool life)  //Заполняю/удаляю, если передали монстра
    {
        if (life == true)
        {
            monsters_list.Add(monster);
        }
        else { monsters_list.Remove(monster); }
    }

    public void AppItems(Items item, bool exist)    //Заполняю/удаляю, если передали предмет
    {
        if (exist == true)
        {
            items_list.Add(item);
        }
        else { items_list.Remove(item); }
    }

    

    public void DelBorders(int[] coordDoor, RealRoom room)  //Заполняю/удаляю, если должна быть дверь
    {
        Borders border = (Borders)DefiningArea(coordDoor, room);
        room.borders_list.Remove(border);
    }




    ///Просто создание char[,] массива в виде карты
    //Создание рандомной мини карты
    public char[,] CreateMiniMap()
    {
        //Начальная комната
        int x_pos = 3; int y_pos = 3;
        char[,] map = new char[10, 10];
        map[y_pos, x_pos] = '#';

        Random random = new Random();
        int c_room = random.Next(7, 10); //Количесвто комнат колеблится от 7 до 10

        map = CreateOkrujMini(map, x_pos, y_pos);

        //Выбирают ту комнату, которой не было и создаю ее окружение, пока количество комнат не сравняется с указанным
        while (CounterRoomsMini(map) <= c_room)
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (map[x, y] == '#')
                    {
                        x_pos = x; y_pos = y;

                    }
                }
            }
            map = CreateOkrujMini(map, x_pos, y_pos);
        }

        //Заполняет мини карту пустотой
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if(map[x,y] != '#')
                {
                    map[x, y] = '.';
                }
            }
        }
        
        return map;
    }

    //Создание комнат вокруг комнаты мини
    public char[,] CreateOkrujMini(char[,] miniMap, int y_pos, int x_pos)
    {

        Random random = new Random();
        int c_door = random.Next(1, 3);

        int[] storons = new int[4] { 1, 2, 3, 4 }; //1-up 2 right 3 down 4 left

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
                miniMap[y_pos - 1, x_pos] = '#';



            if (storons[i] == 2)
                miniMap[y_pos, x_pos + 1] = '#';


            if (storons[i] == 3)
                miniMap[y_pos + 1, x_pos] = '#';

            if (storons[i] == 4)
                miniMap[y_pos, x_pos - 1] = '#';
        }
        return miniMap;
    }

    //Счетчик комнат мини
    public int CounterRoomsMini(char[,] miniMap)
    {
        int n = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (miniMap[x, y] == '#')
                {
                    n++;
                }
            }
        }
        return n;

    }

 
    ///Создание коллекции мини комнат
    //Заполняю коллекцию мини комнат объектами мини комнат
    public List<MiniRoom> AppArrMiniRooms(char[,] miniMap)       
    {
           List<MiniRoom> roomsMini = new List<MiniRoom>();
       int i = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (miniMap[x, y] == '#')
                {
                    //Создаю объек мини комнаты и ставлю для него значения дверей и номер
                    MiniRoom miniRoom = new MiniRoom(i);
                    if (miniMap[x, y + 1] == '#')
                    {
                        miniRoom.rightDoor = true;
                    }
                    if (miniMap[x, y - 1] == '#')
                    {
                        miniRoom.leftDoor = true;
                    }
                    if (miniMap[x - 1, y] == '#')
                    {
                        miniRoom.upDoor = true;
                    }
                    if (miniMap[x + 1, y] == '#')
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


        //Первая строка
        for (int x = 1; x < x_len-1; x++)
        {
            map[0, x] = '═';
            border = new Borders(x, 0);
            roomReal.borders_list.Add(border);
        }
          
        //Остальные строки
        for (int y = 1; y < y_len-1; y++)
        {
            map[y, 0] = '║';
            border = new Borders(0, y);
            roomReal.borders_list.Add(border);
            for (int x = 1; x < x_len - 2; x++)
            {
                map[y, x] = ' ';
            }
            map[y, x_len-1] = '║';

            border = new Borders(x_len - 1, y);
            roomReal.borders_list.Add(border);

        }

        //Последняя строка
        for (int x = 1; x < x_len-1; x++)
        {
            map[y_len-1, x] = '═';
            border = new Borders(x, y_len - 1);
            roomReal.borders_list.Add(border);
        }

        
        //Добовляю двери
        if(miniRoom.rightDoor == true)
        {

            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { x_len - 1, y_len / 2 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor, roomReal);

            map[y_len / 2, x_len - 1] = '╬';
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
            int[] coordDoor = new int[] { (x_len) / 2, 0 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor, roomReal);
            map[0, (x_len) / 2] = '╬';
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
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
            //Console.WriteLine("ss");


            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { ((x_len) / 2), y_len - 1 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor, roomReal);

            map[coordDoor[1], coordDoor[0]] = '╬';
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
            DelBorders(coordDoor, roomReal);

            map[y_len / 2, 0] = '╬';
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
        return roomsReal;
    }


    public Object DefiningArea(int[] coordinates_move, RealRoom room)      //Проверка объекта в координатах(изучение области) куда надо пойти
   {

        foreach(Items item in items_list)
        {
            if (item.coordinates.SequenceEqual(coordinates_move)) //Штучка для сравнения массивов
              {                       
                  return item;                 //Возыращает предмет на указанных координатах
              }
            
        }

        foreach (Monsters monster in monsters_list)
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
        return 0;

        
    }
        
    }

