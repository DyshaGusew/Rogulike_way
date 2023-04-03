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
        }
        else
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
public class Invenary
{
    public char ammunition = "";
    public bool isAmmunitionChosen = false;
    public Invenary() { }
}