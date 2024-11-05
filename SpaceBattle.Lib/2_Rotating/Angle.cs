namespace SpaceBattle.Lib;
public class Angle
{
    public int deg;
    public Angle(int deg)
    {
        this.deg = deg;
    }
    public static Angle operator +(Angle ang1, Angle ang2)
    {
        var ResultAngle = new Angle(0);
        ResultAngle.deg = (ang1.deg + ang2.deg) % 360;
        return ResultAngle;
    }
}
