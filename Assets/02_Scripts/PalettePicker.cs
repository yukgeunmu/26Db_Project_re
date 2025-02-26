using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PalettePicker: MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public RawImage paletteImage;  // UI 팔레트 (RawImage)
    public SpriteRenderer targetSprite; // 색상이 변경될 스프라이트
    private Texture2D paletteTexture; // 동적으로 생성할 팔레트 텍스처

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

    private float baseHue = 0f; // 기본 색상 (빨강 = 0, 노랑 = 60, 파랑 = 240 등)

    private void Start()
    {
        GeneratePalette(baseHue); // 초기 팔레트 생성
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

        // 마우스 클릭 위치를 UI 팔레트 내부 좌표로 변환
        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
            return;

        // UV 좌표 변환 (0~1 범위)
        float uvX = Mathf.InverseLerp(-rectTransform.rect.width / 2, rectTransform.rect.width / 2, localPoint.x);
        float uvY = Mathf.InverseLerp(-rectTransform.rect.height / 2, rectTransform.rect.height / 2, localPoint.y);

        // 픽셀 좌표 변환
        int x = Mathf.RoundToInt(uvX * (paletteTexture.width - 1));
        int y = Mathf.RoundToInt(uvY * (paletteTexture.height - 1));

        // 선택한 색상 가져오기
        Color selectedColor = paletteTexture.GetPixel(x, y);

        // 스프라이트 색상 변경
        targetSprite.color = selectedColor;
    }

    /// <summary>
    /// 특정 계열의 색상을 기반으로 팔레트를 생성
    /// </summary>
    /// <param name="hue">색상 (0=빨강, 60=노랑, 120=초록 등)</param>
    public void GeneratePalette(float hue)
    {
        baseHue = hue;
        int size = 256; // 팔레트 크기 (256x256)
        paletteTexture = new Texture2D(size, size);
        paletteTexture.wrapMode = TextureWrapMode.Clamp;

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float saturation = (float)x / (size - 1); // 좌우로 채도 변화
                float brightness = (float)y / (size - 1); // 상하로 밝기 변화
                Color color = Color.HSVToRGB(hue, saturation, brightness);
                paletteTexture.SetPixel(x, y, color);
            }
        }

        paletteTexture.Apply ();
        paletteImage.texture = paletteTexture;
    }

    /// <summary>
    /// 버튼을 눌러서 팔레트 색상을 변경하는 함수
    /// </summary>
    public void SetPaletteToRed(float hue) => GeneratePalette(hue);
}
