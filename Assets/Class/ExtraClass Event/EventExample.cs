using System;
using UnityEngine;

public class 뻥튀기이벤트Args
{
    public string 아저씨멘트;
    public int 뻥튀기요금;
}

public class 뻥튀기아저씨
{
    public event Action<뻥튀기이벤트Args> 뻥튀기이벤트;

    private int 뻥튀기요금 = 2000;
    private float 조리에걸리는시간 = 5f;
    private float 조리시간 = 0f;

    
    public void 뻥튀기조리()
    {
        // 시간이 경과하면 조리시간이 늘어남
        조리시간 += Time.deltaTime;

        // 조리시간이 5초 이상이 되면 뻥튀기 조리 완료
        if (조리시간 >= 조리에걸리는시간)
        {
            // 뻥튀기 이벤트를 발생시킴(매개변수는 string "뻥이요!")
            뻥튀기이벤트.Invoke(new 뻥튀기이벤트Args() { 아저씨멘트 = "뻥이요", 뻥튀기요금 = 2000});
            조리시간 = 0f;
        }
    }
}

public class 꼬맹이
{
    private int 용돈 = 1000;

    // 생성자 함수
    public 꼬맹이(뻥튀기아저씨 아저씨)
    {
        // += 이벤트를 구독하겠다.

        // -= 이벤트 구독을 취소하겠다.
        아저씨.뻥튀기이벤트 += 뻥튀기완료함수;
    }

    private void 뻥튀기완료함수(뻥튀기이벤트Args args)
    {
        Debug.Log($"뻥튀기 아저씨 : {args.아저씨멘트}\n꼬맹이 : 와 맛있겠다.");

        // 용돈이 뻥튀기 요금보다 낮으면 
        // 못사먹음
        if(용돈 < args.뻥튀기요금)
        {
            Debug.Log("힝 돈이 부족하네...");
        }
        else
        {
            Debug.Log("뻥튀기 하나 주세요.");
        }
    }
}

public class EventExample : MonoBehaviour
{
    // 뻥튀기 아저씨 생성
    private 뻥튀기아저씨 아저씨 = new();

    private 꼬맹이 꼬마;

    private void Awake()
    {
        꼬마 = new(아저씨);
    }

    private void Update()
    {
        아저씨.뻥튀기조리();
    }
}
