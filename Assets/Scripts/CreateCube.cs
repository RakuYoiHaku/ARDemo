using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CreateCube : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;

    private List<ARRaycastHit> _hits = new();

    private void Update()
    {
        /*if (Input.touchCount > 0)
        {
            // ����ڰ� ��ġ�� ���� ������ Touch��� ������ ��� ������ �� �ִ�
            Touch touch = Input.GetTouch(0);

            // ����ڰ� ��ġ(�հ����� ����)�� ���� => ontriggerEnter�� ����� ��
            if (touch.phase != TouchPhase.Began)
                return;

            // ����ĳ��Ʈ�Ŵ����� ������� ��ġ ��Ҹ� �̿��� ����ĳ��Ʈ�� ����
            if (raycastManager.Raycast(touch.position, _hits, TrackableType.PlaneWithinPolygon) == false)
                return;

            // _hits�� ������ ����� �ʾҴٸ� ����
            if(_hits.Count <=0)
                return;

            Pose pose = _hits[0].pose;

            // Pose�� ��ġ�� ȸ������ �̿��� ť�� ����
            Instantiate(raycastManager.raycastPrefab, pose.position, pose.rotation);
        }*/

        // ������� ��ġ�� ������ ����
        if (Input.touchCount <= 0)
            return;

        // ����ڰ� ��ġ�� ���� ������ Touch��� ������ ��� ������ �� �ִ�
        Touch touch = Input.GetTouch(0);

        // ����ڰ� ��ġ(�հ����� ����)�� ���� => ontriggerEnter�� ����� ��
        if (touch.phase != TouchPhase.Began)
            return;

        // ����ĳ��Ʈ�Ŵ����� ������� ��ġ ��Ҹ� �̿��� ����ĳ��Ʈ�� ����
        if (raycastManager.Raycast(touch.position, _hits, TrackableType.PlaneWithinPolygon) == false)
            return;

        // _hits�� ������ ����� �ʾҴٸ� ����
        if (_hits.Count <= 0)
            return;

        Pose pose = _hits[0].pose;

        // Pose�� ��ġ�� ȸ������ �̿��� ť�� ����
        Instantiate(raycastManager.raycastPrefab, pose.position, pose.rotation);
    }
}
