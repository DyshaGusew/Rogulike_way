//Класс который хранит все о мире
using Microsoft.VisualBasic;
using System;

public class MiniRooms
{
    public int x;
    public int y;
    public int number_inputs;
    public bool visitings;

    public bool upDoor;
    public bool downDoor;
    public bool rightDoor;
    public bool leftDoor;
    public MiniRooms() { 
    
    number_inputs = 1;

    visitings = false;
    upDoor = false;
    downDoor = false;
    rightDoor = false;
    leftDoor = false;
    }
}

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
public class World
{
    //Работа с координатами игрока
    int[] coordinates_player;

    //Дефолтные координаты
    public World()
    {
        coordinates_player = new int[2] { 0, 0 };
    }
    //Указанные координаты
    public World(int x, int y) : this()
    {
        coordinates_player = new int[2] { x, y };
    }
    //Вывод координат игрока
    public int[] GetXY()
    {
        return coordinates_player;
    }


    //Работа с монстрами и объектами, добавление их в коллекции
    public Monsters monster;
    public Items item;

    public List<Monsters> monsters_list = new List<Monsters>();  //Пустая коллекция монстров
    public List<Items> items_list = new List<Items>();                  //Пустая коллекция объектов
    public List<Borders> borders_list = new List<Borders>();
    public List<Doors> doors_list = new List<Doors>();

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

    public void AppBorders(Borders border)  //Заполняю/удаляю, если передали стенку
    {
            borders_list.Add(border);
    }

    public void DelBorders(int[] coordDoor)  //Заполняю/удаляю, если передали стенку
    {
        Borders border = (Borders)DefiningArea(coordDoor);
        borders_list.Remove(border);
    }

    public void AppDoors(Doors door)  //Заполняю/удаляю, если передали дверь
    {
        doors_list.Add(door);
    }


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

        //Выбирают ту комнату, которой не было
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
        CreateArrRooms(map);
        return map;
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


    //Коллекция мини комнат со своими координатами
    public List<MiniRooms> roomsMini = new List<MiniRooms>();
    public void CreateArrRooms(char[,] miniMap)
    {
        int i = 0;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (miniMap[x, y] == '#')
                {
                    MiniRooms miniRoom = new MiniRooms();
                    if (miniMap[x, y+1] == '#')
                    {
                        miniRoom.rightDoor = true;
                    }
                    if (miniMap[x, y-1] == '#')
                    {
                        miniRoom.leftDoor = true;
                    }
                    if (miniMap[x-1, y] == '#')
                    {
                        miniRoom.upDoor = true;
                    }
                    if (miniMap[x+1, y] == '#')
                    {
                        miniRoom.downDoor = true;
                    }
                    
                    miniRoom.x = y; miniRoom.y = x;
                    

                    roomsMini.Add(miniRoom);
                }
                
            }
        }
        

    }




    //Создание рандомной комнаты в реальном размере
    public char[,] CreateRoomReal(int count_room) {
        MiniRooms miniRoom = roomsMini[count_room];

        Random random = new Random();

        int x_len = random.Next(30, 55);
        int y_len = random.Next(10, 30);




        char[,] map = new char[y_len, x_len];

        //Угловые элементы
        map[0, 0] = '╔';
        map[0, x_len-1] = '╗';

        map[y_len-1, 0] = '╚';
        map[y_len-1, x_len - 1] = '╝';

        //Добовляю угловые элементы
        Borders border = new Borders(0, 0);
        AppBorders(border);
        border = new Borders(0, x_len - 1);
        AppBorders(border);
        border = new Borders(y_len - 1, 0);
        AppBorders(border);
        border = new Borders(y_len - 1, x_len - 1);
        AppBorders(border);


        //Первая строка
        for (int x = 1; x < x_len-1; x++)
        {
            map[0, x] = '═';
            border = new Borders(x, 0);
            AppBorders(border);
        }
          
        //Остальные строки
        for (int y = 1; y < y_len-1; y++)
        {
            map[y, 0] = '║';
            border = new Borders(0, y);
            AppBorders(border);
            for (int x = 1; x < x_len - 2; x++)
            {
                map[y, x] = ' ';
            }
            map[y, x_len-1] = '║';

            border = new Borders(x_len - 1, y);
            AppBorders(border);

        }

        //Последняя строка
        for (int x = 1; x < x_len-1; x++)
        {
            map[y_len-1, x] = '═';
            border = new Borders(x, y_len - 1);
            AppBorders(border);
        }


        //Добовляю двери
        if(miniRoom.rightDoor == true)
        {
            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { x_len - 1, y_len / 2 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor);

            map[y_len / 2, x_len - 1] = '╬';
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
            AppDoors(door);
        }
       
        
        if (miniRoom.upDoor == true)
        {
            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { (x_len - 1) / 2, 0 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor);

            map[0, (x_len - 1) / 2] = '╬';
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
            AppDoors(door);
        }
        if (miniRoom.downDoor == true)
        {
            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { x_len / 2, y_len - 1 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor);

            map[y_len - 1, x_len/2] = '╬';
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
            AppDoors(door);
        }

        if (miniRoom.leftDoor == true)
        {
            //Указываю серединчатые координаты дверей
            int[] coordDoor = new int[] { 0 , y_len / 2 };

            //Удаляю стену на месте двери
            DelBorders(coordDoor);

            map[y_len / 2, 0] = '╬';
            Doors door = new Doors(coordDoor[0], coordDoor[1]);
            AppDoors(door);
        }
        return map;
    }

    //Добавление дверей в реальную комнату
    //public char[,] AppendDoors(char[,] miniMap, char[,] map)
    //{

   //}


    public Object DefiningArea(int[] coordinates_move)      //Проверка объекта в координатах(изучение области) куда надо пойти
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

        foreach (Borders border in borders_list)
        {
            if (border.coordinates.SequenceEqual(coordinates_move))
            {
                return border;                 //Возыращает монстра на указанных координатах
            }

        }

        foreach (Doors door in doors_list)
        {
            if (door.coordinates.SequenceEqual(coordinates_move))
            {
                return door;                 //Возыращает монстра на указанных координатах
            }

        }
        return 0;

        
    }
        
    }

