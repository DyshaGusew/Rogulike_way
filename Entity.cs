public class Hero
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
}

public class Wizard : Hero
{
    public Wizard()
    {
        StaticHealht = 60;
        NowHealht = StaticHealht;
        StaticStamina = 100;
        NowStamina = StaticStamina;
        damage = 20;
    }
}

public class Barbarian : Hero
{
    public Barbarian()
    {
        StaticHealht = 100;
        NowHealht = StaticHealht;
        StaticStamina = 100;
        NowStamina = StaticStamina;
        damage = 40;
    }
}

public class Prowler : Hero
{
    public Prowler()
    {
        StaticHealht = 60;
        NowHealht = StaticHealht;
        StaticStamina = 60;
        NowStamina = StaticStamina;
        damage = 30;
    }
}

public class Monsters
{
    public string name = "Гоша";
    public int[] coordinates = { 0, 0 };
    public double StaticHealht;
    public double NowHealht;
    public double damage;
    public int level;
    public int experience; // При смерти моба можно передавать его опыт герою
    public double boost;
}
public class Goblins : Monsters
{
    public Goblins(int level)
    {
        name = "Гоблин";
        StaticHealht = 50;
        damage = 20;
        experience = 30;
        this.level = level;

        if (level > 1)
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
        name = "Призрак";
        StaticHealht = 20;
        damage = 40;
        experience = 50;
        this.level = level;

        if (level > 1)
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
        name = "Рыцарь";
        StaticHealht = 120;
        damage = 30;
        experience = 50;
        this.level = level;

        if (level > 1)
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
        name = "Скелет";
        StaticHealht = 50;
        damage = 30;
        experience = 40;
        this.level = level;

        if (level > 1)
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
        name = "Крыса";
        StaticHealht = 10;
        damage = 10;
        experience = 10;
        this.level = level;

        if (level > 1)
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
    public Bosses(double health, double damage, string name)
    {
        this.name = name;
        StaticHealht = health;
        NowHealht = this.StaticHealht;
        this.damage = damage;
        level = 666;
        boost = 1.0;
        experience = 666;
    }
}