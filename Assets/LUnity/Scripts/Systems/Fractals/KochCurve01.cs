using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUnity;
using LUnityGraph;
using Vectrosity;

public class KochCurve01 : MonoBehaviour
{
    public LUnityGraphSystem lSystem;

    private List<Module> _axiom;
    private Production _production1;
    private Graph<Vector3> _kochGraph;
    private List<Module> _outputGrammar;

	// Use this for initialization
	void Start ()
    {
        _axiom = new List<Module>()
        {
            Module.PenDown,
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
        };

        List<Module> p1Successor = new List<Module>()
        {
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawLeft.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
            Module.YawRight.SetParameters(new float[] { 90.0f }),
            Module.Forward.SetParameters(new float[] { 0.5f }),
        };

        _production1 = Production.Build(Module.Forward, p1Successor);

        lSystem.SetAxiom(_axiom);
        lSystem.AddProduction(_production1);
        lSystem.Turtle.DrawLines = true;

        _kochGraph = lSystem.GenerateOutput(4, out _outputGrammar);
        //DrawGraph(_kochGraph);
	}

    private void DrawGraph(Graph<Vector3> graph)
    {
        foreach (Edge<Vector3> edge in graph.EdgeSet)
        {
            VectorLine vL = VectorLine.SetLine3D(Color.red, edge.VertexA.Data, edge.VertexB.Data);
            vL.rectTransform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
