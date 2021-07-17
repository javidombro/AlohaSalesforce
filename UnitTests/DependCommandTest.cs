using AlohaSalesforce.Commands;
using AlohaSalesforce.Entities;
using FluentAssertions;
using System.Linq;

namespace UnitTests
{
    public class DependCommandTest
    {
        // Test disabled (uncomment the line bellow to use it)
        //[Fact]
        public void Depend_A_B_ShouldReturn_ExpectedOutput()
        {
            //Arrange
            var command = new DependCommand();
            var args = new string[]
            {
                "DEPEND", "P1", "P2"
            };

            //Act
            var result = command.Execute(args);

            //Assert
            result.Should().Be("DEPEND P1 P2");
            Component.knownComponents.Should().HaveCount(2);
            var dependenciesFromA = Component.knownComponents.SingleOrDefault(c => c.Key == "P1").Value.Dependencies;
            dependenciesFromA.Should().HaveCount(1);
            dependenciesFromA.FirstOrDefault().Name.Should().Be("P2");
        }

    }
}
