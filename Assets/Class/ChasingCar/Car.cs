using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float moveSpeed     = 0.3f;

    private Reticle _reticle;

    // �ڵ��� �ʱ�ȭ
    public void Initalize(Reticle reticle)
    {
        _reticle = reticle;
    }

    private void Update()
    {
        // �̵��� ���� (��ƼŬ�� ��ġ)
        var destination = _reticle.transform.position;

        // �̵��� �������� �Ÿ��� 0.1�̸��̸� �Լ� ����
        if (Vector3.Distance(destination, transform.position) < 0.1f)
            return;

        // �̵��� ����
        Vector3 direction = (destination - transform.position).normalized;

        // �̵��� ������ ������ ȸ���� ����
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // lookRotation������ ������ ������ ȸ��
        transform.rotation = Quaternion.Lerp(
            transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // destination�� ���� moveSpeed��ŭ �̵�
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
