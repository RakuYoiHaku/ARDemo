using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
     public UnityEvent onPressed;
     public UnityEvent onReleased;
     public UnityEvent onPressStay;

    private bool _pressed;

    // IPointerDownHandler �������̽��� ��ӹ��� ������Ʈ(UI ������Ʈ ����)��
    // ��ġ, ���콺 Ŭ�� �̺�Ʈ�� �߻������� ȣ��Ǵ� �Լ�
    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        onPressed?.Invoke();
    }

    // IPointerUpHandler �������̽��� ��ӹ��� UI ������Ʈ ��ҿ�
    // ��ġ, ���콺 Ŭ�� ������ �������� ȣ��Ǵ� �Լ�
    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
        onReleased?.Invoke();
    }

    private void Update()
    {
        if (_pressed)
            onPressStay?.Invoke();
    }
}
