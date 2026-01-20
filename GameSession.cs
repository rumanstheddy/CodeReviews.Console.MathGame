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

    public void ShowMenu()
    {
        int selectedOption = -1;

        while (selectedOption != 3)
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
            Console.WriteLine();

            var menuOptionsSet = new HashSet<int>() { 1, 2, 3 };

            _ = int.TryParse(Console.ReadLine()!, out selectedOption);
            while (selectedOption == 0 || !menuOptionsSet.Contains(selectedOption))
            {
                Console.WriteLine("Invalid option selected. Please try again.");
                Console.WriteLine("1. Start another round");
                Console.WriteLine("2. Check history of scores");
                Console.WriteLine("3. Quit");
                Console.WriteLine();
                _ = int.TryParse(Console.ReadLine()!, out selectedOption);
            }

            if (selectedOption == 1) StartGame();
            else if (selectedOption == 2) ShowHistory();
        }
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

        var optionsSet = new HashSet<int>(OptionsDictionary.Keys);
        _ = int.TryParse(Console.ReadLine()!, out int option);

        while (option == 0 || !optionsSet.Contains(option))
        {
            Console.WriteLine("Invalid option selected. Please try again.");
            foreach (int o in OptionsDictionary.Keys)
            {
                Console.WriteLine($"{o} : {OptionsDictionary[o]}");
            }
            _ = int.TryParse(Console.ReadLine()!, out option);
        }

        char operation = OptionsDictionary[option];

        GameRound gameRound = new(operation);

        Console.WriteLine();
        Console.WriteLine("You've selected: " + operation);
        for (int i = 0; i < gameRound.Questions.Count; i++)
        {
            (int, int) q = gameRound.Questions[i];
            Console.WriteLine();
            Console.WriteLine($"What is {q.Item1} {operation} {q.Item2}?");

            if (!int.TryParse(Console.ReadLine()!, out int response))
            {
                response = -1;
            }

            while (response == -1)
            {
                Console.WriteLine();
                Console.WriteLine($"Invalid response. Please enter a valid number.");
                if (!int.TryParse(Console.ReadLine()!, out response))
                {
                    response = -1;
                }
            }

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