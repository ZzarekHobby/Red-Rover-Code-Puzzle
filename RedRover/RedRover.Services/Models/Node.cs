namespace RedRover.Services.Models;

public class Node
{
    public string Name { get; set; } = null!;
    public List<Node> SubNodes { get; set; } = new List<Node>();
}
