using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : Singleton<MouseFollow>
{
    public void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.right = direction;
    }
}
