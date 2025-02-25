using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float moveSpeed     = 0.3f;

    private Reticle _reticle;

    // 자동차 초기화
    public void Initalize(Reticle reticle)
    {
        _reticle = reticle;
    }

    private void Update()
    {
        // 이동할 지점 (레티클의 위치)
        var destination = _reticle.transform.position;

        // 이동할 지점과의 거리가 0.1미만이면 함수 종료
        if (Vector3.Distance(destination, transform.position) < 0.1f)
            return;

        // 이동할 방향
        Vector3 direction = (destination - transform.position).normalized;

        // 이동할 방향을 보도록 회전값 설정
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // lookRotation쪽으로 서서히 방향을 회전
        transform.rotation = Quaternion.Lerp(
            transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // destination을 향해 moveSpeed만큼 이동
        transform.position = Vector3.MoveTowards(
            transform.position, destination, Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        PackageBox packageBox = other.GetComponent<PackageBox>();

        if (packageBox != null)
        {
            Destroy(packageBox.gameObject);
        }
    }
}
