using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseCharacterController
{
    private Camera camera;

    protected void Start()
    {
        camera = Camera.main;
        movementDirection = Vector2.right * resourceController.MaxInitialVelocity;
    }

    protected override void Update()
    {
        base.Update();
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
    }

    protected override void HandleAction()
    {
        resourceController.ChangeSpeed();
        movementDirection = Vector2.right;

        //Vector2 mousePosition = Input.mousePosition;
        //Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        //lookDirection = (worldPos - (Vector2)transform.position);

        //if (lookDirection.magnitude < .9f)
        //{
        //    lookDirection = Vector2.zero;
        //}
        //else
        //{
        //    lookDirection = lookDirection.normalized;
        //}
    }
}
