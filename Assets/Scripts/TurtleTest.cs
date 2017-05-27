using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUnity;
using LUnityGraph;
using Vectrosity;

public class TurtleTest : MonoBehaviour
{
    public LUnityTurtle Turtle;

	// Use this for initialization
	void Start ()
    {
        List<Module> turtleCommands = new List<Module>()
        {
            Module.PenDown,
            Module.Forward.SetParameters(new float[] { 0.0f }),
            Module.YawRight.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
            Module.YawRight.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 1.0f }),
        };

        Turtle.Initialise(0, 0, 0);

        Graph<Vector3> turtleOutput = Turtle.GenerateGraph(turtleCommands);
        DrawGraph(turtleOutput);
	}

    private void DrawGraph(Graph<Vector3> graph)
    {
        foreach (Edge<Vector3> edge in graph.EdgeSet)
        {
            VectorLine vL = VectorLine.SetLine3D(Color.red, edge.VertexA.Data, edge.VertexB.Data);          
        }
    }
}
