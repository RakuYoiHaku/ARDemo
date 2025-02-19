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
            // 사용자가 터치한 곳의 정보를 Touch라는 변수에 담아 가져올 수 있다
            Touch touch = Input.GetTouch(0);

            // 사용자가 터치(손가락이 닿음)을 감지 => ontriggerEnter랑 비슷한 듯
            if (touch.phase != TouchPhase.Began)
                return;

            // 레이캐스트매니저가 사용자의 터치 장소를 이용해 레이캐스트를 수행
            if (raycastManager.Raycast(touch.position, _hits, TrackableType.PlaneWithinPolygon) == false)
                return;

            // _hits에 정보가 담기지 않았다면 리턴
            if(_hits.Count <=0)
                return;

            Pose pose = _hits[0].pose;

            // Pose의 위치와 회전값을 이용해 큐브 생성
            Instantiate(raycastManager.raycastPrefab, pose.position, pose.rotation);
        }*/

        // 사용자의 터치가 없으면 리턴
        if (Input.touchCount <= 0)
            return;

        // 사용자가 터치한 곳의 정보를 Touch라는 변수에 담아 가져올 수 있다
        Touch touch = Input.GetTouch(0);

        // 사용자가 터치(손가락이 닿음)을 감지 => ontriggerEnter랑 비슷한 듯
        if (touch.phase != TouchPhase.Began)
            return;

        // 레이캐스트매니저가 사용자의 터치 장소를 이용해 레이캐스트를 수행
        if (raycastManager.Raycast(touch.position, _hits, TrackableType.PlaneWithinPolygon) == false)
            return;

        // _hits에 정보가 담기지 않았다면 리턴
        if (_hits.Count <= 0)
            return;

        Pose pose = _hits[0].pose;

        // Pose의 위치와 회전값을 이용해 큐브 생성
        Instantiate(raycastManager.raycastPrefab, pose.position, pose.rotation);
    }
}
