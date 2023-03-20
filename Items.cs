public class Items
{
    public int[] coordinates;
    public Items()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public Items(int x, int y) : this()
    {
        coordinates = new int[2] { x, y };
    }
}

public class Weapon: Items
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

    // левел ап оружия
    public void levelUp()
    {
        this.level++;
        this.damage += 10; // либо определенная прибавка, либо умножение на коэфициент
    }
}

public class Armor: Items
{
    public int level;
    public int armor;
    public int id;

    public Armor(int _armor)
    {
        level = 1;
        armor = _armor;
    }

    // левел ап брони
    public void levelUp()
    {
        this.level++;
        this.armor += 10; // либо определенная прибавка, либо умножение на коэфициент
    }
}