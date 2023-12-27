using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public bool moveToSleepState;
    public bool moveToChaseTargetState;

    public virtual State Tick(ZombieManager zombieManager)
    {
        if (moveToChaseTargetState)
        {

        }
        else if(moveToSleepState)
        {

        }
        return this;

    }
}
