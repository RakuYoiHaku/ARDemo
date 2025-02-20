using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class FurnitureSelectButton : MonoBehaviour
{
    [SerializeField] private ARPlacementInteractable placement;
    [SerializeField] private GameObject furniturePrefab;

    // ��ư�� ��� �̹���
    private Image _bg;

    // ��ư�� ��� ������Ʈ
    private Toggle _toggle;

    private void Awake()
    {
        _bg = GetComponent<Image>();
        _toggle = GetComponent<Toggle>();

        // ����� isOn ������ ����� �� ������ �Լ��� ���
        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            // ����� isOn�� true�̸� ���� �κ� �ڵ� ����

            _bg.color = Color.blue;

            // ARPlacement�� �������� ��ư�� ������ �ִ� ���������� ����
            placement.placementPrefab = furniturePrefab;
        }

        else
        {
            // ����� isOn�� false�� ���� �κ� �ڵ� ����

            _bg.color = Color.white;
        }
    }
}
