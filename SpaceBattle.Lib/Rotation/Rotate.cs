namespace SpaceBattle.Lib;

public interface IRotating
{
    Angle AnglePos { get; set; }
    Angle RotateVelocity { get; }
}

public class RotateCommand : ICommand
{
    private readonly IRotating obj;
    public RotateCommand(IRotating obj)
    {
        this.obj = obj;
    }
    public void Execute()
    {
        obj.AnglePos = obj.AnglePos + obj.RotateVelocity;
    }
}
