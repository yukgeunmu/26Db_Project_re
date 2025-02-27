using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAnimUI : MonoBehaviour
{
    public CanvasGroup[] background; // ��� �̹��� 4��
    public float fadeDuration = 1.5f; // ���̵� ��/�ƿ� �ð�
    public float displayDuration = 3f; // �� ����� �����Ǵ� �ð�
    public float moveDistance = 100f; // ����� �ڷ� �̵��ϴ� �Ÿ�
    public float moveSpeed = 1f; // �̵� �ӵ�

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
