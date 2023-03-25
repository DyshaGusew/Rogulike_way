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
            Start(Hero, Monster);
            //Console.WriteLine(Monster.damage);
        }
        //Принимает на вход х,у и текст, где х и у координаты куда надо вывести
        public void PositionPrint(int x, int y, string str)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(str);
        }
        
        private void Start(Hero Hero, Monsters Monster)
        {
            Thread.Sleep(1000);
            Console.Clear();
            Console.SetWindowSize(300, 300);
            Console.WriteLine($"Герой\nЗдоровье: {Hero.NowHealht}\nВыносливость: {Hero.NowStamina}\nУрон: {Hero.damage}\nlevel: {Hero.level}");
            int PositionX1 = 130, PositionY1 = 0;
            PositionPrint(PositionX1, PositionY1, $"Монстр");
            PositionPrint(PositionX1, PositionY1+1, $"Здоровье: {Monster.StaticHealht}");
            PositionPrint(PositionX1, PositionY1+2, $"Урон: {Monster.damage}");
            PositionPrint(PositionX1, PositionY1 + 3, $"level: {Monster.level}");
            Console.SetCursorPosition(PositionX1, PositionY1+4);

            int PositionX2 = 65, PositionY2 = 30;
            PositionPrint(PositionX2, PositionY2, "1.Сильный удар");
            PositionPrint(PositionX2, PositionY2+1, "2.Обычный удар");
            PositionPrint(PositionX2, PositionY2+2, "3.Блок");
        }
    }
}
