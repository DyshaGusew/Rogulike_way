

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

    public void DrawBorder()
    {
        for (int i = 0; i < 30; i++)
        {
            Console.Write("█");
        }
        Console.Write("\n");
    }

    public void DrawSpace()
    {
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

    public void WriteButtonName()
    {
        int length = this.name.Length;
        int addition = 0;
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

    public void DrawButton()
    {
        if (isSelected)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        } else
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        DrawBorder();
        DrawSpace();
        WriteButtonName();
        DrawSpace();
        DrawBorder();

        Console.ForegroundColor = ConsoleColor.White;
    }
}

public class Menu
{
    public Menu() {}

    public void DrawButtons(Button button_1,  Button button_2)
    {
        button_1.DrawButton();
        Console.Write("\n\n");
        button_2.DrawButton();
    }

    public void Show()
    {
        Button start = new Button("Начать", true);
        Button exit = new Button("Выйти", false);
        Button[] buttons = { start, exit };
        Button current_button = start;

        ConsoleKeyInfo keyInfo;
        bool a = true;
        while (a)
        {
            Console.Clear();
            DrawButtons(start, exit);
            keyInfo = Console.ReadKey(true);
            switch (keyInfo.KeyChar)
            {
                case 'w': 
                    if (current_button == buttons[0])
                    {
                        current_button.isSelected = false;
                        current_button = buttons[1];
                        current_button.isSelected = true;
                    } else
                    {
                        current_button.isSelected = false;
                        current_button = buttons[0];
                        current_button.isSelected = true;
                    }
                    break;
                case 's':
                    if (current_button == buttons[0])
                    {
                        current_button.isSelected = false;
                        current_button = buttons[1];
                        current_button.isSelected = true;
                    }
                    else
                    {
                        current_button.isSelected = false;
                        current_button = buttons[0];
                        current_button.isSelected = true;
                    }
                    break;
                case 'e':
                    if (current_button == buttons[0])
                    {
                        Console.Clear();
                        Console.WriteLine("Game started");
                        a = false;
                    } else
                    {
                        Environment.Exit(0);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}