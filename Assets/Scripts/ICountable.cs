public interface ICountable 
{
    int CountScoreForUpgrade { get; }
    void AddScore(int score);
    void ResetCountScoreForUpgrade();
}
