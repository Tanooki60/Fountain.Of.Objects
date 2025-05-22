FountainOfObjects.Main();

public class FountainOfObjects
{
    public static void Main()
    {
        World world = new World();

        world.SetWorld();
        world.GetRoom();
    }

    private bool HasWon(Room room)
    {
        if (Room.FountainIsActive == true && (room.Column == 0 && room.Row == 0))
            return true;
        return false;
    }
}

public class World
{
    public static Room[,] Rooms = new Room[4, 4];
    public static Room CurrentRoom { get; set; }
    
    public void SetCurrentRoom(Room room) => CurrentRoom = room;

    public static Room GetRoom(int column, int row)
    {
        return Rooms[CurrentRoom.Column + column, CurrentRoom.Row + row];
    }

    public void SetWorld()
    {
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
            {
                if (i == 0 && j == 0)
                {
                    Rooms[i, j] = new Room(0, 0, RoomState.Entrance);
                    continue;
                }
                if (i == 0 && j == 2)
                {
                    Rooms[i, j] = new Room(0, 2, RoomState.FountainOfObjects);
                    continue;
                }
                
                Rooms[i, j] = new Room(0, 2, RoomState.Nothing);
            }
    }

    public static void Move(string movement)
    {
        if (CurrentRoom.Column < 3 && movement == "move north")
            CurrentRoom = GetRoom(1, 0);
    }
}

public class Room
{
    public int Row { get; }
    public int Column { get; }
    public RoomState RoomState { get; }
    
    public static bool FountainIsActive { get; set; }

    public Room(int row, int column, RoomState roomState)
    {
        Row = row;
        Column = column;
        RoomState = roomState;
    }
}

public enum RoomState {Nothing, Entrance, FountainOfObjects}