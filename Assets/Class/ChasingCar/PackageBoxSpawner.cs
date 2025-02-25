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

        // 고정 평면이 없으면 함수 종료
        if (fixedPlane == null)
            return;

        // 이미 패키지박스가 생성되었으면 함수 종료
        if (PackageBox != null)
            return;

        // 패키지박스 생성
        SpawnPackageBox(fixedPlane);
    }

    private void SpawnPackageBox(ARPlane plane)
    {
        // 평면상의 랜덤한 위치를 받아옴
        var randomPoint = GetRandomPointOnPlane(plane);

        // 위치를 이용해서 해당 위치에 패키지박스 생성
        PackageBox = Instantiate(packageBoxPrefab, randomPoint, Quaternion.identity)
            .GetComponent<PackageBox>();
    }


    // 평면상에 있는 랜덤한 위치를 반환하는 함수
    private Vector3 GetRandomPointOnPlane(ARPlane plane)
    {
        // 1. ARPlane의 Mesh 데이터를 가져옴
        Mesh mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;
        if (mesh == null) return plane.transform.position; // 평면이 없으면 기본 위치를 반환

        // 2. 삼각형 데이터를 가져옴
        int[] triagles = mesh.triangles;
        Vector3[] verticles = mesh.vertices;

        // 3. 무작위 삼각형 선택함
        int triagleIndex = Random.Range(0, triagles.Length / 3) * 3;

        // 4. 선택된 삼각형의 꼭짓점 가져오기
        Vector3 v0 = verticles[triagles[triagleIndex]];
        Vector3 v1 = verticles[triagles[triagleIndex + 1]];
        Vector3 v2 = verticles[triagles[triagleIndex + 2]];

        // 5. 삼각형 내부의 랜덤 위치 찾기 (Barycentric 좌표 사용)
        Vector3 randomPoint = GetRandomPointInTriangle(v0, v1, v2);

        // 6. 로컬 좌표를 월드 좌표로 변환
        return plane.transform.TransformPoint(randomPoint);
    }

    private Vector3 GetRandomPointInTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        // 0.0 ~ 1.0 까지의 숫자중에 랜덤한 값 반환
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
