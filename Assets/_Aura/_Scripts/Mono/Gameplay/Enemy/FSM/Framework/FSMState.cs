
using System;

[Serializable]
public class FSMState
{
    public string Id;
    public FSMAction[] Actions;//e.g MoveAction  | AttackAction etc
    public FSMTransition[] Transitions;//e.g can transition to chase or to attack etc

    public void UpdateState(EnemyBrain enemyBrain)
    {
        ExecuteActions();
        ExecuteTransitions(enemyBrain);
    }

    private void ExecuteActions()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }
    private void ExecuteTransitions(EnemyBrain enemyBrain)
    {
        if(Transitions.Length > 0)
        {
            for(int i = 0;i < Transitions.Length;i++)
            {
                bool value = Transitions[i].Decision.Decide();
                if(value)
                {
                    enemyBrain.ChangeState(Transitions[i].TrueState);
                }
                else
                {
                    enemyBrain.ChangeState(Transitions[i].FalseState);

                }
            }

      
        }
    }

}

