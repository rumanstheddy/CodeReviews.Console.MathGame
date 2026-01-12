class GameRound
{
    public (int, int)[] Questions { get; set; }
    public char Operation { get; }
    public int TotalScore { get; set; }
    private readonly int[] _answers;
    public IReadOnlyList<int> Answers
    {
        get { return _answers; }
    }
    private void ComputeAnswersForOperation()
    {
        for (int i = 0; i < Questions.Length; i++)
        {
            (int, int) question = Questions[i];
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
    public GameRound(char operation, (int, int)[] questions)
    {
        Operation = operation;
        _answers = [.. new int[questions.Length]];
        Questions = questions;
        ComputeAnswersForOperation();
    }
}