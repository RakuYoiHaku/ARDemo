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

        // ���ø����̼��� �����ϴ� ����̽��� ȭ�� ������ �̿��ؼ�,
        // ȭ���� �߾� �κ��� Vector2�� �޾ƿ�
        _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    // UI�� ��ư�� �������� �������� ������
    public void SetStartPosition()
    {
        // _startPosition ������ ��ư�� ������ ����� ȭ�� �߾� ��ġ�� ����
        _startPosition = _currentPose.position;

        _isMeasuring = true;


        // ���η������� ���� �׸��� ���� �غ� ��
        _line.positionCount = 2;

        // ���η������� �������� ������
        _line.SetPosition(0, _startPosition);
    }

    // UI�� ��ư���� ���� ������ ������ ������
    public void SetEndPosition()
    {
        _isMeasuring = false;
    }

    private void Update()
    {
        // �� �����Ӹ��� ����ĳ��Ʈ �Ŵ����� �̿���
        // ȭ���� �߾Ӻκ���(_screenCenter)�� ���� ����ĳ��Ʈ�� ���,
        // ����ĳ��Ʈ�� �����ϸ� _hits �ݷ��ǿ� �� ������ ��´�.
        if (raycastManager.Raycast(_screenCenter, _hits, TrackableType.PlaneWithinPolygon))
        {
            // ����ĳ��Ʈ�� �����ϸ� ���� �κ� ����

            // ȭ�� �߾��� �������� ���� �ʷϻ����� �ٲ�
            imgCenterPoint.color = Color.green;

            // Pose�� �̿��� ����ĳ��Ʈ�� ������ ������ ������ ����
            _currentPose = _hits[0].pose;

            _canMeasure = true;
        }

        else
        {
            // �÷��ο� ����ĳ��Ʈ�� �������� ���� ��� �̺κ� ����

            // �߾ӿ� ���� ���������� �ٲ�
            imgCenterPoint.color = Color.red;

            _canMeasure = false;
        }

        // UI�� ��ư�� ������ ������ ���۵Ǿ�����
        // �� �κ��� ����
        if (_canMeasure && _isMeasuring)
        {
            // ������ ������ ��ġ�� �ְ�, �������̶�� �̺κ��� ����

            // ��ư�� ���������� ������ġ(_startPosition)�� ���� ȭ�� �߾��� �Ÿ��� ���
            _distance = Vector3.Distance(_startPosition, _currentPose.position);

            // ���η������� ������ �����Ͽ� ���� �׷������� ��
            _line.SetPosition(1, _currentPose.position);

            UpdateDistanceText();
        }
    }

    private void UpdateDistanceText()
    {
        // ���η������� ���� �׸��� ���ߴٸ� �Լ� �ٷ� ����
        if (_line.positionCount < 2) 
            return;

        // ���η������� �������� ������ ������
        Vector3 start = _line.GetPosition(0);
        Vector3 end   = _line.GetPosition(1);

        // �������� ������ �̿��� �߰� ������ ���
        // ���η������� ������ ��¦ ���ʿ� ��ġ
        Vector3 middle = ((start + end) / 2) + Vector3.up * 0.05f;

        // ���� ĵ������ ��ġ�� ����
        worldCanvas.transform.position = middle;

        // ���η������� �׸� ������ ������ ���

        // A������ B������ ������,
        // A�������� B�������� ���ϴ� ������ ����ϰ� ������,
        // B�� ��ġ(Vector3)���� A�� ��ġ(Vector3)�� ����,
        // Vector3���� ����ȭ(Nomalize)�ϸ�,
        // ������ ����� �� �ִ�.

        Vector3 direction = (end - start).normalized;

        // �ش� ������ �ٶ󺸴� ȸ������ ������ ���
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // ���η������� ���ΰ� ĵ������ �����ϵ��� ȸ���� ����
        worldCanvas.transform.rotation = lookRotation;

        // ���ڿ� �������� �̿��ؼ� �Ҽ��� ��°�ڸ����� ���ڸ� ǥ��
        txtDistance.text = $"{_distance:F2} M";
    }
}
