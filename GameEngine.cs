//Игровой движок в котором все и происходит

//Создется объек мира
World world = new World();

//Отображение наччала игры
Console.WriteLine("GameStart");

//Создание героя (создается в зависимости от выбора в меню)
Hero hero = new Wizard();

//Пример создания монстра
Monsters Goblin = new Ghost(1);
//Fight fight = new Fight(Hero, Goblin);


//Создаю мини карту(и одновремено при пмощи нее делаю взаимодействия между комнатами)
char[,] miniMap = world.CreateMiniMap();

//Создание первой комнаты
char[,] Room = world.CreateRoomReal(0);

//задаю герою координаты
int[] coordinates_hero = { Room.GetLength(1) / 2, Room.GetLength(0) / 2 };   //Делаю так, чтобы он был посередине
hero.coordinates = coordinates_hero;


//Отрисовываю карту без героя
PaintGame.DraftCart(Room, miniMap);

System.Threading.Thread.Sleep(1000);             //Задежка

//Указываю героя в центре координат
Room[hero.coordinates[1], hero.coordinates[0]] = '@';
PaintGame.DraftCart(Room, miniMap);                     //Отрисовываю карту уже с героем
ConsoleKeyInfo keyInfo;

Console.CursorVisible = false;    //Отключение курсора
do
{
    keyInfo = Console.ReadKey(true);
    if(keyInfo.KeyChar == 'w' || keyInfo.KeyChar == 'ц')
        MovePlayer.Move("Up", Room, world, hero);
   
    else if(keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'ы')
        MovePlayer.Move("Down", Room, world, hero);

    else if (keyInfo.KeyChar == 'd' || keyInfo.KeyChar == 'в')
        MovePlayer.Move("Right", Room, world, hero);

    else if (keyInfo.KeyChar == 'a' || keyInfo.KeyChar == 'ф')
        MovePlayer.Move("Left", Room, world, hero);

} while (keyInfo.KeyChar != 'q');



//Отрисовка игры(необходимо добавить отрисовку статистики персонажа и игровых событий)
class PaintGame
{
    static public int StatX = 20, StatY = 5;  //Смещение карты

    //Отрисовка указанной карты
    static public void DraftCart(char[,] map, char[,] miniMap) //Рисует первую комнату по заготовке
    {
        int x_len = map.GetLength(1);
        int y_len = map.GetLength(0);
       
        Console.Clear();
        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)         
                PutCurs(map[y, x], y, x);
        PaintGame.PaintConsoleRange(miniMap, 80, 0);
    }

    //Добавление символа в необходимой координате
    static public void PutCurs(char ch, int y, int x)
    {
        Console.SetCursorPosition(StatX + x, StatY + y);
        Console.Write(ch);
    }

    static public void PutCursRange(char ch, int y, int x, int statX, int statY)
    {
        Console.SetCursorPosition(statX + x, statY + y);
        Console.Write(ch);
    }

    static public void PaintConsoleRange(char[,] map, int statX, int statY) //Рисует первую комнату по заготовке
    {
        int x_len = map.GetLength(1);
        int y_len = map.GetLength(0);

        for (int y = 0; y < y_len; y++)
            for (int x = 0; x < x_len; x++)
                PutCursRange(map[y, x], y, x, statX, statY);
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

            if (obj is Doors)   //Проверяю принадлежит ли объект классу стен
            {
                
                return;

            }

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
            if (obj is Doors)   //Проверяю принадлежит ли объект классу стен
            {

                return;

            }
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
            if (obj is Doors)   //Проверяю принадлежит ли объект классу стен
            {

                return;

            }
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
            if (obj is Doors)   //Проверяю принадлежит ли объект классу стен
            {

                return;

            }
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


