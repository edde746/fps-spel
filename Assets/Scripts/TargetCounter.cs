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
        mesh.text = $"Targets remaining: {game.RemainingTargetCount()}";
    }
}
