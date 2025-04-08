using System.Threading.Channels;

namespace ConsoleClubPC;

class Program
{
    static void Main(string[] args)
    {
        ComputerClub myClub = new ComputerClub(5);
        myClub.WorkOn();
    }
}

class ComputerClub
{
    private int _ClubBalance = 0;
    
    private List<Computer> _computers = new List<Computer>();
    private Queue<Client> _clients = new Queue<Client>();

    public ComputerClub(int computersCount)
    {
        Random random = new Random();

        for (int i = 0; i < computersCount; i++)
        {
            _computers.Add(new Computer(random.Next(5, 15)));
        }
        
        AddClient(25, random);
    }

    public void AddClient(int clientCount, Random random)
    {
        for (int i = 0; i < clientCount; i++)
        {
            _clients.Enqueue(new Client(random.Next(100, 250), random));
        }
    }

    private void ShowStatsComputers()
    {
        Console.WriteLine("Cписок всех компьютеров:");
        for (int i = 0; i < _computers.Count; i++)
        {
            Console.Write($"{i + 1} - ");
            _computers[i].ShowState();
        }
    }

    public void WorkOn()
    {
        Console.WriteLine("Добро пожаловать в Компьютерный Клуб.\n-------------------------------------------");
            
        while (_clients.Count > 0)
        {
            Client newClient = _clients.Dequeue();
            Console.WriteLine($"\nБаланс компьютерного клуба: {_ClubBalance}\n");
            Console.WriteLine($"У нас новый клиент: \"Хочу купить {newClient.UsingTime} минут.\"\n");
            
            ShowStatsComputers();

            Console.ReadKey();
        }
    }
}

class Computer
{
    private Client _client;
    private int _remainingTime;

    private bool IsRunning
    {
        get
        {
            return _remainingTime > 0;
        }
    }
    
    public int PricePerTime { get; private set; }

    public Computer(int pricePerTime)
    {
        PricePerTime = pricePerTime;
    }

    public void BecomeRunning(Client client)
    {
        _client = client;
        _remainingTime = _client.UsingTime;
    }

    public void BecomeStopped()
    {
        _client = null;
    }

    public void SpendTime()
    {
        _remainingTime--;
    }

    public void ShowState()
    {
        if (IsRunning)
            Console.WriteLine($"Компьютер занят, осталось еще {_remainingTime} минут.");
        else
            Console.WriteLine($"Компьютер готов к использованию, цена за минуту: {PricePerTime}");
    }
}

class Client
{
    private int _money;
    public int UsingTime { get; private set; }

    public Client(int money, Random random)
    {
        _money = money;
        UsingTime = random.Next(10, 40);
    }
}