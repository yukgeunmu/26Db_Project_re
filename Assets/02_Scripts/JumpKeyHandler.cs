using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpKeyHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    PlayerController playerController;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.SetJumpKeyDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerController.SetJumpKeyUp();
    }
}
