using System;
using System.Threading.Tasks;
using UnityEngine;

// �ֺ񼭴� : 
// ��븮�� ���� ȸ�������� ����� ���� �������� ���� ����Ǿ����ϴ�.
// �޽��� Ȯ���Ͻø�(�޽����� Ȯ���Ҷ����� ��ٷȴٰ�) ȸ��(Callback, ���� ������ �����ϰڴ�.) ��Ź�帳�ϴ�.

// �� Ŭ����
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
            Debug.Log($"{officerDelay} : ���� �亯 �����մϴ�.");
        }
        else
        {
            Debug.Log($"{officerDelay} : ���� �亯 ��Ź�帳�ϴ�.");
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

// ���� Ŭ����
public class Officer
{
    // �ݹ� �Լ��� �Ű������� ����
    public async Task OnMessageReceived(Action<int> callback)
    {
        var delay = UnityEngine.Random.Range(1000, 3000);

        await Task.Delay(delay);

        // � ������ �� ��ġ��, �׵ڿ� ��������Ʈ�� ���� �ݹ��Լ��� ����
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
