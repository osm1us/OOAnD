namespace SpaceBattle.Lib
{
    public class ShapeIdentifier : IShapeIdentifier
    {
        public int GetShapeId(string? shapeName)
        {
            if (string.IsNullOrEmpty(shapeName))
            {
                return 0;
            }

            return shapeName.ToLower().GetHashCode();
        }
    }
}
