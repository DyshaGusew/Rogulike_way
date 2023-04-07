using System.Reflection;
using System.Threading;

public class Hero
{
    public int countDeadMonsters = 0;
    public string? name;
    public string ClassName;
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


    public void MonsterLevelUp(Monsters monster_)
    {
        monster_.level++;
        monster_.boost = 1.0 + 0.2 * (monster_.level - 1);
        monster_.StaticHealht *= monster_.boost;
        monster_.NowHealht = monster_.StaticHealht;
        monster_.damage *= monster_.boost;

    }

    public void CheckAndLevelUp(World world)
    // В том числе восполняет хп и стамину
    {
        if (world.hero.experience >= 100)
        {
            world.hero.level++;
            world.hero.boost += 0.1;
            world.hero.StaticHealht *= boost;
            world.hero.NowHealht = HealHp((int)StaticHealht /2);
            world.hero.StaticStamina *= boost;
            world.hero.NowStamina = HealSp((int)StaticStamina / 2);
            world.hero.damage *= boost;
            world.hero.experience = 0;

            //Проверяю все комнаты и увеличиваю силу монстров в каждой
            foreach (RealRoom room in world.roomsReal)
            {
                foreach (Monsters monster in room.monsters_list)
                {
                        MonsterLevelUp(monster);
                        world.AppItemMonster(world.hero, monster);
                }

                if(room.chest != null)
                {
                    world.AppChest(world.hero, room);
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
        ClassName = "Маг";
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
        ClassName = "Варвар";
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
        ClassName = "Ассасин";
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
        damage = 55;
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
