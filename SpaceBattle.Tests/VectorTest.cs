namespace SpaceBattle.Tests;
public class VectorTest
{
    [Fact]
    public void EqualsNotNullTest()
    {
        var testVector = new Vector(new List<int> { 1, 2 });
        Assert.False(Vector.IsNull(testVector));
    }

    [Fact]
    public void EqualVectorTest()
    {
        var testVector = new Vector(new List<int> { 1, 2 });
        Assert.True(Vector.IsVector(testVector));
    }

    [Fact]
    public void EqualsNotDimensionTest()
    {
        var testVector1 = new Vector(new List<int> { 1, 2 });
        var testVector2 = new Vector(new List<int> { 1, 2, 3 });
        Assert.False(testVector1.EqualsDimension(testVector2));
    }

    [Fact]
    public void EqualsCoordinatesTest()
    {
        var testVector1 = new Vector(new List<int> { 1, 2 });
        _ = new Vector(new List<int> { 1, 2 });
        Assert.True(testVector1.EqualsCoordinates(testVector1));
    }

    [Fact]
    public void AdditionTest()
    {
        var testVector1 = new Vector(new List<int> { 1, 2 });
        var testVector2 = new Vector(new List<int> { 3, 4 });
        var testResultVector = testVector1 + testVector2;
        Assert.True(typeof(Vector).IsInstanceOfType(testResultVector));
    }
}
