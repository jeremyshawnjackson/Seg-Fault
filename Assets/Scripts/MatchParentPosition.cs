using UnityEngine;

// I feel like this shouldn't need to be scripted, yet here we are
public class MatchParentPosition : MonoBehaviour
{
        void Update()
    {
        this.transform.localPosition = new Vector3(0, 0, 0);
    }
}