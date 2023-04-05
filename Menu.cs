

// В основном коде создается экземпляр класса Menu (без параметров) и вызывается метод Show().


public class MenuButton
{
    public string name;
    public bool isSelected;

    public MenuButton(string _name, bool _isSelected)
    {
        name = _name;
        isSelected = _isSelected;
    }

    public void DrawBorder(int x, int y)
    {

        Console.CursorVisible = false;
        Console.SetCursorPosition(x, y);
        for (int i = 0; i < 30; i++)
        {
            Console.Write("█");
        }
        Console.Write("\n");
    }

    public void DrawSpace(int x, int y)
    {

        Console.CursorVisible = false;
        Console.SetCursorPosition(x, y);
        for (int i = 0; i < 28; i++)
        {
            if (i == 0 || i == 27)
            {
                Console.Write("██");
            }
            else
            {
                Console.Write(" ");
            }
        }
        Console.Write("\n");
    }

    public void WriteButtonName(int x, int y)
    {

        Console.CursorVisible = false;
        int length = this.name.Length;
        int addition = 0;
        Console.SetCursorPosition(x, y);
        Console.Write("██");
        if (length % 2 != 0) addition = 1;
        for (int i = 0; i < ((26 - length) / 2) + addition; i++)
        {
            Console.Write(" ");
        }
        Console.Write(this.name);
        for (int i = 0; i < (26 - length) / 2; i++)
        {
            Console.Write(" ");
        }
        Console.Write("██");
        Console.Write("\n");
    }

    public void DrawButton(int x, int y)
    {

        Console.CursorVisible = false;
        if (isSelected)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        } else
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        DrawBorder(x, y);
        DrawSpace(x, y + 1);
        WriteButtonName(x, y + 2);
        DrawSpace(x, y + 3);
        DrawBorder(x, y + 4);

        Console.ForegroundColor = ConsoleColor.White;
    }
}

public class Menu
{

    public string hero_class = "";
    public bool isHeroChosen = false;
    public Menu() {}

    public void DrawButtons(MenuButton button_1, MenuButton button_2)
    {

        Console.CursorVisible = false;
        button_1.DrawButton(57, 5);
        button_2.DrawButton(57, 12);
    }

    public void DrawButtons(MenuButton button_1, MenuButton button_2, MenuButton button_3, MenuButton button_4)
    {

        Console.CursorVisible = false;
        button_1.DrawButton(57, 5);
        button_2.DrawButton(57, 12);
        button_3.DrawButton(57, 19);
        button_4.DrawButton(57, 26);
    }

    public void WriteTip()
    {

        Console.CursorVisible = false;
        Console.SetCursorPosition(56, 33);
        Console.WriteLine("W - Вверх, S - Вниз, E - Выбрать");
    }

    public void SwitchButton(ref MenuButton from, ref MenuButton to)
    {
        from.isSelected = false;
        to.isSelected = true;
    }

    public void Show()
    {

        Console.CursorVisible = false;
        MenuButton start = new("Начать", true);
        MenuButton exit = new("Выйти", false);

        ConsoleKeyInfo keyInfo;
        bool continue_cycle = true;

       // Console.SetWindowSize(145, 36);
       // Console.SetBufferSize(145, 36);
        Console.CursorVisible = false;

        while (continue_cycle)
        {
            Console.Clear();
            DrawButtons(start, exit);
            WriteTip();

            keyInfo = Console.ReadKey(true);
            switch (keyInfo.KeyChar)
            {
                case 'w' or 'ц': 
                    if (start.isSelected)
                    {
                        SwitchButton(ref start, ref exit);
                    } else
                    {
                        SwitchButton(ref exit, ref start);
                    }
                    break;
                case 's' or 'ы':
                    if (start.isSelected)
                    {
                        SwitchButton(ref start, ref exit);
                    }
                    else
                    {
                        SwitchButton(ref exit, ref start);
                    }
                    break;
                case 'e' or 'у':
                    if (start.isSelected)
                    {
                        Console.Clear();
                        ChooseCharacter();
                        if (this.isHeroChosen)
                        {
                            continue_cycle = false;
                        }
                    } else
                    {
                        Environment.Exit(0);
                    }
                    break;
                default: break;
            }
        }
    }

    public void ChooseCharacter()
    {
        Console.CursorVisible = false;
        MenuButton wizard = new("Маг", true);
        MenuButton barbarian = new("Варвар", false);
        MenuButton prowler = new("Ассасин", false);
        MenuButton back = new("Назад", false);

        ConsoleKeyInfo keyInfo;
        bool continue_cycle = true;

        while (continue_cycle)
        {
            Console.Clear();
            Console.SetCursorPosition(62, 2);
            Console.WriteLine("Выберите класс героя");
            DrawButtons(wizard, barbarian, prowler, back);
            WriteTip();

            keyInfo = Console.ReadKey(true);
            switch (keyInfo.KeyChar)
            {
                case 'w' or 'ц':
                    if (wizard.isSelected)
                    {
                        SwitchButton(ref wizard, ref back);
                    }
                    else if (barbarian.isSelected)
                    {
                        SwitchButton(ref barbarian, ref wizard);
                    }
                    else if (prowler.isSelected)
                    {
                        SwitchButton(ref prowler, ref barbarian);
                    } else 
                    {
                        SwitchButton(ref back, ref prowler);
                    }
                    break;
                case 's' or 'ы':
                    if (wizard.isSelected)
                    {
                        SwitchButton(ref wizard, ref barbarian);
                    }
                    else if (barbarian.isSelected)
                    {
                        SwitchButton(ref barbarian, ref prowler);
                    }
                    else if (prowler.isSelected)
                    {
                        SwitchButton(ref prowler, ref back);
                    }
                    else
                    {
                        SwitchButton(ref back, ref wizard);
                    }
                    break;
                case 'e' or 'у':
                    if (back.isSelected)
                    {
                        continue_cycle = false;
                    } else
                    {
                        if (wizard.isSelected)
                        {
                            this.hero_class = "wizard";
                        }
                        else if (barbarian.isSelected)
                        {
                            this.hero_class = "barbarian";
                        }
                        else if (prowler.isSelected)
                        {
                            this.hero_class = "prowler";
                        }
                        continue_cycle = false;
                        this.isHeroChosen = true;
                    }
                    break;
                default: break;
            }
        }
    }
}