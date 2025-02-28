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
        // ��� ���� ����
        if (HairSprite != null)
            HairSprite.color = LoadColor("HairSprite");

        // �� ���� ����
        if (FaceSprite != null)
            FaceSprite.color = LoadColor("FaceSprite");

        // ��ī�� ���� ����
        if (ScarfSprite != null)
            ScarfSprite.color = LoadColor("ScarfSprite");

        // ���� ���� ����
        if (ArmorSprite != null)
            ArmorSprite.color = LoadColor("ArmorSprite");

        // �׵θ� ���� ����
        if (FrameSprite != null)
            FrameSprite.color = LoadColor("FrameSprite");
    }

    // Ư�� Ű(prefix)�� HSV ���� �ҷ��ͼ� Color ��ȯ
    private Color LoadColor(string prefix)
    {
        float h = PlayerPrefs.GetFloat(prefix + "H", 360f); // �⺻�� 180�� (�Ķ���)
        float s = PlayerPrefs.GetFloat(prefix + "S", 1f); // �⺻�� 1 (ä�� 100%)
        float v = PlayerPrefs.GetFloat(prefix + "V", 1f); // �⺻�� 1 (�� 100%)

        return Color.HSVToRGB(h, s, v);
    }


    // Ư�� Ű(prefix)�� HSV �� ����
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
