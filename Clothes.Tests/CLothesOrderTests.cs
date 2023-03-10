using Clothes.Implementations;
using Clothes.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Clothes.Tests
{
    public class CLothesOrderTests
    {
        private readonly IServiceProvider _serviceProvider;

        public CLothesOrderTests()
        {
            // setup our DI
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IDFSService, DFSService>()
                .BuildServiceProvider();
        }        

        [Fact]
        public void BuildDependencyGraph_ReturnsCorrectGraph()
        {
            // Arrange
            string[,] depMatrix = CreateInputMatrix();
            var dfsService = _serviceProvider.GetService<IDFSService>();

            // Act
            Dictionary<string, List<string>> depGraph = dfsService.BuildDependencyGraph(depMatrix);

            // Assert
            Assert.NotNull(depGraph);
            Assert.Equal(12, depGraph.Count);
            Assert.Contains("dress shirt", depGraph.Keys);
            Assert.Contains("pants", depGraph.Keys);
            Assert.Contains("suit jacket", depGraph.Keys);
            Assert.Contains("tie", depGraph.Keys);
            Assert.Contains("belt", depGraph.Keys);
            Assert.Contains("overcoat", depGraph.Keys);
            Assert.Contains("sun glasses", depGraph.Keys);
            Assert.Contains("left sock", depGraph.Keys);
            Assert.Contains("left shoe", depGraph.Keys);
            Assert.Contains("right sock", depGraph.Keys);
            Assert.Contains("right shoe", depGraph.Keys);
            Assert.Single(depGraph["dress shirt"]);
            Assert.Equal(3, depGraph["pants"].Count);
            Assert.Equal(5, depGraph["suit jacket"].Count);
            Assert.Single(depGraph["tie"]);
            Assert.Single(depGraph["belt"]);
            Assert.Single(depGraph["sun glasses"]);
            Assert.Single(depGraph["left shoe"]);
            Assert.Single(depGraph["right shoe"]);
        }

        [Fact]
        public void PerformDFS_ReturnsCorrectOrder()
        {
            // Arrange
            string[,] depMatrix = CreateInputMatrix();
            var dfsService = _serviceProvider.GetService<IDFSService>();

            // Act
            Dictionary<string, List<string>> depGraph = dfsService.BuildDependencyGraph(depMatrix);
            List<List<string>> order = dfsService.PerformDFS(depGraph);

            // Assert
            // Assert
            Assert.NotNull(order);
            Assert.Equal(4, order.Count);
            Assert.Equal("dress shirt,t-shirt", string.Join(",", order[0].OrderBy(x => x)));
            Assert.Equal("left sock,pants,right sock", string.Join(",", order[1].OrderBy(x => x)));
            Assert.Equal("belt,suit jacket,tie", string.Join(",", order[2].OrderBy(x => x)));
            Assert.Equal("left shoe,overcoat,right shoe,sun glasses", string.Join(",", order[3].OrderBy(x => x)));
        }

        private string[,] CreateInputMatrix()
        {
            return new string[,]
            {
                {"t-shirt", "dress shirt"},
                {"dress shirt", "pants"},
                {"dress shirt", "suit jacket"},
                {"tie", "suit jacket"},
                {"pants", "suit jacket"},
                {"belt", "suit jacket"},
                {"suit jacket", "overcoat"},
                {"dress shirt", "tie"},
                {"suit jacket", "sun glasses"},
                {"sun glasses", "overcoat"},
                {"left sock", "pants"},
                {"pants", "belt"},
                {"suit jacket", "left shoe"},
                {"suit jacket", "right shoe"},
                {"left shoe", "overcoat"},
                {"right sock", "pants"},
                {"right shoe", "overcoat"},
                {"t-shirt", "suit jacket"}
            };
        }
    }
}
