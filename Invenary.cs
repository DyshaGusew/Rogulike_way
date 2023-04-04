/*  Ничего не работает
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

    public string hero_class = "";
    public bool isHeroChosen = false;
    public Invenary() { }

    public void DrawButtons(Button button_1, Button button_2, Button button_3, Button 4, Button button_5, Button button_6, Button button_7, Button button_8, Button button_9)
    {

        Console.CursorVisible = false;
        button_1.DrawButton(10, 5);
        Console.Write("  ");
        button_2.DrawButton(27, 5);
        Console.Write("  ");
        button_3.DrawButton(44, 5);
        Console.Write("\n\n");
        button_3.DrawButton(10, 12);
        Console.Write("  ");
        button_5.DrawButton(27, 12);
        Console.Write("  ");
        button_6.DrawButton(44, 12);
        Console.Write("\n\n");
        button_7.DrawButton(10, 19);
        Console.Write("  ");
        button_8.DrawButton(27, 19);
        Console.Write("  ");
        button_9.DrawButton(44, 19);
    }

    public void DrawButtons(Button button_1, Button button_2, Button button_3,)
    {

        Console.CursorVisible = false;
        button_1.DrawButton(57, 5);
        Console.Write("\n\n");
        button_2.DrawButton(57, 12);
        Console.Write("\n\n");
        button_3.DrawButton(57, 19);
        
    }

    public void WriteTip()
    {

        Console.CursorVisible = false;
        Console.SetCursorPosition(56, 33);
        Console.WriteLine("W - Вверх, S - Вниз, A - Влево, D - Вправо, E - Выбрать, Q - Выйти");
    }



    public void ChooseAmmunition()
    {

        Console.CursorVisible = false;
        Button Cell1 = new Button("Посох", true);
        Button Cell2 = new Button("Оружие", false);
        Button Cell3 = new Button("Броня", false);
        Button Cell4 = new Button("Зелье", false);
        Button Cell5 = new Button("Пусто", false);
        Button Cell6 = new Button("Пусто", false);
        Button Cell7 = new Button("Пусто", false);
        Button Cell8 = new Button("Пусто", false);
        Button Cell9 = new Button("Пусто", false);

        ConsoleKeyInfo keyInfo;
        bool continue_cycle = true;

        while (continue_cycle)
        {
            Console.Clear();
            Console.SetCursorPosition(62, 2);
            DrawButtons(Cell1, Cell2, Cell3, Cell4, Cell5, Cell6, Cell7, Cell8, Cell9);
            WriteTip();

            keyInfo = Console.ReadKey(true);
            switch (keyInfo.KeyChar)
            {
                case 'w' or 'ц':
                    if (Cell1.isSelected)
                    {
                        Cell1.isSelected = false;
                        Cell7.isSelected = true;
                    }
                    else if (Cell4.isSelected)
                    {
                        Cell4.isSelected = false;
                        Cell1.isSelected = true;
                    }
                    else if (Cell7.isSelected)
                    {
                        Cell7.isSelected = false;
                        Cell4.isSelected = true;
                    }
                    else if (Cell2.isSelected)
                    {
                        Cell2.isSelected = false;
                        Cell8.isSelected = true;
                    }
                    else if (Cell5.isSelected)
                    {
                        Cell5.isSelected = false;
                        Cell2.isSelected = true;
                    }
                    else if (Cell8.isSelected)
                    {
                        Cell8.isSelected = false;
                        Cell5.isSelected = true;
                    }
                    else if (Cell3.isSelected)
                    {
                        Cell3.isSelected = false;
                        Cell9.isSelected = true;
                    }
                    else if (Cell6.isSelected)
                    {
                        Cell6.isSelected = false;
                        Cell3.isSelected = true;
                    }
                    else if (Cell9.isSelected)
                    {
                        Cell9.isSelected = false;
                        Cell6.isSelected = true;
                    }
                    break;
                case 's' or 'ы':
                    if (Cell1.isSelected)
                    {
                        Cell1.isSelected = false;
                        Cell4.isSelected = true;
                    }
                    else if (Cell4.isSelected)
                    {
                        Cell4.isSelected = false;
                        Cell7.isSelected = true;
                    }
                    else if (Cell7.isSelected)
                    {
                        Cell7.isSelected = false;
                        Cell1.isSelected = true;
                    }
                    else if (Cell2.isSelected)
                    {
                        Cell2.isSelected = false;
                        Cell5.isSelected = true;
                    }
                    else if (Cell5.isSelected)
                    {
                        Cell5.isSelected = false;
                        Cell8.isSelected = true;
                    }
                    else if (Cell8.isSelected)
                    {
                        Cell8.isSelected = false;
                        Cell2.isSelected = true;
                    }
                    else if (Cell3.isSelected)
                    {
                        Cell3.isSelected = false;
                        Cell6.isSelected = true;
                    }
                    else if (Cell6.isSelected)
                    {
                        Cell6.isSelected = false;
                        Cell9.isSelected = true;
                    }
                    else if (Cell9.isSelected)
                    {
                        Cell9.isSelected = false;
                        Cell3.isSelected = true;
                    }
                    break;
                case 'd' or 'в':
                    if (Cell1.isSelected)
                    {
                        Cell1.isSelected = false;
                        Cell2.isSelected = true;
                    }
                    else if (Cell2.isSelected)
                    {
                        Cell2.isSelected = false;
                        Cell3.isSelected = true;
                    }
                    else if (Cell3.isSelected)
                    {
                        Cell3.isSelected = false;
                        Cell1.isSelected = true;
                    }
                    else if (Cell4.isSelected)
                    {
                        Cell4.isSelected = false;
                        Cell5.isSelected = true;
                    }
                    else if (Cell5.isSelected)
                    {
                        Cell5.isSelected = false;
                        Cell6.isSelected = true;
                    }
                    else if (Cell6.isSelected)
                    {
                        Cell6.isSelected = false;
                        Cell4.isSelected = true;
                    }
                    else if (Cell7.isSelected)
                    {
                        Cell7.isSelected = false;
                        Cell8.isSelected = true;
                    }
                    else if (Cell8.isSelected)
                    {
                        Cell8.isSelected = false;
                        Cell9.isSelected = true;
                    }
                    else if (Cell9.isSelected)
                    {
                        Cell9.isSelected = false;
                        Cell7.isSelected = true;
                    }
                    break;

                case 'a' or 'ф':
                    if (Cell1.isSelected)
                    {
                        Cell1.isSelected = false;
                        Cell3.isSelected = true;
                    }
                    else if (Cell2.isSelected)
                    {
                        Cell2.isSelected = false;
                        Cell1.isSelected = true;
                    }
                    else if (Cell3.isSelected)
                    {
                        Cell3.isSelected = false;
                        Cell2.isSelected = true;
                    }
                    else if (Cell4.isSelected)
                    {
                        Cell4.isSelected = false;
                        Cell6.isSelected = true;
                    }
                    else if (Cell5.isSelected)
                    {
                        Cell5.isSelected = false;
                        Cell4.isSelected = true;
                    }
                    else if (Cell6.isSelected)
                    {
                        Cell6.isSelected = false;
                        Cell5.isSelected = true;
                    }
                    else if (Cell7.isSelected)
                    {
                        Cell7.isSelected = false;
                        Cell9.isSelected = true;
                    }
                    else if (Cell8.isSelected)
                    {
                        Cell8.isSelected = false;
                        Cell7.isSelected = true;
                    }
                    else if (Cell9.isSelected)
                    {
                        Cell9.isSelected = false;
                        Cell8.isSelected = true;
                    }
                    break;

                case 'e' or 'у':
                    if (Cell9.isSelected)
                    {
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                        if (this.isHeroChosen)
                        {
                            continue_cycle = false;
                        }
                    }

                    break;
                default: break;
            }
        }
    }
    public void SelectAction()
    {

        Console.CursorVisible = false;
        Button choice = new Button("Взять", true);
        Button blowoutn = new Button("Выбросить", false);
        Button cancellation = new Button("Отмена", false);


        ConsoleKeyInfo keyInfo;
        bool continue_cycle = true;

        while (continue_cycle)
        {
            Console.Clear();
            Console.SetCursorPosition(62, 2);
            DrawButtons(wizard, barbarian, prowler, back);
            WriteTip();

            keyInfo = Console.ReadKey(true);
            switch (keyInfo.KeyChar)
            {
                case 'w' or 'ц':
                    if (choice.isSelected)
                    {
                        choice.isSelected = false;
                        cancellation.isSelected = true;
                    }
                    else if (blowoutn.isSelected)
                    {
                        blowoutn.isSelected = false;
                        choice.isSelected = true;
                    }
                    else if (cancellation.isSelected)
                    {
                        cancellation.isSelected = false;
                        blowoutn.isSelected = true;
                    }
                    break;
                case 's' or 'ы':
                    if (choice.isSelected)
                    {
                        choice.isSelected = false;
                        blowoutn.isSelected = true;
                    }
                    else if (blowoutn.isSelected)
                    {
                        blowoutn.isSelected = false;
                        cancellation.isSelected = true;
                    }
                    else if (prowler.isSelected)
                    {
                        cancellation.isSelected = false;
                        choice.isSelected = true;
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
                    }
                    else
                    {
                        continue_cycle = false;
                    }
                    break;
                default: break;
            }
        }
    }
}


*/
