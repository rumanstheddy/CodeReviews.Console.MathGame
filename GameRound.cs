class GameRound
{
    public char Operation { get; }
    public int TotalScore { get; set; }
    private readonly int _questionsPerRound = 10;
    public int QuestionsPerRound
    {
        get { return _questionsPerRound; }
    }
    private readonly (int, int)[] _questions;
    public IReadOnlyList<(int, int)> Questions
    {
        get { return _questions; }
    }
    private readonly int[] _answers;
    public IReadOnlyList<int> Answers
    {
        get { return _answers; }
    }
    private void GenerateQuestionsForOperation()
    {
        Random rnd = new();
        var uniqueQuestions = new HashSet<(int, int)>();

        int i = 0;
        while (i < _questionsPerRound)
        {
            int a = rnd.Next(1, 101);
            int b = rnd.Next(1, 101);
            if (Operation == '-')
            {
                b = rnd.Next(1, a + 1);
            }
            else if (Operation == '/')
            {
                int k = rnd.Next(1, 21);
                b = rnd.Next(2, 21);
                a = b * k;
            }

            var question = (a, b);
            if (uniqueQuestions.Add(question))
            {
                _questions[i] = question;
                i += 1;
            }
        }
    }
    private void ComputeAnswersForOperation()
    {
        for (int i = 0; i < _questions.Length; i++)
        {
            (int, int) question = _questions[i];
            int answer = Operation switch
            {
                '+' => question.Item1 + question.Item2,
                '*' => question.Item1 * question.Item2,
                '-' => question.Item1 - question.Item2,
                '/' => question.Item1 / question.Item2,
                _ => throw new InvalidOperationException("Unknown Operator.")
            };

            _answers[i] = answer;
        }
    }
    public GameRound(char operation)
    {
        Operation = operation;
        _questions = [.. new (int, int)[_questionsPerRound]];
        GenerateQuestionsForOperation();
        _answers = [.. new int[_questionsPerRound]];
        ComputeAnswersForOperation();
    }
}