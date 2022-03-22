using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class clickController 
{
    public static Vector2 worldPoint(Vector2 x)
    {
        return Camera.main.ScreenToWorldPoint(x);
    }
    public static Collider2D clickedObject(Vector2 mousePos)
    {
        mousePos = worldPoint(mousePos);
        Collider2D obj = Physics2D.OverlapCircle(mousePos, .02f,LayerMask.GetMask("Interactables"));
        return obj;
    }
    public static bool canInteract(Vector2 mousePos, Vector2 playerPos)
    {
        mousePos = worldPoint(mousePos);
        if((mousePos-playerPos).sqrMagnitude < .66f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
