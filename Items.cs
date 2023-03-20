public class Items
{
    int[] coordinates;
    public Items()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public Items(int x, int y) : this()
    {
        coordinates = new int[2] { x, y };
    }
}

public class Weapon
{
    public string name;
    public int level;
    public int damage;
    public int id;

    public Weapon(string _name, int _damage)
    {
        name = _name;
        level = 1;
        damage = _damage;
    }

    public void level_up()
    {
        this.level++;
        this.damage += 0;
    }
}

public class Armor
{
    public string name;
    public int level;
    public int armor;
    public int id;

    public Armor(string _name, int _armor)
    {
        name = _name;
        level = 1;
        armor = _armor;
    }

    public void level_up()
    {
        this.level++;
        this.armor += 0;
    }
}