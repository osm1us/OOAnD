namespace SpaceBattle.Lib;

public class RotatingAdapter : IRotating
{
    private readonly IDictionary<string, object> _gameObject;

    public RotatingAdapter(IDictionary<string, object> gameObject)
    {
        _gameObject = gameObject;
    }

    public Angle AnglePos
    {
        get => (Angle)_gameObject["AnglePos"];
        set => _gameObject["AnglePos"] = value;
    }

    public Angle RotateVelocity => (Angle)_gameObject["RotateVelocity"];
}
