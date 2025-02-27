using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [Header("배경 및 이동 오브젝트")]
    public GameObject[] easyBackgrounds;    // 이지 난이도 배경 배열
    public GameObject[] normalBackgrounds;  // 노멀 난이도 배경 배열
    public GameObject[] hardBackgrounds;    // 하드 난이도 배경 배열
    public GameObject[] extremeBackgrounds; // 익스트림 난이도 배경 배열
    public Image fadeImage; // 페이드 효과용 UI Image
    public float fadeSpeed = 0.05f; // 페이드 속도
    
    [Header("배경 오브젝트 이동 (구름, 산, 땅)")]
    public float resetPositionX = -20f; // 왼쪽 끝으로 이동 시 리셋할 위치
    public float startPositionX = 20f; // 오른쪽에서 새롭게 시작할 위치
    public int maxRepeats = 1; // 각 배경이 반복되는 최대 횟수
    public Transform mainCamera; // 카메라 참조
    private Dictionary<string, GameObject[]> backgroundDict = new Dictionary<string, GameObject[]>(); // 난이도별 배경 관리
    private GameObject[] currentBackgrounds; // 현재 선택된 난이도의 배경 배열
    private GameObject currentBackground; // 현재 배경 오브젝트
    private List<Transform> clouds = new List<Transform>(); // 구름 리스트
    private List<Transform> mountains = new List<Transform>(); // 산 리스트
    private List<Transform> ground = new List<Transform>(); // 땅 리스트
    private int currentStage = 0; // 현재 스테이지 번호
    private int repeatCount = 0; // 현재 배경 반복 횟수
    private string currentDifficulty = "Normal"; // 기본 난이도
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform; // 메인 카메라 자동 할당
        }

        // 난이도별 배경 등록
        backgroundDict.Add("Easy", easyBackgrounds);
        backgroundDict.Add("Normal", normalBackgrounds);
        backgroundDict.Add("Hard", hardBackgrounds);
        backgroundDict.Add("Extreme", extremeBackgrounds);

        ChangeDifficulty("Normal"); // 기본 난이도를 노멀로 설정
    }

    void Update()
    {
        FollowCamera(); // 배경을 카메라와 함께 이동
        MoveElements(clouds, 0.5f);   // 구름은 천천히 이동
        MoveElements(mountains, 1.5f); // 산은 중간 속도로 이동
        MoveElements(ground, 2.5f);    // 땅은 빠르게 이동
    }

    // 배경이 카메라를 따라가도록 설정
    private void FollowCamera()
    {
        if (currentBackground != null)
        {
            Vector3 newPos = currentBackground.transform.position;
            newPos.x = mainCamera.position.x; // 배경의 X 좌표를 카메라의 X 좌표로 고정
            currentBackground.transform.position = newPos;
        }
    }

    // 난이도를 변경하여 해당 난이도의 배경 배열을 적용
    public void ChangeDifficulty(string difficulty)
    {
        if (!backgroundDict.ContainsKey(difficulty))
        {
            Debug.LogError($"존재하지 않는 난이도: {difficulty}");
            return;
        }

        currentDifficulty = difficulty;
        currentBackgrounds = backgroundDict[difficulty];

        Debug.Log($"난이도 변경: {difficulty}, 배경 개수: {currentBackgrounds.Length}");
        ChangeStage(0); // 첫 번째 스테이지부터 시작
    }

    public void ChangeStage(int newStage)
    {
        if (currentBackgrounds == null || newStage >= currentBackgrounds.Length) return;
        StartCoroutine(FadeTransition(newStage));
    }

    private IEnumerator FadeTransition(int newStage)
    {
        Debug.Log($"페이드 아웃 시작, 변경할 배경: {newStage}");

        // 1. 페이드 이미지 활성화
        if (fadeImage == null)
        {
            Debug.LogError(" 페이드 이미지가 설정되지 않았습니다!");
            yield break;
        }
        fadeImage.gameObject.SetActive(true);

        // 2. 페이드 아웃 (화면이 점점 어두워짐)
        for (float i = 0; i <= 1; i += fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, i);
            yield return new WaitForSecondsRealtime(fadeSpeed); //  Time.timeScale 영향 받지 않음
        }

        // 3. 기존 배경 삭제
        if (currentBackground != null)
        {
            Debug.Log($"기존 배경 삭제: {currentBackground.name}");
            Destroy(currentBackground);
        }

        // 4. 새로운 배경 생성
        if (currentBackgrounds == null || newStage >= currentBackgrounds.Length || currentBackgrounds[newStage] == null)
        {
            Debug.LogError($"스테이지 {newStage}의 배경이 null이거나 존재하지 않습니다!");
            yield break;
        }

        currentBackground = Instantiate(currentBackgrounds[newStage], Vector3.zero, Quaternion.identity);
        currentBackground.transform.position = new Vector3(mainCamera.position.x, 0, 0); //  카메라 위치로 설정
        currentBackground.SetActive(true);

        Debug.Log($"새로운 배경 생성 완료: {currentBackground.name}");

        currentStage = newStage;
        repeatCount = 0; //  반복 횟수 초기화

        //  5. 새 배경의 자식 오브젝트 자동 할당
        AssignBackgroundElements();

        //  6. 페이드 인 (화면이 점점 밝아짐)
        for (float i = 1; i >= 0; i -= fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, i);
            yield return new WaitForSecondsRealtime(fadeSpeed); //  Time.timeScale 영향 받지 않음
        }

        //  7. 페이드 이미지 비활성화
        fadeImage.gameObject.SetActive(false);
        Debug.Log($"🎉 스테이지 변경 완료: {currentStage}");
    }


    // **부모 배경(기본 바탕)의 자식 오브젝트(구름, 산, 땅)를 자동 할당**
    private void AssignBackgroundElements()
    {
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

        Debug.Log($"구름 개수: {clouds.Count}, 산 개수: {mountains.Count}, 땅 개수: {ground.Count}");
    }

    // **개별 오브젝트 이동 처리 (구름, 산, 땅 등)**
    private void MoveElements(List<Transform> elements, float speed)
    {
        if (elements.Count == 0) return;

        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].position += Vector3.left * speed * Time.deltaTime;

            if (elements[i].position.x <= resetPositionX)
            {
                elements[i].position = new Vector3(startPositionX, elements[i].position.y, elements[i].position.z);

                // 구름이 한 번 왼쪽 끝까지 이동했을 때 카운트 증가
                if (elements == clouds)
                {
                    repeatCount++;
                    Debug.Log($"배경 반복 횟수: {repeatCount} / {maxRepeats}");

                    if (repeatCount >= maxRepeats)
                    {
                        // 다음 배경으로 이동
                        int nextStage = (currentStage + 1) % currentBackgrounds.Length;
                        Debug.Log($"배경 변경: {currentStage} → {nextStage}");
                        ChangeStage(nextStage);
                    }
                }
            }
        }
    }
}
