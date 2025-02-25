using System.Collections.Generic;
using UnityEngine;

public class ARLineDrawer : MonoBehaviour
{
    // Draw() 함수를 호출했을때 라인렌더러 게임오브젝트를 생성하기위한 프리팹
    [SerializeField] LineRenderer lineRendererPrefab;

    // 라인렌더러에게 화면 가운데의 3D 좌표를 알려주기 위한 기준점
    [SerializeField] Transform    pivot;

    // Draw() 함수가 호출될때마다 생성되는 라인렌더러를 저장할 리스트 콜렉션
    // 만약 '모두 지우기' 같은 기능을 만들때 기존에 생성했던 라인렌더러를 참조하기 위한 리스트
    private List<LineRenderer> _lineList = new();

    // Draw() 함수가 호출되서 생성된 현재 라인렌더러 변수
    private LineRenderer       _currentLine;

    private bool _isDrawing;

    public void Draw()
    {
        // 현재 사용하고 있는 라인렌더러가 없으면,
        if (_currentLine == null)
        {
            // 프리팹을 이용해서 하나 생성하고 _currentLine에 할당
            _currentLine = Instantiate(lineRendererPrefab);

            // 라인 렌더러 초기화
            _currentLine.positionCount = 1;
            _currentLine.SetPosition(0, pivot.position);

            // 라인렌더러의 선 색을 빨간색으로 설정
            _currentLine.startColor = _currentLine.endColor = Color.red;

            _lineList.Add(_currentLine);
        }

        _isDrawing = true;
    }

    public void StopDraw()
    {
        // _isDraw 변수를 false로 만들어서 Update함수가 실행되지 않도록
        _isDrawing = false;

        // Draw() 함수가 호출되면 다시 새로운 라인렌더러를 생성하기위해 null로 만듦.
        _currentLine = null;
    }

    private void Update()
    {
        // _isDrawing 변수가 false면 Update함수 바로 종료
        if (_isDrawing == false)
            return;

        // 라인렌더러가 선을 그리는 방법
        // positionCount(라인 렌더러가 저장하고 있는 좌표의 갯수)를 하나씩 늘려서
        // 해당 position(좌표)를 잇는 선을 그려냄

        // 라인렌더러가 저장하고 있는 좌표의 갯수를 하나 늘림
        {
            //_currentLine.positionCount = _currentLine.positionCount + 1;

            // 현재 라인렌더러가 저장하고있는 좌표의 갯수
            int currentPosionCount = _currentLine.positionCount;

            // 라인렌더러가 저장할 좌표의 갯수를 하나 늘림
            int nextPositionCount = currentPosionCount + 1;
            _currentLine.positionCount = nextPositionCount;
        }

        // 추가된 좌표에 pivot(카메라보다 살짝 앞에 있는 게임오브젝트)의 위치를 전달
        {
            //_currentLine.SetPosition(_currentLine.positionCount - 1, pivot.position);

            // 인덱스는 1이 아니라 0부터 시작하기 때문에 1을 빼준다.
            int index = _currentLine.positionCount - 1;

            // SetPosition의 매개변수는
            // index : '몇번째' 좌표에 값을 전달할지
            // position : 좌표에 '어떤 값'을 전달할지
            _currentLine.SetPosition(index, pivot.position);
        }
    }
}
