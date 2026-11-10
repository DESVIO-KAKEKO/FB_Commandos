using UnityEngine;

public class GuardShootingState : GuardBaseState
{
    public GuardShootingState(GuardController guard) : base(guard)
    {
    }

    public override void Enter()
    {
        Guard.MoveTo(Guard.LastKnownPosition);
    }

    public override void Tick()
    {
        if(Guard.HasReachedCurrentDestination())
        {
            if (Guard.CanSeeTarget())
            {
                Guard.FaceTarget();
            }
            else Guard.SwitchToPatrol();
        }
    }

    public override void Exit()
    {
        
    }
}
