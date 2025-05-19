using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class RegisterIoCDependencyShapeIdentifierTests
    {
        public RegisterIoCDependencyShapeIdentifierTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        }

        [Fact]
        public void Execute_Should_Register_ShapeIdentifier()
        {
            new RegisterIoCDependencyShapeIdentifier().Execute();

            var shapeIdentifier = IoC.Resolve<IShapeIdentifier>("Collision.ShapeIdentifier");
            Assert.NotNull(shapeIdentifier);

            var result = shapeIdentifier.GetShapeId("square");
            Assert.Equal("square".ToLower().GetHashCode(), result);
        }
    }
}
