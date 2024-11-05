public class Vector
{
    private int[] coordinates { get; set; }

    public Vector(params int[] coordinates)
    {
        this.coordinates = coordinates;
    }

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        if (!vector1.EqualsDimension(vector2))
        {
            throw new ArgumentException("Размерности векторов не совпадают.");
        }

        var resultCoordinates = new Vector(new int[vector1.Dimension]);
        for (var i = 0; i < vector1.Dimension; i++)
        {
            resultCoordinates.coordinates[i] = vector1.coordinates[i] + vector2.coordinates[i];
        }

        return resultCoordinates;
    }
    public int Dimension => coordinates.Length;

    public static bool IsNull(Vector? vector) => vector == null;

    public static bool IsVector(object vector) => vector is Vector;

    public bool EqualsDimension(Vector vector) => coordinates.Length == vector.Dimension;

    public bool EqualsCoordinates(Vector vector) => coordinates.SequenceEqual(vector.coordinates);
}