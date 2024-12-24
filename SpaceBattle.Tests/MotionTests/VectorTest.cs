namespace SpaceBattle.Tests;

public class VectorTest
{
    [Fact]
    public void Vector_Creation_Empty_Coordinates_Test()
    {
        Assert.Throws<ArgumentException>(() => new Vector());
    }

    [Fact]
    public void Vector_Creation_Null_Coordinates_Test()
    {
        Assert.Throws<ArgumentException>(() => new Vector(null));
    }

    [Fact]
    public void Vector_Equality_First_Null_Test()
    {
        Vector? vector1 = null;
        var vector2 = new Vector(1, 2);
        Assert.False(vector1 == vector2);
        Assert.True(vector1 != vector2);
    }

    [Fact]
    public void Vector_Equality_Second_Null_Test()
    {
        var vector1 = new Vector(1, 2);
        Vector? vector2 = null;
        Assert.False(vector1 == vector2);
        Assert.True(vector1 != vector2);
    }

    [Fact]
    public void Vector_Equality_Both_Null_Test()
    {
        Vector? vector1 = null;
        Vector? vector2 = null;
        Assert.False(vector1 == vector2);
        Assert.True(vector1 != vector2);
    }

    [Fact]
    public void Vector_Addition_Zero_Result_Test()
    {
        var vector1 = new Vector(1, -1, 2);
        var vector2 = new Vector(-1, 1, -2);
        var result = vector1 + vector2;
        Assert.Equal(new Vector(0, 0, 0), result);
    }

    [Fact]
    public void Vector_Equals_Null_Object_Test()
    {
        var vector = new Vector(1, 2, 3);
        Assert.False(vector.Equals(null));
    }

    [Fact]
    public void Vector_Equals_Different_Type_Test()
    {
        var vector = new Vector(1, 2, 3);
        Assert.False(vector.Equals(""));
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
        var vector1 = new Vector(1, 2, 3);
        var vector2 = new Vector(1, 2, 3);
        Assert.Equal(vector1.GetHashCode(), vector2.GetHashCode());
    }
}
