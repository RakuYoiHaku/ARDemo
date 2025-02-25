using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARRuler : MonoBehaviour
{
    [SerializeField] private GameObject       worldCanvas;
    [SerializeField] private TMP_Text         txtDistance;
    [SerializeField] private Image            imgCenterPoint;
    [SerializeField] private ARRaycastManager raycastManager;

    private LineRenderer _line;

    private Vector3 _startPosition;

    private Vector2 _screenCenter;

    private List<ARRaycastHit> _hits = new();

    private Pose _currentPose;

    private bool _canMeasure;
    private bool _isMeasuring;

    private float _distance;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();

        // 어플리케이션을 실행하는 디바이스의 화면 정보를 이용해서,
        // 화면의 중앙 부분을 Vector2로 받아옴
        _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    // UI의 버튼을 눌렀을때 시작점을 세팅함
    public void SetStartPosition()
    {
        // _startPosition 변수에 버튼이 눌렸을 당시의 화면 중앙 위치을 저장
        _startPosition = _currentPose.position;

        _isMeasuring = true;


        // 라인렌더러로 선을 그리기 위한 준비를 함
        _line.positionCount = 2;

        // 라인렌더러의 시작점을 전달함
        _line.SetPosition(0, _startPosition);
    }

    // UI의 버튼에서 손을 뗏을때 측정을 중지함
    public void SetEndPosition()
    {
        _isMeasuring = false;
    }

    private void Update()
    {
        // 매 프레임마다 레이캐스트 매니저를 이용해
        // 화면의 중앙부분을(_screenCenter)를 향해 레이캐스트를 쏘고,
        // 레이캐스트에 성공하면 _hits 콜렉션에 그 정보를 담는다.
        if (raycastManager.Raycast(_screenCenter, _hits, TrackableType.PlaneWithinPolygon))
        {
            // 레이캐스트를 성공하면 이쪽 부분 실행

            // 화면 중앙의 빨간점의 색을 초록색으로 바꿈
            imgCenterPoint.color = Color.green;

            // Pose를 이용해 레이캐스트를 성공한 지점의 정보를 저장
            _currentPose = _hits[0].pose;

            _canMeasure = true;
        }

        else
        {
            // 플레인에 레이캐스트를 성공하지 못한 경우 이부분 실행

            // 중앙에 점을 빨간색으로 바꿈
            imgCenterPoint.color = Color.red;

            _canMeasure = false;
        }

        // UI의 버튼을 눌러서 측정이 시작되었으면
        // 이 부분이 실행
        if (_canMeasure && _isMeasuring)
        {
            // 측정이 가능한 위치에 있고, 측정중이라면 이부분이 실행

            // 버튼을 눌렀을때의 시작위치(_startPosition)과 현재 화면 중앙의 거리를 계산
            _distance = Vector3.Distance(_startPosition, _currentPose.position);

            // 라인렌더러의 끝점을 전달하여 선을 그려내도록 함
            _line.SetPosition(1, _currentPose.position);

            UpdateDistanceText();
        }
    }

    private void UpdateDistanceText()
    {
        // 라인렌더러가 선을 그리지 못했다면 함수 바로 종료
        if (_line.positionCount < 2) 
            return;

        // 라인렌더러의 시작점과 끝점을 가져옴
        Vector3 start = _line.GetPosition(0);
        Vector3 end   = _line.GetPosition(1);

        // 시작점과 끝점을 이용해 중간 지점을 계산
        // 라인렌더러의 선보다 살짝 윗쪽에 위치
        Vector3 middle = ((start + end) / 2) + Vector3.up * 0.05f;

        // 월드 캔버스의 위치를 조정
        worldCanvas.transform.position = middle;

        // 라인렌더러가 그린 라인의 방향을 계산

        // A지점과 B지점이 있을때,
        // A지점에서 B지점으로 향하는 방향을 계산하고 싶으면,
        // B의 위치(Vector3)에서 A의 위치(Vector3)를 빼고,
        // Vector3값을 정규화(Nomalize)하면,
        // 방향을 계산할 수 있다.

        Vector3 direction = (end - start).normalized;

        // 해당 방향을 바라보는 회전값이 얼마인지 계산
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // 라인렌더러의 라인과 캔버스가 평행하도록 회전값 적용
        worldCanvas.transform.rotation = lookRotation;

        // 문자열 포매팅을 이용해서 소수점 둘째자리까지 숫자를 표시
        txtDistance.text = $"{_distance:F2} M";
    }
}
