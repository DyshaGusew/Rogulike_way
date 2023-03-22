


// Желательно сделать специальный список, где будут значения для определенных предметов (а может даже отдельный
// файл) и подгружать их при создании экземпляров предметов



public class Borders
{
    public int[] coordinates;
    public Borders()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public Borders(int x, int y) : this()
    {
        coordinates = new int[2] { x, y };
    }
}

public class Items
{
    public int[] coordinates;
    public string name;
    public Items(string _name)
    {
        coordinates = new int[2] { 0, 0 };
        name = _name;
    }

    public Items(int x, int y, string _name) : this(_name)
    {
        coordinates = new int[2] { x, y };
    }
}


public class Weapon : Items
{
    public int level;
    public int damage;
    public int stamina;
    public int id;

    public Weapon(int x, int y, string _name, int _damage, int _stamina) : base(x, y, _name)
    {
        name = _name;
        level = 1;
        damage = _damage;
        stamina = _stamina;
    }

    // левел ап оружия
    public void LevelUp()
    {
        this.level++;
        this.damage += 10; // либо определенная прибавка, либо умножение на коэфициент
    }
}

public class Armor : Items
{
    public int level;
    public int armor;
    public int id;

    public Armor(int x, int y, string _name, int _armor) : base(x, y, _name)
    {
        level = 1;
        armor = _armor;
    }

    // левел ап брони
    public void LevelUp()
    {
        this.level++;
        this.armor += 10; // либо определенная прибавка, либо умножение на коэфициент
    }
}