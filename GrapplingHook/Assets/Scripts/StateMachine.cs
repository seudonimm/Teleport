using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{

    private IState currentState, previousState;
    public IState state;

    public void ChangeState(IState newState)
    {
        if(this.currentState != null)
        {
            this.currentState.Exit();

        }
        this.previousState = this.currentState;

        this.currentState = newState;

        currentState = newState;
        currentState.Enter();
    }

    // Update is called once per frame
    public void ExecuteStateUpdate()
    {
        if(this.currentState != null)
        {
            currentState.Execute();
        }
    }

    public void SwitchToPreviousState()
    {
        this.currentState.Exit();
        this.currentState = this.previousState;
        this.currentState.Enter();
    }

    public IState CurrentState()
    {

        return this.currentState;
    }
}
