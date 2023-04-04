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

    public void CheckAndLevelUp()
    // В том числе восполняет хп и стамину
    {
        if (experience >= 100)
        {
            this.level++;
            this.boost += 0.1;
            this.StaticHealht *= this.boost;
            this.NowHealht = this.StaticHealht;
            this.StaticStamina *= this.boost;
            this.NowStamina = this.StaticStamina;
            this.damage *= this.boost;
            this.experience = 0;
        }
    }

    public void HealHp(int HowMany)
    {
        NowHealht += HowMany;
        if (NowHealht > StaticHealht) { NowHealht = StaticHealht; }
    }

    public void HealSp(int HowMany)
    {
        NowStamina += HowMany;
        if (NowStamina > StaticStamina) { NowStamina = StaticStamina; }
    }
}

public class Wizard : Hero
{
    public Wizard()
    {
        name = "Маг";
        StaticHealht = 60;
        NowHealht = 60;
        StaticStamina = 100;
        NowStamina = 100;
        armor = 20;
        damage = 20;
    }
}

public class Barbarian : Hero
{
    public Barbarian()
    {
        name = "Варвар";
        StaticHealht = 100;
        NowHealht = 100;
        StaticStamina = 100;
        NowStamina = 100;
        armor = 40;
        damage = 40;
    }
}

public class Prowler : Hero
{
    public Prowler()
    {
        name = "Бродяга";
        StaticHealht = 60;
        NowHealht = 60;
        StaticStamina = 60;
        NowStamina = 60;
        armor = 30;
        damage = 30;
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
    public int level;
    public int experience; // При смерти моба можно передавать его опыт герою
    public double boost;

    public Monsters() { }

    public static void CreateMonsters(RealRoom room, Hero hero)
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
                        monster = new Ghost(hero.level);
                        break;
                    }
                case 2:
                    {
                        monster = new Ork(hero.level);
                        break;
                    }
                case 3:
                    {
                        monster = new Rat(hero.level);
                        break;
                    }
                case 4:
                    {
                        monster = new Knight(hero.level);
                        break;
                    }
                case 5:
                    {
                        monster = new Skeleton(hero.level);
                        break;
                    }

            }
            monster.coordinates = new int[] { random.Next(1, room.map.GetLength(1) - 2), random.Next(2, room.map.GetLength(0) - 2) };
            room.map[monster.coordinates[1], monster.coordinates[0]] = monster.designation;
            room.monsters_list.Add(monster);
           
        }
    }
}
public class Ork : Monsters
{
    public Ork(int level)
    {
        designation = 'O';
        name = "Орк";
        StaticHealht = 50;
        damage = 20;
        experience = 30;
        this.level = level;

        if (level > 0)
        {
            boost = 1.0 + 0.1 * (this.level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        };
    }
}
public class Ghost : Monsters
{
    public Ghost(int level)
    {
        designation = 'G';
        name = "Призрак";
        StaticHealht = 20;
        damage = 40;
        experience = 50;
        this.level = level;

        if (level > 0)
        {
            boost = 1.0 + 0.1 * (this.level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        }
    }
}
public class Knight : Monsters
{
    public Knight(int level)
    {
        designation = 'K';
        name = "Рыцарь";
        StaticHealht = 120;
        damage = 30;
        experience = 50;
        this.level = level;

        if (level > 0)
        {
            boost = 1.0 + 0.1 * (this.level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        }
    }
}
public class Skeleton : Monsters
{
    public Skeleton(int level)
    {
        designation = 'S';
        name = "Скелет";
        StaticHealht = 50;
        damage = 30;
        experience = 40;
        this.level = level;

        if (level > 0)
        {
            boost = 1.0 + 0.1 * (this.level - 1);
            StaticHealht *= boost;
            NowHealht = StaticHealht;
            damage *= boost;
        }
    }
}
public class Rat : Monsters
{
    public Rat(int level)
    {
        designation = 'R';
        name = "Крыса";
        StaticHealht = 10;
        damage = 10;
        experience = 10;
        this.level = level;

        if (level > 0)
        {
            boost = 1.0 + 0.1 * (this.level - 1);
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
