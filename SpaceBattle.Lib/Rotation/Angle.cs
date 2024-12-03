namespace SpaceBattle.Lib;
public class Angle
{
    private int deg { get; }
    public Angle(int deg)
    {
        this.deg = deg % 360;
    }
    public static Angle operator +(Angle ang1, Angle ang2)
    {
        return new Angle(ang1.deg + ang2.deg);
    }
    public override bool Equals(object? obj) => obj != null && obj is Angle angle && deg == angle.deg;

    public override int GetHashCode() => deg.GetHashCode();
}
