using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAnimUI : MonoBehaviour
{
    public CanvasGroup[] background; // 배경 이미지 4개
    public float fadeDuration = 1.5f; // 페이드 인/아웃 시간
    public float displayDuration = 3f; // 각 배경이 유지되는 시간
    public float moveDistance = 100f; // 배경이 뒤로 이동하는 거리
    public float moveSpeed = 1f; // 이동 속도

    private int currentIndex = 0;

    private void Start()
    {
        StartCoroutine(BackgroundLoop());
    }

    private IEnumerator BackgroundLoop()
    {
 
            int nextIndex = (currentIndex + 1) % background.Length;

            yield return StartCoroutine(FadeOut(background[currentIndex]));
            yield return StartCoroutine(FadeIn(background[nextIndex]));

            currentIndex = nextIndex;
            yield return new WaitForSeconds(displayDuration);

        StartCoroutine(BackgroundLoop());

    }
    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {

        float timer = 0f;


        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            timer += Time.unscaledDeltaTime;
            yield return null ;
        }
        canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float timer = 0f;


        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }


}
