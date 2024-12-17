namespace SpaceBattle.Tests;

public class VectorTest
{
    [Fact]
    public void Vector_Addition_Zero_Result_Test()
    {
        var vector1 = new Vector(1, -1, 2);
        var vector2 = new Vector(-1, 1, -2);
        var result = vector1 + vector2;
        Assert.Equal(new Vector(0, 0, 0), result);
    }

    [Fact]
    public void Vector_Addition_Different_Dimensions_First_Larger_Test()
    {
        var vector1 = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2);
        Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void Vector_Addition_Different_Dimensions_Second_Larger_Test()
    {
        var vector1 = new Vector(1, 2);
        var vector2 = new Vector(1, 2, 3);
        Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void Equal_Vectors_Equals_Method_Test()
    {
        var vector1 = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 3);
        Assert.True(vector1.Equals(vector2));
    }

    [Fact]
    public void Equal_Vectors_Equality_Operator_Test()
    {
        var vector1 = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 3);
        Assert.True(vector1 == vector2);
    }

    [Fact]
    public void Different_Vectors_Equals_Method_Test()
    {
        var vector1 = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 4);
        Assert.False(vector1.Equals(vector2));
    }

    [Fact]
    public void Different_Vectors_Inequality_Operator_Test()
    {
        var vector1 = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 4);
        Assert.True(vector1 != vector2);
    }

    [Fact]
    public void Vector_Has_HashCode_Test()
    {
        var vector = new Vector(1, 2, 3);
        _ = vector.GetHashCode();
        Assert.True(true);
    }
}
