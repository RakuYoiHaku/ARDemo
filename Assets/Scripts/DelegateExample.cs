using System;
using System.Threading.Tasks;
using UnityEngine;

// 최비서님 : 
// 김대리님 내일 회의일정이 사장님 개인 사정으로 인해 변경되었습니다.
// 메시지 확인하시면(메시지를 확인할때까지 기다렸다가) 회신(Callback, 다음 동작을 수행하겠다.) 부탁드립니다.

// 비서 클래스
public class Sacretary
{
    public async Task CheckOfficerCanJoin(Officer officer)
    {
        await officer.OnMessageReceived(CheckNumber);
    }

    private void CheckNumber(int officerDelay)
    {
        if (officerDelay >= 1500)
        {
            Debug.Log($"{officerDelay} : 빠른 답변 감사합니다.");
        }
        else
        {
            Debug.Log($"{officerDelay} : 빠른 답변 부탁드립니다.");
        }
    }
}

public class Boss
{
    public async Task CheckOfficer(Officer officer)
    {
        await officer.OnMessageReceived(OnCallback);
    }

    private void OnCallback(int officerDelay)
    {
        if (officerDelay >= 800)
        {
            Debug.Log($"{officerDelay} : Fire!");
        }
    }
}

// 직원 클래스
public class Officer
{
    // 콜백 함수를 매개변수로 받음
    public async Task OnMessageReceived(Action<int> callback)
    {
        var delay = UnityEngine.Random.Range(1000, 3000);

        await Task.Delay(delay);

        // 어떤 행위를 다 마치고, 그뒤에 델리게이트를 통해 콜백함수를 실행
        callback.Invoke(delay);
    }
}

public class DelegateExample : MonoBehaviour
{
    Boss      boss = new();
    Sacretary sacretary = new();
    Officer   officer = new();

    private async void Awake()
    {
        await boss.CheckOfficer(officer);
        await sacretary.CheckOfficerCanJoin(officer);
    }
}
