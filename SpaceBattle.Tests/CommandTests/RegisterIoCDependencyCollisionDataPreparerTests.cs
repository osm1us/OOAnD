using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyCollisionDataPreparerTests
    {
        public RegisterIoCDependencyCollisionDataPreparerTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

            new RegisterIoCDependencyShapeIdentifier().Execute();
        }

        [Fact]
        public void Execute_Should_Register_CollisionDataPreparer()
        {
            new RegisterIoCDependencyCollisionDataPreparer().Execute();

            var preparer = IoC.Resolve<ICollisionDataPreparer>("Collision.DataPreparer");
            Assert.NotNull(preparer);
        }
    }
}
