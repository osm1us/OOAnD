using SpaceBattle.Lib;
namespace Spacebattle.Tests;

public class AngleTests
{
    [Fact]
    public void AngleSumTest()
    {
        var angle1 = new Angle(45);
        var angle2 = new Angle(55);
        Assert.Equal(new Angle(100), angle1 + angle2);
    }
    [Fact]
    public void RemainsTest()
    {
        var angle1 = new Angle(848);
        var angle2 = new Angle(4054);
        Assert.Equal(new Angle(222), angle1 + angle2);
    }
    [Fact]
    public void GetHashCode_ShouldReturnSameHash_ForEqualAngles()
    {
        var angle1 = new Angle(90);
        var angle2 = new Angle(90);
        Assert.Equal(angle1.GetHashCode(), angle2.GetHashCode());
    }
    [Fact]
    public void GetHashCode_ShouldReturnDifferentHash_ForDifferentAngles()
    {
        var angle1 = new Angle(90);
        var angle2 = new Angle(180);
        Assert.NotEqual(angle1.GetHashCode(), angle2.GetHashCode());
    }
    [Fact]
    public void GetHashCode_ShouldReturnSameHash_ForAnglesWithSameValueAfterNormalization()
    {
        var angle1 = new Angle(450); // 450 % 360 = 90
        var angle2 = new Angle(90);
        Assert.Equal(angle1.GetHashCode(), angle2.GetHashCode());
    }
    [Fact]
    public void Equals_ShouldReturnTrue_ForSameAngle()
    {
        var angle1 = new Angle(90);
        var angle2 = new Angle(90);
        Assert.True(angle1.Equals(angle2));
    }
    [Fact]
    public void Equals_ShouldReturnFalse_ForDifferentAngles()
    {
        var angle1 = new Angle(90);
        var angle2 = new Angle(180);
        Assert.False(angle1.Equals(angle2));
    }
    [Fact]
    public void Equals_ShouldReturnFalse_ForNullObject()
    {
        var angle = new Angle(90);
        Assert.False(angle.Equals(null));
    }
    [Fact]
    public void Equals_ShouldReturnFalse_ForNonAngleObject()
    {
        var angle = new Angle(90);
        Assert.False(angle.Equals("Не Angle"));
    }
}
