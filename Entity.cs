using System.Reflection;
using System.Threading;

public class Hero
{
    public string? name;
    public int[] coordinates = { 0, 0 };
    public double StaticHealht = 100;
    public double NowHealht = 100;
    public double StaticStamina = 100;
    public double NowStamina = 100;
    public int armor = 0;
    public double damage = 40;
    public int level = 1;
    public int experience = 0;
    public double boost = 1.0;
    public Inventory inventory = new Inventory();


    public void MonsterLevelUp(Monsters monster, int nowLevel)
    {
        for(int i = nowLevel; i>0; i--)
        {
            monster.level++;

            monster.boost = 1.0 + 0.1 * (monster.level - 1);
            monster.StaticHealht *= monster.boost;
            monster.NowHealht = monster.StaticHealht;
            monster.damage *= monster.boost;

        }
    }

    public void CheckAndLevelUp(World world)
    // В том числе восполняет хп и стамину
    {
        if (experience >= 100)
        {
            level++;
            boost += 0.1;
            StaticHealht *= boost;
            NowHealht = HealHp((int)StaticHealht /2);
            StaticStamina *= boost;
            NowStamina = HealSp((int)StaticStamina / 2);
            damage *= boost;
            experience = 0;

            //Проверяю все комнаты и увеличиваю силу монстров в каждой
            foreach (RealRoom room in world.roomsReal)
            {
                foreach (Monsters monster in room.monsters_list)
                {
                    if(world.hero.level - monster.level == 1)
                    {
                        MonsterLevelUp(monster, 1);
                        world.AppItemMonster(world.hero, monster);
                    }

                }
            }
            
        }
    }

    public double HealHp(int HowMany)
    {
        NowHealht += HowMany;
        if (NowHealht > StaticHealht) { NowHealht = StaticHealht; }
        return NowHealht;
    }

    public double HealSp(int HowMany)
    {
        NowStamina += HowMany;
        if (NowStamina > StaticStamina) { NowStamina = StaticStamina; }
        return NowHealht;
    }
}

public class Wizard : Hero
{
    public Wizard()
    {
        name = "Маг";
        StaticHealht = 50;
        NowHealht = 50;
        StaticStamina = 80;
        NowStamina = 80;
        armor = 10;
        damage = 40;
    }
}

public class Barbarian : Hero
{
    public Barbarian()
    {
        name = "Варвар";
        StaticHealht = 110;
        NowHealht = 110;
        StaticStamina = 45;
        NowStamina = 45;
        armor = 60;
        damage = 25;
    }
}

public class Prowler : Hero
{
    public Prowler()
    {
        name = "Ассасин";
        StaticHealht = 60;
        NowHealht = 60;
        StaticStamina = 60;
        NowStamina = 60;
        armor = 25;
        damage = 35;
    }
}

public class Monsters
{
    public char designation;
    public string name = "Гоша";
    public int[] coordinates = { 0, 0 };
    public double StaticHealht;
    public double NowHealht;
    public double damage;
    public int level = 1;
    public int experience; // При смерти моба можно передавать его опыт герою
    public double boost;
    public Items item = null;

    public Monsters() { }


    public static void CreateMonsters(RealRoom room, World world)
    {
        Random random = new Random();
        int count_monsters;
        if (room.map.GetLength(1) * room.map.GetLength(0) < 450)
        {
            count_monsters = random.Next(1, 3);
        }
        else if(room.map.GetLength(1) * room.map.GetLength(0) <= 700 && room.map.GetLength(1) * room.map.GetLength(0) >= 450) 
        { 
            count_monsters = random.Next(2, 3);
        }
        else if (room.map.GetLength(1) * room.map.GetLength(0) > 700)
        {
            count_monsters = random.Next(3, 4);
        }
        else
        {
            count_monsters = random.Next(1, 5);
        }
        

        for (int i = 0; i < count_monsters; i++)
        {
            Monsters monster = new Monsters();
            switch (random.Next(1, 6))
            {
                case 1:
                    {
                        monster = new Ghost();
                        break;
                    }
                case 2:
                    {
                        monster = new Ork();
                        break;
                    }
                case 3:
                    {
                        monster = new Rat();
                        break;
                    }
                case 4:
                    {
                        monster = new Knight();
                        break;
                    }
                case 5:
                    {
                        monster = new Skeleton();
                        break;
                    }

            }
            world.AppItemMonster(world.hero, monster);
            monster.coordinates = new int[] { random.Next(1, room.map.GetLength(1) - 2), random.Next(2, room.map.GetLength(0) - 2) };
            room.map[monster.coordinates[1], monster.coordinates[0]] = monster.designation;
            room.monsters_list.Add(monster);
           
        }
    }
}
public class Ork : Monsters
{
    public Ork()
    {
        designation = 'O';
        name = "Орк";
        StaticHealht = 50;
        damage = 20;
        experience = 40;
        if (level > 0)
        {
            boost = 1.0 + 0.1 * (level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        };

    }
}
public class Ghost : Monsters
{
    public Ghost()
    {
        designation = 'G';
        name = "Призрак";
        StaticHealht = 35;
        damage = 40;
        experience = 45;

        if (level > 0)
        {
            boost = 1.0 + 0.1 * (level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        }
    }
}
public class Knight : Monsters
{
    public Knight()
    {
        designation = 'K';
        name = "Рыцарь";
        StaticHealht = 85;
        damage = 30;
        experience = 60;

        if (level > 0)
        {
            boost = 1.0 + 0.1 * (level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        }
    }
}
public class Skeleton : Monsters
{
    public Skeleton()
    {
        designation = 'S';
        name = "Скелет";
        StaticHealht = 50;
        damage = 30;
        experience = 35;


        if (level > 0)
        {
            boost = 1.0 + 0.1 * (level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        }
    }
}
public class Rat : Monsters
{
    public Rat()
    {
        designation = 'R';
        name = "Крыса";
        StaticHealht = 10;
        damage = 15;
        experience = 15;


        if (level > 0)
        {
            boost = 1.0 + 0.1 * (level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        }
    }
}
public class Bosses : Monsters
{
    public Bosses(int health, int damage, string name)
    {
        this.name = name;
        StaticHealht = health;
        NowHealht = this.StaticHealht;
        this.damage = damage;
        level = 666;
        experience = 666;
    }
}
