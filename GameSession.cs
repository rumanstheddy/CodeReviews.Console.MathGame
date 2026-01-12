using System.Collections.Immutable;

class GameSession
{
    public List<GameRound> History { get; set; }
    private bool _isFirstRound;
    public ImmutableDictionary<int, char> OptionsDictionary = new Dictionary<int, char>
    {
        {1, '+'},
        {2, '-'},
        {3, '*'},
        {4, '/'},
    }.ToImmutableDictionary();

    // TODO: Move away from static questions (maybe from a csv file?)
    public static readonly ImmutableDictionary<char, (int, int)[]> QuestionsDictionary = new Dictionary<char, (int, int)[]>
    {
        {'+', [(1, 2), (3, 4), (5, 6), (7, 8), (9, 10), (11, 12), (13, 14), (15, 16), (17, 18), (19, 20)]},
        {'-', [(10, 2), (15, 5), (20, 7), (18, 9), (12, 3), (22, 11), (30, 10), (25, 8), (40, 20), (50, 25)]},
        {'*', [(2, 3), (4, 5), (6, 2), (7, 3), (8, 4), (5, 5), (9, 2), (3, 6), (10, 3), (4, 7)]},
        {'/', [(8, 2), (15, 3), (18, 6), (20, 4), (21, 7), (24, 6), (30, 5), (36, 6), (40, 8), (27, 3)]},
    }.ToImmutableDictionary();

    public void ShowMenu()
    {
        if (_isFirstRound)
        {
            _isFirstRound = false;
            Console.WriteLine("WELCOME TO THE MATH QUIZ!");
            StartGame();
        }

        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Start another round");
        Console.WriteLine("2. Check history of scores");
        Console.WriteLine("3. Quit");

        int selectedOption = int.Parse(Console.ReadLine()!);
        switch (selectedOption)
        {
            case 1:
                StartGame();
                break;
            case 2:
                ShowHistory();
                break;
            case 3:
                Environment.Exit(0);
                break;
            default:
                ShowMenu();
                break;
        }

        Console.WriteLine();
    }

    public void StartGame()
    {
        Console.WriteLine();
        Console.WriteLine("******************************* ROUND STARTED *******************************");
        Console.WriteLine();
        Console.WriteLine("Choose an operation to get started: ");
        foreach (int o in OptionsDictionary.Keys)
        {
            Console.WriteLine($"{o} : {OptionsDictionary[o]}");
        }
        int option = int.Parse(Console.ReadLine()!);
        char operation = OptionsDictionary[option];

        GameRound gameRound = new(operation, QuestionsDictionary[operation]);

        Console.WriteLine();
        Console.WriteLine("You've selected: " + operation);
        for (int i = 0; i < gameRound.Questions.Length; i++)
        {
            (int, int) q = gameRound.Questions[i];
            Console.WriteLine();
            Console.WriteLine($"What is {q.Item1} {operation} {q.Item2}?");
            int response = int.Parse(Console.ReadLine()!);
            if (response == gameRound.Answers[i])
            {
                gameRound.TotalScore += 1;
            }
        }

        Console.WriteLine();
        Console.WriteLine($"YOU SCORED: {gameRound.TotalScore} POINT(S)!");
        Console.WriteLine();
        Console.WriteLine("******************************* ROUND ENDED *******************************");
        Console.WriteLine();
        History.Add(gameRound);

        ShowMenu();
    }

    public void ShowHistory()
    {
        Console.WriteLine();
        Console.WriteLine("History of scores for this session:");
        for (int i = 0; i < History.Count; i++)
        {
            Console.WriteLine($"Game {i}: {History[i].TotalScore} POINT(S)");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to exit the history section...");
        Console.ReadLine();
    }

    public GameSession()
    {
        History = [];
        _isFirstRound = true;
    }
}