using AlohaSalesforce.Commands;
using AlohaSalesforce.Entities;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class DependCommandTest
    {
        [Fact]
        public void Depend_A_B_ShouldReturn_ExpectedOutput()
        {
            //Arrange
            var command = new DependCommand();
            var args = new string[]
            {
                "DEPEND", "A", "B"
            };

            //Act
            var result = command.Execute(args);

            //Assert
            result.Should().Be("DEPEND A B");
            Component.knownComponents.Should().HaveCount(2);
            var dependenciesFromA = Component.knownComponents.SingleOrDefault(c => c.Key == "A").Value.Dependencies;
            dependenciesFromA.Should().HaveCount(1);
            dependenciesFromA.FirstOrDefault().Name.Should().Be("B");
        }
        
    }
}
