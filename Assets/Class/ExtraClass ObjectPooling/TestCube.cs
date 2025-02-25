using System;
using UnityEngine;
using UnityEngine.Pool;

public class TestCube : MonoBehaviour
{
    // 오브젝트 풀 변수
    private static IObjectPool<TestCube> _pool;

    // 오브젝트 풀 프로퍼티
    private static IObjectPool<TestCube> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<TestCube>(
                    createFunc: () =>
                    {
                        var prefab = Resources.Load<GameObject>("Cube");
                        var cube = Instantiate(prefab).GetComponent<TestCube>();
                        return cube;
                    },
                    actionOnGet: cube =>
                    {
                        cube.gameObject.SetActive(true);
                    },
                    actionOnRelease: cube =>
                    {
                        cube.gameObject.SetActive(false);
                    }
                );
            }

            return _pool;
        }
    }

    public static TestCube Create(Vector3 position,  Quaternion rotation)
    {
        var cube = Pool.Get();

        cube.transform.position = position;
        cube.transform.rotation = rotation;

        return cube;
    }

    /*****************************************************************************************/
    /*****************************************************************************************/

    public void Release()
    {
        Pool?.Release(this);
    }
}
