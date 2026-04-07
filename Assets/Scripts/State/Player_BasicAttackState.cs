using JetBrains.Annotations;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Player_BasicAttackState : EntityState {

    private float atkTimer;
    private float lastAtkTimer;

    private const int Combo_First_Atk = 1;
    private int comboAtkIndex = 1;
    private int comboAtkLimit = 3;

    private bool comboAtkQueued;

    private Vector2 comboAtkMove;

    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName) {

        // Safety check
        if (comboAtkIndex != player.atkRepulse.Length) {
            comboAtkIndex = player.atkRepulse.Length;
            Logger.Log($"ComboAtkIndex: {comboAtkIndex}, atkrepulse: {player.atkRepulse.Length}");
        }

    }

    public override void Enter() {
        base.Enter();

        comboAtkQueued = false;

        ComboAtkReset();

        animator.SetInteger("BasicAttackIndex", comboAtkIndex);

        GenerateAttackVelocity();
    }

    public override void Update() {
        base.Update();

        HandleAttackVelocity();

        if (player.playerInputActions.Player.Attack.WasPerformedThisFrame())
            comboAtkQueued = true;

        if (isbasicAttack) {
            if(comboAtkQueued){
                animator.SetBool(animBoolName, false);

                player.EnterAttackStateWithDelay();
            }
            else{
                stateMachine.ChangeState(player.idleState);
            }
        }

    }

    public override void Exit() {
        base.Exit();

        lastAtkTimer = Time.time;
        comboAtkIndex++;
    }

    private void HandleAttackVelocity() {
        atkTimer -= Time.deltaTime;

        if (atkTimer < 0) {
            player.SetVelocity(0, rb.linearVelocity.y);
        }
    }

    private void GenerateAttackVelocity() {
        atkTimer = player.atkTimer;

        comboAtkMove = player.atkRepulse[comboAtkIndex-1];

        //player.SetVelocity(player.attackRepulseMovement * player.facingDir, rb.linearVelocity.y);

        player.SetVelocity(comboAtkMove.x * player.facingDir, comboAtkMove.y);
    }

    private void ComboAtkReset() {
        if(Time.time > lastAtkTimer + player.comboResetTime)
            comboAtkIndex = Combo_First_Atk;

        if (comboAtkIndex > comboAtkLimit)
            comboAtkIndex = Combo_First_Atk;
    }

}
