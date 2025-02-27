using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HueController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public RawImage paletteImage;
    private Texture2D paletteTexture;
    [SerializeField] private PaletteController _palettePicker;

    public void Start()
    {
        GeneratePalette();
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
        float h, s, v;
        Color.RGBToHSV(selectedColor, out h, out s, out v);

        _palettePicker.SetPaletteToRed(h);
    }

    /// <summary>
    /// 특정 계열의 색상을 기반으로 팔레트를 생성
    /// </summary>
    /// <param name="hue">색상 (0=빨강, 60=노랑, 120=초록 등)</param>
    public void GeneratePalette()
    {
        int size = 256; // 팔레트 크기 (256x256)
        paletteTexture = new Texture2D(size, 1);
        paletteTexture.wrapMode = TextureWrapMode.Clamp;

        for (int hue = 0; hue < size; hue++)
        {
           Color color = Color.HSVToRGB(hue / 360f, 1f, 1f);
           paletteTexture.SetPixel(hue, 1, color);
        }

        paletteTexture.Apply();
        paletteImage.texture = paletteTexture;
    }
}