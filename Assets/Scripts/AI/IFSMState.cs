
public enum FSMStateType
{
    None,
    Patrol,
    Chase
}

public interface IFSMState
{
    FSMStateType StateName { get; }
    void OnEnter();
    void OnExit();
    void DoAction();
    FSMStateType ShouldTransitionToState();
}
