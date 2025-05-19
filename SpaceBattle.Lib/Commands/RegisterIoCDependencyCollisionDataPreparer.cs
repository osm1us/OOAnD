namespace SpaceBattle.Lib
{
    public class RegisterIoCDependencyCollisionDataPreparer : ICommand
    {
        public void Execute()
        {
            IoC.Resolve<ICommand>(
                "IoC.Register",
                "Collision.DataPreparer",
                (object[] args) => new CollisionDataPreparer(
                    IoC.Resolve<IShapeIdentifier>("Collision.ShapeIdentifier")
                )
            ).Execute();
        }
    }
}
