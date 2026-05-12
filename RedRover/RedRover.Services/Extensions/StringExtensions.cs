using RedRover.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RedRover.Services.Extensions;

public static partial class StringExtensions
{
    [GeneratedRegex("^.*?(?=\\(|\\)|$)")]
    private static partial Regex BeforeParentheses();

    public static List<Node> ToNodes(this string text)
    {
        List<Node> nodes = new List<Node>();

        text = Unwrap(text);

        while (!string.IsNullOrWhiteSpace(text))
        {
            List<Node> subNodes = new List<Node>();
            (string node, string remainingText) = text.SplitOnFirst(',');

            string name = BeforeParentheses().Match(node).Groups[0].Value.Trim();

            if (node.Contains("(")) {
                string subset = text.FindParenthesesPair();
                text = text.Replace(subset, "");
                text = text.TrimStart(name.ToCharArray());
                text = text.TrimStart(',').Trim();
                subNodes = subset.ToNodes();
            }
            else
            {
                text = remainingText;
            }

            nodes.Add(new Node() { Name = name, SubNodes = subNodes });     

        }
        return nodes;
    }

    internal static string Unwrap(string text, char start = '(', char end = ')')
    {
        if(text.LastIndexOf(end) != text.Length-1)
            return text;

        text = text.Remove(text.Length - 1); 

        return text = text.TrimStart(start);
    }

    public static (string, string) SplitOnFirst(this string text, char firstInstance)
    {
        int index = text.IndexOf(firstInstance);
        if (index < 0)
            return (text, "");

        return (text.Substring(0, index), text.Substring(index+1).TrimStart());
    }

    public static string FindParenthesesPair(this string text)
    {
        if (!text.HasEvenNumberOfBrackets())
            return "";

        var matches = Regex.Match(text, @"\((?:[^()]*|\((?:[^()]*|\([^()]*\))\))*\)");
        return matches.Groups.Count > 0 ? matches.Groups[0].Value : string.Empty;
    }

    public static bool HasEvenNumberOfBrackets(this string text, char start = '(', char end = ')')
    {
        return text.Count(x => x == start) == text.Count(x => x == end);
    }

}
