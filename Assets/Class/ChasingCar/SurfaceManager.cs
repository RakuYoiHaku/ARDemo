using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SurfaceManager : MonoBehaviour
{
    [SerializeField] private Reticle        reticle;
    [SerializeField] private ARPlaneManager planeManager;

    public event Action TouchEvent;

    // 읽기전용 프로퍼티
    public ARPlane FixedPlane { get; private set; }

    private void Awake()
    {
        // 터치이벤트가 발생했을때 FixPlane()함수를 호출하도록
        // 델리게이트 체이닝
        TouchEvent += FixPlane;
    }

    private void Update()
    {
        DetectTouch();

        {
            // FixedPlane이 없으면 함수 종료
            if (FixedPlane == null)
                return;

            // FixedPlane이 확장되지 않았으면 함수 종료
            if (FixedPlane.subsumedBy == null)
                return;

            // 더 큰 평면으로 확장되었을때 큰 평면을 FixedPlane으로 설정
            FixedPlane = FixedPlane.subsumedBy;
        }

        //if (FixedPlane?.subsumedBy != null)
        //    FixedPlane = FixedPlane.subsumedBy;
    }

    // 사용자가 선택한 평면 하나만 고정시키고
    // 나머지 평면은 없앰
    private void FixPlane()
    {
        // 현재 레티클이 비추고있는 평면이 없으면 바로 함수 종료
        if (reticle.CurrentPlane == null)
            return;

        FixedPlane = reticle.CurrentPlane;

        // planeManager가 가진 평면들을 모두 순회하면서
        // FixedPlane을 제외한 다른 평면들은 모두 파괴
        foreach(var plane in planeManager.trackables)
        {
            //plane.gameObject.SetActive(plane == FixedPlane);

            if (plane == FixedPlane) continue;

            Destroy(plane.gameObject);
        }
            

        // 평면 정보가 업데이트되는 이벤트가 발생하면
        // DisableNewPlanes() 함수를 호출하도록 델리게이트 체이닝 (구독)
        planeManager.planesChanged += DestroyNewPlanes;

        // 터치이벤트 구독 취소
        TouchEvent -= FixPlane;
    }

    private void DestroyNewPlanes(ARPlanesChangedEventArgs args)
    {
        // FixedPlane 이외에 새로 추가되는 평면들은
        // 모두 비활성화
        foreach (var plane in args.added)
            Destroy(plane.gameObject);
    }

    private void DetectTouch()
    {
        // 터치가 없는 경우 바로 함수 종료
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // 손가락이 닿았을때가 아니면 종료
        // (손가락이 떼어지는 이벤트도 존재하기 때문)
        if (touch.phase != TouchPhase.Began) return;

        // 사용자의 터치에 대한 이벤트를 발생시킴
        TouchEvent?.Invoke();
    }
}
