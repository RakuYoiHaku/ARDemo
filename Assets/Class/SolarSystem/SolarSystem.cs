using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    [Header("Celestial Bodies")]
    public GameObject sun;
    public GameObject mercury;
    public GameObject venus;
    public GameObject earth;
    public GameObject mars;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject uranus;
    public GameObject neptune;

    [Header("Orbit Speeds (Relative to Earth = 1)")]
    public float mercuryOrbitSpeed = 4.15f;  // ����
    public float venusOrbitSpeed = 1.63f;    // �ݼ�
    public float earthOrbitSpeed = 1.00f;    // ����
    public float marsOrbitSpeed = 0.53f;     // ȭ��
    public float jupiterOrbitSpeed = 0.08f;  // ��
    public float saturnOrbitSpeed = 0.03f;   // �伺
    public float uranusOrbitSpeed = 0.01f;   // õ�ռ�
    public float neptuneOrbitSpeed = 0.006f; // �ؿռ�

    [Header("Scale (Relative to Sun = 1, Enlarged)")]
    public float scaleRatio = 5f;
    public float mercuryScale = 0.0035f;
    public float venusScale = 0.0087f;
    public float earthScale = 0.0092f;
    public float marsScale = 0.0049f;
    public float jupiterScale = 0.1f;
    public float saturnScale = 0.0835f;
    public float uranusScale = 0.0364f;
    public float neptuneScale = 0.0353f;

    [Header("Rotation Speeds (Relative to Earth = 1)")]
    public float mercuryRotationSpeed = 0.17f;
    public float venusRotationSpeed = 0.04f;
    public float earthRotationSpeed = 1.00f;
    public float marsRotationSpeed = 0.98f;
    public float jupiterRotationSpeed = 2.42f;
    public float saturnRotationSpeed = 2.25f;
    public float uranusRotationSpeed = -1.40f;  // ������
    public float neptuneRotationSpeed = 1.49f;

    [Header("Distance from Sun (Relative to Earth = 10)")]
    public float mercuryDistance = 3.9f;
    public float venusDistance = 7.2f;
    public float earthDistance = 10.0f;
    public float marsDistance = 15.2f;
    public float jupiterDistance = 52.0f;
    public float saturnDistance = 95.8f;
    public float uranusDistance = 192.2f;
    public float neptuneDistance = 300.5f;

    private void Start()
    {
        // �༺ ũ�� ����
        SetInitialScales();

        // �ʱ� �Ÿ� ����
        SetInitialPositions();
    }

    private void Update()
    {
        // �� �༺ ����
        OrbitPlanet(mercury, mercuryOrbitSpeed);
        OrbitPlanet(venus, venusOrbitSpeed);
        OrbitPlanet(earth, earthOrbitSpeed);
        OrbitPlanet(mars, marsOrbitSpeed);
        OrbitPlanet(jupiter, jupiterOrbitSpeed);
        OrbitPlanet(saturn, saturnOrbitSpeed);
        OrbitPlanet(uranus, uranusOrbitSpeed);
        OrbitPlanet(neptune, neptuneOrbitSpeed);

        // �� �༺ ����
        RotatePlanet(sun, 1f);
        RotatePlanet(mercury, mercuryRotationSpeed);
        RotatePlanet(venus, venusRotationSpeed);
        RotatePlanet(earth, earthRotationSpeed);
        RotatePlanet(mars, marsRotationSpeed);
        RotatePlanet(jupiter, jupiterRotationSpeed);
        RotatePlanet(saturn, saturnRotationSpeed);
        RotatePlanet(uranus, uranusRotationSpeed);
        RotatePlanet(neptune, neptuneRotationSpeed);
    }

    private void SetInitialScales()
    {
        mercury.transform.localScale = Vector3.one * mercuryScale * scaleRatio;
        venus.transform.localScale = Vector3.one * venusScale * scaleRatio;
        earth.transform.localScale = Vector3.one * earthScale * scaleRatio;
        mars.transform.localScale = Vector3.one * marsScale * scaleRatio;
        jupiter.transform.localScale = Vector3.one * jupiterScale * scaleRatio;
        saturn.transform.localScale = Vector3.one * saturnScale * scaleRatio;
        uranus.transform.localScale = Vector3.one * uranusScale * scaleRatio;
        neptune.transform.localScale = Vector3.one * neptuneScale * scaleRatio;
    }

    private void SetInitialPositions()
    {
        mercury.transform.position = sun.transform.position + Vector3.right * mercuryDistance;
        venus.transform.position = sun.transform.position + Vector3.right * venusDistance;
        earth.transform.position = sun.transform.position + Vector3.right * earthDistance;
        mars.transform.position = sun.transform.position + Vector3.right * marsDistance;
        jupiter.transform.position = sun.transform.position + Vector3.right * jupiterDistance;
        saturn.transform.position = sun.transform.position + Vector3.right * saturnDistance;
        uranus.transform.position = sun.transform.position + Vector3.right * uranusDistance;
        neptune.transform.position = sun.transform.position + Vector3.right * neptuneDistance;
    }

    // �¾� ���� ����
    private void OrbitPlanet(GameObject planet, float speed)
    {
        planet.transform.RotateAround(sun.transform.position, Vector3.up, speed * Time.deltaTime * 10f);
    }

    // �� �༺ ����
    private void RotatePlanet(GameObject planet, float speed)
    {
        planet.transform.Rotate(Vector3.up, speed * Time.deltaTime * 10f);
    }
}
