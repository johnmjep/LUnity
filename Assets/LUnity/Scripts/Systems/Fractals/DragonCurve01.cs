using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUnity;
using LUnityGraph;

public class DragonCurve01 : MonoBehaviour
{
    public LUnityGraphSystem lSystem;

    private List<Module> _axiom;
    private Production _production1;
    private Production _production2;
    private Graph<Vector3> _dragonGraph;
    private List<Module> _outputGrammar;

    // Use this for initialization
    void Start ()
    {
        Module forwardLeft = new Module("Fl", new float[] { 0.5f });
        Module forwardRight = new Module("Fr", new float[] { 0.5f });
        Module turnRight = new Module(Module.YawRight).SetParameters(new float[] { 90.0f });
        Module turnLeft = new Module(Module.YawLeft).SetParameters(new float[] { 90.0f });

        _axiom = new List<Module>()
        {
            Module.PenDown,
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            forwardLeft
        };

        List<Module> p1Successor = new List<Module>()
        {
            forwardLeft,
            turnRight,
            forwardRight,
            turnRight
        };

        List<Module> p2Successor = new List<Module>()
        {
            turnLeft,
            forwardLeft,
            turnLeft,
            forwardRight
        };

        _production1 = Production.Build(forwardLeft, p1Successor);
        _production2 = Production.Build(forwardRight, p2Successor);

        lSystem.SetAxiom(_axiom);
        lSystem.AddProduction(_production1);
        lSystem.AddProduction(_production2);

        lSystem.Turtle.Initialise(0, 0, 0);

        List<Module> turtleCommands = new List<Module>();
        List<Module> grammar = lSystem.GenerateGrammar(12);
        foreach (Module m in grammar)
        {
            if (m.Name == forwardLeft.Name || m.Name == forwardRight.Name)
            {
                turtleCommands.Add(Module.Forward.SetParameters(new float[] { m[0] }));
            }
            else
            {
                turtleCommands.Add(m);
            }
        }

        _dragonGraph = lSystem.ProduceGraphFromGrammar(turtleCommands);
    }
	
}
