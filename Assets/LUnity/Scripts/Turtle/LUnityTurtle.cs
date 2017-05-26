using System;
using System.Collections.Generic;
using UnityEngine;
using LUnityGraph;

namespace LUnity
{
    /// <summary>
    /// LSystem Modules for controlling the turtle
    /// </summary>
    public partial class Module
    {        
        public static Module Forward { get { return new Module("F", new float[] { float.NaN }); } }
        public static Module ForwardBlank { get { return new Module("f", new float[] { float.NaN }); } }
        public static Module Backward { get { return new Module("B", new float[] { float.NaN }); } }
        public static Module BackwardBlank { get { return new Module("b", new float[] { float.NaN }); } }
        public static Module YawRight { get { return new Module("+", new float[] { float.NaN }); } }
        public static Module YawLeft { get { return new Module("-", new float[] { float.NaN }); } }
        public static Module PitchUp { get { return new Module("^", new float[] { float.NaN }); } }
        public static Module PitchDown { get { return new Module("&", new float[] { float.NaN }); } }
        public static Module RollLeft { get { return new Module("\\", new float[] { float.NaN }); } }
        public static Module RollRight { get { return new Module("/", new float[] { float.NaN }); } }
        public static Module SetPosition { get { return new Module("S", new float[] { float.NaN, float.NaN, float.NaN }); } }
        public static Module SetHeading { get { return new Module("H", new float[] { float.NaN, float.NaN, float.NaN }); } }
        public static Module StampPoint { get { return new Module("@"); } }
        public static Module PushStack { get { return new Module("["); } }
        public static Module PopStack { get { return new Module("]"); } }
        public static Module PenUp { get { return new Module("U"); } }
        public static Module PenDown { get { return new Module("D"); } }
    }

    public class LUnityTurtle : MonoBehaviour
    {
        #region Fields
        private Graph<Vector3> _outputGraph;
           
        public bool IsPenDown { get; private set; }

        private Dictionary<string, Action<float[]>> _cmdInterpreter;

        private Vertex<Vector3> _previousVertex = null;
        private Vertex<Vector3> _currentVertex = null;

        private Stack<LUnityTurtleStackData> _turtleDataStack = new Stack<LUnityTurtleStackData>();

