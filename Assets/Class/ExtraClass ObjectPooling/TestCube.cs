using System;
using UnityEngine;
using UnityEngine.Pool;

public class TestCube : MonoBehaviour
{
    // ������Ʈ Ǯ ����
    private static IObjectPool<TestCube> _pool;

    // ������Ʈ Ǯ ������Ƽ
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
