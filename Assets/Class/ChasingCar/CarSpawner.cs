using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject     carPrefab;
    [SerializeField] SurfaceManager surfaceManager;
    [SerializeField] Reticle        reticle;

    private void Start()
    {
        // surfaceManager�� ��ġ�̺�Ʈ�� �߻��ϸ�
        // SpawnCar() �Լ��� ����ǵ��� ��������Ʈ ü�̴� (����)
        surfaceManager.TouchEvent += SpawnCar;
    }

    private void SpawnCar()
    {
        // ������ ����� ������� �Լ� ����
        if (surfaceManager.FixedPlane == null)
            return;

        var carObj = Instantiate(
            carPrefab, reticle.transform.position, reticle.transform.rotation);

        var car = carObj.AddComponent<Car>();
        car.Initalize(reticle);

        surfaceManager.TouchEvent -= SpawnCar;
    }
}
