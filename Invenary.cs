
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections.Concurrent;

public class Cell
{
    public string name;
    public bool isSelected;

    public Cell(string _name, bool _isSelected)
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

    public void WriteCellName(int x, int y)
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

    public void DrawCell(int x, int y)
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
        WriteCellName(x, y + 2);
        DrawSpace(x, y + 3);
        DrawBorder(x, y + 4);

        Console.ForegroundColor = ConsoleColor.White;
    }
}


  


public class Inventory
{

    public string ammunition = "";
    public bool isHeroChosen = false;
    public Inventory() {}
    //List<Items> bag = new List<Items>();    
    //List<Items> hand = new List<Items>();
    List<string> bag = new List<string>(9);
    List<string> hand = new List<string>(9);
    //Пустая функция для переданных предметов
    public void AcceptItem(Items item)
    {
        //bag.Add(item);
        Console.Clear();
        Console.WriteLine($"Предмет {item.name} в инвентаре");
        System.Threading.Thread.Sleep(1500);
       // List<Items> bag = new List<Items>();
    }
    int n;
    
    /*
    public class Items
    {
        string name;
    }
    */



    public void DrawButtons(Cell Cell_1, Cell Cell_2, Cell Cell_3, Cell Cell_4, Cell Cell_5, Cell Cell_6, Cell Cell_7, Cell Cell_8, Cell Cell_9)
    {

        // Console.CursorVisible = false;
        Cell_1.DrawCell(30, 5);
        Cell_2.DrawCell(62, 5);
        Cell_3.DrawCell(94, 5);
        Cell_4.DrawCell(30, 12);
        Cell_5.DrawCell(62, 12);
        Cell_6.DrawCell(94, 12);
        Cell_7.DrawCell(30, 19);
        Cell_8.DrawCell(62, 19);
        Cell_9.DrawCell(94, 19);

        
    }

    public void DrawButtons(Cell Cell_1, Cell Cell_2, Cell Cell_3)
    {

        Console.CursorVisible = false;
        Cell_1.DrawCell(57, 5);
        Cell_2.DrawCell(57, 12);
        Cell_3.DrawCell(57, 19);
        
    }

    public void WriteTip()
    {
        //name[0] = "fsk";
        Console.CursorVisible = false;
        Console.SetCursorPosition(56, 33);
        Console.WriteLine("W - Вверх, S - Вниз, A - Влево, D - Вправо, E - Выбрать, Q - Выйти");
        //Console.WriteLine(namber[0]);
        //Console.WriteLine(Person(0));

    }
    


    public void ChooseAmmunition()
    {
        //namber.Add("посох");
        Console.CursorVisible = false;
        //Console.WriteLine(namber[1]);
        bag.Add("Каменный меч");
        
        bag.Add("Латы царя");

        for (int i=0; i < 10; i++)
         {
             bag.Add("Пусто");
         }
        /*
        bag.Add("Оружие");
        bag.Add("Посох");
        bag.Add("Броня");
        bag.Add("Пусто");
        bag.Add("Пусто");
        bag.Add("Пусто");
        bag.Add("Пусто");
        bag.Add("Пусто");
        bag.Add("Пусто");
        */
        //bool Contains(bag[0] item);
        /*
        internal void PossibleDereferenceNullExamples(Items? messege)
        {
            for (int i = 0; i < 10; i++)
                bag.Add(NULL);
        }
        */

        //string ass = name[0];
        //Console.WriteLine(ass);

        Cell Cell1 = new Cell(bag[0], true);
        
        Cell Cell2 = new Cell(bag[1], false);
        Cell Cell3 = new Cell(bag[2], false);
        Cell Cell4 = new Cell(bag[3], false);
        Cell Cell5 = new Cell(bag[4], false);
        Cell Cell6 = new Cell(bag[5], false);
        Cell Cell7 = new Cell(bag[6], false);
        Cell Cell8 = new Cell(bag[7], false);
        Cell Cell9 = new Cell(bag[8], false);
        /*
        Cell Cell1 = new Cell();
        if (bag[0] == NULL)
            Cell1("Пусто", true);
        else
            Cell1(bag[0].name, true);
        */


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
                    if (Cell1.isSelected)
                    {
                        n = 0;
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }
                    else if (Cell2.isSelected)
                    {
                        n = 1;
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }
                    else if (Cell3.isSelected)
                    {
                        n = 2;
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }
                    else if (Cell4.isSelected)
                    {
                        n = 3;
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }
                    else if (Cell5.isSelected)
                    {
                        n = 4;
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }
                    else if (Cell6.isSelected)
                    {
                        n = 5;
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }
                    else if (Cell7.isSelected)
                    {
                        n = 6; 
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }
                    else if (Cell8.isSelected)
                    {
                        n = 7;
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }
                    else if (Cell9.isSelected)
                    {
                        n = 8;
                        Console.Clear();
                        SelectAction();
                        continue_cycle = false;
                    }

                    break;

                case 'q' or 'й':
                    Console.Clear();
                    return ;

                default: break;

            }
            
        }
    }
    public void SelectAction()
    {

        Console.CursorVisible = false;
        Cell choice = new Cell("Взять", true);
        Cell blowoutn = new Cell("Выбросить", false);
        Cell cancellation = new Cell("Отмена", false);


        ConsoleKeyInfo keyInfo;
        bool continue_cycle = true;

        while (continue_cycle)
        {
            Console.Clear();
            Console.SetCursorPosition(62, 2);
            DrawButtons(choice, blowoutn, cancellation);
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
                    else if (cancellation.isSelected)
                    {
                        cancellation.isSelected = false;
                        choice.isSelected = true;
                    }

                    break;
                case 'e' or 'у':
                    
                    if (choice.isSelected)
                    {
                        hand.Add(bag[n]);
                        Console.Clear();
                        ChooseAmmunition();

                    }
                    else if (blowoutn.isSelected)
                    {
                        bag.RemoveAt(n);
                        bag.Add("Пусто");
                        Console.Clear();
                        ChooseAmmunition();
                    }
                   
                    
                    if (cancellation.isSelected)
                    {
                        Console.Clear();
                        ChooseAmmunition();
                    }
                    break;
                case 'q' or 'й':
                    Console.Clear();
                    return;

                default: break;
            }
        }
    }
}

