

// Желательно сделать специальный список, где будут значения для определенных предметов (а может даже отдельный
// файл) и подгружать их при создании экземпляров предметов


// Общий класс для всех предметов
using static System.Net.Mime.MediaTypeNames;

public class Items
{
    public int level;
    public string name = "";

    static readonly Items WoodSword = new Weapon("Деревянный меч", 1, 10, 5);
    static readonly Items RookieStick = new Weapon("Посох новичка", 1, 15, 8);
    static readonly Items SmallKnives = new Weapon("Маленькие клинки", 1, 12, 6);
    static readonly Items LeatherArmor = new Armor("Кожанный плащ", 1, 10);
    static readonly Items Hil1 = new HealingPotion(1, 30);
    static readonly Items Stam1 = new StaminaPotion(1, 20);

    static readonly Items StounSword = new Weapon("Каменный меч", 2, 15, 8);
    static readonly Items WarriorStick = new Weapon("Посох воина", 2, 20, 10);
    static readonly Items SteelKnives = new Weapon("Стальные кинжалы", 2, 17, 9);
    static readonly Items MailArmor = new Armor("Кольчуга воина", 2, 20);
    static readonly Items Hil2 = new HealingPotion(2, 35);
    static readonly Items Stam2 = new StaminaPotion(1, 30);

    static readonly Items SteelSword = new Weapon("Стальной меч", 3, 20, 10);
    static readonly Items EnlightenedStick = new Weapon("Посох просвещенного", 3, 25, 13);
    static readonly Items SilverKnives = new Weapon("Серебряные кинжалы", 3, 22, 11);
    static readonly Items KnightArmor = new Armor("Латы рыцаря", 3, 30);
    static readonly Items Hil3 = new HealingPotion(3, 35);
    static readonly Items Stam3 = new StaminaPotion(1, 35);

    static readonly Items SilverSword = new Weapon("Серебрянный меч", 4, 25, 13);
    static readonly Items MasterStick = new Weapon("Посох магистра", 4, 30, 15);
    static readonly Items SteelSwords = new Weapon("Стальные одноручные мечи", 4, 27, 14);
    static readonly Items JadeArmor = new Armor("Нефритовый панцирь", 4, 40);
    static readonly Items Hil4 = new HealingPotion(4, 40);
    static readonly Items Stam4 = new StaminaPotion(1, 40);

    static readonly Items JadeSword = new Weapon("Нефритовый меч", 5, 30, 15);
    static readonly Items GreatStick = new Weapon("Посох Великого Мага", 5, 35, 18);
    static readonly Items SilverSwords = new Weapon("Серебряные одноручные мечи", 5, 32, 16);
    static readonly Items MagicArmor = new Armor("Доспехи мага", 5, 50);
    static readonly Items Hil5  = new HealingPotion(5, 50);
    static readonly Items Stam5 = new StaminaPotion(1, 45);




    public List<Items> itemsArr1 = CreateItemsArr(1);
    public List<Items> itemsArr2 = CreateItemsArr(2);
    public List<Items> itemsArr3 = CreateItemsArr(3);
    public List<Items> itemsArr4 = CreateItemsArr(4);
    public List<Items> itemsArr5 = CreateItemsArr(5);

    //Заполняю массивы предметов в зависимости от уровня
    static List<Items>  CreateItemsArr(int level)
    {
        List<Items> itemsArr = new List<Items>();
        if (level == 1)
        {
            itemsArr.Add(WoodSword); itemsArr.Add(RookieStick); itemsArr.Add(SmallKnives); itemsArr.Add(LeatherArmor); itemsArr.Add(Hil1);
        }
    
        else if (level == 2)
        {
            itemsArr.Add(StounSword); itemsArr.Add(WarriorStick); itemsArr.Add(SteelKnives); itemsArr.Add(MailArmor); itemsArr.Add(Hil2);
        }
         
        else if (level == 3)
        {
            itemsArr.Add(SteelSword); itemsArr.Add(EnlightenedStick); itemsArr.Add(SilverKnives); itemsArr.Add(KnightArmor); itemsArr.Add(Hil3);
        } 
       
        else if (level == 4)
        {
            itemsArr.Add(SilverSword); itemsArr.Add(MasterStick); itemsArr.Add(SteelSwords); itemsArr.Add(JadeArmor); itemsArr.Add(Hil4);
        }

        else if (level == 5)
        {
            itemsArr.Add(JadeSword); itemsArr.Add(GreatStick); itemsArr.Add(SilverSwords); itemsArr.Add(MagicArmor); itemsArr.Add(Hil5);
        }
        
        return itemsArr;
    }
}

// Оружие (5 мечей, 5 посохов, 5 клинков для бродяги[ассассина])
public class Weapon : Items
{

    public int damage;
    public int stamina;
    public int id;

    public Weapon() : base() {}
    public Weapon(string name_, int level_, int damage_, int stamina_) : base() {
        this.level = level_;
        this.damage = damage_;
        this.stamina = stamina_;
        this.name = name_;
    
    }
}


// Броня
public class Armor : Items
{
    public int armor;
    public int id;

    public Armor() : base() {}

    public Armor(string name, int level, int arm) : base() { 
        this.name = name;
        this.level = level;
        this.armor = arm; 
    }
}


// Зелье восстановления ХП
public class HealingPotion: Items {
    public int heal;
    public new string name = "Зелье здоровья";
    public HealingPotion(int level, int heal) : base()
    {
        this.level = level;
        this.heal = heal;
    }
}

// Зелье восстановления выносливости
public class StaminaPotion : Items
{
    public int stamina;
    public new string name = "Зелье выносливости";

    public StaminaPotion(int level, int stamina) : base()
    {
        this.level = level;
        this.stamina = stamina;
    }
}