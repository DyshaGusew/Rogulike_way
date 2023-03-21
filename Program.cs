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
    Console.WriteLine(box_items.name);
}


