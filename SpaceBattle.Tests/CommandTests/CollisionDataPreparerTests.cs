using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;

namespace SpaceBattle.Tests
{
    public class CollisionDataPreparerTests
    {
        public CollisionDataPreparerTests()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<ICommand>("Scopes.Current.Set",
                IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
                ).Execute();
        }

        [Fact]
        public void PrepareCollisionData_ReturnsCorrectSequence()
        {
            var mockShapeIdentifier = new Mock<IShapeIdentifier>();

            mockShapeIdentifier.Setup(s => s.GetShapeId("square")).Returns("square".GetHashCode());
            mockShapeIdentifier.Setup(s => s.GetShapeId("triangle")).Returns("triangle".GetHashCode());

            var position1 = new Vector(10, 20);
            var position2 = new Vector(5, 15);
            var velocity1 = new Vector(1, 2);
            var velocity2 = new Vector(3, 4);

            var preparer = new CollisionDataPreparer(mockShapeIdentifier.Object);
            var result = preparer.PrepareCollisionData(
                position1, velocity1, "square",
                position2, velocity2, "triangle").ToArray();

            Assert.NotNull(result);

            Assert.Equal(10, result.Length);

            Assert.Equal(10, result[0]);
            Assert.Equal(20, result[1]);
            Assert.Equal(5, result[2]);
            Assert.Equal(15, result[3]);

            Assert.Equal(1, result[4]);
            Assert.Equal(2, result[5]);
            Assert.Equal(3, result[6]);
            Assert.Equal(4, result[7]);

            Assert.Equal("square".GetHashCode(), result[8]);
            Assert.Equal("triangle".GetHashCode(), result[9]);
        }

        [Fact]
        public void PrepareCollisionData_WithDifferentShapes_ReturnsCorrectShapeIds()
        {
            var mockShapeIdentifier = new Mock<IShapeIdentifier>();

            mockShapeIdentifier.Setup(s => s.GetShapeId("square")).Returns("square".GetHashCode());
            mockShapeIdentifier.Setup(s => s.GetShapeId("triangle")).Returns("triangle".GetHashCode());
            mockShapeIdentifier.Setup(s => s.GetShapeId("circle")).Returns("circle".GetHashCode());
            mockShapeIdentifier.Setup(s => s.GetShapeId("unknown")).Returns("unknown".GetHashCode());

            var position = new Vector(0, 0);
            var velocity = new Vector(0, 0);

            var preparer = new CollisionDataPreparer(mockShapeIdentifier.Object);

            var result1 = preparer.PrepareCollisionData(
                position, velocity, "square",
                position, velocity, "triangle").ToArray();

            Assert.Equal("square".GetHashCode(), result1[8]);
            Assert.Equal("triangle".GetHashCode(), result1[9]);

            var result2 = preparer.PrepareCollisionData(
                position, velocity, "circle",
                position, velocity, "unknown").ToArray();

            Assert.Equal("circle".GetHashCode(), result2[8]);
            Assert.Equal("unknown".GetHashCode(), result2[9]);
        }
    }
}
