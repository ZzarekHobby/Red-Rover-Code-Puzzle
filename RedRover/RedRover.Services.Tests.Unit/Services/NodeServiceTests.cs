using RedRover.Services.Extensions;
using RedRover.Services.Models;
using RedRover.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRover.Services.Tests.Unit.Service;

public class NodeServiceTests
{


    [Fact]
    public void ReformatString_ValidInput_FormatsAsExpected()
    {
        string expectedResult =
            $" - magic{Environment.NewLine}" +
            $"   - string{Environment.NewLine}" +
            $"     - hocus{Environment.NewLine}" +
            $" - first{Environment.NewLine}" +
            $" - test2{Environment.NewLine}" +
            $"   - other{Environment.NewLine}" +
            $"   - example{Environment.NewLine}";

        var nodeService = new NodeService();
        var result = nodeService.ReformatString("(magic(string(hocus)), first, test2(other, example))");
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("((some, test)")]
    [InlineData("(some, test), again)")]
    public void ReformatString_InvalidInput_RunsDefault(string invalidInput)
    {
        string expectedResult =
          $" - id{Environment.NewLine}" +
          $" - name{Environment.NewLine}" +
          $" - email{Environment.NewLine}" +
          $" - type{Environment.NewLine}" +
          $"   - id{Environment.NewLine}" +
          $"   - name{Environment.NewLine}" +
          $"   - customFields{Environment.NewLine}" +
          $"     - c1{Environment.NewLine}" +
          $"     - c2{Environment.NewLine}" +
          $"     - c3{Environment.NewLine}" +
          $" - externalId{Environment.NewLine}";

        var nodeService = new NodeService();
        var result = nodeService.ReformatString(invalidInput);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void FormatNodesToText_NestedNodes_ReturnsCorrectString()
    {
        string expectedResult =
            $" - magic{Environment.NewLine}" +
            $"   - string{Environment.NewLine}" +
            $"     - hocus{Environment.NewLine}" +
            $" - first{Environment.NewLine}" +
            $" - test2{Environment.NewLine}" +
            $"   - other{Environment.NewLine}" +
            $"   - example{Environment.NewLine}";

        List<Node> nodes = new List<Node>
        {
            new Node { Name = "magic",
                      SubNodes = new List<Node> {
                          new Node { Name = "string",
                            SubNodes = new List<Node> {
                                new Node { Name = "hocus" }
                            }
                      }
                  }
            },
            new Node { Name = "first" },
            new Node { Name = "test2",
                      SubNodes = new List<Node> {
                          new Node { Name = "other" },
                          new Node { Name = "example" },
                      }
            },
        };

        var service = new NodeService();

        var result = service.FormatNodesToText(nodes);

        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void FormatNodesToText_NestedNodesAlphabetized_ReturnsCorrectString()
    {
        string expectedResult =
            $" - first{Environment.NewLine}" +
            $" - magic{Environment.NewLine}" +
            $"   - string{Environment.NewLine}" +
            $"     - hocus{Environment.NewLine}" +
            $" - test2{Environment.NewLine}" +
            $"   - example{Environment.NewLine}" +
            $"   - other{Environment.NewLine}";

        List<Node> nodes = new List<Node>
        {
            new Node { Name = "magic",
                      SubNodes = new List<Node> {
                          new Node { Name = "string",
                            SubNodes = new List<Node> {
                                new Node { Name = "hocus" }
                            }
                      }
                  }
            },
            new Node { Name = "first" },
            new Node { Name = "test2",
                      SubNodes = new List<Node> {
                          new Node { Name = "other" },
                          new Node { Name = "example" },
                      }
            },
        };

        var service = new NodeService();

        var result = service.FormatNodesToText(nodes, true);

        Assert.Equal(expectedResult, result);
    }
}
