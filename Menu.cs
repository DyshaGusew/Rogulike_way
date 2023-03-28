﻿

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

        ConsoleKeyInfo keyInfo;
        bool continue_cycle = true;

        Console.SetWindowSize(145, 36);
        Console.SetBufferSize(145, 36);
        Console.CursorVisible = false;

        while (continue_cycle)
        {
            Console.Clear();
            DrawButtons(start, exit);
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
                        continue_cycle = false;
                    } else
                    {
                        Environment.Exit(0);
                    }
                    break;
                default: break;
            }
        }
    }
}