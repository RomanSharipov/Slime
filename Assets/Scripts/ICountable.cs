public interface ICountable 
{
    int RequiredScoreForUpgrade { get; }

    void AddScore(int score);
    void ResetCountScoreForUpgrade();
}
