
string[] y = new string[4];
string[] x = new string[4];
Drawer.DrawBoard();
Console.SetCursorPosition(0, 0);
Highlighter.Highlight(x, y);

class MoveCursor
{
    public static void MoveUp(string[] y) => Console.CursorTop = (Console.CursorTop - 1 < 0) ? y.Length - 1 : (Console.CursorTop - 1) % (y.Length - 1);
    public static void MoveDown(string[] y) => Console.CursorTop = (Console.CursorTop + 1 > 3) ? 0 : (Console.CursorTop + 1) % y.Length;
    public static void MoveLeft(string[] x) => Console.CursorLeft = (Console.CursorLeft - 1 < 0) ? x.Length-1 : Console.CursorLeft - 1;
    public static void MoveRight(string[] x) => Console.CursorLeft = (Console.CursorLeft + 1 > 3) ? 0 : (Console.CursorLeft + 1) % x.Length;
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
    public static void MoveUpAndColor(string[] y )
    {
        Coloring.UnColor(Console.CursorLeft,Console.CursorTop);
        MoveCursor.MoveUp(y);
        Coloring.Color(Console.CursorLeft%4, Console.CursorTop);
    }
    public static void MoveDownAndColor(string[] y)
    {
        Coloring.UnColor(Console.CursorLeft, Console.CursorTop);
        MoveCursor.MoveDown(y);
        Coloring.Color(Console.CursorLeft%4, Console.CursorTop);
    }
    public static void MoveRightAndColor(string[] x)
    {
        Coloring.UnColor(Console.CursorLeft, Console.CursorTop);
        MoveCursor.MoveRight(x);
        Coloring.Color(Console.CursorLeft%4, Console.CursorTop);
    }
    public static void MoveLeftAndColor(string[] x)
    {
        Coloring.UnColor(Console.CursorLeft, Console.CursorTop);
        MoveCursor.MoveLeft(x);
        Coloring.Color(Console.CursorLeft%4, Console.CursorTop);
    }
}
class Drawer
{
    public static void DrawBoard()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.Write("O");
            }
            Console.WriteLine();
        }
    }
}
class Highlighter
{
    public static int Highlight(string[] x, string[] y)
    {
        int number = 0;
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
                case ConsoleKey.Enter: number = Console.CursorTop; break;
            }
        } while (key != ConsoleKey.Enter);
        return number;
    }
}