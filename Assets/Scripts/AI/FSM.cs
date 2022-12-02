using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: Chandler Hummingbird
 * Date Created: Dec 05, 2020
 * Date Modified: Dec 05, 2020
 * Description: The Finite State Machine is the system on which AI is built. States are created as
 * separate classes deriving from IFSMState.
 * For multiple AI types, two options are to created different FSMs or exclusive states for each.
 */

public class FSM : MonoBehaviour
{
    public FSMStateType startState;
    private IFSMState[] statePool;
    private IFSMState currentState;
    private readonly IFSMState EmptyState = new EmptyAction();

    private void Awake()
    {
        statePool = GetComponents<IFSMState>();
        for(int i = 0; i < statePool.Length; i++)
        {
            print(i + " " + statePool[i]);
        }
    }

    private void Start()
    {
        currentState = EmptyState;
        TransitionToState(startState);
    }

    private void TransitionToState(FSMStateType StateName)
    {
        currentState.OnExit();
        currentState = GetState(StateName);
        currentState.OnEnter();
    }

    IFSMState GetState(FSMStateType stateName)
    {
        foreach (var state in statePool)
        {
            print(state.StateName);
            if (state.StateName == stateName)
            {
                return state;
            }
        }
        print("Warning: FSM transitioned in Empty State!");
        return EmptyState;
    }

    private void Update()
    {
        currentState.DoAction();
        FSMStateType transitionState = currentState.ShouldTransitionToState();
        if (transitionState != currentState.StateName)
        {
            TransitionToState(transitionState);
        }
    }
}
