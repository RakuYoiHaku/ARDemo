using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Reticle : MonoBehaviour
{
    // 읽기 전용 프로퍼티
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
            // 레이캐스트가 성공하면 이 부분이 실행

            // 레티클 게임오브젝트를 활성화
            reticleModel.SetActive(true);

            // 레티클 게임오브젝트의 위치를 레이캐스트된 위치로 이동
            Pose currentPose = _hits[0].pose;
            transform.position = currentPose.position;

            // 레이캐스트가 성공했을 경우, ARRaycastHit가 가진 trackableID를 활용해
            // PlaneManager로 부터 레이캐스트가 성공한 평면을 가져와서 CurrentPlane에 할당
            CurrentPlane = planeManager.GetPlane(_hits[0].trackableId);
        }
        else
        {
            // 실패하면 이 부분이 실행
            reticleModel.SetActive(false);

            // 레이캐스트가 안되면 CurrentPlane은 없음
            CurrentPlane = null;
        }
    }
}
