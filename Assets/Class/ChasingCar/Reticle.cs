using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Reticle : MonoBehaviour
{
    // �б� ���� ������Ƽ
    public ARPlane CurrentPlane { get; private set;}

    [SerializeField] private ARPlaneManager   planeManager;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject       reticleModel;

    private List<ARRaycastHit> _hits          = new();
    private Vector2            _screenCenter;

    private void Awake()
    {
        _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update()
    {
        if (raycastManager.Raycast(_screenCenter, _hits, TrackableType.PlaneWithinBounds))
        {
            // ����ĳ��Ʈ�� �����ϸ� �� �κ��� ����

            // ��ƼŬ ���ӿ�����Ʈ�� Ȱ��ȭ
            reticleModel.SetActive(true);

            // ��ƼŬ ���ӿ�����Ʈ�� ��ġ�� ����ĳ��Ʈ�� ��ġ�� �̵�
            Pose currentPose = _hits[0].pose;
            transform.position = currentPose.position;

            // ����ĳ��Ʈ�� �������� ���, ARRaycastHit�� ���� trackableID�� Ȱ����
            // PlaneManager�� ���� ����ĳ��Ʈ�� ������ ����� �����ͼ� CurrentPlane�� �Ҵ�
            CurrentPlane = planeManager.GetPlane(_hits[0].trackableId);
        }
        else
        {
            // �����ϸ� �� �κ��� ����
            reticleModel.SetActive(false);

            // ����ĳ��Ʈ�� �ȵǸ� CurrentPlane�� ����
            CurrentPlane = null;
        }
    }
}
