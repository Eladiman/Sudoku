namespace Sudoku.src.Entities.Models;
/// <summary>
/// The following class responsible for representing a coordinate in board
/// represents by (x,y)
/// </summary>
public class Coordinate
{
    private int _x;
    private int _y;

    public Coordinate()
    {
        _x = 0; _y = 0;
    }
    public Coordinate(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    public int X
    {
        get { return _x; }
        set { _x = value; }
    }

    public int Y
    {
        get { return _y; }
        set { _y = value; }
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        Coordinate other = (Coordinate)obj;
        if (_x == other._x && _y == other._y) return true;
        return false;
    }
}
