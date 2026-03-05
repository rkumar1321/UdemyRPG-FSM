using UnityEngine;

public class TimerTest : MonoBehaviour
{
    public float time;
    public float timeScale;

    private void Update() {
        time = Time.time;
        timeScale += Time.deltaTime;
    }
}
