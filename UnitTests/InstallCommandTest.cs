using AlohaSalesforce.Commands;
using AlohaSalesforce.Entities;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class InstallCommandTest
    {
        [Fact]
        public void Depend_A_B_And_Depend_B_C_ShouldBeTransitive()
        {
            //Arrange
            var dependCommand = new DependCommand();
            var args_1 = new string[]
            {
                "DEPEND", "A", "B"
            };
            var args_2 = new string[]
            {
                "DEPEND", "B", "C"
            };

            var installCommand = new InstallCommand();
            var args_3 = new string[]
            {
                "INSTALL", "A"
            };
            var expected = "INSTALL A\r\nInstalling C\r\nInstalling B\r\nInstalling A\r\n";

            //Act
            dependCommand.Execute(args_1);
            dependCommand.Execute(args_2);
            var result = installCommand.Execute(args_3);

            //Assert
            result.Should().Be(expected);
            Component.knownComponents.Should().HaveCount(3);
            Component.knownComponents.All(c => c.Value.IsInstalled).Should().BeTrue();
            var Acomponent = Component.knownComponents.Single(c => c.Value.Name == "A").Value;
            var Bcomponent = Component.knownComponents.Single(c => c.Value.Name == "B").Value;
            var Ccomponent = Component.knownComponents.Single(c => c.Value.Name == "C").Value;
            Acomponent.ExplicityInstalled.Should().BeTrue();
            Bcomponent.ExplicityInstalled.Should().BeFalse();
            Ccomponent.ExplicityInstalled.Should().BeFalse();
        }
    }
}
