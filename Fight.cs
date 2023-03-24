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

        //Начало файта
        public Fight(Hero Hero, Monsters Monster)
        {
            //Start(Hero, Monster);
            Console.WriteLine(Monster.damage);
        }
        
        public void Start(Hero Hero, Monsters Monster)
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetWindowSize(300, 300);
            Console.WriteLine($"Герой\nЗдоровье: {Hero.NowHealht}\nВыносливость: {Hero.NowStamina}\nУрон: {Hero.damage}\nlevel: {Hero.level}");
            int PositionX1 = 130, PositionY1 = 0;
            Console.SetCursorPosition(PositionX1, PositionY1);
            Console.WriteLine($"Монстр");
            Console.SetCursorPosition(PositionX1, PositionY1+1);
            Console.WriteLine($"Здоровье: {Monster.NowHealht}");
            Console.SetCursorPosition(PositionX1, PositionY1+2);
            Console.WriteLine($"Урон: {Monster.damage}");
            Console.SetCursorPosition(PositionX1, PositionY1+3);
            Console.WriteLine($"level: {Monster.level}");
            Console.SetCursorPosition(PositionX1, PositionY1+4);

            int PositionX2 = 65, PositionY2 = 30;
            Console.SetCursorPosition(PositionX2, PositionY2);
            Console.WriteLine("1.Сильный удар");
            Console.SetCursorPosition(PositionX2, PositionY2+1);
            Console.WriteLine("2.Обычный удар");
            Console.SetCursorPosition(PositionX2, PositionY2+2);
            Console.WriteLine("3.Блок");
        }
    }
}
