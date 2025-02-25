using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SurfaceManager : MonoBehaviour
{
    [SerializeField] private Reticle        reticle;
    [SerializeField] private ARPlaneManager planeManager;

    public event Action TouchEvent;

    // �б����� ������Ƽ
    public ARPlane FixedPlane { get; private set; }

    private void Awake()
    {
        // ��ġ�̺�Ʈ�� �߻������� FixPlane()�Լ��� ȣ���ϵ���
        // ��������Ʈ ü�̴�
        TouchEvent += FixPlane;
    }

    private void Update()
    {
        DetectTouch();

        {
            // FixedPlane�� ������ �Լ� ����
            if (FixedPlane == null)
                return;

            // FixedPlane�� Ȯ����� �ʾ����� �Լ� ����
            if (FixedPlane.subsumedBy == null)
                return;

            // �� ū ������� Ȯ��Ǿ����� ū ����� FixedPlane���� ����
            FixedPlane = FixedPlane.subsumedBy;
        }

        //if (FixedPlane?.subsumedBy != null)
        //    FixedPlane = FixedPlane.subsumedBy;
    }

    // ����ڰ� ������ ��� �ϳ��� ������Ű��
    // ������ ����� ����
    private void FixPlane()
    {
        // ���� ��ƼŬ�� ���߰��ִ� ����� ������ �ٷ� �Լ� ����
        if (reticle.CurrentPlane == null)
            return;

        FixedPlane = reticle.CurrentPlane;

        // planeManager�� ���� ������ ��� ��ȸ�ϸ鼭
        // FixedPlane�� ������ �ٸ� ������ ��� �ı�
        foreach(var plane in planeManager.trackables)
        {
            //plane.gameObject.SetActive(plane == FixedPlane);

            if (plane == FixedPlane) continue;

            Destroy(plane.gameObject);
        }
            

        // ��� ������ ������Ʈ�Ǵ� �̺�Ʈ�� �߻��ϸ�
        // DisableNewPlanes() �Լ��� ȣ���ϵ��� ��������Ʈ ü�̴� (����)
        planeManager.planesChanged += DestroyNewPlanes;

        // ��ġ�̺�Ʈ ���� ���
        TouchEvent -= FixPlane;
    }

    private void DestroyNewPlanes(ARPlanesChangedEventArgs args)
    {
        // FixedPlane �̿ܿ� ���� �߰��Ǵ� ������
        // ��� ��Ȱ��ȭ
        foreach (var plane in args.added)
            Destroy(plane.gameObject);
    }

    private void DetectTouch()
    {
        // ��ġ�� ���� ��� �ٷ� �Լ� ����
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // �հ����� ��������� �ƴϸ� ����
        // (�հ����� �������� �̺�Ʈ�� �����ϱ� ����)
        if (touch.phase != TouchPhase.Began) return;

        // ������� ��ġ�� ���� �̺�Ʈ�� �߻���Ŵ
        TouchEvent?.Invoke();
    }
}
