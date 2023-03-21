//Класс который хранит все о мире
using Microsoft.VisualBasic;

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

    //Создание рандомной комнаты
    public char[,] CreateMap() {
        Random random = new Random();
        int x_len = random.Next(10, 50);
        int y_len = random.Next(10, 25);

        char[,] map = new char[y_len, x_len];

        //Первая строка
        for (int x = 0; x < x_len; x++)
        {
            map[0, x] = '#';
            Borders border = new Borders(x, 0);
            AppBorders(border);
        }
          
        //Остальные строки
        for (int y = 1; y < y_len-1; y++)
        {
            map[y, 0] = '#';
            Borders border = new Borders(0, y);
            AppBorders(border);
            for (int x = 1; x < x_len - 2; x++)
            {
                map[y, x] = ' ';
            }
            map[y, x_len-1] = '#';

            border = new Borders(x_len - 1, y);
            AppBorders(border);

        }

        //Последняя строка
        for (int x = 0; x < x_len; x++)
        {
            map[y_len - 1, x] = '#';
            Borders border = new Borders(x, y_len - 1);
            AppBorders(border);
        }
            

        return map;
    }
      
    
    

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
        return 0;

        
    }
        
    }

