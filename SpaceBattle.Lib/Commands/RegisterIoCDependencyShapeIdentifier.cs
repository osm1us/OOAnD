namespace SpaceBattle.Lib
{
    public class RegisterIoCDependencyShapeIdentifier : ICommand
    {
        public void Execute()
        {
            IoC.Resolve<ICommand>(
                "IoC.Register",
                "Collision.ShapeIdentifier",
                (object[] args) => new ShapeIdentifier()
            ).Execute();
        }
    }
}
