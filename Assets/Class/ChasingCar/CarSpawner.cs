using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject     carPrefab;
    [SerializeField] SurfaceManager surfaceManager;
    [SerializeField] Reticle        reticle;

    private void Start()
    {
        // surfaceManager의 터치이벤트가 발생하면
        // SpawnCar() 함수가 실행되도록 델리게이트 체이닝 (구독)
        surfaceManager.TouchEvent += SpawnCar;
    }

    private void SpawnCar()
    {
        // 고정된 평면이 없을경우 함수 종료
        if (surfaceManager.FixedPlane == null)
            return;

        var carObj = Instantiate(
            carPrefab, reticle.transform.position, reticle.transform.rotation);

        var car = carObj.AddComponent<Car>();
        car.Initalize(reticle);

        surfaceManager.TouchEvent -= SpawnCar;
    }
}
