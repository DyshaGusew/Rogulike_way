using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Rogulike_way
{/*public class Hero
{
    public int[] coordinates = { 0, 0 };
    public double StaticHealht = 100;
    public double NowHealht = 100;
    public double StaticStamina = 100;
    public double NowStamina = 100;
    public double damage = 40;
    public int level = 1;
    public int experience = 0;
    public double boost = 1.0;
*/

    /*public class Monsters
   {
       public string name = "Gosha";
       public int[] coordinates = { 0, 0 };
       public double StaticHealht;
       public double NowHealht;
       public double damage;
       public int level;
       public int experience; // При смерти моба можно передавать его опыт герою
       public double boost;
   }
    */
    public class Fight
    {
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
            Console.WriteLine($"Герой\nЗдоровье: {HeroHealht}\nВыносливость: {HeroST}\nУрон: {Hero.damage}\nlevel: {Hero.level}");
            int PositionX1 = 130, PositionY1 = 0;
            string MonsterHealht = Monster.NowHealht.ToString("F1");
            //string MonsterST = Monster.NowStamina.ToString("F1");
            PositionPrint(PositionX1, PositionY1, $"Монстр");
            PositionPrint(PositionX1, PositionY1 + 1, $"{Monster.name}");
            PositionPrint(PositionX1, PositionY1 + 2, $"Здоровье: {MonsterHealht}");
            PositionPrint(PositionX1, PositionY1 + 3, $"Урон: {Monster.damage}");
            PositionPrint(PositionX1, PositionY1 + 4, $"level: {Monster.level}");
            Console.SetCursorPosition(PositionX1, PositionY1 + 5);

            int PositionX2 = 65, PositionY2 = 30;
            PositionPrint(PositionX2, PositionY2, "1.Сильный удар");
            PositionPrint(PositionX2, PositionY2 + 1, "2.Обычный удар");
            PositionPrint(PositionX2, PositionY2 + 2, "3.Блок");
            Console.SetCursorPosition(PositionX2, PositionY2 + 5);
        }


        private void Start(Hero Hero, Monsters Monster)
        {
            Thread.Sleep(1000);
            Console.CursorVisible = false;
            Rendering(Hero, Monster);
            double MonsterAt = 0;
            double HeroAt = Alpha(Hero.damage);//атака героя
            MonsterAt = Alpha(Monster.damage);//атака монстра
            while (true)
            {
                while (true)
                {
                    var Key = Console.ReadKey(true);
                    //обычный удар
                    if ((Key.KeyChar == '2') && (Hero.NowStamina >= 10))
                    {
                        Monster.NowHealht -= HeroAt;
                        Hero.NowStamina -= 10;
                        break;
                    }
                    //Cильный удар
                    else if ((Key.KeyChar == '1') && (Hero.NowStamina >= 25))
                    {
                        HeroAt = HeroAt * 1.7;
                        Monster.NowHealht -= HeroAt;
                        Hero.NowStamina -= 25;
                        break;
                    }
                    //блок
                    else if (Key.KeyChar == '3')
                    {
                        Hero.NowStamina += 10;
                        Random random = new Random();
                        int Rand = random.Next(50, 100);
                        MonsterAt = MonsterAt - MonsterAt * ((double)Rand / 100);
                        break;
                    }
                }
                //Момент с отрицательными числами хп и стамины
                if (Hero.NowStamina > Hero.StaticStamina){Hero.NowStamina = Hero.StaticStamina;}
                if (Hero.NowHealht <= 0) { Hero.NowHealht = 0; }
                if (Monster.NowHealht <= 0) { Monster.NowHealht = 0;}
               // Rendering(Hero, Monster);
                string HeroAtt = HeroAt.ToString("F1");//Чтобы выводилось до одной цифры после запятой
                                                       //PositionPrint(63, 35, $"Вы нанесли: {HeroAtt}");

                //*double HeroAt = MonsterAt;
                //Момент с отрицательными числами хп и стамины               
                Console.CursorVisible = false;
                Rendering(Hero, Monster);
                PositionPrint(63, 35, $"Вы нанесли: {HeroAtt}");
                if (Monster.NowHealht > 0)
                {
                    Thread.Sleep(3000);
                    string MonsterAtt = MonsterAt.ToString("F1");//Чтобы выводилось до одной цифры после запятой
                    Hero.NowHealht -= MonsterAt;
                    if (Hero.NowStamina > Hero.StaticStamina) { Hero.NowStamina = Hero.StaticStamina; }
                    if (Hero.NowHealht <= 0) { Hero.NowHealht = 0; }
                    if (Monster.NowHealht <= 0) { Monster.NowHealht = 0; }
                    Rendering(Hero, Monster);
                    PositionPrint(63, 35, $"Вы нанесли: {HeroAtt}");
                    PositionPrint(63, 36, $"Вам нанесли: {MonsterAtt}");                    
                    
                }
            }
        }
    }
}
