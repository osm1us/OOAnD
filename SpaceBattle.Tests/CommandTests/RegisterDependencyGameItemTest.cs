using Hwdtech.Ioc;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class RegisterDependencyGameItemTests
    {
        public RegisterDependencyGameItemTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        }

        [Fact]
        public void GameItem_Add_ShouldPreserveExistingId()
        {
            new RegisterDependencyGameItem().Execute();

            var spaceship = new Dictionary<string, object>
            {
                { "Id", "id" }
            };

            IoC.Resolve<ICommand>("GameItem.Add", spaceship).Execute();

            var retrieved = IoC.Resolve<Dictionary<string, object>>("GameItem.Get", "id");

            Assert.Equal("id", retrieved["Id"]);
        }

        [Fact]
        public void GameItem_Add_Then_Get_ShouldReturnSameItem()
        {
            new RegisterDependencyGameItem().Execute();

            var spaceship = new Dictionary<string, object>();

            IoC.Resolve<ICommand>("GameItem.Add", spaceship).Execute();

            var retrieved = IoC.Resolve<Dictionary<string, object>>("GameItem.Get", spaceship["Id"]);

            Assert.NotNull(retrieved);
            Assert.Equal(spaceship, retrieved);
        }

        [Fact]
        public void GameItem_Add_Duplicate_ShouldThrowException()
        {
            new RegisterDependencyGameItem().Execute();
            var spaceship = new Dictionary<string, object>();

            IoC.Resolve<ICommand>("GameItem.Add", spaceship).Execute();

            Assert.Throws<Exception>(() =>
            {
                IoC.Resolve<ICommand>("GameItem.Add", spaceship).Execute();
            });
        }

        [Fact]
        public void GameItem_Remove_ShouldRemoveItem()
        {
            new RegisterDependencyGameItem().Execute();
            var spaceship = new Dictionary<string, object>();

            IoC.Resolve<ICommand>("GameItem.Add", spaceship).Execute();
            IoC.Resolve<ICommand>("GameItem.Remove", spaceship["Id"]).Execute();

            Assert.Throws<Exception>(() =>
            {
                IoC.Resolve<Dictionary<string, object>>("GameItem.Get", spaceship["Id"]);
            });
        }

        [Fact]
        public void GameItem_Get_Nonexistent_ShouldThrowException()
        {
            new RegisterDependencyGameItem().Execute();

            Assert.Throws<Exception>(() =>
            {
                IoC.Resolve<Dictionary<string, object>>("GameItem.Get", Guid.NewGuid().ToString());
            });
        }
    }
}
