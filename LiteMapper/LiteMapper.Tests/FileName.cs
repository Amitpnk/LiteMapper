using LiteMapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteMapper.Tests
{
    public class CustomerMappingTests
    {
        [Fact]
        public void Map_ShouldMapSimplePropertiesCorrectly()
        {
            // Arrange
            var sourceCustomer = new SourceCustomer
            {
                CustomerId = "CUST001",
                FullName = "John Doe",
                Email = "john.doe@example.com"
            };

            // Act
            var destinationCustomer = ObjectMapper.Map<SourceCustomer, DestinationCustomer>(sourceCustomer);

            // Assert
            Assert.NotNull(destinationCustomer);
            Assert.Equal(sourceCustomer.CustomerId, destinationCustomer.Id);
            Assert.Equal(sourceCustomer.FullName, destinationCustomer.Name);
            Assert.Equal(sourceCustomer.Email, destinationCustomer.EmailAddress);
        }

        [Fact]
        public void Map_ShouldHandleNullSourceObject()
        {
            // Arrange
            SourceCustomer sourceCustomer = null;

            // Act
            var destinationCustomer = ObjectMapper.Map<SourceCustomer, DestinationCustomer>(sourceCustomer);

            // Assert
            Assert.Null(destinationCustomer);
        }

        [Fact]
        public void Map_ShouldHandleEmptyStringProperties()
        {
            // Arrange
            var sourceCustomer = new SourceCustomer
            {
                CustomerId = string.Empty,
                FullName = string.Empty,
                Email = string.Empty
            };

            // Act
            var destinationCustomer = ObjectMapper.Map<SourceCustomer, DestinationCustomer>(sourceCustomer);

            // Assert
            Assert.NotNull(destinationCustomer);
            Assert.Equal(string.Empty, destinationCustomer.Id);
            Assert.Equal(string.Empty, destinationCustomer.Name);
            Assert.Equal(string.Empty, destinationCustomer.EmailAddress);
        }

        [Fact]
        public void Map_ShouldHandleNullProperties()
        {
            // Arrange
            var sourceCustomer = new SourceCustomer
            {
                CustomerId = null,
                FullName = null,
                Email = null
            };

            // Act
            var destinationCustomer = ObjectMapper.Map<SourceCustomer, DestinationCustomer>(sourceCustomer);

            // Assert
            Assert.NotNull(destinationCustomer);
            Assert.Null(destinationCustomer.Id);
            Assert.Null(destinationCustomer.Name);
            Assert.Null(destinationCustomer.EmailAddress);
        }
    }

    // Source and Destination classes for testing
    public class SourceCustomer
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class DestinationCustomer
    {
        public string Id { get; set; } // Maps from CustomerId
        public string Name { get; set; } // Maps from FullName
        public string EmailAddress { get; set; } // Maps from Email
    }
}
