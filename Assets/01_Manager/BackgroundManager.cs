using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [Header("배경 및 이동 오브젝트")]
    public GameObject[] backgrounds; // 스테이지별 배경 프리팹 배열 (부모 배경)
    public Image fadeImage; // 페이드 효과용 UI Image
    public float fadeSpeed = 0.05f; // 페이드 속도

    [Header("배경 오브젝트 이동 (구름, 산, 땅)")]
    public float resetPositionX; // 왼쪽 끝으로 이동 시 리셋할 위치
    public float startPositionX; // 오른쪽에서 새롭게 시작할 위치

    private GameObject currentBackground; // 현재 배경 오브젝트 (부모)
    private List<Transform> clouds = new List<Transform>(); // 구름 리스트
    private List<Transform> mountains = new List<Transform>(); // 산 리스트
    private List<Transform> ground = new List<Transform>(); // 땅 리스트
    private int currentStage = 0; // 현재 스테이지 번호

    void Start()
    {
        // 첫 번째 배경 로드
        ChangeStage(0);
    }

    void Update()
    {
        MoveElements(clouds, 0.5f);   // 구름은 천천히 이동
        MoveElements(mountains, 1.5f); // 산은 중간 속도로 이동
        MoveElements(ground, 2.5f);    // 땅은 빠르게 이동
    }

    public void ChangeStage(int newStage)
    {
        if (newStage >= backgrounds.Length) return; // 스테이지 범위 초과 방지
        StartCoroutine(FadeTransition(newStage));
    }

    private IEnumerator FadeTransition(int newStage)
    {
        // 1. 페이드 아웃 (화면이 점점 어두워짐)
        for (float i = 0; i <= 1; i += fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(fadeSpeed);
        }

        // 2. 기존 배경 삭제 후 새로운 배경 생성
        if (currentBackground != null)
        {
            Destroy(currentBackground);
        }
        currentBackground = Instantiate(backgrounds[newStage], Vector3.zero, Quaternion.identity);
        currentStage = newStage;

        // 3. 새 배경의 자식 오브젝트를 자동으로 가져옴
        AssignBackgroundElements();

        // 4. 페이드 인 (화면이 점점 밝아짐)
        for (float i = 1; i >= 0; i -= fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

    // **부모 배경(기본 바탕)의 자식 오브젝트(구름, 산, 땅)를 자동 할당**
    private void AssignBackgroundElements()
    {
        // 기존 리스트 초기화
        clouds.Clear();
        mountains.Clear();
        ground.Clear();

        if (currentBackground == null) return;

        foreach (Transform child in currentBackground.transform)
        {
            if (child.name.Contains("Cloud")) clouds.Add(child);
            else if (child.name.Contains("Mountain")) mountains.Add(child);
            else if (child.name.Contains("Ground")) ground.Add(child);
        }
    }

    // **개별 오브젝트 이동 처리 (구름, 산, 땅 등)**
    private void MoveElements(List<Transform> elements, float speed)
    {
        if (elements.Count == 0) return;

        foreach (Transform element in elements)
        {
            // 각 오브젝트를 왼쪽으로 이동
            element.position += Vector3.left * speed * Time.deltaTime;

            // 왼쪽 끝에 도달하면 다시 오른쪽에서 등장
            if (element.position.x <= resetPositionX)
            {
                element.position = new Vector3(startPositionX, element.position.y, element.position.z);
            }
        }
    }
}
