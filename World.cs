//Класс который хранит все о мире
using Microsoft.VisualBasic;

public class World
{
    //Работа с координатами игрока
    int[] coordinates_player;

    public World()
    {
        coordinates_player = new int[2] { 0, 0 };
    }

    public World(int x, int y) : this()
    {
        coordinates_player = new int[2] { x, y };
    }
    public int[] GetXY()      //Вывод координат игрока
    {
        return coordinates_player;
    }


    //Работа с монстрами и объектами
    public Monsters monster;
    public Items item;

    public List<Monsters> monsters_list = new List<Monsters>();  //Пустая коллекция монстров
    List<Items> items_list = new List<Items>();     //Пустая коллекция объектов

    public void AppMonsters(Monsters monster, bool life)  //Заполняю/удаляю, если передали монстра
    {
        if (life == true)
        {
            monsters_list.Add(monster);
        }
        else { monsters_list.Remove(monster); }
    }

    public void AppItems(Items item, bool exist) //Заполняю/удаляю, если передали предмет
    {
        if (exist == true)
        {
            items_list.Add(item);
        }
        else { items_list.Remove(item); }
    }


    public Object DefiningArea(int[] coordinates)      //Проверка объекта в координатах(изучение области) куда надо пойти
   {

        foreach(Items item in items_list)
        {

            //Console.WriteLine(item.coordinates);
            if (item.coordinates.SequenceEqual(coordinates)) //Штучка для сравнения массивов
              {                       
                  return item;                 //Возыращает предмет на указанных координатах
              }
            
        }

        foreach (Monsters monster in monsters_list)
        {
            if (monster.coordinates.SequenceEqual(coordinates))
            {
                return monster;                 //Возыращает монстра на указанных координатах
            }

        }
        return 0;

        
    }
}

public class Monsters
{
    public int[] coordinates;
    public string name;
    public Monsters()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public Monsters(int x, int y) : this()
    {
        coordinates = new int[2] { x, y };
    }
}

