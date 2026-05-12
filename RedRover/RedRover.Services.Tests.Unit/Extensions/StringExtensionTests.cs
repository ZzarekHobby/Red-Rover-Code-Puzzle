using RedRover.Services.Extensions;
using RedRover.Services.Models;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace RedRover.Services.Tests.Unit.Extensions;

public class StringExtensionTests
{
    [Fact]
    public void ToNodes_MultipleParentheses_GetsCorrectSubnodes()
    {
        string testString = "(magic(string), test, test2(example, other))";

        List<Node> expectedNodes = new List<Node>
        {
            new Node { Name = "magic",
                      SubNodes = new List<Node> {
                          new Node { Name = "string" }
                      }
            },
            new Node { Name = "test" },
            new Node { Name = "test2",
                      SubNodes = new List<Node> {
                          new Node { Name = "example" },
                          new Node { Name = "other" },
                      }
            },
        };

        var result = StringExtensions.ToNodes(testString);

        Assert.Equal(expectedNodes.Count, result.Count);
        Assert.Equal(expectedNodes.First(x => x.Name == "test2").SubNodes.Count,
            result.First(x => x.Name == "test2").SubNodes.Count);
    }

    [Fact]
    public void ToNodes_MultipleNestedParentheses_GetsCorrectSubnodes()
    {
        string testString = "(magic(string(hocus, pocus)), test, test2)";

        List<Node> expectedNodes = new List<Node>
        {
            new Node { Name = "magic",
                      SubNodes = new List<Node> {
                          new Node { Name = "string",
                              SubNodes = new List<Node> {
                                new Node { Name = "hocus" },
                                new Node { Name = "pocus" }
                              }
                      }
                }
            },
            new Node { Name = "test" },
            new Node { Name = "test2" }
        };

        var result = StringExtensions.ToNodes(testString);

        Assert.Equal(expectedNodes.Count, result.Count);
        Assert.Equal(expectedNodes.First().SubNodes.First().SubNodes.Count,
            result.First().SubNodes.First().SubNodes.Count);
    }

    [Fact]
    public void Unwrap_MultipleParentheses_DoesNotDeleteMultiple()
    {
        string testString = "(IHadOverzealous(Deletions))";
        string expected = "IHadOverzealous(Deletions)";

        var resut = StringExtensions.Unwrap(testString);
        Assert.Equal(expected, resut);
    }

    [Theory]
    [InlineData(",magic", "", "magic")]
    [InlineData("magic, string", "magic", "string")]
    [InlineData("magicstring", "magicstring", "")]
    [InlineData("my,magic, string", "my", "magic, string")]
    public void SplitOnFirst_WithComma_ReturnsExpected(string testString, string expectedFirst, string expectedSecond)
    {

        (string firstResult, string secondResult) = testString.SplitOnFirst(',');
        Assert.Equal(expectedFirst, firstResult);
        Assert.Equal(expectedSecond, secondResult);
    }

    [Theory]
    [InlineData("magic", "")]
    [InlineData("magic(string)", "(string)")]
    [InlineData("magic (string)", "(string)")]
    [InlineData("expected(magic)(string)", "(magic)")]
    public void FindParenthesesPair_HappyPath_ReturnsExpected(string testString, string expected)
    {

        var result = testString.FindParenthesesPair();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("magic(string(substring))", "(string(substring))")]
    [InlineData("magic(str(substring)ing)", "(str(substring)ing)")]
    [InlineData("magic((m)s(ag)tr(substring)in(ic)g)", "((m)s(ag)tr(substring)in(ic)g)")]
    public void FindParenthesesPair_WithSubsets_ReturnsOuter(string testString, string expected)
    {
        var result = testString.FindParenthesesPair();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("magic(string(substring)", "")]
    [InlineData("magicstr(substring)ing)", "")]
    [InlineData("magic((m)s(agtr(substring)in(ic)g)", "")]
    public void FindParenthesesPair_BadInput_HandlesGracefully(string testString, string expected)
    {
        var result = testString.FindParenthesesPair();
        Assert.Equal(expected, result);
    }


    [Theory]
    [InlineData("magic((m)s(ag)tr(substring)in(ic)g)", true)]
    [InlineData("magic((m)s(agtr(substring)in(ic)g)", false)]
    public void HasEvenNumberOfBrackets_GivenInput_ReturnsExpected(string testString, bool expected)
    {
        var result = testString.HasEvenNumberOfBrackets();
        Assert.Equal(expected, result);
    }

}