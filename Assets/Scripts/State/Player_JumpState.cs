using UnityEngine;

public class Player_JumpState : Player_AirState {
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {
    }


    public override void Enter() {
        base.Enter();

        // Jump
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, player.jumpSpeed);
    }

    public override void Update() {
        base.Update();


        //Enter fallState if player falling
        if(rb.linearVelocityY < 0) {
            stateMachine.ChangeState(player.fallState);
        }
    }
}