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
        float h, s, v;
        Color.RGBToHSV(selectedColor, out h, out s, out v);

        _palettePicker.SetPaletteToRed(h);
    }

    /// <summary>
    /// Ư�� �迭�� ������ ������� �ȷ�Ʈ�� ����
    /// </summary>
    /// <param name="hue">���� (0=����, 60=���, 120=�ʷ� ��)</param>
    public void GeneratePalette()
    {
        int size = 256; // �ȷ�Ʈ ũ�� (256x256)
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