public interface IEatable 
{
    int RequiredLevel { get; }
    int Reward { get; }

    void BeEaten(Slime slime);
}
