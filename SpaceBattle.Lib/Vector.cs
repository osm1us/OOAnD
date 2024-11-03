
public class Vector
{
    private List<int> coordinates;

    public Vector(List<int> coordinates)
    {
        this.coordinates = coordinates;
    }

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        if(!vector1.EqualsDimension(vector2)) throw new ArgumentException("Размерности векторов не совпадаютю");

        var resultCoordinates = new Vector(new List<int>());
        for (var i = 0; i < vector1.Dimension; i++)
        {
            resultCoordinates.coordinates.Add(vector1.coordinates[i] + vector2.coordinates[i]);
        }

        return resultCoordinates;
    }
    public int Dimension => coordinates.Count;

    public static bool IsNull(Vector vector) => (vector == null) || (vector.Dimension == 0);

    public static bool IsVector(object vector) => vector is Vector;

    public bool EqualsDimension(Vector vector) => coordinates.Count == vector.Dimension;

    public bool EqualsCoordinates(Vector vector) => coordinates.SequenceEqual(vector.coordinates);
}
