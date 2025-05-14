using SpaceBattle.Lib;

public interface ISpatialMap
{
    void Insert(IMoving obj);
    void Remove(IMoving obj);
    void Move(IMoving obj, Vector newPos);
    IEnumerable<IMoving> GetNearby(IMoving obj);
    IEnumerable<(int, int)> GetAllOccupiedCells();
    IEnumerable<IMoving> GetObjectsInCell((int, int) cell);
}
