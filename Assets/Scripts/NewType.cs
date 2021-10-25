using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewType : MobileUnit
{
    private bool _firstTarget = true;

    public override void HasReachedTarget()
    {
        //test if agent has reached target
        if (!Agent.pathPending) //if agent is looking for target it hasn't reached target
        {
            if (Agent.remainingDistance <= Agent.stoppingDistance) //agent is as close as it can get
            {
                if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f) //if agent isn't moving
                {
                    Debug.Log("Target Reached!!!");

                    //make it go to a new target
                    if (_firstTarget)
                    {
                        GameObject target = Manager.Instance.GetTarget();
                        UpdateTarget(target);
                        return;
                    }

                    //if a factory has already been created, we destroy
                    if (Manager.Instance.IsTargetOccupied(Target)) { Destroy(gameObject); }

                    _reachedTarget = true;
                    Agent.enabled = false;
                }
            }
        }
    }
}