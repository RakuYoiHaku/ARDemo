using System;
using UnityEngine;

public class ��Ƣ���̺�ƮArgs
{
    public string ��������Ʈ;
    public int ��Ƣ����;
}

public class ��Ƣ�������
{
    public event Action<��Ƣ���̺�ƮArgs> ��Ƣ���̺�Ʈ;

    private int ��Ƣ���� = 2000;
    private float �������ɸ��½ð� = 5f;
    private float �����ð� = 0f;

    
    public void ��Ƣ������()
    {
        // �ð��� ����ϸ� �����ð��� �þ
        �����ð� += Time.deltaTime;

        // �����ð��� 5�� �̻��� �Ǹ� ��Ƣ�� ���� �Ϸ�
        if (�����ð� >= �������ɸ��½ð�)
        {
            // ��Ƣ�� �̺�Ʈ�� �߻���Ŵ(�Ű������� string "���̿�!")
            ��Ƣ���̺�Ʈ.Invoke(new ��Ƣ���̺�ƮArgs() { ��������Ʈ = "���̿�", ��Ƣ���� = 2000});
            �����ð� = 0f;
        }
    }
}

public class ������
{
    private int �뵷 = 1000;

    // ������ �Լ�
    public ������(��Ƣ������� ������)
    {
        // += �̺�Ʈ�� �����ϰڴ�.

        // -= �̺�Ʈ ������ ����ϰڴ�.
        ������.��Ƣ���̺�Ʈ += ��Ƣ��Ϸ��Լ�;
    }

    private void ��Ƣ��Ϸ��Լ�(��Ƣ���̺�ƮArgs args)
    {
        Debug.Log($"��Ƣ�� ������ : {args.��������Ʈ}\n������ : �� ���ְڴ�.");

        // �뵷�� ��Ƣ�� ��ݺ��� ������ 
        // �������
        if(�뵷 < args.��Ƣ����)
        {
            Debug.Log("�� ���� �����ϳ�...");
        }
        else
        {
            Debug.Log("��Ƣ�� �ϳ� �ּ���.");
        }
    }
}

public class EventExample : MonoBehaviour
{
    // ��Ƣ�� ������ ����
    private ��Ƣ������� ������ = new();

    private ������ ����;

    private void Awake()
    {
        ���� = new(������);
    }

    private void Update()
    {
        ������.��Ƣ������();
    }
}
