using System.Collections.Generic;
using LUnityGraph;
using LUnity;
using UnityEngine;

namespace LUnity
{
    /// <summary>
    /// Implementation of LSystem to create a Graph output
    /// </summary>
    public class LUnityGraphSystem : LSystem<Graph<Vector3>>
    {
        #region Fields
        public LUnityTurtle Turtle { get { return _turtle; } }
        [SerializeField]
        private LUnityTurtle _turtle;
        #endregion Fields
        
        #region Methods
        /// <summary>
        /// Runs the L-System and produces a Graph output
        /// </summary>
        /// <param name="generations"></param>
        /// <param name="grammarOutput"></param>
        /// <param name="startPoint"></param>
        /// <returns></returns>
        public override Graph<Vector3> GenerateOutput(int generations, out List<Module> grammarOutput, List<Module> startPoint = null)
        {
            _turtle.Initialise(transform.position.x, transform.position.y, transform.position.z);
            grammarOutput = GenerateGrammar(generations, startPoint);
            return ProduceGraphFromGrammar(grammarOutput);
        }

        /// <summary>
        /// Generates the L-System Grammar output
        /// </summary>
        /// <param name="generations">Number of generations to perform</param>
        /// <param name="startPoint">Starting point of the L-System (could be the axiom or previous output)</param>
        /// <returns>Grammar as List of Modules</returns>
        public List<Module> GenerateGrammar(int generations, List<Module> startPoint = null)
        {
            List<Module> output = (startPoint == null) ? new List<Module>(_axiom) : startPoint;
            Module leftContext;
            Module rightContext;
            for (int gen = 0; gen < generations; gen++)
            {
                List<Module> working = new List<Module>();

                for (int x = 0; x < output.Count; x++)
                {
                    leftContext = x > 0 ? output[x - 1] : Module.Empty;
                    rightContext = x < (output.Count - 1) ? output[x + 1] : Module.Empty;
                    working.AddRange(_pSet.ApplyTo(leftContext, output[x], rightContext, gen));
                }
                output = working;
            }
            return output;
        }

        /// <summary>
        /// Produces a Graph from the L-System Grammar
        /// </summary>
        /// <param name="grammar">L-System Grammar as a List of Modules</param>
        /// <returns>Graph of Vector2</returns>
        public Graph<Vector3> ProduceGraphFromGrammar(List<Module> grammar)
        {            
            return _turtle.GenerateGraph(grammar);
        }
        #endregion Methods
    }
}
