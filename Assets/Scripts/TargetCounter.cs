using TMPro;
using UnityEngine;

public class TargetCounter : MonoBehaviour
{
    TextMeshProUGUI mesh;
    Progress game;
    void Start()
    {
        mesh = GetComponent<TextMeshProUGUI>();
        game = GameObject.FindWithTag("GameController").GetComponent<Progress>();
    }

    void Update()
    {
        if (game.state == Progress.GameState.PLAYING)
            mesh.text = $"Targets remaining: {game.RemainingTargetCount()}\nTime remaining: {Mathf.Round(game.time)}s";
        else if (game.state == Progress.GameState.LOST)
            mesh.text = "You Lost\nPress R to restart"; // Inefficient to update every frame
        else if (game.state == Progress.GameState.WON)
            mesh.text = "You Won\nPress R to restart";
        else
            mesh.text = "Move to start";
    }
}
