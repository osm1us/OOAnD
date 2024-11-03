
public class Vector
{
    private readonly List<int> coordinates;

    public Vector(List<int> coordinates)
    {
        if (coordinates == null || coordinates.Count == 0)
        {
            throw new ArgumentException("Координаты вектора не могут быть пустыми.", nameof(coordinates));
        }

        this.coordinates = coordinates;
    }

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        if (vector1.Dimension != vector2.Dimension)
        {
            throw new ArgumentException("Размерности векторов не совпадают.");
        }

        var resultCoordinates = new Vector(new List<int>());
        for (var i = 0; i < vector1.Dimension; i++)
        {
            resultCoordinates.coordinates.Add(vector1.coordinates[i] + vector2.coordinates[i]);
        }

        return resultCoordinates;
    }
    public int Dimension => coordinates.Count;
}
