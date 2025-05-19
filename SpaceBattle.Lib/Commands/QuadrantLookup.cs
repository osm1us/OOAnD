namespace SpaceBattle.Lib;

public class QuadrantLookup
{
    private readonly int _cellSize;
    private readonly Dictionary<(int, int), HashSet<IMoving>> _grid = new();

    public QuadrantLookup(int cellSize)
    {
        _cellSize = cellSize;
    }

    private (int, int) GetCellCoords(Vector pos)
    {
        var x = pos.Coordinates[0];
        var y = pos.Coordinates[1];
        return ((int)Math.Floor((double)x / _cellSize), (int)Math.Floor((double)y / _cellSize));
    }

    public void Insert(IMoving obj)
    {
        var pos = obj.Position;
        var coords = GetCellCoords(pos);
        if (!_grid.ContainsKey(coords))
        {
            _grid[coords] = new HashSet<IMoving>();
        }

        _grid[coords].Add(obj);
    }

    public void Remove(IMoving obj)
    {
        var pos = obj.Position;
        var coords = GetCellCoords(pos);
        if (_grid.TryGetValue(coords, out var set))
        {
            set.Remove(obj);
            if (!set.Any())
            {
                _grid.Remove(coords);
            }
        }
    }

    public void Move(IMoving obj, Vector newPos)
    {
        var oldQuadrant = GetCellCoords(obj.Position)
        var newQuadrant = GetCellCoords(newpos)
        if (oldQuadrant == newQuadrant){
            obj.Position = newPos;
            return;
        }
        Remove(obj);
        obj.Position = newPos;
        Insert(obj);
    }

    public IEnumerable<IMoving> GetNearby(IMoving obj)
    {
        var pos = obj.Position;
        var (cx, cy) = GetCellCoords(pos);

        var offsets = from dx in Enumerable.Range(-1, 3)
                      from dy in Enumerable.Range(-1, 3)
                      select (cx + dx, cy + dy);

        var nearby = offsets
            .Where(cell => _grid.ContainsKey(cell))
            .SelectMany(cell => _grid[cell])
            .Where(o => o != obj);

        return nearby;

    }

    public IEnumerable<(int, int)> GetAllOccupiedCells() => _grid.Keys;

    public IEnumerable<IMoving> GetObjectsInCell((int, int) cell) =>
        _grid.TryGetValue(cell, out var set) ? set : Enumerable.Empty<IMoving>();
}
