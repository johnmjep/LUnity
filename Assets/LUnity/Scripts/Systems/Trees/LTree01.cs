using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LUnity;
using LUnityGraph;

public class LTree01 : MonoBehaviour
{
    public LUnityGraphSystem lSystem;

    private List<Module> _axiom;
    private Production _production1a;
    private Production _production1b;
    private Production _production1c;
    private Graph<Vector3> _treeGraph;
    private List<Module> _outputGrammar;

    // Use this for initialization
    void Start ()
    {
        float angle = 25.7f;
        float stepDistance = 0.1f;

        Module forwardStep = Module.Forward.SetParameters(new float[] { stepDistance });
        Module turnRight = Module.YawRight.SetParameters(new float[] { angle });
        Module turnLeft = Module.YawLeft.SetParameters(new float[] { angle });

        _axiom = new List<Module>()
        {
            Module.PenDown,
            Module.PitchUp.SetParameters(new float[] { 90.0f }),
            forwardStep,
        };

        List<Module> _p1aSuccessor = new List<Module>()
        {
            forwardStep,
            Module.PushStack,
            turnRight,
            forwardStep,
            Module.PopStack,
            forwardStep,
            Module.PushStack,
            turnLeft,
            forwardStep,
            Module.PopStack,
            forwardStep
        };

        _production1a = Production.Build(forwardStep, _p1aSuccessor)
                                 .SetProbability(0.33f);

        List<Module> _p1bSuccessor = new List<Module>()
        {
            forwardStep,
            Module.PushStack,
            turnRight,
            forwardStep,
            Module.PopStack,
            forwardStep
        };

        _production1b = Production.Build(forwardStep, _p1bSuccessor)
                                  .SetProbability(0.33f);

        List<Module> _p1cSuccessor = new List<Module>()
        {
            forwardStep,
            Module.PushStack,
            turnLeft,
            forwardStep,
            Module.PopStack,
            forwardStep
        };

        _production1c = Production.Build(forwardStep, _p1cSuccessor)
                                  .SetProbability(0.34f);

        lSystem.SetAxiom(_axiom);
        lSystem.AddStochasticProductionSet(new List<Production>()
        {
            _production1a,
            _production1b,
            _production1c
        });
        lSystem.Turtle.DrawLines = true;

        _treeGraph = lSystem.GenerateOutput(5, out _outputGrammar);
    }
}
