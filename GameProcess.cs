﻿//Класс для работы самой игры, работа с картой, игроком в реальном времени

//Примеры создание объектов, при создании необходмо сразу добавлять в коллекцию
//Monsters goblin = new Monsters(1, 3);
//goblin.name = "Dodo";

//Items sword = new Items(1, 6, "LOX");
//sword.name = "LOX";

World world = new World(1, 5);

//world.AppMonsters(goblin, true);
//world.AppItems(sword, true);


int[] coordinates = { 1, 5 };
coordinates[1] = 6;               //Были координаты и они поменялись на другие



//Напоминание о том, как перебирать что-либо, то есть на каждом шаге надо проверять 
/*
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
*/

//Основнй цикл
//Создаю текущую карту
char[,] map = world.CreateMap();

//Создаю героя указываю координаты
Hero gg = new Hero();
int[] coordinates_hero = { map.GetLength(1) / 2, map.GetLength(0) / 2 };   //Делаю так, чтобы он был посередине
gg.coordinates = coordinates_hero;
PaintGame.PaintConsole(map);                     //Отрисовываю карту без героя

System.Threading.Thread.Sleep(1000);             //Задежка

//Указываю героя в центре координат
map[gg.coordinates[1], gg.coordinates[0]] = '@';
PaintGame.PaintConsole(map);                     //Отрисовываю карту
ConsoleKeyInfo keyInfo;

Console.CursorVisible = false;    //Отключение курсора
do
{
    keyInfo = Console.ReadKey(true);
    switch (keyInfo.KeyChar)
    {
        
        case 'w': MovePlayer.Move("Up", map, world, gg);  break;
        case 's': MovePlayer.Move("Down", map, world,  gg); break;
        case 'd': MovePlayer.Move("Right", map, world,  gg); break;
        case 'a': MovePlayer.Move("Left", map, world, gg); break;
        default:
            break;
    }
} while (keyInfo.KeyChar != 'q');

//Отрисовка игры(необходимо добавить отрисовку статистики персонажа и игровых событий)
class PaintGame
{
    static public int StatX = 40, StatY = 20;  //Смещение карты

    //Отрисовка указанной карты
    static public void PaintConsole(char[,] map) //Рисует первую комнату по заготовке
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
}


//Передвижение героя(необходимо добавить проверку на наличие чего-то кроме стен)
class MovePlayer
{
    static public void Move(string trend, char[,] map, World world, Hero hero)
    {

        if (trend == "Left")
        {
            int[] move_coordinates = { hero.coordinates[0] - 1, hero.coordinates[1]};   //Указываю каково смещение
            Object obj = world.DefiningArea(move_coordinates);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо(пока только стены)
            //Если стена, то не двигаюсь
            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            //Меняю карту и соответсвенно меняю координаты героя
            map[hero.coordinates[1], hero.coordinates[0]] = ' ';
            PaintGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
            hero.coordinates[0] -= 1; hero.coordinates[1] -= 0;
            map[hero.coordinates[1], hero.coordinates[0]] = '@';
            PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
        }

        if(trend == "Right")
        {
            int[] move_coordinates = { hero.coordinates[0] +1, hero.coordinates[1] };   //Указываю каково смещение
            Object obj = world.DefiningArea(move_coordinates);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо(пока только стены)
            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            map[hero.coordinates[1], hero.coordinates[0]] = ' ';
            PaintGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
            hero.coordinates[0] += 1; hero.coordinates[1] -= 0;
            map[hero.coordinates[1], hero.coordinates[0]] = '@';
            PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
        }

        if(trend == "Up")
        {
            int[] move_coordinates = { hero.coordinates[0], hero.coordinates[1]-1};   //Указываю каково смещение
            Object obj = world.DefiningArea(move_coordinates);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо(пока только стены)
            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            map[hero.coordinates[1], hero.coordinates[0]] = ' ';
            PaintGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
            hero.coordinates[0] += 0; hero.coordinates[1] -= 1;
            map[hero.coordinates[1], hero.coordinates[0]] = '@';
            PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
        }
        if(trend == "Down")
        {
            int[] move_coordinates = { hero.coordinates[0], hero.coordinates[1]+1};   //Указываю каково смещение
            Object obj = world.DefiningArea(move_coordinates);  //Какой-то объект пока неизвестно какой на предположительно измененных координатах

            //Проверка на наличее в перемещаемой координате чего-либо(пока только стены)
            if (obj is Borders)   //Проверяю принадлежит ли объект классу стен
            {
                return;
            }

            map[hero.coordinates[1], hero.coordinates[0]] = ' ';
            PaintGame.PutCurs(' ', hero.coordinates[1], hero.coordinates[0]);
            hero.coordinates[0] += 0; hero.coordinates[1] += 1;
            map[hero.coordinates[1], hero.coordinates[0]] = '@';
            PaintGame.PutCurs('@', hero.coordinates[1], hero.coordinates[0]);
        }
    }
}

