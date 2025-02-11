namespace Sudoku.src.Entities.Models;
/// <summary>
/// The following class responsible for representing a coordinate in board
/// represents by (x,y)
/// </summary>
public class Coordinate
{
    private int x;
    private int y;

    public Coordinate()
    {
        x = 0; y = 0;
    }
    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int X
    {
        get { return x; }
        set { x = value; }
    }

    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        Coordinate other = (Coordinate)obj;
        if (x == other.x && y == other.y) return true;
        return false;
    }
}
