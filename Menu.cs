

// В основном коде создается экземпляр класса Menu (без параметров) и вызывается метод Show().


public class Button
{
    public string name;
    public bool isSelected;

    public Button(string _name, bool _isSelected)
    {
        name = _name;
        isSelected = _isSelected;
    }

    public void DrawBorder(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        for (int i = 0; i < 30; i++)
        {
            Console.Write("█");
        }
        Console.Write("\n");
    }

    public void DrawSpace(int x, int y)
    {
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

    public void DrawButtons(Button button_1,  Button button_2)
    {
        button_1.DrawButton(57, 5);
        Console.Write("\n\n");
        button_2.DrawButton(57, 12);
    }

    public void DrawButtons(Button button_1, Button button_2, Button button_3, Button button_4)
    {
        button_1.DrawButton(57, 5);
        Console.Write("\n\n");
        button_2.DrawButton(57, 12);
        Console.Write("\n\n");
        button_3.DrawButton(57, 19);
        Console.Write("\n\n");
        button_4.DrawButton(57, 26);
    }

    public void WriteTip()
    {
        Console.SetCursorPosition(56, 33);
        Console.WriteLine("W - Вверх, S - Вниз, E - Выбрать");
    }

    public void Show()
    {
        Button start = new Button("Начать", true);
        Button exit = new Button("Выйти", false);

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
                        start.isSelected = false;
                        exit.isSelected = true;
                    } else
                    {
                        exit.isSelected = false;
                        start.isSelected = true;
                    }
                    break;
                case 's' or 'ы':
                    if (start.isSelected)
                    {
                        start.isSelected = false;
                        exit.isSelected = true;
                    }
                    else
                    {
                        exit.isSelected = false;
                        start.isSelected = true;
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
        Button wizard = new Button("Маг", true);
        Button barbarian = new Button("Варвар", false);
        Button prowler = new Button("Бродяга", false);
        Button back = new Button("Назад", false);

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
                        wizard.isSelected = false;
                        back.isSelected = true;
                    }
                    else if (barbarian.isSelected)
                    {
                        barbarian.isSelected = false;
                        wizard.isSelected = true;
                    }
                    else if (prowler.isSelected)
                    {
                        prowler.isSelected = false;
                        barbarian.isSelected = true;
                    } else
                    {
                        back.isSelected = false;
                        prowler.isSelected = true;
                    }
                    break;
                case 's' or 'ы':
                    if (wizard.isSelected)
                    {
                        wizard.isSelected = false;
                        barbarian.isSelected = true;
                    }
                    else if (barbarian.isSelected)
                    {
                        barbarian.isSelected = false;
                        prowler.isSelected = true;
                    }
                    else if (prowler.isSelected)
                    {
                        prowler.isSelected = false;
                        back.isSelected = true;
                    }
                    else
                    {
                        back.isSelected = false;
                        wizard.isSelected = true;
                    }
                    break;
                case 'e' or 'у':
                    if (wizard.isSelected)
                    {
                        this.hero_class = "wizard";
                        continue_cycle = false;
                        this.isHeroChosen = true;
                    }
                    else if (barbarian.isSelected)
                    {
                        this.hero_class = "barbarian";
                        continue_cycle = false;
                        this.isHeroChosen = true;
                    }
                    else if (prowler.isSelected)
                    {
                        this.hero_class = "prowler";
                        continue_cycle = false;
                        this.isHeroChosen = true;
                    } else
                    {
                        continue_cycle = false;
                    }
                    break;
                default: break;
            }
        }
    }
}