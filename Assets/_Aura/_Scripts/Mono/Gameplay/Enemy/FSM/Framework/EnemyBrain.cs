
using UnityEngine;

/// <summary>
/// This will control our enemy AI
/// </summary>
public class EnemyBrain:MonoBehaviour
{
    [Tooltip("Enemies initial state")]
    [SerializeField] private string initState;

    [SerializeField] private FSMState[] states;
    public FSMState CurrentState { get; set; } = new FSMState();

    //reference to the ninja player once detected
    public Transform Player { get; set; }
    private void Start()
    {
        ChangeState(initState);
    }
    private void Update()
    {
        CurrentState?.UpdateState(this);
    }

    public void ChangeState(string newStateID)
    {
        //does the new state exist
        var state = GetState(newStateID);
        if (state == null) return;
        CurrentState = state;
   
    }

    private FSMState GetState(string newStateID)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].Id == newStateID)
            {
                return states[i];
            }
        }

        return null;
    }
}

