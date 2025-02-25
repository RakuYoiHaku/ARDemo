using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PressHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
     public UnityEvent onPressed;
     public UnityEvent onReleased;
     public UnityEvent onPressStay;

    private bool _pressed;

    // IPointerDownHandler 인터페이스를 상속받은 컴포넌트(UI 컴포넌트 한정)에
    // 터치, 마우스 클릭 이벤트가 발생했을때 호출되는 함수
    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        onPressed?.Invoke();
    }

    // IPointerUpHandler 인터페이스를 상속받은 UI 컴포넌트 요소에
    // 터치, 마우스 클릭 동작이 끝났을때 호출되는 함수
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
