using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State startState;
    public List<State> stateTemplates;
    private Dictionary<StateID, State> states;
    [SerializeField] private Brain brain;

    [Tooltip("Debug")]
    [SerializeField] private State currentState;

    private void Awake()
    {
        foreach (State state in stateTemplates)
        {
            State stateCopy = Instantiate(state);
            stateCopy.Initialize(brain);
            states.Add(stateCopy.id, stateCopy);
        }

        currentState = startState;
    }

    private void Update()
    {
        currentState.Update();
    }
}
