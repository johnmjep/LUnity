using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUnityGraph;

namespace LUnity
{
    public class LUnityTurtleStackData
    {
        #region Fields
        public Vector3 Position;
        public Quaternion Heading;
        public Vertex<Vector3> PreviousVertex;
        public Vertex<Vector3> CurrentVertex;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position">Position of turtle</param>
        /// <param name="heading">Heading of turtle</param>
        public LUnityTurtleStackData(Vector3 position, Quaternion heading, Vertex<Vector3> previousVertex,
                                     Vertex<Vector3> currentVertex)
        {
            Position = position;
            Heading = heading;
            PreviousVertex = previousVertex;
            CurrentVertex = currentVertex;
        }
        #endregion Constructors
    }
}
