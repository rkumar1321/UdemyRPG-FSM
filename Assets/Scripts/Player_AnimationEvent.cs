using UnityEngine;

public class Player_AnimationEvent : MonoBehaviour
{

    private Player player;

    private void Awake() {
        player = GetComponentInParent<Player>();
    }

    private void AttackOver() {
        //Logger.Log("Attack Oer");
        player.CallAnimationTriggerEvents();
        //Logger.Log("AnimatorEventss");
    }
}
