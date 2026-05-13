using RedRover.Services.Extensions;
using RedRover.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRover.Services.Services;

public class NodeService
{
    public const string DEFAULT_INPUT = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
    private const string double_space = "  ";

    public NodeService()
    {
            
    }

    public string ReformatString(string validInput, bool orderAlphabetically = false)
    {
        if (string.IsNullOrEmpty(validInput) || !validInput.HasEvenNumberOfBrackets())
            validInput = DEFAULT_INPUT;

        var nodes = validInput.ToNodes();
        return FormatNodesToText(nodes, orderAlphabetically);
    }

    internal string FormatNodesToText(List<Node> nodes, bool orderAlphabetically = false)
    {
        StringBuilder sb = BuildNodeString(nodes, orderAlphabetically: orderAlphabetically);
        return sb.ToString();
    }

    private StringBuilder BuildNodeString(List<Node> nodes, string prefix = "", bool orderAlphabetically = false) {
        StringBuilder builder = new StringBuilder();

        if (orderAlphabetically)
            nodes = nodes.OrderBy(x => x.Name).ToList();

        foreach (Node node in nodes) {
            builder.AppendLine($"{prefix} - {node.Name}");

            if(node.SubNodes.Any())
                builder.Append(BuildNodeString(node.SubNodes, $"{double_space}{prefix}", orderAlphabetically));
        }

        return builder;
    }
}
