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
    new public double StaticHealht = 60;
    new public double NowHealht = 60;
    new public double StaticStamina = 100;
    new public double NowStamina = 100;
    new public double damage = 20;
}

public class Barbarian : Hero
{
    new public double StaticHealht = 100;
    new public double NowHealht = 100;
    new public double StaticStamina = 100;
    new public double NowStamina = 100;
    new public double damage = 40;
}

public class Prowler : Hero
{
    new public double StaticHealht = 60;
    new public double NowHealht = 60;
    new public double StaticStamina = 60;
    new public double NowStamina = 60;
    new public double damage = 30;
}

public class Monsters
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
public class Goblins : Monsters
{
    new public string name = "Goblin";
    new public double StaticHealht = 50;
    new public double damage = 20;
    new public int experience = 30;
    public Goblins(int level)
    {
        this.level = level;
        if (this.level > 1)
        {
            this.boost = 1.0 + 0.1 * (this.level - 1);
            this.StaticHealht *= this.boost;
            this.NowHealht = this.StaticHealht;
            this.damage *= this.boost;
        };
    }
}
public class Ghost : Monsters
{
    new public string name = "Ghost";
    new public double StaticHealht = 20;
    new public double damage = 40;
    new public int experience = 50;
    public Ghost(int level)
    {
        this.level = level;
        if (this.level > 1)
        {
            this.boost = 1.0 + 0.1 * (this.level - 1);
            this.StaticHealht *= this.boost;
            this.NowHealht = this.StaticHealht;
            this.damage *= this.boost;
        }
    }
}
public class Knight : Monsters
{
    new public string name = "Knight";
    new public double StaticHealht = 120;
    new public double damage = 30;
    new public int experience = 50;
    public Knight(int level)
    {
        this.level = level;
        if (this.level > 1)
        {
            this.boost = 1.0 + 0.1 * (this.level - 1);
            this.StaticHealht *= this.boost;
            this.NowHealht = this.StaticHealht;
            this.damage *= this.boost;
        }
    }
}
public class Skeleton : Monsters
{
    new public string name = "Skeleton";
    new public double StaticHealht = 50;
    new public double damage = 30;
    new public int experience = 40;
    public Skeleton(int level)
    {
        this.level = level;
        if (this.level > 1)
        {
            this.boost = 1.0 + 0.1 * (this.level - 1);
            this.StaticHealht *= this.boost;
            this.NowHealht = this.StaticHealht;
            this.damage *= this.boost;
        }
    }
}
public class Rat : Monsters
{
    new public string name = "Klenin";
    new public double StaticHealht = 10;
    new public double damage = 10;
    new public int experience = 10;
    public Rat(int level)
    {
        this.level = level;
        if (this.level > 1)
        {
            this.boost = 1.0 + 0.1 * (this.level - 1);
            this.StaticHealht *= this.boost;
            this.NowHealht = this.StaticHealht;
            this.damage *= this.boost;
        }
    }
}
public class Bosses : Monsters
{
    public Bosses(double health, double damage, string name)
    {
        this.name = name;
        this.StaticHealht = health;
        this.NowHealht = this.StaticHealht;
        this.damage = damage;
        this.level = 666;
        this.boost = 1.0;
        this.experience = 666;
    }
}