namespace LUnityGraph
{
    /// <summary>
    /// Edge class, holds two vertices indicating the start and end of the edge
    /// </summary>
    public class Edge<T>
    {
        #region Fields    
        public Vertex<T> VertexA { get; private set; }
        public Vertex<T> VertexB { get; private set; }
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Class Constructor
        /// </summary>
        /// <param name="vertexA">Start vertex of Edge</param>
        /// <param name="vertexB">End vertex of Edge</param>
        public Edge(Vertex<T> vertexA, Vertex<T> vertexB)
        {
            VertexA = vertexA;
            VertexB = vertexB;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Overrides base Equals method
        /// </summary>
        /// <remarks>Returns true if both vertices match</remarks>
        /// <param name="obj">Object to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is Edge<T>)
            {
                Edge<T> eObj = obj as Edge<T>;
                if (eObj.VertexA.Equals(VertexA) &&
                    eObj.VertexB.Equals(VertexB))
                {
                    isEqual = true;
                }
            }
            return isEqual;
        }

        /// <summary>
        /// Overrides base ToString method
        /// </summary>
        /// <returns>String representation of object</returns>
        public override string ToString()
        {
            return string.Format("$$ EDGE | VertexA: {0}, VertexB: {1}", VertexA.ToString(), VertexB.ToString());
        }

        /// <summary>
        /// Overrides base GetHashCode
        /// </summary>
        /// <returns>Hash Code for object</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion Methods
    }
}