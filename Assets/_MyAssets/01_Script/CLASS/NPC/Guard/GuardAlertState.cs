using UnityEngine;

public class GuardAlertState : GuardBaseState
{
    private float timer = .0f;
    private float endTimer = .0f;
    public GuardAlertState(GuardController guard) : base(guard)
    {
    }

    public override void Enter()
    {
        Guard.FaceTarget();
    }

    public override void Tick()
    {
        if (timer > Guard.WaypointWaitTime)
        {
            if (Guard.CanSeeTarget())
            {
                Guard.SwitchToShooting();
            }
            else
            {
                Guard.MoveTo(Guard.LastKnownPosition);
            }

            if (Guard.HasReachedCurrentDestination())
            {
                if (endTimer > Guard.WaypointWaitTime)
                {
                    if (Guard.CanSeeTarget())
                    {
                        timer = .0f;
                        endTimer = .0f;
                    }
                    else
                    {
                        Guard.SwitchToPatrol();
                    }
                }
                else endTimer += Time.deltaTime;
            }
        }
        else timer += Time.deltaTime;
    }

    public override void Exit()
    {
        Guard.StopMoving();
        timer = .0f;
        endTimer = .0f;
    }
}
