using System;
using UnityEngine;

public class ChangeMouseOnHover : MonoBehaviour, IChangeMouseSource
{
    public int Priority => 0;
    
    public CursorKey Hover;

    private void OnMouseEnter()
    {
        CursorUtility.SetCursor(this, Hover);
    }

    private void OnMouseExit()
    {
        CursorUtility.ClearCursor(this);
    }

    private void OnDisable()
    {
        CursorUtility.ClearCursor(this);
    }
}