Monsters goblin = new Monsters(1, 5);
Items sword = new Items(1, 3);

World world = new World(1, 5);

world.monster = goblin;
world.item = sword;


Console.WriteLine(world.GetXY()[1]);

