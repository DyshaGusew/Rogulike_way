public class Item
{

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