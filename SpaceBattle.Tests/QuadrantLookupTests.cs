using SpaceBattle.Lib;

namespace SpaceBattle.Tests;

public class QuadrantLookupTests
{
    private class TestMoving : IMoving
    {
        public Vector Position { get; set; }
        public Vector Velocity { get; } = new Vector(0, 0);
        private readonly Guid _id = Guid.NewGuid();

        public TestMoving(params int[] coords)
        {
            Position = new Vector(coords);
        }

        public override bool Equals(object? obj)
        {
            return obj is TestMoving other && _id.Equals(other._id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }

    [Fact]
    public void AddToMap_OneObject()
    {
        var obj = new TestMoving(15, 25);
        var grid = new QuadrantLookup(10);

        grid.Insert(obj);

        var cell = (1, 2);
        var objects = grid.GetObjectsInCell(cell);

        Assert.Contains(obj, objects);
        Assert.Single(objects);
    }

    [Fact]
    public void AddToMap_TwoObjects()
    {
        var obj1 = new TestMoving(15, 25);
        var obj2 = new TestMoving(16, 26);
        var grid = new QuadrantLookup(10);

        grid.Insert(obj1);
        grid.Insert(obj2);

        var cell = (1, 2);
        var objects = grid.GetObjectsInCell(cell);

        Assert.Contains(obj1, objects);
        Assert.Contains(obj2, objects);
        Assert.Equal(2, objects.Count());
    }

    [Fact]
    public void RemoveFromMap_ExistingObject()
    {
        var obj = new TestMoving(15, 25);
        var grid = new QuadrantLookup(10);
        grid.Insert(obj);

        grid.Remove(obj);

        var cell = (1, 2);
        var objects = grid.GetObjectsInCell(cell);

        Assert.DoesNotContain(obj, objects);
        Assert.Empty(objects);
    }

    [Fact]
    public void RemoveFromMap_NonExistingObject()
    {
        var obj = new TestMoving(15, 25);
        var grid = new QuadrantLookup(10);

        var cell = (1, 2);
        grid.Remove(obj);

        var objects = grid.GetObjectsInCell(cell);
        Assert.Empty(objects);
    }

    [Fact]
    public void MoveObjectToSameCell()
    {
        var obj = new TestMoving(15, 25);
        var grid = new QuadrantLookup(10);
        grid.Insert(obj);

        grid.Move(obj, new Vector(new int[] { 16, 26 }));

        var cell = (1, 2);
        var objects = grid.GetObjectsInCell(cell);
        Assert.Contains(obj, objects);
    }

    [Fact]
    public void MoveObjectToDifferentCell()
    {
        var obj = new TestMoving(15, 25);
        var grid = new QuadrantLookup(10);
        grid.Insert(obj);

        grid.Move(obj, new Vector(new int[] { 35, 45 }));

        var oldCell = (1, 2);
        var newCell = (3, 4);

        Assert.DoesNotContain(obj, grid.GetObjectsInCell(oldCell));
        Assert.Contains(obj, grid.GetObjectsInCell(newCell));
    }

    [Fact]
    public void GetNearbyObjects_SameCell()
    {
        var obj1 = new TestMoving(15, 25);
        var obj2 = new TestMoving(16, 26);
        var grid = new QuadrantLookup(10);
        grid.Insert(obj1);
        grid.Insert(obj2);

        var nearby = grid.GetNearby(obj1);

        Assert.Contains(obj2, nearby);
        Assert.DoesNotContain(obj1, nearby);
    }

    [Fact]
    public void GetNearbyObjects_NeighboringCell()
    {
        var obj1 = new TestMoving(15, 25);
        var obj2 = new TestMoving(25, 25);
        var grid = new QuadrantLookup(10);
        grid.Insert(obj1);
        grid.Insert(obj2);

        var nearby = grid.GetNearby(obj1);
        Assert.Contains(obj2, nearby);
        Assert.DoesNotContain(obj1, nearby);
    }

    [Fact]
    public void GetNearbyObjects_EmptyMap()
    {
        var obj = new TestMoving(15, 25);
        var grid = new QuadrantLookup(10);

        var nearby = grid.GetNearby(obj);
        Assert.Empty(nearby);
    }

    [Fact]
    public void GetObjectsInCell_ExistingCell()
    {
        var obj = new TestMoving(15, 25);
        var grid = new QuadrantLookup(10);
        grid.Insert(obj);

        var cell = (1, 2);
        var objects = grid.GetObjectsInCell(cell);

        Assert.Contains(obj, objects);
    }

    [Fact]
    public void GetObjectsInCell_EmptyCell()
    {
        var grid = new QuadrantLookup(10);
        var cell = (1, 2);

        var objects = grid.GetObjectsInCell(cell);
        Assert.Empty(objects);
    }

    [Fact]
    public void GetAllOccupiedCells()
    {
        var obj1 = new TestMoving(15, 25);
        var obj2 = new TestMoving(35, 45);
        var grid = new QuadrantLookup(10);

        grid.Insert(obj1);
        grid.Insert(obj2);

        var cells = grid.GetAllOccupiedCells().ToList();
        Assert.Contains((1, 2), cells);
        Assert.Contains((3, 4), cells);
        Assert.Equal(2, cells.Count);
    }
}
