using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private void OnEnable()
    {
        TestCube.Create(transform.position, transform.rotation);
    }
}
