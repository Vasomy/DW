using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{

    public GameMode(string state_name)
    {
        Name = state_name;

        GameStateMachine.instance.mModes.Add(state_name, this);
    }

    public virtual void Update()
    {
        
    }

    public virtual void BeginMode()
    {

    }

    public virtual void EndMode()
    {

    }
    public string Name;
}
