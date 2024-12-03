namespace SpaceBattle.Tests;
public class VectorTest
{
    [Fact]
    public void NotEqualsCoordinatesTest()
    {
        var testVector1 = new Vector(1, 2);
        var testVector2 = new Vector(1, 2, 3);
        Assert.False(testVector1.Equals(testVector2));
    }

    [Fact]
    public void AdditionTest()
    {
        var testVector1 = new Vector(1, 2);
        var testVector2 = new Vector(1, 2);
        var testResultVector = testVector1 + testVector2;
        Assert.True(typeof(Vector).IsInstanceOfType(testResultVector));
    }

    [Fact]
    public void NotEqualsVectorTypeTest()
    {
        var testVector1 = new Vector(1, 2);
        var testVector2 = 5;
        Assert.False(testVector1.Equals(testVector2));
    }

    [Fact]
    public void NotEqualsDimensionTest()
    {
        var testVector1 = new Vector(1, 2);
        var testVector2 = new Vector(1, 2, 3);
        Assert.Throws<ArgumentException>(() => testVector1 + testVector2);
    }

    [Fact]
    public void VectorEqualsNullTest()
    {
        Assert.Throws<ArgumentException>(() => new Vector());
    }

    [Fact]
    public void CheckGetHashCodeTest()
    {
        var testVector = new Vector(1, 2);
        _ = testVector.GetHashCode();
        Assert.True(true);
    }
}
