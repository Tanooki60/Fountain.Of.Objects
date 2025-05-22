FountainOfObjects.Main();

public class FountainOfObjects
{
    public static void Main()
    {
        World world = new World();

        world.SetWorld();
        //world.GetRoom(0,0);
        
        Console.WriteLine(World.CurrentRoom.RoomState);
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

    public static Room GetRoom(int row, int column)
    {
        return Rooms[CurrentRoom.Row + row, CurrentRoom.Column + column];
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

        CurrentRoom = Rooms[0, 0];
    }

    public static void Move(string movement)
    {
        if (CurrentRoom.Column < 3 && movement == "move north")
            CurrentRoom = GetRoom(1, 0);
        if (CurrentRoom.Column > 0 && movement == "move south")
            CurrentRoom = GetRoom( -1, 0);
        if (CurrentRoom.Row < 3 && movement == "move east")
            CurrentRoom = GetRoom(0, 1);
        if (CurrentRoom.Row > 0 && movement == "move west")
            CurrentRoom = GetRoom(0, -1);
    }

    public static void ActivateFountainCheck(string command)
    {
        if (!Room.FountainIsActive && command == "enable fountain")
            Room.FountainIsActive = true;
    }

    public string? RoomStateCheck()
    {
        if (CurrentRoom.RoomState == RoomState.Entrance && Room.FountainIsActive)
            return "The Fountain of Objects has been reactivated, and you have escaped with your life!";
        if (CurrentRoom.RoomState == RoomState.FountainOfObjects && Room.FountainIsActive)
            return "You hear the rushing waters from the Fountain of Objects. It has been reactivated!";
        if (CurrentRoom.RoomState == RoomState.FountainOfObjects)
            return "You hear water dripping in this room. The Fountain of Objects is here!";
        if (CurrentRoom.RoomState == RoomState.Entrance)
            return "You see light coming from the cavern entrance";
        
        return null;
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