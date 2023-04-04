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
            if (Hero.name == "Маг")
            {
                PositionPrint(0, 5, "" +
                    "                                                            \r\n" +
                    "            @@@@@++++++,                                    \r\n" +
                    "              ?@@@@@@@@@@@@@%+                              \r\n" +
                    "                   ?@@@@@@@@@@@@@#+                         \r\n" +
                    "               .%@@@@@@@@@@@@@@@@@@@@,                      \r\n" +
                    "              @@@@@@@@@@@@@@@@@@@@@@@@+    ???:             \r\n" +
                    "                     @   ?@@   ?         +@@@@@?            \r\n" +
                    "                     @@@@@@@@@@S         +@@@@@%            \r\n" +
                    "                      +@@@@@@S+,          ++++,             \r\n" +
                    "                       .@@@@.             ?##+             \r\n" +
                    "                   ,@@@@@@@@@@@?         @@@@@             \r\n" +
                    "                 ,@@@@@@@@@@@@@@S.        @@@@              \r\n" +
                    "                .+@@@@@@@@@@@@@@@@+       @@@               \r\n" +
                    "                ,@@@@@@@@@@@@@@@@@@,      @@%               \r\n" +
                    "                S@@@@@@@@@@@@@@@@@@@?:    @@%               \r\n" +
                    "                @@@@@@@@@@@@@@@@%.@@@@@@@@@@                \r\n" +
                    "                @@@@@@@@@@@@@@@@%         @+                \r\n" +
                    "               ,@@@@@@@@@@@@@@@@@         @+               \r\n" +
                    "               +@@@@@@@@@@@@@@@@@?        @+               \r\n" +
                    "              *@@@@@@@@@@@@@@@@@%         @+                \r\n" +
                    "             ?@@@@@@@@@@@@@@@@@@@         @%                \r\n" +
                    "             @@@@@@@@@@@@@@@@@@@@#        @@                \r\n" +
                    "            @@@@@@@@@@@@@@@@@@@@@@+       @@                \r\n" +
                    "           +@@@@@@@@@@@@@@@@@@@@@@@       @@                \r\n" +
                    "           @@@@@@@@@@@@@@@@@@@@@@@@       @@                \r\n" +
                    "          @@@@@@@@@@@@@@@@@@@@@@@@@%      @@                \r\n" +
                    "         @@@@@S@@@@@@@@@@@?@@@@@@@@@      @@                \r\n" +
                    "         @@@@+  @@@   @@@  @@@@+ @@@     +@+                \r\n" +
                    "                 S@@   :%  +@    +%%                        \r\n");
            }
            if(Hero.name == "Варвар")
            {
                PositionPrint(0, 5, "" +
                    "                    +@             +@+                      \r\n" +
                    "                   @*                S@                     \r\n" +
                    "                   @+    ?????+       +@                    \r\n" +
                    "                   ?@#?%        @@+  @%                     \r\n" +
                    "                      ?%.@+..++@.@S%                        \r\n" +
                    "                      @%        %@,                         \r\n" +
                    "                       @@      @%                           \r\n" +
                    "                         %S@@@                              \r\n" +
                    "                       ?@@@@@@@@@?                        \r\n" +
                    "                 ##@@@@@@@@@@@@@@@@@@@@#                +@@@\r\n" +
                    "@@             +@@@@@@@@@@@@@@@@@@@@@@@@@              ,@@@#\r\n" +
                    "@@%           %@@@@@@@@@@@@@@@@@@@@@@@@@@@            ?@@@? \r\n" +
                    "@@@@          @@@@@@@@@@@@@@@@@@@@@@@@@@@@           #@@@@  \r\n" +
                    "@@@@+         +@@@@@@ @@@@@@@@@@@@@%%@@@@%          %@@@%   \r\n" +
                    "@@@@@          @@@@@: @@@@@@@@@@@@@% ?@@@@@@%     %@@@@?    \r\n" +
                    " @@@@@      #@@@@@@+  @@@@@@@@@@@@@%  @@@@@@@@   ?@@@@@     \r\n" +
                    " ?@@@@@    *@@@@@@@   ?@@@@@@@@@@@@    @@@@@@@  @@@@@       \r\n" +
                    "  @@@@@%  ?@@@@@@%    @@@@@@@@@@@@@%    %@@@@@ %@@@@,       \r\n" +
                    "  +@@@@@%.@@@@@@+     ?@@@@@@@@@@@@%     +@@@@+@@@%         \r\n" +
                    "    ?@@@@@@@@@#       @@@@@@@@@@@@@       S@@@@@+           \r\n" +
                    "     @@@@@?:         @@@@@@@@@@@@@@S       %@@++            \r\n" +
                    "       @@@@         @@@@@@+    @@@@@@      ?@              \r\n" +
                    "          S@       @@@@@@+     @@@@@@@                    \r\n" +
                    "                  @@@@@@+      +@@@@@@                     \r\n" +
                    "                  @@@@@%         @@@@@                      \r\n" +
                    "                  @@@@@*         @@@@@%                     \r\n" +
                    "                 @@@@@@          @@@@@%                     \r\n" +
                    "                 @@@@@+           @@@@%                     \r\n" +
                    "                @@@@@@            @@@@@@                      \r\n" +
                    "             ?@@@@@@@@+           @@@@@@@@");

            }
            if (Hero.name == "Бродяга")
            {
                PositionPrint(0, 5, "" +
                    "                                                            \r\n" +
                    "                          :******:                          \r\n" +
                    "                        ?####@@####%                        \r\n" +
                    "                      ,S####????####S,                      \r\n" +
                    "                      S###%,    ,%###S                      \r\n" +
                    "                     %@@S+        +S@@%                     \r\n" +
                    "                    +###+          +###+                    \r\n" +
                    "                   *####SSSSSSSSSSSS####*.                  \r\n" +
                    "                ,%######@@@@@@@@@@@@######%,                \r\n" +
                    "               *########@@@@@@@@@@@@########*               \r\n" +
                    "              ?#####@@@@@@@@@@@@@@@@@@@@#####?              \r\n" +
                    "             ,#@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#,             \r\n" +
                    "             ,#@@@@@####@@@@@@@@@@@@####@@@@@#,             \r\n" +
                    "             *#@@@@#? ##@@@@@@@@@@@@##+?#@@@@#*             \r\n" +
                    "            *##@@##?  ?#@@@@@@@@@@@@#?  ?##@@##*            \r\n" +
                    "          :%###@@#:   ,#@@@@@@@@@@@@#    :#@@###%:          \r\n" +
                    "   :?    +######%:     *#@@@@@@@@@@@*     :%######+    ?:   \r\n" +
                    "    ?S%SS####%+        ,#@@@@@@@@@@#         +%####SS%S?    \r\n" +
                    "     +#####%:           #@@@@@@@@@@#           :%#####+     \r\n" +
                    "    ,S####*             #@@@####@@@#             +####S     \r\n" +
                    "        ?S#?,          +#@@@#%%#@@@#+          ,?#S?        \r\n" +
                    "          :S#%*       ,#@@@@?  ?@@@@#,       *%#S+          \r\n" +
                    "             ,++*,    ,#@@@#    #@@@#,    ,*++,             \r\n" +
                    "                      ,#@@@#    #@@@#,                      \r\n" +
                    "                      *####S    %####*                      \r\n" +
                    "                      %####+    +####%                      \r\n" +
                    "                     :#####,    ,#####+                     \r\n" +
                    "                     *####*      *####*                     \r\n" +
                    "                   +S#####*      +#####S+                   \r\n" +
                    "                 :?#@@@@#%       %#@@@@#?:                     ");

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
            if(Monster.name == "Рыцарь")
            {
                PositionX1 = 90;
                PositionPrint(PositionX1, PositionY1 + 5, "");
                PositionPrint(PositionX1, PositionY1 + 6, "                                                             ");
                PositionPrint(PositionX1, PositionY1 + 7, "                                                             ");
                PositionPrint(PositionX1, PositionY1 + 8 , "                                                            ");
                PositionPrint(PositionX1, PositionY1 + 9 , "                                                            ");
                PositionPrint(PositionX1, PositionY1 + 10 , "                                                           ");
                PositionPrint(PositionX1, PositionY1 + 11 , "                                                           ");
                PositionPrint(PositionX1, PositionY1 + 12 , "                                                           ");
                PositionPrint(PositionX1, PositionY1 + 13, "                                                            ");
                PositionPrint(PositionX1, PositionY1 + 14, "                     @?+++++++++++++++?@@@@:                ");
                PositionPrint(PositionX1, PositionY1 + 15, "       SSSSSSSSSSSSSSSSSS:              @%.@%               ");
                PositionPrint(PositionX1, PositionY1 + 16, "       SSSSSSSSSSSSSSSSSS:              S@  +@+             ");
                PositionPrint(PositionX1, PositionY1 + 17, "       SSSSSSSSSSSSSSSSSS:              @%    %S            ");
                PositionPrint(PositionX1, PositionY1 + 18, "       SSSSSSSSSSSSSSSSSS:              @%#    @@+          ");
                PositionPrint(PositionX1, PositionY1 + 19, "       SSSSSSSSSSSSSSSSSS:              @:@?    @@,         ");
                PositionPrint(PositionX1, PositionY1 + 20, "       SSSSSSSSSSSSSSSSSS:              @  %+    %@         ");
                PositionPrint(PositionX1, PositionY1 + 21, "       SSSSSSSSSSSSSSSSSS:              @   +     +.        ");
                PositionPrint(PositionX1, PositionY1 + 22, "       +SSSSSSSSSSSSSSSS?               @     @   @@@+@:    ");
                PositionPrint(PositionX1, PositionY1 + 23, "         :SSSSSSSSSSSS#                 @      ?.?.@@@      ");
                PositionPrint(PositionX1, PositionY1 + 24, "           :SSSSSSSS?.@                 @+       ?@@@%      ");
                PositionPrint(PositionX1, PositionY1 + 25, "             +SSSS?   ?@+          +++@@@+       .@@@?      ");
                PositionPrint(PositionX1, PositionY1 + 26, "               :*      @++@@@@++++%S++?@%       @@@@+S      ");
                PositionPrint(PositionX1, PositionY1 + 27, "                      ?%    @%     @@+  ??     #@@@         ");
                PositionPrint(PositionX1, PositionY1 + 28, "                     +@    @+      @.*  ?%    @@@@*         ");
                PositionPrint(PositionX1, PositionY1 + 29, "                     @@   @        @.?  ?%  ,@@@@+          ");
                PositionPrint(PositionX1, PositionY1 + 30, "                    +@   @%        @@   ?%  #@@@            ");
                PositionPrint(PositionX1, PositionY1 + 31, "                    +@   @          @,   @  S@@*            ");
                PositionPrint(PositionX1, PositionY1 + 32, "                    @@   @          @+.  +@                 ");
                PositionPrint(PositionX1, PositionY1 + 33, "                   ?%    ?%         @@   +@                 ");
                PositionPrint(PositionX1, PositionY1 + 34, "                 ++S     ?@         @@%   %@++,             ");
                PositionPrint(PositionX1, PositionY1 + 35, "             +@@@@@%?????S@         @@??????%@@@?");
            }
            int PositionX2 = 65, PositionY2 = 30;
            PositionPrint(PositionX2, PositionY2, "1.Сильный удар");
            PositionPrint(PositionX2, PositionY2 + 1, "2.Обычный удар");
            PositionPrint(PositionX2, PositionY2 + 2, "3.Блок");
            Console.SetCursorPosition(PositionX2, PositionY2 + 5);
        }
        private void Blinking()
        {
            System.Threading.Thread.Sleep(1000);
            Console.CursorVisible = false;
            //Моргание
            //i меньше чего-то - количество interval - время
            for (int i = 0; i < 3; i++)
            {
                int interval = 500;
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
                        int Rand = random.Next(50, 100);
                        MonsterAt = MonsterAt - MonsterAt * ((double)Rand / 100);  
                        break;
                    }
                }
                //Броня, которая срезает часть урона
                MonsterAt = MonsterAt - MonsterAt * Hero.armor / 100;


                //Момент с отрицательными числами хп и стамины
                if (Hero.NowStamina > Hero.StaticStamina){Hero.NowStamina = Hero.StaticStamina;}
                if (Hero.NowHealht <= 0) { Hero.NowHealht = 0; }
                if (Monster.NowHealht <= 0) { Monster.NowHealht = 0;}
               // Rendering(Hero, Monster);
                string HeroAtt = HeroAt.ToString("F1");//Чтобы выводилось до одной цифры после запятой
                //Момент с отрицательными числами хп и стамины               
                Console.CursorVisible = false;
                Rendering(Hero, Monster);
                if (Keys != '3')
                {
                    PositionPrint(63, 35, $"Вы нанесли: {HeroAtt}");
                }

                if ((Monster.NowHealht <= 0) && (Hero.NowHealht>0))
                {
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
                    System.Threading.Thread.Sleep(2000);
                    return 1;
                }
                



                if (Monster.NowHealht > 0)
                {
                    Thread.Sleep(1500);
                    string MonsterAtt = MonsterAt.ToString("F1");//Чтобы выводилось до одной цифры после запятой
                    Hero.NowHealht -= MonsterAt;
                    //Момент с отрицательными числами хп
                    if (Hero.NowStamina > Hero.StaticStamina) { Hero.NowStamina = Hero.StaticStamina; }
                    if (Hero.NowHealht <= 0) { Hero.NowHealht = 0; }
                    if (Monster.NowHealht <= 0) { Monster.NowHealht = 0; }
                    Rendering(Hero, Monster);
                    if (Keys != '3')
                    {
                        PositionPrint(63, 35, $"Вы нанесли: {HeroAtt}");
                        PositionPrint(63, 36, $"Вам нанесли: {MonsterAtt}");
                    }                   
                    else { PositionPrint(63, 35, $"Вам нанесли: {MonsterAtt}");}
                }
                if ((Monster.NowHealht >= 0) && (Hero.NowHealht <= 0))
                {
                    System.Threading.Thread.Sleep(1500);
                    Console.Clear();
                    Console.WriteLine(
                        "\n\n\n\n      ППППППППППППППППППППППППППППППП          OOOOOOOOO          MMMMMMMM               MMMMMMMM     EEEEEEEEEEEEEEEEEEEEEE     RRRRRRRRRRRRRRRRR   \r\n" +
                        "      П:::::::::::::::::::::::::::::П        OO:::::::::OO        M:::::::M             M:::::::M     E::::::::::::::::::::E     R::::::::::::::::R  \r\n" +
                        "      П:::::::::::::::::::::::::::::П      OO:::::::::::::OO      M::::::::M           M::::::::M     E::::::::::::::::::::E     R::::::RRRRRR:::::R \r\n" +
                        "      П::::::ППППППППППППППППП::::::П     O:::::::OOO:::::::O     M:::::::::M         M:::::::::M     EE::::::EEEEEEEEE::::E     RR:::::R     R:::::R\r\n" +
                        "      П::::::П               П::::::П     O::::::O   O::::::O     M::::::::::M       M::::::::::M       E:::::E       EEEEEE       R::::R     R:::::R\r\n" +
                        "      П::::::П               П::::::П     O:::::O     O:::::O     M:::::::::::M     M:::::::::::M       E:::::E                    R::::R     R:::::R\r\n" +
                        "      П::::::П               П::::::П     O:::::O     O:::::O     M:::::::M::::M   M::::M:::::::M       E::::::EEEEEEEEEE          R::::RRRRRR:::::R \r\n" +
                        "      П::::::П               П::::::П     O:::::O     O:::::O     M::::::M M::::M M::::M M::::::M       E:::::::::::::::E          R:::::::::::::RR  \r\n" +
                        "      П::::::П               П::::::П     O:::::O     O:::::O     M::::::M  M::::M::::M  M::::::M       E:::::::::::::::E          R::::RRRRRR::R \r\n" +
                        "      П::::::П               П::::::П     O:::::O     O:::::O     M::::::M   M:::::::M   M::::::M       E::::::EEEEEEEEEE          R::::R            \r\n" +
                        "      П::::::П               П::::::П     O:::::O     O:::::O     M::::::M    M:::::M    M::::::M       E:::::E                    R::::R            \r\n" +
                        "      П::::::П               П::::::П     O::::::O   O::::::O     M::::::M     MMMMM     M::::::M       E:::::E       EEEEEE       R::::R            \r\n" +
                        "      П::::::П               П::::::П     O:::::::OOO:::::::O     M::::::M               M::::::M     EE::::::EEEEEEEE:::::E     RR:::::R            \r\n" +
                        "      П::::::П               П::::::П      OO:::::::::::::OO      M::::::M               M::::::M     E::::::::::::::::::::E     R::::::R            \r\n" +
                        "      П::::::П               П::::::П        OO:::::::::OO        M::::::M               M::::::M     E::::::::::::::::::::E     R::::::R            \r\n" +
                        "      ПППППППП               ПППППППП          OOOOOOOOO          MMMMMMMM               MMMMMMMM     EEEEEEEEEEEEEEEEEEEEEE     RRRRRRRR          ");
                    System.Threading.Thread.Sleep(2000);
                    return 0;
                }
            }
        }
    }
}
