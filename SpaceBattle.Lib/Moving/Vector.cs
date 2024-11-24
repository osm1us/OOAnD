public class Vector
{
    private int[] coordinates;

    public Vector(params int[] coordinates)
    {
        if (coordinates == null || coordinates.Length == 0)
        {
            throw new ArgumentException(nameof(coordinates), "Вектор не может быть пустым.");
        }

        this.coordinates = coordinates;
    }

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        if (vector1.Dimension != vector2.Dimension)
        {
            throw new ArgumentException("Размерности векторов не совпадают.");
        }

        var resultCoordinates = new Vector(new int[vector1.Dimension]);
        resultCoordinates.coordinates = vector1.coordinates.Select((с, index) => с + vector2.coordinates[index]).ToArray();
        return resultCoordinates;
    }
    public override bool Equals(object? obj)
    {
        return obj != null && obj is Vector otherVector && coordinates.SequenceEqual(otherVector.coordinates);
    }

    public override int GetHashCode()
    {
        return coordinates.GetHashCode();
    }
    public int Dimension => coordinates.Length;

}
