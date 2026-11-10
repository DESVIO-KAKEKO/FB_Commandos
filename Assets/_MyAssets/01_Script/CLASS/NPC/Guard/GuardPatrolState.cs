using System.Collections;
using UnityEngine;

public class GuardPatrolState : GuardBaseState
{
    float timer = .0f;
    public GuardPatrolState(GuardController guard) : base(guard) { }
    
    public override void Enter()
    {
        Guard.MoveTo(Guard.GetCurrentWaypoint().position);
    }

    public override void Tick()
    {
        if (Guard.CanSeeTarget())
        {
            Guard.SwitchToAlert();
        }
        if (Guard.HasReachedCurrentDestination())
        {
            if (timer >= Guard.WaypointWaitTime)
            {
                timer = .0f;
                Guard.AdvanceToNextWaypoint();
                Guard.MoveTo(Guard.GetCurrentWaypoint().position); 
            }
            else timer += Time.deltaTime;
        }
    }

    public override void Exit()
    {
        Guard.StopMoving();
        timer = .0f;
    }

}
