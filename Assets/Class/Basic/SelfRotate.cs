using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 10f);
    }
}
