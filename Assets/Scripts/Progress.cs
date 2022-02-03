using UnityEngine;

public class Progress : MonoBehaviour
{
    public Transform targetContainer;

    void Update()
    {
        if (RemainingTargetCount() == 0) Debug.Log("Won");
    }

    public int RemainingTargetCount()
    {
        return targetContainer.childCount;
    }
}
