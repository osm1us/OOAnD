namespace SpaceBattle.Lib;

public class MovingAdapter : IMoving
{
    private readonly IDictionary<string, object> _gameObject;

    public MovingAdapter(IDictionary<string, object> gameObject)
    {
        _gameObject = gameObject;
    }

    public Vector Position
    {
        get => (Vector)_gameObject["Position"];
        set => _gameObject["Position"] = value;
    }

    public Vector Velocity => (Vector)_gameObject["Velocity"];
}
