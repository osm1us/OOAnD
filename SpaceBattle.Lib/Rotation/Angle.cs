public class Angle
{
    private int num { get; set; }
    private int den { get; }

    public Angle(int num, int den)
    {
        this.den = den;
        this.num = ((num % den) + den) % den;
    }

    public static Angle operator +(Angle ang1, Angle ang2)
    {
        return new Angle(ang1.num + ang2.num, ang1.den);
    }

    public override bool Equals(object? obj) => obj != null && obj is Angle angle && num == angle.num && den == angle.den;

    public override int GetHashCode() => num.GetHashCode();

    public static bool operator ==(Angle ang1, Angle ang2)
    {
        return ang1.Equals(ang2);
    }

    public static bool operator !=(Angle ang1, Angle ang2)
    {
        return !(ang1 == ang2);
    }
    public static implicit operator double(Angle angle)
    {
        return 2 * Math.PI * angle.num / angle.den;
    }
}
