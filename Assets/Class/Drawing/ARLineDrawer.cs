using System.Collections.Generic;
using UnityEngine;

public class ARLineDrawer : MonoBehaviour
{
    // Draw() �Լ��� ȣ�������� ���η����� ���ӿ�����Ʈ�� �����ϱ����� ������
    [SerializeField] LineRenderer lineRendererPrefab;

    // ���η��������� ȭ�� ����� 3D ��ǥ�� �˷��ֱ� ���� ������
    [SerializeField] Transform    pivot;

    // Draw() �Լ��� ȣ��ɶ����� �����Ǵ� ���η������� ������ ����Ʈ �ݷ���
    // ���� '��� �����' ���� ����� ���鶧 ������ �����ߴ� ���η������� �����ϱ� ���� ����Ʈ
    private List<LineRenderer> _lineList = new();

    // Draw() �Լ��� ȣ��Ǽ� ������ ���� ���η����� ����
    private LineRenderer       _currentLine;

    private bool _isDrawing;

    public void Draw()
    {
        // ���� ����ϰ� �ִ� ���η������� ������,
        if (_currentLine == null)
        {
            // �������� �̿��ؼ� �ϳ� �����ϰ� _currentLine�� �Ҵ�
            _currentLine = Instantiate(lineRendererPrefab);

            // ���� ������ �ʱ�ȭ
            _currentLine.positionCount = 1;
            _currentLine.SetPosition(0, pivot.position);

            // ���η������� �� ���� ���������� ����
            _currentLine.startColor = _currentLine.endColor = Color.red;

            _lineList.Add(_currentLine);
        }

        _isDrawing = true;
    }

    public void StopDraw()
    {
        // _isDraw ������ false�� ���� Update�Լ��� ������� �ʵ���
        _isDrawing = false;

        // Draw() �Լ��� ȣ��Ǹ� �ٽ� ���ο� ���η������� �����ϱ����� null�� ����.
        _currentLine = null;
    }

    private void Update()
    {
        // _isDrawing ������ false�� Update�Լ� �ٷ� ����
        if (_isDrawing == false)
            return;

        // ���η������� ���� �׸��� ���
        // positionCount(���� �������� �����ϰ� �ִ� ��ǥ�� ����)�� �ϳ��� �÷���
        // �ش� position(��ǥ)�� �մ� ���� �׷���

        // ���η������� �����ϰ� �ִ� ��ǥ�� ������ �ϳ� �ø�
        {
            //_currentLine.positionCount = _currentLine.positionCount + 1;

            // ���� ���η������� �����ϰ��ִ� ��ǥ�� ����
            int currentPosionCount = _currentLine.positionCount;

            // ���η������� ������ ��ǥ�� ������ �ϳ� �ø�
            int nextPositionCount = currentPosionCount + 1;
            _currentLine.positionCount = nextPositionCount;
        }

        // �߰��� ��ǥ�� pivot(ī�޶󺸴� ��¦ �տ� �ִ� ���ӿ�����Ʈ)�� ��ġ�� ����
        {
            //_currentLine.SetPosition(_currentLine.positionCount - 1, pivot.position);

            // �ε����� 1�� �ƴ϶� 0���� �����ϱ� ������ 1�� ���ش�.
            int index = _currentLine.positionCount - 1;

            // SetPosition�� �Ű�������
            // index : '���°' ��ǥ�� ���� ��������
            // position : ��ǥ�� '� ��'�� ��������
            _currentLine.SetPosition(index, pivot.position);
        }
    }
}
