using LiteMapper.Extensions;

namespace LiteMapper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Map_ShouldMapSimplePropertiesCorrectly()
        {
            // Arrange
            var sourceOrder = new SourceOrder
            {
                Id = "ORD123",
                Items = new List<SourceItem>
                {
                    new SourceItem { Name = "Item1", Price = 10 },
                    new SourceItem { Name = "Item2", Price = 20 }
                }
            };

            // Act
            var destinationOrder = ObjectMapper.Map<SourceOrder, DestinationOrder>(sourceOrder);

            // Assert
            Assert.NotNull(destinationOrder);
            Assert.Equal(sourceOrder.Id, destinationOrder.Id);
            Assert.NotNull(destinationOrder.Items);
            Assert.Equal(sourceOrder.Items.Count, destinationOrder.Items.Count);
        }

        [Fact]
        public void Map_ShouldMapNestedObjectsCorrectly()
        {
            // Arrange
            var sourceOrder = new SourceOrder
            {
                Id = "ORD456",
                Items = new List<SourceItem>
                {
                    new SourceItem { Name = "NestedItem1", Price = 15 },
                    new SourceItem { Name = "NestedItem2", Price = 25 }
                }
            };

            // Act
            var destinationOrder = ObjectMapper.Map<SourceOrder, DestinationOrder>(sourceOrder);

            // Assert
            Assert.NotNull(destinationOrder.Items);
            for (int i = 0; i < sourceOrder.Items.Count; i++)
            {
                Assert.Equal(sourceOrder.Items[i].Name, destinationOrder.Items[i].Name);
                Assert.Equal(sourceOrder.Items[i].Price, destinationOrder.Items[i].Price);
            }
        }

        [Fact]
        public void Map_ShouldHandleEmptySourceCollection()
        {
            // Arrange
            var sourceOrder = new SourceOrder
            {
                Id = "ORD789",
                Items = new List<SourceItem>() // Empty collection
            };

            // Act
            var destinationOrder = ObjectMapper.Map<SourceOrder, DestinationOrder>(sourceOrder);

            // Assert
            Assert.NotNull(destinationOrder);
            Assert.NotNull(destinationOrder.Items);
            Assert.Empty(destinationOrder.Items);
        }

        [Fact]
        public void Map_ShouldHandleNullSourceObject()
        {
            // Arrange
            SourceOrder sourceOrder = null;

            // Act
            var destinationOrder = ObjectMapper.Map<SourceOrder, DestinationOrder>(sourceOrder);

            // Assert
            Assert.Null(destinationOrder);
        }

        [Fact]
        public void Map_ShouldHandleNullNestedCollection()
        {
            // Arrange
            var sourceOrder = new SourceOrder
            {
                Id = "ORD999",
                Items = null // Null collection
            };

            // Act
            var destinationOrder = ObjectMapper.Map<SourceOrder, DestinationOrder>(sourceOrder);

            // Assert
            Assert.NotNull(destinationOrder);
            Assert.Null(destinationOrder.Items);
        }
    }

    // Source and Destination classes for testing
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
}