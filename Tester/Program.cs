Loop.MainLoop();
class Loop
{
    public static void MainLoop()
    {
        string[] menu = ["Menu", "Wybierz miejsce", "Sprawdź wolne miejsca"];
        string[] y = new string[4];
        string[] x = new string[4];
        List<string> seatList = Initializer.SeatListCreater(y, x);
        switch (MenuHighlight.Highlight(menu))
        {
            case 1: StateChanger.Changer(seatList, x, y); break;
            case 2: Drawer.DrawBoard(seatList); break;
        }
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
    public static void Changer(List<string> seatList, string[] x, string[] y)
    {
        int[] tab = Highlighter.Highlight(x, y, seatList);
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        seatList[(tab[1] * x.Length) + tab[0]] = "X";
        Console.SetCursorPosition(0, 0);
        Drawer.DrawBoard(seatList);
    }
}
class MoveCursor
{
    public static void MoveUp(string[] y) => Console.CursorTop = (Console.CursorTop - 1 < 0) ? y.Length - 1 : (Console.CursorTop - 1) % (y.Length - 1);
    public static void MoveDown(string[] y) => Console.CursorTop = (Console.CursorTop + 1 > 3) ? 0 : (Console.CursorTop + 1) % y.Length;
    public static void MoveLeft(string[] x) => Console.CursorLeft = (Console.CursorLeft - 1 < 0) ? x.Length - 1 : Console.CursorLeft - 1;
    public static void MoveRight(string[] x) => Console.CursorLeft = (Console.CursorLeft + 1 > 3) ? 0 : (Console.CursorLeft + 1) % x.Length;
}
class MoveCursorOnMenu
{
    public static void MoveUp(string[] menu) => Console.CursorTop = (Console.CursorTop - 1 < 1) ? menu.Length - 1 : (Console.CursorTop - 1) % menu.Length;
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
class Coloring
{
    public static void Color(int left, int right)
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Write("O");
        Console.SetCursorPosition(left, right);
    }
    public static void UnColor(int left, int right)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write("O");
        Console.SetCursorPosition(left, right);
    }
}
class MoveAndColor
{
    public static void MoveUpAndColor(string[] y)
    {
        Coloring.UnColor(Console.CursorLeft, Console.CursorTop);
        MoveCursor.MoveUp(y);
        Coloring.Color(Console.CursorLeft % y.Length, Console.CursorTop);
    }
    public static void MoveDownAndColor(string[] y)
    {
        Coloring.UnColor(Console.CursorLeft, Console.CursorTop);
        MoveCursor.MoveDown(y);
        Coloring.Color(Console.CursorLeft % y.Length, Console.CursorTop);
    }
    public static void MoveRightAndColor(string[] x)
    {
        Coloring.UnColor(Console.CursorLeft, Console.CursorTop);
        MoveCursor.MoveRight(x);
        Coloring.Color(Console.CursorLeft % x.Length, Console.CursorTop);
    }
    public static void MoveLeftAndColor(string[] x)
    {
        Coloring.UnColor(Console.CursorLeft, Console.CursorTop);
        MoveCursor.MoveLeft(x);
        Coloring.Color(Console.CursorLeft % x.Length, Console.CursorTop);
    }
}
class Drawer
{
    public static void DrawBoard(List<string> SeatList)
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
        for (int i = 0; i < SeatList.Count; i++)
        {
            Console.Write((i % 4 == 3) ? SeatList[i] + "\n" : SeatList[i]);
        }
    }
}
class Highlighter
{
    public static int[] Highlight(string[] x, string[] y, List<string> seatList)
    {
        int[] number = new int[2];
        Console.Clear();
        Drawer.DrawBoard(seatList);
        Console.CursorTop = 0;
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow: MoveAndColor.MoveUpAndColor(y); break;
                case ConsoleKey.DownArrow: MoveAndColor.MoveDownAndColor(y); break;
                case ConsoleKey.RightArrow: MoveAndColor.MoveRightAndColor(x); break;
                case ConsoleKey.LeftArrow: MoveAndColor.MoveLeftAndColor(x); break;
                case ConsoleKey.Enter: number = [Console.CursorLeft, Console.CursorTop]; break;
            }
        } while (key != ConsoleKey.Enter);
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
