//Класс который хранит все о мире
public class World
{
    //Работа с координатами игрока
    int[] coordinates;

    public World()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public World(int x, int y) : this()
    {
        coordinates = new int[2] { x, y };
    }
    public int[] GetXY()      //Вывод координат игрока
    {
        return coordinates;
    }


    //Работа с монстрами и объектами
    public Monsters monster;
    public Items item;

    public List<Monsters> monsters_list = new List<Monsters>();  //Пустая коллекция монстров
    List<Items> items_list = new List<Items>();     //Пустая коллекция объектов

    public World(Monsters monster, bool life) : this()   //Заполняю/удаляю, если передали монстра
    {
        if (life == true)
        {
            monsters_list.Add(monster);
        }
        else { monsters_list.Remove(monster); }
    }

    public World(Items item, bool exist) : this()   //Заполняю/удаляю, если передали предмет
    {
        if (exist == true)
        {
            items_list.Add(item);
        }
        else { items_list.Remove(item); }
    }


    //public string DefiningArea(int[] coordinates)      //Проверка объекта в координатах(изучение области) куда надо пойти
   //{

   
   // }
}

public class Monsters
{
    int[] coordinates;
    public Monsters()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public Monsters(int x, int y) : this()
    {
        coordinates = new int[2] { x, y };
    }
}

