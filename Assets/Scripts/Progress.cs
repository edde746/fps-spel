using UnityEngine;
using UnityEngine.SceneManagement;

public class Progress : MonoBehaviour
{
    public Transform targetContainer;
    public enum GameState
    {
        BEFORE_PLAY = -1,
        PLAYING = 0,
        OVER = 990,
        WON = 991,
        LOST = 992,
    }
    public GameState state = GameState.BEFORE_PLAY;
    public float time = 60f;

    void Update()
    {
        if (state == GameState.BEFORE_PLAY && Input.anyKey)
            state = GameState.PLAYING;

        if (state == GameState.PLAYING)
            time -= Time.deltaTime;

        if (RemainingTargetCount() == 0)
            state = GameState.WON;
        else if (time < 0f)
            state = GameState.LOST;

        if (state > Progress.GameState.OVER && Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int RemainingTargetCount()
    {
        return targetContainer.childCount;
    }
}
