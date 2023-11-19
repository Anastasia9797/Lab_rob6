public class Viewer // клас глядач
{

    string nomer; //поле глядач
    public string Viewernomer // властивість читання поля
    {
        get { return nomer; }
    }
    public Viewer(string nomer) // конструктор з параметром
    {
        this.nomer = nomer;
    }
}
//клас кіно
public class Cinema
{
    public delegate void NotPlacesEventHandler();
    public static event NotPlacesEventHandler NotPlaces;

    int totalSeats; //кільність місць у залі
    int occupiedSeats = 0; //зайняті місця

    public Cinema(int seats) //конструктор з параметром
    {
        this.totalSeats = seats;
    }

    public void PushViewer(Viewer viewer) // клас, де глядачі займають по черзі місця
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // заповнюємо зал глядачами
        occupiedSeats++;
        Console.ForegroundColor = System.ConsoleColor.Green;
        Console.WriteLine($"{viewer.Viewernomer} зайняв своє місце.");

        if ((NotPlaces != null) && occupiedSeats == totalSeats) //генерація події
        {
            NotPlaces();
        }
    }  
}
public class Security //клас дежурний
{
    public delegate void SwitchOffEventHandler(); 
    public static event SwitchOffEventHandler SwitchOff;

    public void CloseZal() //закриття залу
    {
        Console.ForegroundColor = System.ConsoleColor.Blue;
        Console.WriteLine("Дежурний закрив зал.");
        if (SwitchOff != null) //генерація події SwichOff
        {
            SwitchOff();
        }
    } 
}

public class Light //клас світло
{
    public delegate void BeginEventHandler();
    public static event BeginEventHandler Begin;
    public void Turn() // повідомлення про вимикання світла
    {
        Console.WriteLine("Вимикаємо світло!");
        if (Begin != null) //генерація події
        {
            Begin();
        }
    }
}

public class Hardware // клас апаратна
{
    string filmName; // назва фільму

    public Hardware(string name) //конструктор з параметром
    {
        this.filmName = name;
    }

    public void FilmOn() // повідомлення про початок фільму
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine($"Починається фільм {filmName}");
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Введіть кількість місць у залі:");
        int seats = int.Parse(Console.ReadLine());
        Console.WriteLine("Введіть назву фільму:");
        string filmName = Console.ReadLine();

        Cinema cinema = new Cinema(seats);
        Security security = new Security();
        Light light = new Light();
        Hardware hardware = new Hardware(filmName);

        Cinema.NotPlaces += security.CloseZal;
        Security.SwitchOff += light.Turn;
        Light.Begin += hardware.FilmOn;

        for (int i = 1; i <= seats; i++)
        {
            Viewer viewer = new Viewer($"Глядач {i}");
            cinema.PushViewer(viewer);
        }
        Console.ReadLine();
    }
}