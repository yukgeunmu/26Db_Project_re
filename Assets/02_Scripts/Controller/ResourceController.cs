using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    private AnimationHandler animationHandler;
    [SerializeField] private SpriteRenderer HairSprite;
    [SerializeField] private SpriteRenderer FaceSprite;
    [SerializeField] private SpriteRenderer ScarfSprite;
    [SerializeField] private SpriteRenderer ArmorSprite;
    [SerializeField] private SpriteRenderer FrameSprite;
    private StatHandler statHandler;
    public float CurrentHealth { get; private set; }
    public float CurrentVelocity { get; private set; }
    public float CurrentJumpPower { get; private set; }
    public int CurrentJumpCount { get; private set; }
    public float CurrentJumpTime { get; private set; }
    public float CurrentJumpHeight { get; private set; }
    public float MaxHealth => statHandler.MaxHealth;

    public void OnAnimationIdle() => animationHandler.Idle();
    public void OnAnimationMove(Vector2 obj) => animationHandler.Moving(obj);
    public void OnAnimationJump(int jumpCount) => animationHandler.Jumping(jumpCount);
    public void OnAnimationSlide(bool isTrue) => animationHandler.Sliding(isTrue);

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();
        CurrentHealth = statHandler.MaxHealth;
        CurrentVelocity = statHandler.MaxVelocity;
        CurrentJumpPower = statHandler.MaxJumpPower;
        CurrentJumpCount = statHandler.MaxJumpCount;
        CurrentJumpTime = statHandler.MaxJumpTime;
        CurrentJumpHeight = statHandler.MaxJumpHeight;
    }

    private void Start()
    {
        animationHandler.Init();
        ApplySavedColors();
    }

    void ApplySavedColors()
    {
        // 헤어 색상 적용
        if (HairSprite != null)
            HairSprite.color = LoadColor("HairSprite");

        // 얼굴 색상 적용
        if (FaceSprite != null)
            FaceSprite.color = LoadColor("FaceSprite");

        // 스카프 색상 적용
        if (ScarfSprite != null)
            ScarfSprite.color = LoadColor("ScarfSprite");

        // 갑옷 색상 적용
        if (ArmorSprite != null)
            ArmorSprite.color = LoadColor("ArmorSprite");

        // 테두리 색상 적용
        if (FrameSprite != null)
            FrameSprite.color = LoadColor("FrameSprite");
    }

    // 특정 키(prefix)로 HSV 값을 불러와서 Color 반환
    private Color LoadColor(string prefix)
    {
        float h = PlayerPrefs.GetFloat(prefix + "H", 360f); // 기본값 180도 (파란색)
        float s = PlayerPrefs.GetFloat(prefix + "S", 1f); // 기본값 1 (채도 100%)
        float v = PlayerPrefs.GetFloat(prefix + "V", 1f); // 기본값 1 (명도 100%)

        return Color.HSVToRGB(h, s, v);
    }


    // 특정 키(prefix)로 HSV 값 저장
    public void SaveColor(string prefix, float h, float s, float v)
    {
        PlayerPrefs.SetFloat(prefix + "H", h);        
        PlayerPrefs.SetFloat(prefix + "S", s);
        PlayerPrefs.SetFloat(prefix + "V", v);
    }

    public void ChangeHealth(float damage)
    {
        CurrentHealth += damage;
    }
     
    public void ChangeSpeed(float speed)
    {
        CurrentVelocity = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            //collision.GetComponent<ItemInstance>().Take(ResourceController resourceController);
            Destroy(collision.gameObject);
        }
    }
}
