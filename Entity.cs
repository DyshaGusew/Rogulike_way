public class Hero
{
    public int[] coordinates;
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
public class Monsters
{
    public string name = "Gosha";
    public int[] coordinates;
    public double StaticHealht;
    public double NowHealht;
    public double damage;
    public int level;
    public int experience; // При смерти моба можно передавать его опыт герою
    public double boost;
}
public class Goblins : Monsters
{
    public string name = "Goblin";
    public double StaticHealht = 50;
    public double damage = 20;
    public int experience = 30;
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
    public string name = "Ghost";
    public double StaticHealht = 20;
    public double damage = 40;
    public int experience = 50;
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
    public string name = "Knight";
    public double StaticHealht = 120;
    public double damage = 30;
    public int experience = 50;
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
    public string name = "Skeleton";
    public double StaticHealht = 50;
    public double damage = 30;
    public int experience = 40;
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
    public string name = "Klenin";
    public double StaticHealht = 10;
    public double damage = 10;
    public int experience = 10;
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