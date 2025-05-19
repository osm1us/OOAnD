namespace SpaceBattle.Lib
{
    public interface ICollisionDataPreparer
    {
        IEnumerable<int> PrepareCollisionData(
            Vector position1, Vector velocity1, string shape1,
            Vector position2, Vector velocity2, string shape2);
    }
}
