namespace SpaceBattle.Lib
{
    public class CollisionDataPreparer : ICollisionDataPreparer
    {
        private readonly IShapeIdentifier _shapeIdentifier;

        public CollisionDataPreparer(IShapeIdentifier shapeIdentifier)
        {
            _shapeIdentifier = shapeIdentifier;
        }

        public IEnumerable<int> PrepareCollisionData(
            Vector position1, Vector velocity1, string shape1,
            Vector position2, Vector velocity2, string shape2)
        {
            return position1.Coordinates
                .Concat(position2.Coordinates)
                .Concat(velocity1.Coordinates)
                .Concat(velocity2.Coordinates)
                .Append(_shapeIdentifier.GetShapeId(shape1))
                .Append(_shapeIdentifier.GetShapeId(shape2));
        }
    }
}
