using UnityEngine;

public abstract class EntityState {

    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }


    public virtual void Enter() {
        Logger.Log($"Enter: {stateName}");
    }

    public virtual void Update() {
        Logger.Log($"Update: {stateName}");
    }

    public virtual void Exit() {
        Logger.Log($"Exit: {stateName}");
    }
}