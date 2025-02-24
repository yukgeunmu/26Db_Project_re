using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject backgroundPrefab1, backgroundPrefab2; // 두 개의 배경 프리팹
    public float scrollSpeed = 5f; // 배경 이동 속도
    public float backgroundWidth = 20f; // 배경 한 개의 너비
    public int stageRepeatCount = 5; // 스테이지별 반복 횟수
    private int currentRepeatCount = 0; // 현재 반복 횟수

    private GameObject bg1, bg2; // 생성된 배경 오브젝트
    private Vector3 bg1StartPos, bg2StartPos; // 시작 위치 저장

    void Start()
    {
        // 배경 초기화
        bg1 = Instantiate(backgroundPrefab1, new Vector3(0, 0, 0), Quaternion.identity);
        bg2 = Instantiate(backgroundPrefab2, new Vector3(backgroundWidth, 0, 0), Quaternion.identity);
        
        bg1StartPos = bg1.transform.position;
        bg2StartPos = bg2.transform.position;
    }

    void Update()
    {
        if (currentRepeatCount >= stageRepeatCount) return; // 반복 횟수를 초과하면 멈춤

        // 배경을 왼쪽(-x) 방향으로 이동
        bg1.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
        bg2.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // 배경이 화면 밖으로 나가면 위치 재설정
        if (bg1.transform.position.x <= -backgroundWidth)
        {
            ResetBackground(bg1);
            currentRepeatCount++; // 반복 횟수 증가
        }
        if (bg2.transform.position.x <= -backgroundWidth)
        {
            ResetBackground(bg2);
            currentRepeatCount++; // 반복 횟수 증가
        }
    }

    // 배경을 오른쪽으로 다시 이동시켜서 무한 반복 효과
    void ResetBackground(GameObject bg)
    {
        bg.transform.position = new Vector3(backgroundWidth, 0, 0);
    }

    // 스테이지 변경 시 반복 횟수 설정
    public void SetStageRepeatCount(int newRepeatCount)
    {
        stageRepeatCount = newRepeatCount;
        currentRepeatCount = 0; // 새 스테이지에서 반복 횟수 초기화
    }
}
