public interface IEatable 
{
    int RequiredLevel { get; }
    int Reward { get; }
    void BeEaten(Slime slime);
    void BeNotEaten(Slime slime);
}
