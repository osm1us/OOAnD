using SpaceBattle.Lib;
namespace Spacebattle.Tests;

public class AngleTests
{
    [Fact]
    public void AngleSumTest()
    {
        var angle1 = new Angle(5, 8);
        var angle2 = new Angle(7, 8);
        Assert.Equal(new Angle(4, 8), angle1 + angle2);
    }

    [Fact]
    public void AngleSum_WithOverflow_Test()
    {
        var angle1 = new Angle(7, 8);
        var angle2 = new Angle(6, 8);
        Assert.Equal(new Angle(5, 8), angle1 + angle2);
    }

    [Fact]
    public void EqualsTest_ShouldReturnTrue()
    {
        var angle1 = new Angle(15, 8);
        var angle2 = new Angle(23, 8);
        Assert.True(angle1.Equals(angle2));
    }

    [Fact]
    public void OperatorEqualsTest_ShouldReturnTrue()
    {
        var angle1 = new Angle(15, 8);
        var angle2 = new Angle(23, 8);
        Assert.True(angle1 == angle2);
    }

    [Fact]
    public void EqualsTest_ShouldReturnFalse()
    {
        var angle1 = new Angle(1, 8);
        var angle2 = new Angle(2, 8);
        Assert.False(angle1.Equals(angle2));
    }

    [Fact]
    public void OperatorNotEqualsTest_ShouldReturnTrue()
    {
        var angle1 = new Angle(1, 8);
        var angle2 = new Angle(2, 8);
        Assert.True(angle1 != angle2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnHashCode()
    {
        var angle = new Angle(1, 8);
        Assert.NotEqual(0, angle.GetHashCode());
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForNullObject()
    {
        var angle = new Angle(1, 8);
        Assert.False(angle.Equals(null));
    }

    [Fact]
    public void Equals_ShouldReturnFalse_ForNonAngleObject()
    {
        var angle = new Angle(1, 8);
        Assert.False(angle.Equals("Не Angle"));
    }

    [Fact]
    public void Sin_ShouldReturnCorrectValue()
    {
        var angle = new Angle(1, 8);
        Assert.Equal(2 * Math.PI * 1 / 8, angle.Sin());
    }

    [Fact]
    public void Constructor_ShouldNormalizeNegativeAngle()
    {
        var angle = new Angle(-1, 8);
        Assert.Equal(new Angle(7, 8), angle);
    }

    [Fact]
    public void Constructor_ShouldNormalizeOverflowAngle()
    {
        var angle = new Angle(9, 8);
        Assert.Equal(new Angle(1, 8), angle);
    }
}
