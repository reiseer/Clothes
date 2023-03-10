using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothes.Interfaces
{
    public interface IDFSService
    {
        /// <summary>
        /// Builds a dependency graph from bidimensional array
        /// </summary>
        /// <param name="matrix"> Bidimensional array of dependency - item pairs </param>
        /// <returns> Dictionary of items with list of dependencies (aka before items) </returns>
        Dictionary<string, List<string>> BuildDependencyGraph(string[,] matrix);

        /// <summary>
        /// Performs Depth First Search algorithm on provided graph
        /// </summary>
        /// <param name="graph"> Input graph to perform DFS, in this case dictionary of strings with dependencies </param>
        /// <returns> List of List<string> with items that belongs to the same group (priority) </returns>
        List<List<string>> PerformDFS(Dictionary<string, List<string>> graph);
    }
}
