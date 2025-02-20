using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class FurnitureSelectButton : MonoBehaviour
{
    [SerializeField] private ARPlacementInteractable placement;
    [SerializeField] private GameObject furniturePrefab;

    // 버튼의 배경 이미지
    private Image _bg;

    // 버튼의 토글 컴포넌트
    private Toggle _toggle;

    private void Awake()
    {
        _bg = GetComponent<Image>();
        _toggle = GetComponent<Toggle>();

        // 토글의 isOn 변수가 변경될 때 실행할 함수를 등록
        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            // 토글의 isOn이 true이면 이쪽 부분 코드 실행

            _bg.color = Color.blue;

            // ARPlacement의 프리팹을 버튼이 가지고 있는 프리팹으로 변경
            placement.placementPrefab = furniturePrefab;
        }

        else
        {
            // 토글의 isOn이 false면 이쪽 부분 코드 실행

            _bg.color = Color.white;
        }
    }
}