        private bool _generateGraph = true;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public LUnityTurtle()
        {
            InitialiseCmdInterpreter();
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Sets up command inter
        /// </summary>
        private void InitialiseCmdInterpreter()
        {
            _cmdInterpreter = new Dictionary<string, Action<float[]>>()
            {
                { Module.Forward.Name, Forward },
                { Module.ForwardBlank.Name, ForwardBlank },
                { Module.Backward.Name, Backward },
                { Module.BackwardBlank.Name, BackwardBlank },
                { Module.YawLeft.Name, YawLeft },
                { Module.YawRight.Name, YawRight },
                { Module.PitchUp.Name, PitchUp },
                { Module.PitchDown.Name, PitchDown },
                { Module.RollLeft.Name, RollLeft },
                { Module.RollRight.Name, RollRight },
                { Module.SetPosition.Name, SetPosition },
                { Module.SetHeading.Name, SetHeading },
                { Module.PenUp.Name, PenUp },
                { Module.PenDown.Name, PenDown },
                { Module.StampPoint.Name, StampPoint },
                { Module.PushStack.Name, PushStack },
                { Module.PopStack.Name, PopStack },
            };
        }

        /// <summary>
        /// Generates a Graph from a string of turtle commands
        /// </summary>
        /// <param name="turtleCommands">String containing valid turtle commands</param>
        /// <returns>Graph representing turtle movement</returns>
        public Graph<Vector3> GenerateGraph(List<Module> turtleCommands)
        {
            _outputGraph = new Graph<Vector3>();
            _generateGraph = true;
            ExecuteCommands(turtleCommands);
            return _outputGraph;
        }

        /// <summary>
        /// Determines where the turtle is after a string of commands
        /// </summary>
        /// <param name="turtleCommands"></param>
        /// <returns></returns>
        public Vector3 FindFinalPositionFrom(List<Module> turtleCommands)
        {
            _generateGraph = false;
            ExecuteCommands(turtleCommands);
            return transform.position;
        }

        /// <summary>
        /// Executes the turtle commands
        /// </summary>
        /// <param name="turtleCommands">Input command string</param>
        private void ExecuteCommands(List<Module> turtleCommands)
        {
            foreach (Module command in turtleCommands)
            {
                if (_cmdInterpreter.ContainsKey(command.Name))
                {
                    _cmdInterpreter[command.Name](command.Parameters);
                }
            }
        }

        /// <summary>
        /// Initialise Turtle
        /// </summary>
        /// <remarks>Must be called before any commands are issued</remarks>
        /// <param name="x">Starting X position</param>
        /// <param name="y">Starting Y position</param>
        /// <param name="z">Starting Z position</param>
        public void Initialise(float x, float y, float z)
        {
            _outputGraph = new Graph<Vector3>();
            transform.position = new Vector3(x, y, z);
        }

        /// <summary>
        /// Move turtle forward by specified amount
        /// </summary>
        /// <param name="parameters">First index specifies distance</param>
        public void Forward(float[] parameters)
        {
            transform.Translate(Vector3.forward * parameters[0]);

            if (IsPenDown)
            {
                StampPoint(null);
                AddEdge();
            }
        }

        /// <summary>
        /// Move turtle forward by specified amount and don't draw
        /// </summary>
        /// <param name="parameters">First index specifies distance</param>
        public void ForwardBlank(float[] parameters)
        {
            transform.Translate(Vector3.forward * parameters[0]);
            _previousVertex = null;
            _currentVertex = null;
        }

        /// <summary>
        /// Move turtle backward by specified amount
        /// </summary>
        /// <param name="parameters">First index specifies distance</param>
        public void Backward(float[] parameters)
        {
            transform.Translate(Vector3.back * parameters[0]);

            if (IsPenDown)
            {
                StampPoint(null);
                AddEdge();
            }
        }

        /// <summary>
        /// Move turtle backward by specified amount and don't draw
        /// </summary>
        /// <param name="parameters">First index specifies distance</param>
        public void BackwardBlank(float[] parameters)
        {
            transform.Translate(Vector3.back * parameters[0]);
            _previousVertex = null;
            _currentVertex = null;
        }

        /// <summary>
        /// Turn turtle left by specified angle
        /// </summary>
        /// <param name="parameters">First index specifies angle in degrees</param>
        public void YawLeft(float[] parameters)
        {
            transform.Rotate(Vector3.up, parameters[0]);
        }

        /// <summary>
        /// Turn turtle right by specified angle
        /// </summary>
        /// <param name="parameters">First index specifies angle in degrees</param>
        public void YawRight(float[] parameters)
        {
            transform.Rotate(Vector3.up, -parameters[0]);
        }

        public void RollLeft(float[] parameters)
        {
            transform.Rotate(Vector3.forward, parameters[0]);
        }

        public void RollRight(float[] parameters)
        {
            transform.Rotate(Vector3.forward, -parameters[0]);
        }

        public void PitchUp(float[] parameters)
        {
            transform.Rotate(Vector3.left, parameters[0]);
        }

        public void PitchDown(float[] parameters)
        {
            transform.Rotate(Vector3.left, -parameters[0]);
        }

        /// <summary>
        /// Explicitly sets the turtle position
        /// </summary>
        /// <param name="parameters">index 0 species X, index 1 specifies Y</param>
        public void SetPosition(float[] parameters)
        {
            transform.position = new Vector3(parameters[0], parameters[1], parameters[2]);

            if (IsPenDown)
            {
                StampPoint(null);
                AddEdge();
            }
        }

        /// <summary>
        /// Explicitly sets the turtle heading in degrees
        /// </summary>
        /// <param name="parameters">First index specifies heading in degrees</param>
        public void SetHeading(float[] parameters)
        {
            transform.rotation = Quaternion.Euler(parameters[0], parameters[1], parameters[2]);
        }

        /// <summary>
        /// Lifts the pen (disables drawing)
        /// </summary>
        /// <param name="parameters">Not used</param>
        public void PenUp(float[] parameters)
        {
            IsPenDown = false;
        }

        /// <summary>
        /// Lowers the pen (enables drawing)
        /// </summary>
        /// <param name="parameters">Not used</param>
        public void PenDown(float[] parameters)
        {
            IsPenDown = true;
        }

        /// <summary>
        /// Adds a vertex to the graph at the current position
        /// </summary>
        /// <param name="parameters">Not used</param>
        public void StampPoint(float[] parameters)
        {
            if (_generateGraph)
            {
                _previousVertex = _currentVertex;
                _currentVertex = new Vertex<Vector3>(transform.position);
                _outputGraph.AddVertex(_currentVertex);
            }
        }

        /// <summary>
        /// Pushes current turtle state onto stack
        /// </summary>
        /// <param name="parameters">Not used</param>
        public void PushStack(float[] parameters)
        {
            _turtleDataStack.Push(new LUnityTurtleStackData(transform.position, transform.rotation, _previousVertex, _currentVertex));
        }

        /// <summary>
        /// Pops turtle state off of stack
        /// </summary>
        /// <param name="parameters">Not used</param>
        public void PopStack(float[] parameters)
        {
            LUnityTurtleStackData tSD = _turtleDataStack.Pop();
            transform.position = tSD.Position;
            transform.rotation = tSD.Heading;
            _previousVertex = tSD.PreviousVertex;
            _currentVertex = tSD.CurrentVertex;
        }

        /// <summary>
        /// Adds a directed adge from the previous vertex to the current vertex
        /// </summary>
        private void AddEdge()
        {
            if (IsPenDown && _previousVertex != null && _currentVertex != null)
            {
                _outputGraph.AddDirectedEdge(_previousVertex, _currentVertex);
            }
        }
        #endregion Methods
    }
}
