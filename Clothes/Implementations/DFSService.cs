using Clothes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothes.Implementations
{
    public class DFSService : IDFSService
    {
        /// <summary>
        /// Builds a dependency graph from bidimensional array
        /// </summary>
        /// <param name="matrix"> Bidimensional array of dependency - item pairs </param>
        /// <returns> Dictionary of items with list of dependencies (aka before items) </returns>
        public Dictionary<string, List<string>> BuildDependencyGraph(string[,] matrix)
        {
            // Build a directed acyclic graph (DAG) from the input
            var depGraph = new Dictionary<string, List<string>>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var dependency = matrix[i, 0];
                var item = matrix[i, 1];

                if (!depGraph.ContainsKey(item))
                {
                    depGraph[item] = new List<string>();
                }
                if (!string.IsNullOrEmpty(dependency))
                {
                    if (!depGraph.ContainsKey(dependency))
                    {
                        depGraph[dependency] = new List<string>();
                    }
                    depGraph[item].Add(dependency);
                }
            }

            return depGraph;
        }

        /// <summary>
        /// Performs Depth First Search algorithm on provided graph
        /// </summary>
        /// <param name="graph"> Input graph to perform DFS, in this case dictionary of strings with dependencies </param>
        /// <returns> List of List<string> with items that belongs to the same group (priority) </returns>
        public List<List<string>> PerformDFS(Dictionary<string, List<string>> graph)
        {
            

            // Perform a depth-first search (DFS) on the graph starting from each unvisited root node
            var visited = new HashSet<string>();
            var order = new List<List<string>>();
            foreach (var item in graph.Keys)
            {
                if (!visited.Contains(item))
                {
                    var group = new List<string>();
                    DFS(item, graph, visited, group);
                    order.Add(group);
                }
            }

            return order;
        }

        // Recursive DFS auxiliar function to visit each node in the graph
        private void DFS(string item, Dictionary<string, List<string>> graph, HashSet<string> visited, List<string> group)
        {
            visited.Add(item);
            foreach (var dependency in graph[item])
            {
                if (!visited.Contains(dependency))
                {
                    DFS(dependency, graph, visited, group);
                }
            }
            group.Add(item);
        }
    }
}
