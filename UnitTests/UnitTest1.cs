using AlohaSalesforce;
using FluentAssertions;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void CompleteTest_ShouldReturn_ExpectedOutput()
        {
            //Arrange
            string input = @"DEPEND TELNET TCPIP NETCARD
                            DEPEND TCPIP NETCARD
                            DEPEND NETCARD TCPIP
                            DEPEND DNS TCPIP NETCARD
                            DEPEND BROWSER TCPIP HTML
                            INSTALL NETCARD
                            INSTALL TELNET
                            INSTALL foo
                            REMOVE NETCARD
                            INSTALL BROWSER
                            INSTALL DNS
                            LIST
                            REMOVE TELNET
                            REMOVE NETCARD
                            REMOVE DNS
                            REMOVE NETCARD
                            INSTALL NETCARD
                            REMOVE TCPIP
                            REMOVE BROWSER
                            REMOVE TCPIP
                            LIST
                            END";
            using TextReader reader = new StringReader(input);
            Console.SetIn(reader);
            using StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            string expected = "DEPEND TELNET TCPIP NETCARD\r\n" +
                                "DEPEND TCPIP NETCARD\r\n" +
                                "DEPEND NETCARD TCPIP\r\n" +
                                "TCPIP depends on NETCARD, ignoring command\r\n" +
                                "DEPEND DNS TCPIP NETCARD\r\n" +
                                "DEPEND BROWSER TCPIP HTML\r\n" +
                                "INSTALL NETCARD\r\n" +
                                "Installing NETCARD\r\n" +
                                "INSTALL TELNET\r\n" +
                                "Installing TCPIP\r\n" +
                                "Installing TELNET\r\n" +
                                "INSTALL foo\r\n" +
                                "Installing foo\r\n" +
                                "REMOVE NETCARD\r\n" +
                                "NETCARD is still needed\r\n" +
                                "INSTALL BROWSER\r\n" +
                                "Installing HTML\r\n" +
                                "Installing BROWSER\r\n" +
                                "INSTALL DNS\r\n" +
                                "Installing DNS\r\n" +
                                "LIST\r\n" +
                                "BROWSER\r\n" +
                                "DNS\r\n" +
                                "foo\r\n" +
                                "HTML\r\n" +
                                "NETCARD\r\n" +
                                "TCPIP\r\n" +
                                "TELNET\r\n" +
                                "REMOVE TELNET\r\n" +
                                "Removing TELNET\r\n" +
                                "REMOVE NETCARD\r\n" +
                                "NETCARD is still needed\r\n" +
                                "REMOVE DNS\r\n" +
                                "Removing DNS\r\n" +
                                "REMOVE NETCARD\r\n" +
                                "NETCARD is still needed\r\n" +
                                "INSTALL NETCARD\r\n" +
                                "NETCARD is already installed\r\n" +
                                "REMOVE TCPIP\r\n" +
                                "TCPIP is still needed\r\n" +
                                "REMOVE BROWSER\r\n" +
                                "Removing BROWSER\r\n" +
                                "Removing TCPIP\r\n" +
                                "Removing HTML\r\n" +
                                "REMOVE TCPIP\r\n" +
                                "TCPIP is not installed\r\n" +
                                "LIST\r\n" +
                                "foo\r\n" +
                                "NETCARD\r\n" +
                                "END\r\n";

            //Act
            Program.Main();

            //Assert
            var result = sw.ToString();
            result = Regex.Replace(result, @"\s+", string.Empty);
            expected = Regex.Replace(expected, @"\s+", string.Empty);
            expected.Should().Be(result);

        }
    }
}
