using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PackageBoxSpawner : MonoBehaviour
{
    public PackageBox PackageBox { get; private set; }

    [SerializeField] private SurfaceManager surfaceManager;
    [SerializeField] private GameObject     packageBoxPrefab;

    private void Update()
    {
        var fixedPlane = surfaceManager.FixedPlane;

        // ���� ����� ������ �Լ� ����
        if (fixedPlane == null)
            return;

        // �̹� ��Ű���ڽ��� �����Ǿ����� �Լ� ����
        if (PackageBox != null)
            return;

        // ��Ű���ڽ� ����
        SpawnPackageBox(fixedPlane);
    }

    private void SpawnPackageBox(ARPlane plane)
    {
        // ������ ������ ��ġ�� �޾ƿ�
        var randomPoint = GetRandomPointOnPlane(plane);

        // ��ġ�� �̿��ؼ� �ش� ��ġ�� ��Ű���ڽ� ����
        PackageBox = Instantiate(packageBoxPrefab, randomPoint, Quaternion.identity)
            .GetComponent<PackageBox>();
    }


    // ���� �ִ� ������ ��ġ�� ��ȯ�ϴ� �Լ�
    private Vector3 GetRandomPointOnPlane(ARPlane plane)
    {
        // 1. ARPlane�� Mesh �����͸� ������
        Mesh mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;
        if (mesh == null) return plane.transform.position; // ����� ������ �⺻ ��ġ�� ��ȯ

        // 2. �ﰢ�� �����͸� ������
        int[] triagles = mesh.triangles;
        Vector3[] verticles = mesh.vertices;

        // 3. ������ �ﰢ�� ������
        int triagleIndex = Random.Range(0, triagles.Length / 3) * 3;

        // 4. ���õ� �ﰢ���� ������ ��������
        Vector3 v0 = verticles[triagles[triagleIndex]];
        Vector3 v1 = verticles[triagles[triagleIndex + 1]];
        Vector3 v2 = verticles[triagles[triagleIndex + 2]];

        // 5. �ﰢ�� ������ ���� ��ġ ã�� (Barycentric ��ǥ ���)
        Vector3 randomPoint = GetRandomPointInTriangle(v0, v1, v2);

        // 6. ���� ��ǥ�� ���� ��ǥ�� ��ȯ
        return plane.transform.TransformPoint(randomPoint);
    }

    private Vector3 GetRandomPointInTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        // 0.0 ~ 1.0 ������ �����߿� ������ �� ��ȯ
        float u = Random.value;
        float v = Random.value;

        if (u + v > 1)
        {
            u = 1 - u;
            v = 1 - v;
        }

        float w = 1 - (u + v);

        return (v0 * w) + (v1 * u) + (v2 * v);
    }
}
