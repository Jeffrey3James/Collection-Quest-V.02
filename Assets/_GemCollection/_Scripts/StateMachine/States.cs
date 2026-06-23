using UnityEngine;

public abstract class States: IState
{
    protected readonly PlayerController playerController;
    protected readonly Animator animator;

    #region States
    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected static readonly int JumpHash = Animator.StringToHash("Jump");
    #endregion

    protected const float CROSS_FADE_DURATION = .1f;

    virtual public string StateName { get => throw new System.NotImplementedException(); }

    protected States(PlayerController playerController, Animator animator)
    {
        this.playerController = playerController;
        this.animator = animator;
    }   

    public virtual void FixedUpdate()
    {
        //NOOP
    }

    public virtual void OnEnter()
    {
        //NOOP
    }

    public virtual void OnExit()
    {
        //NOOP
    }

    public virtual void Update()
    {
        //NOOP
    }
}

public class LocomotionState : States
{
    public LocomotionState(PlayerController playerController, Animator animator) : base(playerController, animator)
    { }

    public override void OnEnter()
    {
        animator.CrossFade(LocomotionHash, CROSS_FADE_DURATION);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        playerController.MovePlayer();
    }

    public override string StateName => "locomotionState";
}
public class JumpState : States
{
    public JumpState(PlayerController playerController, Animator animator) : base(playerController, animator)
    { }

    public override void OnEnter()
    {
        animator.CrossFade(JumpHash, CROSS_FADE_DURATION);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        playerController.MovePlayer();
    }

    public override string StateName => "jumpState";
}


