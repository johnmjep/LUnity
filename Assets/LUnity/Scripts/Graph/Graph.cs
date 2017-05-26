using System.Collections.Generic;

namespace LUnityGraph
{
    /// <summary>
    /// Graph Class
    /// </summary>
    /// <remarks>Maintains a set of Vertices and a set of Edges</remarks>
    /// <typeparam name="T">Data type to be held in vertices</typeparam>
    public class Graph<T>
    {
        #region Fields
        private List<Vertex<T>> _vertexSet;
        public List<Vertex<T>> VertexSet
        {
            get { return _vertexSet; }
        }

        private List<Edge<T>> _edgeSet;
        public List<Edge<T>> EdgeSet
        {
            get { return _edgeSet; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Graph()
            : this(null)
        { }
        /// <summary>
        /// Constructor to initialise Vertex Set
        /// </summary>
        /// <param name="vertexSet">Vertex Set to initialise with</param>
        public Graph(List<Vertex<T>> vertexSet)
        {
            if (vertexSet == null)
            {
                _vertexSet = new List<Vertex<T>>();
            }
            else
            {
                _vertexSet = vertexSet;
            }
            _edgeSet = new List<Edge<T>>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the provided Vertex to the Vertex set
        /// </summary>
        /// <param name="vertex">Vertex to add</param>
        public void AddVertex(Vertex<T> vertex)
        {
            _vertexSet.Add(vertex);
        }

        /// <summary>
        /// Adds a vertex with the specified value to the Vertex set
        /// </summary>
        /// <param name="value">Value of the vertex to add</param>
        public void AddVertex(T value)
        {
            AddVertex(new Vertex<T>(value));
        }

        /// <summary>
        /// Adds a unidirectional edge between the specified verticies
        /// </summary>
        /// <param name="from">Start of Edge</param>
        /// <param name="to">End of Edge</param>
        public void AddDirectedEdge(Vertex<T> from, Vertex<T> to)
        {
            from.AdjacentVertices.Add(to);
            _edgeSet.Add(new Edge<T>(from, to));
        }

        /// <summary>
        /// Adds edges in both directions between the specied Vertices
        /// </summary>
        /// <param name="from">Start of Edge</param>
        /// <param name="to">End of Edge</param>
        public void AddUndirectedEdge(Vertex<T> from, Vertex<T> to)
        {
            from.AdjacentVertices.Add(to);
            _edgeSet.Add(new Edge<T>(from, to));
            to.AdjacentVertices.Add(from);
            _edgeSet.Add(new Edge<T>(to, from));
        }

        /// <summary>
        /// Returns true if Vertex list contains a vertex with the specified value
        /// </summary>
        /// <param name="value">Value of Vertex to search for</param>
        /// <returns>True if value exists in list</returns>
        public bool Contains(T value)
        {
            return _vertexSet.Exists(v => v.Data.Equals(value));
        }

        /// <summary>
        /// Removes a Vertex with the specified value from the list
        /// </summary>
        /// <param name="value">Value held by vertex</param>
        /// <returns>True if successful</returns>
        public bool Remove(T value)
        {
            bool success = false;
            if (Contains(value))
            {
                Vertex<T> vToRemove = _vertexSet.Find(v => v.Data.Equals(value));
                _vertexSet.Remove(vToRemove);

                foreach (Vertex<T> v in _vertexSet)
                {
                    int index = v.AdjacentVertices.IndexOf(vToRemove);
                    if (index != -1)
                    {
                        v.AdjacentVertices.RemoveAt(index);
                    }
                }
                success = true;
                RefreshEdges();
            }
            return success;
        }

        /// <summary>
        /// Clears edge list and generates it from scratch
        /// </summary>
        private void RefreshEdges()
        {
            _edgeSet.Clear();
            foreach (Vertex<T> from in _vertexSet)
            {
                foreach (Vertex<T> to in from.AdjacentVertices)
                {
                    _edgeSet.Add(new Edge<T>(from, to));
                }
            }
        }
        #endregion
    }
}