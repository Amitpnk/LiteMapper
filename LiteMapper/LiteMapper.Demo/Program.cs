using LiteMapper.Extensions;

var order = new SourceOrder
{
    Id = "ORD123",
    Items = new List<SourceItem>
    {
        new SourceItem { Name = "Item1", Price = 10 },
        new SourceItem { Name = "Item2", Price = 20 }
    }
};

var mappedOrder = ObjectMapper.Map<SourceOrder, DestinationOrder>(order);


Console.WriteLine(mappedOrder.Id);

public class SourceOrder
{
    public string Id { get; set; }
    public List<SourceItem> Items { get; set; }
}

public class SourceItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class DestinationOrder
{
    public string Id { get; set; }
    public List<DestinationItem> Items { get; set; }
}

public class DestinationItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Usage: