FountainOfObjects.Main();

public class FountainOfObjects
{
    public static void Main()
    {
        FountainOfObjects newGame = new FountainOfObjects();
        World world = new World();

        world.SetWorld();
        
        bool gameOver = false;

        string? userInput;

        do
        {
            GameText(world);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("What do you want to do: ");
            
            userInput = newGame.GetInput();
            
            world.Move(userInput);
            
            World.ActivateFountainCheck(userInput);

            gameOver = newGame.HasWon(World.CurrentRoom);
        }
        while (!gameOver);
        
        GameText(world);
    }
    private bool HasWon(Room room)
    {
        if (Room.FountainIsActive == true && (room.Column == 0 && room.Row == 0))
            return true;
        return false;
    }

    private string? GetInput()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        return Console.ReadLine();
    }

    private static void GameText(World world)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"You are currently in the room at (Row:{World.CurrentRoom.Row}, Column:{World.CurrentRoom.Column})");
        Console.WriteLine(world.RoomStateCheck());
    }
}
public class World
{
    public Room[,] Rooms = new Room[4, 4];
    public static Room CurrentRoom { get; private set; }
    
    public void SetCurrentRoom(Room room) => CurrentRoom = room;

    private Room GetRoom(int row, int column)
    {
        return Rooms[CurrentRoom.Row + row, CurrentRoom.Column + column];
    }
    
    /*I originally just set up a foreach loop to create all the rooms, but it was confusing to look at and
    caused some weird problems with the grid.*/ 
    public void SetWorld()
    {
        Rooms[0, 0] = new Room(0, 0, RoomState.Entrance);
        Rooms[0, 1] = new Room(0, 1, RoomState.Nothing);
        Rooms[0, 2] = new Room(0, 2, RoomState.FountainOfObjects);
        Rooms[0, 3] = new Room(0, 3, RoomState.Nothing);
        Rooms[1, 0] = new Room(1, 0, RoomState.Nothing);
        Rooms[1, 1] = new Room(1, 1, RoomState.Nothing);
        Rooms[1, 2] = new Room(1, 2, RoomState.Nothing);
        Rooms[1, 3] = new Room(1, 3, RoomState.Nothing);
        Rooms[2, 0] = new Room(2, 0, RoomState.Nothing);
        Rooms[2, 1] = new Room(2, 1, RoomState.Nothing);
        Rooms[2, 2] = new Room(2, 2, RoomState.Nothing);
        Rooms[2, 3] = new Room(2, 3, RoomState.Nothing);
        Rooms[3, 0] = new Room(3, 0, RoomState.Nothing);
        Rooms[3, 1] = new Room(3, 1, RoomState.Nothing);
        Rooms[3, 2] = new Room(3, 2, RoomState.Nothing);
        Rooms[3, 3] = new Room(3, 3, RoomState.Nothing);
        
        CurrentRoom = Rooms[0, 0];
    }

    public void Move(string movement)
    {
        if (CurrentRoom.Row < 3 && movement == "move north")
            SetCurrentRoom(GetRoom(1, 0));
        if (CurrentRoom.Row > 0 && movement == "move south")
            SetCurrentRoom(GetRoom( -1, 0));
        if (CurrentRoom.Column < 3 && movement == "move east")
            SetCurrentRoom(GetRoom(0, 1));
        if (CurrentRoom.Column > 0 && movement == "move west")
            SetCurrentRoom(GetRoom(0, -1));
    }

    public static void ActivateFountainCheck(string command)
    {
        if (!Room.FountainIsActive && command == "enable fountain")
            Room.FountainIsActive = true;
    }

    //I could just make this void, but it works as is.
    public string? RoomStateCheck()
    {
        if (CurrentRoom.RoomState == RoomState.Entrance && Room.FountainIsActive)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
        }

        if (CurrentRoom.RoomState == RoomState.FountainOfObjects && Room.FountainIsActive)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
        }

        if (CurrentRoom.RoomState == RoomState.FountainOfObjects && !Room.FountainIsActive)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
        }

        if (CurrentRoom.RoomState == RoomState.Entrance && !Room.FountainIsActive)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You see light coming from the cavern entrance");
        }
        
        return "------------------------------------------------------------------";
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