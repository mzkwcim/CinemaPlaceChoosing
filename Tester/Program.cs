
Loop.MainLoop();
class Loop
{
    public static string[] y = new string[6];
    public static string[] x = new string[12];
    public static void MainLoop()
    {
        string[] menu = ["Menu", "Wybierz miejsce", "Sprawdź wolne miejsca", "Odwołaj rezerwacje", "Sprawdź liczbę wolnych miejsc", "Wyjdź z kina"];
        List<string> seatList = Initializer.SeatListCreater(y, x);
        bool koniec = true;
        while (koniec)
        {
            switch (MenuHighlight.Highlight(menu))
            {
                case 0: break;
                case 1: seatList = StateChanger.Changer(seatList, "X"); break;
                case 2:DrawBoardHelper(seatList); break;
                case 3: seatList = StateChanger.Changer(seatList, "O"); break;
                case 4: FreeSeatCounter.Counter(seatList); break;
                case 5: koniec = Exit.ExitACinema();  break;
            }
        }
    }
    public static void DrawBoardHelper(List<string> seatList)
    {
        Drawer.DrawBoard(seatList);
        Console.WriteLine("\nAby wrócić do menu głównego wciśnij dowolny klawisz");
        Console.ReadKey();
    }
}
class Exit
{
    public static bool ExitACinema()
    {
        BlackAndClear();
        if (YesNo() == 1)
        {
            BlackAndClear();
            return true;
        }
        else
        {
            BlackAndClear();
            Console.WriteLine("Dziękujemy za wizytę w kinie");
            return false;
        }
    }
    public static void BlackAndClear()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
    }
    public static int YesNo()
    {
        int number = 1;
        Console.WriteLine("Czy na pewno chcesz wyjść z kina?");
        string[] response = ["Nie", "Tak"];
        Console.WriteLine($"{response[0]}\n{response[1]}");
        ConsoleKey key;
        Console.SetCursorPosition(0, 1);
        do
        {
            key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow: MoveUpAndColor(response) ; break;
                case ConsoleKey.DownArrow: MoveDownAndColor(response); break;
                case ConsoleKey.Enter: number = Console.CursorTop; break;
            }
        } while (key != ConsoleKey.Enter);
        return number;
    }
    public static void Color(string[] menu)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Write(menu[Console.CursorTop-1]);
    }
    public static void UnColor(string[] menu)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write(menu[Console.CursorTop-1]);
    }
    public static void MoveUpAndColor(string[] menu )
    {
        UnColor(menu);
        MoveUp(menu.Length);
        Color(menu);
    }
    public static void MoveDownAndColor(string[] menu)
    {
        UnColor(menu);
        MoveDown(menu.Length);
        Color(menu);
    }
    public static void MoveUp(int y) => Console.CursorTop = (Console.CursorTop - 1 == 0) ? 2 : 1;
    public static void MoveDown(int y) => Console.CursorTop = (Console.CursorTop + 1 == 3) ? 1 : 2;
}
class FreeSeatCounter
{
    public static void Counter(List<string> seatList)
    {
        Exit.BlackAndClear();
        int adder = 0;
        for (int i = 0; i < seatList.Count; i++)
        {
            adder += (seatList[i] == "O") ? 1 : 0;
        }
        Console.WriteLine($"liczba wolnych miejsc {adder}\n\nkliknij dowolny przycisk, aby wrócić do menu głównego");
        Console.ReadKey();
        Console.Clear();
    }
}
class Initializer
{
    public static List<string> SeatListCreater(string[] x, string[] y)
    {
        List<string> SeatList = new List<string>();
        for (int i = 0; i < x.Length * y.Length; i++)
        {
            SeatList.Add("O");
        }
        return SeatList;
    }
}
class StateChanger
{
    public static List<string> Changer(List<string> seatList, string sign)
    {
        bool koniec = true;
        do
        {
            try
            {
                int[] tab = Highlighter.Highlight(seatList);
                Exit.BlackAndClear();
                if (seatList[(tab[1] * Loop.x.Length) + tab[0]] == sign)
                {
                    Console.WriteLine((sign == "X") ? "Nie możesz wybrać zajętego miejsca\nWciśnij dowolny klawisz aby kontynuować" : "Nie możesz odwołać rezerwacji wolnego miejsca");
                    Console.ReadKey();
                }
                else
                {
                    seatList[(tab[1] * Loop.x.Length) + tab[0]] = sign;
                    Console.SetCursorPosition(0, 0);
                    Drawer.DrawBoard(seatList);
                    Console.WriteLine((sign == "X") ? $"\nwybrałeś miejsce w rzędzie {tab[1] + 1} kolumna {tab[0] + 1}" : $"Odwołałeś rezerwacje miejsca  w rzędzie {tab[1] + 1} kolumnie {tab[0] + 1}");
                    Console.WriteLine("Aby wrócić do menu głównego wciśnij dowolny klawisz");
                    Console.ReadKey();
                    koniec = false;
                }
            }
            catch
            {
                koniec = false;
            }
        } while (koniec);
        return seatList;
    }
}
class MoveCursor
{
    public static void MoveUp(int y) => Console.CursorTop = (Console.CursorTop - 1 < 0) ? (y - 1) : (Console.CursorTop - 1) % y;
    public static void MoveDown(int y) => Console.CursorTop = (Console.CursorTop + 1 > y) ? 0 : (Console.CursorTop + 1) % y;
    public static void MoveLeft(int x) => Console.CursorLeft = (Console.CursorLeft - 1 < 0) ? x + 1 : (Console.CursorLeft - 1 == 4 || Console.CursorLeft - 1 == 9) ? (Console.CursorLeft - 2) % (x + 2) : (Console.CursorLeft - 1) % (x + 2);
    public static void MoveRight(int x) => Console.CursorLeft = (Console.CursorLeft + 1 > x + 1) ? 0 : (Console.CursorLeft + 1 == 4 || Console.CursorLeft + 1 == 9) ? (Console.CursorLeft + 2) % (x + 2) : (Console.CursorLeft + 1) % (x + 2);
}
class MoveCursorOnMenu
{
    public static void MoveUp(string[] menu) => Console.CursorTop = (Console.CursorTop - 1 < 1) ? menu.Length-1 : (Console.CursorTop - 1) % menu.Length;
    public static void MoveDown(string[] menu) => Console.CursorTop = ((Console.CursorTop + 1) % menu.Length == 0) ? 1 : (Console.CursorTop + 1) % menu.Length;
    public static void MoveUpAndColor(string[] menu)
    {
        MenuHighlight.UnColor(menu);
        MoveUp(menu);
        MenuHighlight.Color(menu);
    }
    public static void MoveDownAndColor(string[] menu)
    {
        MenuHighlight.UnColor(menu);
        MoveDown(menu);
        MenuHighlight.Color(menu);
    }
}
class BoardColoring
{
    public static void Color(int x, int y, List<string> seatList)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Write(seatList[(y * seatList.Count / Loop.y.Length) + ((x < 4) ? x : (x > 4 && x < 9) ? x - 1 : x - 2)]);
        Console.SetCursorPosition(x, y);
    }
    public static void UnColor(int x, int y, List<string> seatList)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write(seatList[(y * seatList.Count / Loop.y.Length) + ((x < 4) ? x : (x > 4 && x < 9) ? x - 1 : x - 2)]);
        Console.SetCursorPosition(x, y);
    }
}
class MoveAndColor
{
    public static void MoveUpAndColor(int y, List<string> seatList)
    {
        BoardColoring.UnColor(Console.CursorLeft, Console.CursorTop, seatList);
        MoveCursor.MoveUp(y);
        BoardColoring.Color(Console.CursorLeft, Console.CursorTop, seatList);
    }
    public static void MoveDownAndColor(int y, List<string> seatList)
    {
        BoardColoring.UnColor(Console.CursorLeft, Console.CursorTop, seatList);
        MoveCursor.MoveDown(y);
        BoardColoring.Color(Console.CursorLeft, Console.CursorTop, seatList);
    }
    public static void MoveRightAndColor(int x, List<string> seatList)
    {
        BoardColoring.UnColor(Console.CursorLeft, Console.CursorTop, seatList);
        MoveCursor.MoveRight(x);
        BoardColoring.Color(Console.CursorLeft % (x + 2), Console.CursorTop, seatList);
    }
    public static void MoveLeftAndColor(int x, List<string> seatList)
    {
        BoardColoring.UnColor(Console.CursorLeft, Console.CursorTop, seatList);
        MoveCursor.MoveLeft(x);
        BoardColoring.Color(Console.CursorLeft % (x + 2), Console.CursorTop, seatList);
    }
}
class Drawer
{
    public static void DrawBoard(List<string> SeatList)
    {
        Exit.BlackAndClear();
        for (int i = 0; i < SeatList.Count; i++)
        {
            Console.Write((i % 4 == 3) ? (i % 12 == 11) ? SeatList[i] + "\n" : SeatList[i] + " " : SeatList[i]);
        }
    }
}
class Highlighter
{
    public static int[] Highlight(List<string> seatList)
    {
        int[] number = [0, 0];
        Console.Clear();
        Drawer.DrawBoard(seatList);
        Console.WriteLine("\nAby wrócić do menu głównego wciśnij escape");
        Console.SetCursorPosition(0, 0);
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow: MoveAndColor.MoveUpAndColor(Loop.y.Length, seatList); break;
                case ConsoleKey.DownArrow: MoveAndColor.MoveDownAndColor(Loop.y.Length, seatList); break;
                case ConsoleKey.RightArrow: MoveAndColor.MoveRightAndColor(Loop.x.Length, seatList); break;
                case ConsoleKey.LeftArrow: MoveAndColor.MoveLeftAndColor(Loop.x.Length, seatList); break;
                case ConsoleKey.Enter: number = [(Console.CursorLeft < 4) ? Console.CursorLeft : (Console.CursorLeft > 4 && Console.CursorLeft < 9) ? Console.CursorLeft - 1 : Console.CursorLeft - 2, Console.CursorTop]; break;
                case ConsoleKey.Escape: number = [-1, -1]; break;
            }
        } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);
        return number;
    }
}
class DrawMenu
{
    public static void Menu(string[] menu)
    {
        Console.Clear();
        for (int i = 0; i < menu.Length; i++)
        {
            Console.WriteLine(menu[i]);
        }
    }
}
class MenuHighlight
{
    public static int Highlight(string[] menu)
    {
        int number = 1;
        ConsoleKey key;
        DrawMenu.Menu(menu);
        Console.SetCursorPosition(0, 1);
        do
        {
            Console.CursorVisible = false;
            key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow: MoveCursorOnMenu.MoveUpAndColor(menu); break;
                case ConsoleKey.DownArrow: MoveCursorOnMenu.MoveDownAndColor(menu); break;
                case ConsoleKey.Enter: number = Console.CursorTop; break;
            }
        } while (key != ConsoleKey.Enter);
        return number;
    }
    public static void Color(string[] menu)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Write(menu[Console.CursorTop]);
    }
    public static void UnColor(string[] menu)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write(menu[Console.CursorTop]);
    }
}
