using Clothes.Implementations;
using Clothes.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        //setup our DI
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IDFSService, DFSService>()
            .BuildServiceProvider();

        // Bidimensional array of dependencies,items
        var depMatrix = new string[,]
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

        // get instance of DFS Service
        var dfsService = serviceProvider.GetService<IDFSService>();

        // create dependency graph
        var depGraph = dfsService.BuildDependencyGraph(depMatrix);

        // perform DFS on dependency graph
        var order = dfsService.PerformDFS(depGraph);

        // Sort the items in each group alphabetically separating groups per line
        var output = string.Join(Environment.NewLine, order.Select(group => string.Join(",", group.OrderBy(x => x))));
        Console.WriteLine(output);
    }
}
