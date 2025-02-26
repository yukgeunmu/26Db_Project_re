using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PalettePicker: MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public RawImage paletteImage;  // UI �ȷ�Ʈ (RawImage)
    public SpriteRenderer targetSprite; // ������ ����� ��������Ʈ
    private Texture2D paletteTexture; // �������� ������ �ȷ�Ʈ �ؽ�ó

    public SpriteRenderer hair;
    public SpriteRenderer armor;
    public SpriteRenderer face;
    public SpriteRenderer frame;
    public SpriteRenderer scarf;

    public void SetSpriteHair() => targetSprite = hair;
    public void SetSpriteArmor() => targetSprite = armor;
    public void SetSpriteFace() => targetSprite = face;
    public void SetSpriteFrame() => targetSprite = frame;
    public void SetSpriteScarf() => targetSprite = scarf;

    private float baseHue = 0f; // �⺻ ���� (���� = 0, ��� = 60, �Ķ� = 240 ��)

    private void Start()
    {
        GeneratePalette(baseHue); // �ʱ� �ȷ�Ʈ ����
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColor(eventData);
    }

    private void UpdateColor(PointerEventData eventData)
    {
        RectTransform rectTransform = paletteImage.rectTransform;

        // ���콺 Ŭ�� ��ġ�� UI �ȷ�Ʈ ���� ��ǥ�� ��ȯ
        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
            return;

        // UV ��ǥ ��ȯ (0~1 ����)
        float uvX = Mathf.InverseLerp(-rectTransform.rect.width / 2, rectTransform.rect.width / 2, localPoint.x);
        float uvY = Mathf.InverseLerp(-rectTransform.rect.height / 2, rectTransform.rect.height / 2, localPoint.y);

        // �ȼ� ��ǥ ��ȯ
        int x = Mathf.RoundToInt(uvX * (paletteTexture.width - 1));
        int y = Mathf.RoundToInt(uvY * (paletteTexture.height - 1));

        // ������ ���� ��������
        Color selectedColor = paletteTexture.GetPixel(x, y);

        // ��������Ʈ ���� ����
        targetSprite.color = selectedColor;
    }

    /// <summary>
    /// Ư�� �迭�� ������ ������� �ȷ�Ʈ�� ����
    /// </summary>
    /// <param name="hue">���� (0=����, 60=���, 120=�ʷ� ��)</param>
    public void GeneratePalette(float hue)
    {
        baseHue = hue;
        int size = 256; // �ȷ�Ʈ ũ�� (256x256)
        paletteTexture = new Texture2D(size, size);
        paletteTexture.wrapMode = TextureWrapMode.Clamp;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float saturation = (float)x / (size - 1); // �¿�� ä�� ��ȭ
                float brightness = (float)y / (size - 1); // ���Ϸ� ��� ��ȭ
                Color color = Color.HSVToRGB(hue, saturation, brightness);
                paletteTexture.SetPixel(x, y, color);
            }
        }

        paletteTexture.Apply ();
        paletteImage.texture = paletteTexture;
    }

    /// <summary>
    /// ��ư�� ������ �ȷ�Ʈ ������ �����ϴ� �Լ�
    /// </summary>
    public void SetPaletteToRed(float hue) => GeneratePalette(hue);
}
