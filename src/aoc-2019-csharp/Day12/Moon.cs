namespace aoc_2019_csharp.Day12;

public class Moon
{
    public Point Position { get; set; }
    public Point Velocity { get; set; }

    public Moon(Point position, Point velocity)
    {
        Position = position;
        Velocity = velocity;
    }

    public int GetTotalEnergy() => GetPotentialEnergy() * GetKineticEnergy();

    private int GetPotentialEnergy() => Position.GetMagnitude();

    private int GetKineticEnergy() => Velocity.GetMagnitude();
}
