

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

    public Weapon() : base() {}

    // левел ап оружия
    public void LevelUp()
    {
        this.level++;
        this.damage += 10;
    }
}

public class WoodSword : Weapon
{
    public WoodSword() : base() 
    {
        name = "Деревянный меч";
        level = 1;
        damage = 10;
        stamina = 5;
    }
}

public class StoneSword : Weapon
{
    public StoneSword() : base()
    {
        name = "Каменный меч";
        level = 2;
        damage = 15;
        stamina = 8;
    }
}

public class SteelSword : Weapon
{
    public SteelSword() : base()
    {
        name = "Стальной меч";
        level = 3;
        damage = 20;
        stamina = 10;
    }
}

public class SilverSword : Weapon
{
    public SilverSword() : base()
    {
        name = "Серебряный меч";
        level = 4;
        damage = 25;
        stamina = 13;
    }
}

public class JadeSword : Weapon
{
    public JadeSword() : base()
    {
        name = "Нефритовый меч";
        level = 5;
        damage = 30;
        stamina = 15;
    }
}

public class RookieStick : Weapon
{
    public RookieStick() : base()
    {
        name = "Посох новичка";
        level = 1;
        damage = 15;
        stamina = 8;
    }
}

public class WarriorStick : Weapon
{
    public WarriorStick() : base()
    {
        name = "Посох воина";
        level = 2;
        damage = 20;
        stamina = 10;
    }
}

public class EnlightenedStick : Weapon
{
    public EnlightenedStick() : base()
    {
        name = "Посох просвещенного";
        level = 3;
        damage = 25;
        stamina = 13;
    }
}

public class MasterStick : Weapon
{
    public MasterStick() : base()
    {
        name = "Посох магистра";
        level = 4;
        damage = 30;
        stamina = 15;
    }
}

public class GreatStick : Weapon
{
    public GreatStick() : base()
    {
        name = "Посох Великого Мага";
        level = 5;
        damage = 35;
        stamina = 18;
    }
}

public class SmallKnives : Weapon
{
    public SmallKnives() : base()
    {
        name = "Маленькие клинки";
        level = 1;
        damage = 12;
        stamina = 6;
    }
}

public class SteelKnives : Weapon
{
    public SteelKnives() : base()
    {
        name = "Стальные кинжалы";
        level = 2;
        damage = 17;
        stamina = 9;
    }
}

public class SilverKnives : Weapon
{
    public SilverKnives() : base()
    {
        name = "Серебряные кинжалы";
        level = 3;
        damage = 22;
        stamina = 11;
    }
}

public class SteelSwords : Weapon
{
    public SteelSwords() : base()
    {
        name = "Стальные одноручные мечи";
        level = 4;
        damage = 27;
        stamina = 14;
    }
}

public class SilverSwords : Weapon
{
    public SilverSwords() : base()
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

    public Armor() : base() {}

    // левел ап брони
    public void LevelUp()
    {
        this.level++;
        this.armor += 10; // либо определенная прибавка, либо умножение на коэфициент
    }
}

public class LeatherArmor : Armor
{
    public LeatherArmor() : base()
    {
        name = "Кожаный плащ";
        level = 1;
        armor = 10;
    }
}

public class MailArmor : Armor
{
    public MailArmor() : base()
    {
        name = "Кольчуга воина";
        level = 2;
        armor = 20;
    }
}
public class KnightArmor : Armor
{
    public KnightArmor() : base()
    {
        name = "Латы рыцаря";
        level = 3;
        armor = 30;
    }
}

public class JadeArmor : Armor
{
    public JadeArmor() : base()
    {
        name = "Нефритовый панцирь";
        level = 4;
        armor = 40;
    }
}

public class MagicArmor : Armor
{
    public MagicArmor() : base()
    {
        name = "Доспехи мага";
        level = 5;
        armor = 50;
    }
}

// Зелье восстановления ХП
public class HealingPotion: Items {
    public int heal;

    public HealingPotion(int _heal) : base()
    {
        heal = _heal;
    }
}

// Зелье восстановления выносливости
public class StaminaPotion : Items
{
    public int stamina;

    public StaminaPotion(int _stamina) : base()
    {
        stamina = _stamina;
    }
}