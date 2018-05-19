using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortestPathAlgorithm
{
    //Finds the shortest path of all vertices 
    //from a given source vertex
    //using Dijkstra's algorithm
    class ShortestPathAlgorithm
    {
        //Number of Vertices in the graph
        const int vertices = 9;

        static void Main(string[] args)
        {
            //Create a graph with 9 vertices and define all distances between the vertices.
            int[,] graph = new int[,]
                    {
                      {0, 4, 0, 0, 0, 0, 0, 8, 0},
                      {4, 0, 8, 0, 0, 0, 0, 11, 0},
                      {0, 8, 0, 7, 0, 4, 0, 0, 2},
                      {0, 0, 7, 0, 9, 14, 0, 0, 0},
                      {0, 0, 0, 9, 0, 10, 0, 0, 0},
                      {0, 0, 4, 14, 10, 0, 2, 0, 0},
                      {0, 0, 0, 0, 0, 2, 0, 1, 6},
                      {8, 11, 0, 0, 0, 0, 1, 0, 7},
                      {0, 0, 2, 0, 0, 0, 6, 7, 0}
                    };
            
            //Set all 0 values in the graph to the max value.
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    if (graph[i, j] == 0)
                    { 
                        graph[i, j] = Int32.MaxValue;
                    }
                }
            }

            ShortestPathAlgorithm s = new ShortestPathAlgorithm();
            s.ApplyDijkstra(graph, 0);
        }

        //Implements Dijkstra's algorithm for finding shortest path.
        void ApplyDijkstra(int[,] graph, int sourceVertex)
        {
            //Array stores the minimum distance of each vertex from the source vertex
            int[] arrDistance = new int[vertices];
            
            //Array stores the list of vertices in the shortest path
            Boolean[] arrShortestPath = new Boolean[vertices];

            //Array traces the shortest path taken from the source vertex to each vertice
            string[] arrParentPath = new string[vertices];

            //Initialize all vertices to max value in the distance array
            //Initialize all vertices to false indicating they are not in the shortest path
            for (int i = 0; i < arrDistance.Length; i++)
            {
                //Distance of the source vertex is 0. All others will be set to max value
                arrDistance[i] = i == sourceVertex ? 0 : Int32.MaxValue;
                arrShortestPath[i] = false;
            }

            //Set the parent path for the source vertex
            arrParentPath[sourceVertex] = sourceVertex.ToString();

            //Calculate the shortest path to all the vertices from the source vertex
            for (int i = 0; i < arrDistance.Length - 1; i++)
            {
                //Go through all the vertices not in shortest path array and pick the vertex with the minimum distance
                int spVertex = this.FindMinimumDistance(arrDistance, arrShortestPath);

                //Include this vertex in the shortest path array by setting it to true
                arrShortestPath[spVertex] = true;

                //Run through the adjoining vertices of spVertex (those not having max value)
                //while excluding those vertices already present in the shortest path array
                //Then store the distance to each adjoining vertex from the source vertex
                for (int j = 0; j < arrDistance.Length; j++)
                { 
                    if (!arrShortestPath[j] 
                        && graph[spVertex, j] != Int32.MaxValue 
                        && arrDistance[spVertex] + graph[spVertex, j] < arrDistance[j])
                    { 
                        arrDistance[j] = arrDistance[spVertex] + graph[spVertex, j];
                        arrParentPath[j] = arrParentPath[spVertex] + "," + j;
                    }
                }
            }

            //Print the shortest distance of each vertex from the source vertex.
            Console.WriteLine("Vertex \t\t Source Vertex Distance \t Shortest Path Trace");
            for (int i = 0; i < arrDistance.Length; i++)
            {
                Console.WriteLine(i + " \t\t " + arrDistance[i] + " \t\t\t\t " + arrParentPath[i]);
            }

            // print the constructed distance array
            //this.printSolution(arrDistance, V);
        }

        //Calculates the vertice with minimum distance from the source vertex  
        int FindMinimumDistance(int[] arrDistance, Boolean[] arrShortestPath)
        {
            int minDistance = Int32.MaxValue;
            int vertex = 0;

            for (int i = 0; i < arrDistance.Length; i++)
            { 
                //Find the vertice that is not in the shortest path array
                //and has the minimum distance from the source vertex
                if (!arrShortestPath[i] && arrDistance[i] <= minDistance)
                {
                    minDistance = arrDistance[i];
                    vertex = i;
                }
            }
            return vertex;
        }

    }
}
