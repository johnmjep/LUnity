using System.Collections.Generic;

namespace LUnityGraph
{
    /// <summary>
    /// Graph Vertex Class, holds a data item and a list of connected Vertices
    /// </summary>
    public class Vertex<T>
    {
        #region Fields
        private T _data;
        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private List<Vertex<T>> _adjacentVertices = new List<Vertex<T>>();
        public List<Vertex<T>> AdjacentVertices
        {
            get { return _adjacentVertices; }
        }
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Vertex() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Vertex data of type T</param>
        public Vertex(T data)
            : this(data, null)
        { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">Vertex data of type T</param>
        /// <param name="adjacentVertices">List of adj</param>
        public Vertex(T data, List<Vertex<T>> adjacentVertices)
        {
            _data = data;
            if (adjacentVertices != null)
            {
                _adjacentVertices = adjacentVertices;
            }
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Overrides base Equals method
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if objects match</returns>
        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is Vertex<T>)
            {
                if ((obj as Vertex<T>).Data.Equals(this.Data))
                {
                    isEqual = true;
                }
            }
            return isEqual;
        }

        /// <summary>
        /// Overrides base ToString method
        /// </summary>
        /// <returns>Returns a string represenation of this object</returns>
        public override string ToString()
        {
            return string.Format("$$ VERTEX | Data: {1}, AdjacentVertices: {2}", _data.ToString(), _adjacentVertices.ToString());
        }

        /// <summary>
        /// Overrides base GetHashCode method
        /// </summary>
        /// <returns>Hash code for this object</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        #endregion Methods
    }
}