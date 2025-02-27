using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlideKeyHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerController playerController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.SetSlideKeyDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerController.SetSlideKeyUp();
    }
}
