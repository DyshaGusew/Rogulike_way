Monsters goblin = new Monsters(1, 3);
goblin.name = "Dodo";

Items sword = new Items(1, 6);
sword.name = "LOX";

World world = new World(1, 5);

world.AppMonsters(goblin, true);
world.AppItems(sword, true);


int[] coordinates = { 1, 5 };
coordinates[1] = 6;               //Были координаты и они поменялись на другие

Object obj = world.DefiningArea(coordinates);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

if (obj is Monsters)   //Проверяю принадлежит ли объект классу монстров
{
    Monsters box_monster = (Monsters)obj; // (Monsters) приводит тип обджект к типу монстров
    Console.WriteLine(box_monster.name);
}

else if (obj is Items)   //Проверяю принадлежит ли объект классу предметов
{
    Items box_items = (Items)obj;  
    //Console.WriteLine(box_items.name);
}

//Основнй цикл
PaintGame.PaintConsole(world.CreateMap());
ConsoleKeyInfo keyInfo;
do
{
    keyInfo = Console.ReadKey(true);
    switch (keyInfo.KeyChar)
    {
        //case 'w': Move(0, -1); break;
       // case 's': Move(0, 1); break;
        //case 'd': Move(1, 0); break;
        //case 'a': Move(-1, 0); break;
        default:
            break;
    }
} while (keyInfo.KeyChar != 'q');


class PaintGame
{
    static int StatX = 40, StatY = 20; 
    static public void PaintConsole(char[,] map) //Рисует первую комнату по заготовке
    {
        int x_len = map.GetLength(1);
        int y_len = map.GetLength(0);
       
        Console.Clear();
        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)         
                PutCurs(map[y, x], y, x);
    }
    static public void PutCurs(char ch, int y, int x)
    {
        Console.SetCursorPosition(StatX + x, StatY + y);
        Console.Write(ch);
    }
}

//class MovePlayer
//{
  //  public void Move(string trend)
    //{
        //if 
   // }
//}


