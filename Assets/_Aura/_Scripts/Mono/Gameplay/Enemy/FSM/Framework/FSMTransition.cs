
using System;

[Serializable]
public class FSMTransition
{
    public FSMDecision Decision;//think of this as a check e.g isPlayerInRangeOfAttack
    /*
     * if Decision returns true, the TrueState member variable below represents the 
     * state we shall transition to. If on the other hand, false is returned..then the 
     * FalseState member variable represents the state we shall transition to
     */
    public string TrueState;
    public string FalseState;
}

