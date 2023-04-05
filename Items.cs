

// Желательно сделать специальный список, где будут значения для определенных предметов (а может даже отдельный
// файл) и подгружать их при создании экземпляров предметов


// Общий класс для всех предметов
using static System.Net.Mime.MediaTypeNames;

public class Items
{
    public string name = "";
    public int[] coordinates;
    public Items()
    {
        coordinates = new int[2] { 0, 0 };
    }

    public Items(int x, int y)
    {
        coordinates = new int[2] { x, y };
    }
}

// Оружие (5 мечей, 5 посохов, 5 клинков для бродяги[ассассина])
public class Weapon : Items
{
    public int level;
    public int damage;
    public int stamina;
    public int id;

    public Weapon(int x, int y) : base(x, y) {}

    // левел ап оружия
    public void LevelUp()
    {
        this.level++;
        this.damage += 10;
    }
}

public class WoodSword : Weapon
{
    public WoodSword(int x, int y) : base(x, y) 
    {
        name = "Деревянный меч";
        level = 1;
        damage = 10;
        stamina = 5;
    }
}

public class StoneSword : Weapon
{
    public StoneSword(int x, int y) : base(x, y)
    {
        name = "Каменный меч";
        level = 2;
        damage = 15;
        stamina = 8;
    }
}

public class SteelSword : Weapon
{
    public SteelSword(int x, int y) : base(x, y)
    {
        name = "Стальной меч";
        level = 3;
        damage = 20;
        stamina = 10;
    }
}

public class SilverSword : Weapon
{
    public SilverSword(int x, int y) : base(x, y)
    {
        name = "Серебряный меч";
        level = 4;
        damage = 25;
        stamina = 13;
    }
}

public class JadeSword : Weapon
{
    public JadeSword(int x, int y) : base(x, y)
    {
        name = "Нефритовый меч";
        level = 5;
        damage = 30;
        stamina = 15;
    }
}

public class RookieStick : Weapon
{
    public RookieStick(int x, int y) : base(x, y)
    {
        name = "Посох новичка";
        level = 1;
        damage = 15;
        stamina = 8;
    }
}

public class WarriorStick : Weapon
{
    public WarriorStick(int x, int y) : base(x, y)
    {
        name = "Посох воина";
        level = 2;
        damage = 20;
        stamina = 10;
    }
}

public class EnlightenedStick : Weapon
{
    public EnlightenedStick(int x, int y) : base(x, y)
    {
        name = "Посох просвещенного";
        level = 3;
        damage = 25;
        stamina = 13;
    }
}

public class MasterStick : Weapon
{
    public MasterStick(int x, int y) : base(x, y)
    {
        name = "Посох магистра";
        level = 4;
        damage = 30;
        stamina = 15;
    }
}

public class GreatStick : Weapon
{
    public GreatStick(int x, int y) : base(x, y)
    {
        name = "Посох Великого Мага";
        level = 5;
        damage = 35;
        stamina = 18;
    }
}

public class SmallKnives : Weapon
{
    public SmallKnives(int x, int y) : base(x, y)
    {
        name = "Маленькие клинки";
        level = 1;
        damage = 12;
        stamina = 6;
    }
}

public class SteelKnives : Weapon
{
    public SteelKnives(int x, int y) : base(x, y)
    {
        name = "Стальные кинжалы";
        level = 2;
        damage = 17;
        stamina = 9;
    }
}

public class SilverKnives : Weapon
{
    public SilverKnives(int x, int y) : base(x, y)
    {
        name = "Серебряные кинжалы";
        level = 3;
        damage = 22;
        stamina = 11;
    }
}

public class SteelSwords : Weapon
{
    public SteelSwords(int x, int y) : base(x, y)
    {
        name = "Стальные одноручные мечи";
        level = 4;
        damage = 27;
        stamina = 14;
    }
}

public class SilverSwords : Weapon
{
    public SilverSwords(int x, int y) : base(x, y)
    {
        name = "Серебряные одноручные мечи";
        level = 5;
        damage = 32;
        stamina = 16;
    }
}

// Броня
public class Armor : Items
{
    public int level;
    public int armor;
    public int id;

    public Armor(int x, int y) : base(x, y) {}

    // левел ап брони
    public void LevelUp()
    {
        this.level++;
        this.armor += 10; // либо определенная прибавка, либо умножение на коэфициент
    }
}

public class LeatherArmor : Armor
{
    public LeatherArmor(int x, int y) : base(x, y)
    {
        name = "Кожаный плащ";
        level = 1;
        armor = 10;
    }
}

public class MailArmor : Armor
{
    public MailArmor(int x, int y) : base(x, y)
    {
        name = "Кольчуга воина";
        level = 2;
        armor = 20;
    }
}
public class KnightArmor : Armor
{
    public KnightArmor(int x, int y) : base(x, y)
    {
        name = "Латы рыцаря";
        level = 3;
        armor = 30;
    }
}

public class JadeArmor : Armor
{
    public JadeArmor(int x, int y) : base(x, y)
    {
        name = "Нефритовый панцирь";
        level = 4;
        armor = 40;
    }
}

public class MagicArmor : Armor
{
    public MagicArmor(int x, int y) : base(x, y)
    {
        name = "Доспехи мага";
        level = 5;
        armor = 50;
    }
}

// Зелье восстановления ХП
public class HealingPotion: Items {
    public int heal;

    public HealingPotion(int x, int y, int _heal) : base(x, y)
    {
        heal = _heal;
    }
}

// Зелье восстановления выносливости
public class StaminaPotion : Items
{
    public int stamina;

    public StaminaPotion(int x, int y, int _stamina) : base(x, y)
    {
        stamina = _stamina;
    }
}