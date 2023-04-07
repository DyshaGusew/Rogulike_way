using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace Rogulike_way
{
    public class Fight
    {
        public Fight()
        {

        }
        //передаем сюда Героя и монстра,которые будут сражаться
        public Fight(Hero Hero, Monsters Monster)
        {
            Start(Hero, Monster);
            //Console.WriteLine(Monster.damage);
        }
        //Принимает на вход х,у и текст, где х и у координаты куда надо вывести
        private void PositionPrint(int x, int y, string str)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(str);
        }
        //Атака будет наноситься с разбросом +-25%
        private double Alpha(double Damage)
        {
            //Создание объекта для генерации чисел
            Random random = new Random();
            //Получить случайное число (в диапазоне от 0 до 10)
            int Rand = random.Next(-25, 25);
            Damage = Damage + Damage * ((double)Rand / 100);
            return Damage;
        }
        private void Rendering(Hero Hero, Monsters Monster)
        {
            
            Console.Clear();
            Console.CursorVisible = false;
            string HeroHealht = Hero.NowHealht.ToString("F1");
            string HeroST = Hero.NowStamina.ToString("F1");
            string HeroAT = Hero.damage.ToString("F1");
            Console.WriteLine($"Герой\nЗдоровье: {HeroHealht}\nВыносливость: {HeroST}\nУрон: {HeroAT}\nlevel: {Hero.level}");
            FileStream fileStream = new FileStream("Knight.txt", FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            int i = 0; ;
            // считываем строки из файла
            string line;
            // закрываем StreamReader и файловый поток
            streamReader.Close();
            fileStream.Close();
            if (Hero.ClassName == "Маг")
            {
                fileStream = new FileStream("Wizard.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
            }
            if(Hero.ClassName == "Варвар")
            {
                fileStream = new FileStream("Barbarian.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
            }
            if (Hero.ClassName == "Ассасин")
            {
                fileStream = new FileStream("Assasin.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
            }
            while ((line = streamReader.ReadLine()) != null)
            {
                PositionPrint(0, 5 + i, line);
                i++;
            }


            int PositionX1 = 130, PositionY1 = 0;
            string MonsterHealht = Monster.NowHealht.ToString("F1");
            string MonsterDA = Monster.damage.ToString("F1");
            //string MonsterST = Monster.NowStamina.ToString("F1");
            PositionPrint(PositionX1, PositionY1, $"Монстр");
            PositionPrint(PositionX1, PositionY1 + 1, $"{Monster.name}");
            PositionPrint(PositionX1, PositionY1 + 2, $"Здоровье: {MonsterHealht}");
            PositionPrint(PositionX1, PositionY1 + 3, $"Урон: {MonsterDA}");
            PositionPrint(PositionX1, PositionY1 + 4, $"level: {Monster.level}");
            Console.SetCursorPosition(PositionX1, PositionY1 + 5);

            i = 0; ;
            // считываем строки из файла
            PositionX1 = 100;

            // закрываем StreamReader и файловый поток
            streamReader.Close();
            fileStream.Close();

            if (Monster.name == "Рыцарь")
            {
                fileStream = new FileStream("Knight.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
                i = 10;
                // считываем строки из файла
                PositionX1 = 100; 
            }
            if (Monster.name == "Скелет")
            {
                fileStream = new FileStream("Skeleton.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
                i = 5;
                // считываем строки из файла
                PositionX1 = 95;
            }
            if (Monster.name == "Орк")
            {
                fileStream = new FileStream("Ork.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
                i = 5;
                // считываем строки из файла
                PositionX1 = 95;
            }
            if (Monster.name == "Крыса")
            {
                fileStream = new FileStream("Rat.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
                i = 15;
                // считываем строки из файла
                PositionX1 = 95;
            }
            if (Monster.name == "Призрак")
            {
                fileStream = new FileStream("Ghost.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
                i = 10;
                // считываем строки из файла
                PositionX1 = 95;
            }
            if (Monster.name == "БАЛРОГ, демон тьмы")
            {
                fileStream = new FileStream("Boss.txt", FileMode.Open);
                streamReader = new StreamReader(fileStream);
                i = 3;
                // считываем строки из файла
                PositionX1 = 83;
            }
            while ((line = streamReader.ReadLine()) != null)
            {
                PositionPrint(PositionX1, PositionY1 + i, line);
                i++;
            }

            // закрываем StreamReader и файловый поток
            streamReader.Close();
            fileStream.Close();

            int PositionX2 = 65, PositionY2 = 35;
            PositionPrint(PositionX2, PositionY2, "1.Сильный удар");
            PositionPrint(PositionX2, PositionY2 + 1, "2.Обычный удар");
            PositionPrint(PositionX2, PositionY2 + 2, "3.Блок");
            //Console.SetCursorPosition(PositionX2, PositionY2 + 5);
        }
        private void Blinking()
        {
            System.Threading.Thread.Sleep(300);
            Console.CursorVisible = false;
            //Моргание
            //i меньше чего-то - количество interval - время
            for (int i = 0; i < 3; i++)
            {
                int interval = 100;
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                System.Threading.Thread.Sleep(interval);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                System.Threading.Thread.Sleep(interval);
            }
        }

        public int Start(Hero Hero, Monsters Monster)
        {
            Blinking();
            Rendering(Hero, Monster);
            double HeroAt = Alpha(Hero.damage);//атака героя
            char Keys = ' ';//не получается присвоить Keys значение KeyChar
            Console.SetCursorPosition(0, 0);
            while (true)
            {
                double MonsterAt = Alpha(Monster.damage);
                while (true)
                {
                    var Key = Console.ReadKey(true);
                    //обычный удар
                    if ((Key.KeyChar == '2') && (Hero.NowStamina >= 10))
                    {
                        Monster.NowHealht -= HeroAt;
                        Hero.NowStamina -= 10;
                        Keys = '2'; 
                        break;
                    }
                    //Cильный удар
                    else if ((Key.KeyChar == '1') && (Hero.NowStamina >= 25))
                    {
                        HeroAt = HeroAt * 1.7;
                        Monster.NowHealht -= HeroAt;
                        Hero.NowStamina -= 25;
                        Keys = '1';
                        break;
                    }
                    //блок
                    else if (Key.KeyChar == '3')
                    {
                        Keys = '3';
                        Hero.NowStamina += 10;
                        Random random = new Random();
                        int Rand = random.Next(40, 61);
                        MonsterAt = MonsterAt - (MonsterAt * ((double)Rand / 100));  
                        break;
                    }
                }
                //Броня, которая срезает часть урона
                MonsterAt = MonsterAt - MonsterAt * Hero.armor / 100;


                //Момент с отрицательными числами хп и стамины
                if (Hero.NowStamina > Hero.StaticStamina){Hero.NowStamina = Hero.StaticStamina;}
                if (Hero.NowHealht <= 0) { Hero.NowHealht = 0; }
                if (Monster.NowHealht <= 0) { Monster.NowHealht = 0;}
                Rendering(Hero, Monster);
                string HeroAtt = HeroAt.ToString("F1");//Чтобы выводилось до одной цифры после запятой
                //Момент с отрицательными числами хп и стамины               
                Console.CursorVisible = false;
                Rendering(Hero, Monster);
                Console.SetCursorPosition(0, 0);
                if (Keys != '3')
                {
                    PositionPrint(63, 0, $"Вы нанесли: {HeroAtt}");
                }

                  if ((Monster.NowHealht <= 0) && (Hero.NowHealht>0))
                  {
                    /*
                      PositionPrint(63, 0, $"Вы нанесли: {HeroAtt}");
                      System.Threading.Thread.Sleep(1500);
                      Console.Clear();
                      Console.WriteLine("         " +
                          "" +
                          "  \r\nYYYYYYY       YYYYYYY     OOOOOOOOO     UUUUUUUU     UUUUUUUU     WWWWWWWW" +
                          "                           WWWWWWWWIIIIIIIIIINNNNNNNN        NNNNNNNN      !!! \r\n" +
                          "Y:::::Y       Y:::::Y   OO:::::::::OO   U::::::U     U::::::U     W::::::W                           " +
                          "" +
                          "W::::::WI::::::::IN:::::::N       N::::::N     !!:!!\r\n" +
                          "Y:::::Y       Y:::::Y OO:::::::::::::OO U::::::U     U::::::U     W::::::W                           W::::::WI::::::::IN::::::::N      N::::::N     !:::!\r\n" +
                          "Y::::::Y     Y::::::YO:::::::OOO:::::::OUU:::::U     U:::::UU     W::::::W                           W::::::WII::::::IIN:::::::::N     N::::::N     !:::!\r\n" +
                          "YYY:::::Y   Y:::::YYYO::::::O   O::::::O U:::::U     U:::::U       W:::::W           WWWWW           W:::::W   I::::I  N::::::::::N    N::::::N     !:::!\r\n" +
                          "   Y:::::Y Y:::::Y   O:::::O     O:::::O U:::::D     D:::::U        W:::::W         W:::::W         W:::::W    I::::I  N:::::::::::N   N::::::N     !:::!\r\n" +
                          "    Y:::::Y:::::Y    O:::::O     O:::::O U:::::D     D:::::U         W:::::W       W:::::::W       W:::::W     I::::I  N:::::::N::::N  N::::::N     !:::!\r\n " +
                          "    Y:::::::::Y     O:::::O     O:::::O U:::::D     D:::::U          W:::::W     W:::::::::W     W:::::W      I::::I  N::::::N N::::N N::::::N     !:::!\r\n" +
                          "      Y:::::::Y      O:::::O     O:::::O U:::::D     D:::::U           W:::::W   W:::::W:::::W   W:::::W       I::::I  N::::::N  N::::N:::::::N     !:::!\r\n " +
                          "      Y:::::Y       O:::::O     O:::::O U:::::D     D:::::U            W:::::W W:::::W W:::::W W:::::W        I::::I  N::::::N   N:::::::::::N     !:::!\r\n " +
                          "      Y:::::Y       O:::::O     O:::::O U:::::D     D:::::U             W:::::W:::::W   W:::::W:::::W         I::::I  N::::::N    N::::::::::N     !!:!!\r\n " +
                          "      Y:::::Y       O::::::O   O::::::O U::::::U   U::::::U              W:::::::::W     W:::::::::W          I::::I  N::::::N     N:::::::::N      !!! \r\n" +
                          "       Y:::::Y       O:::::::OOO:::::::O U:::::::UUU:::::::U               W:::::::W       W:::::::W         II::::::IIN::::::N      N::::::::N          \r\n" +
                          "    YYYY:::::YYYY     OO:::::::::::::OO   UU:::::::::::::UU                 W:::::W         W:::::W          I::::::::IN::::::N       N:::::::N      !!! \r\n" +
                          "    Y:::::::::::Y       OO:::::::::OO       UU:::::::::UU                    W:::W           W:::W           I::::::::IN::::::N        N::::::N     !!:!!\r\n" +
                          "    YYYYYYYYYYYYY         OOOOOOOOO           UUUUUUUUU                       WWW             WWW            IIIIIIIIIINNNNNNNN         NNNNNNN      !!! ");
                      */
                    System.Threading.Thread.Sleep(1500);              
                    return 1;
                }
                



                if (Monster.NowHealht > 0)
                {
                    Thread.Sleep(200);
                    string MonsterAtt = MonsterAt.ToString("F1");//Чтобы выводилось до одной цифры после запятой
                    Hero.NowHealht -= MonsterAt;
                    //Момент с отрицательными числами хп
                    if (Hero.NowStamina > Hero.StaticStamina) { Hero.NowStamina = Hero.StaticStamina; }
                    if (Hero.NowHealht <= 0) { Hero.NowHealht = 0; }
                    if (Monster.NowHealht <= 0) { Monster.NowHealht = 0; }
                    Rendering(Hero, Monster);
                    if (Keys != '3')
                    {
                        PositionPrint(63, 0, $"Вы нанесли: {HeroAtt}");
                        PositionPrint(63, 1, $"Вам нанесли: {MonsterAtt}");
                        Console.SetCursorPosition(0, 0);
                    }                   
                    else { 
                        PositionPrint(63, 0, $"Вам нанесли: {MonsterAtt}");                      
                    }
                }
                if ((Monster.NowHealht >= 0) && (Hero.NowHealht <= 0))
                {
                    Console.SetCursorPosition(0, 0);
                    System.Threading.Thread.Sleep(1500);
                    Console.Clear();
                    FileStream fileStream = new FileStream("Dead.txt", FileMode.Open);
                    StreamReader streamReader = new StreamReader(fileStream);
                    string line;
                    int i = 0;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        PositionPrint(5, 5 + i, line);
                        i++;
                    }

                    // закрываем StreamReader и файловый поток
                    streamReader.Close();
                    fileStream.Close();
                    System.Threading.Thread.Sleep(2000);
                    return 0;
                }
            }
        }
    }
}
