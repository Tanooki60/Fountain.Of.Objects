static void Main()
{
    Console.WriteLine("Hello");
}
public class World
{
    public Room[,] Rooms = new Room[4, 4];
    
}

public class Room
{
    public int Row { get; }
    public int Column { get; }
    public RoomState RoomState { get; }

    public Room(int row, int column, RoomState roomState)
    {
        Row = row;
        Column = column;
        RoomState = roomState;
    }
}

public enum RoomState {Nothing, Entrance, FountainOfObjects}