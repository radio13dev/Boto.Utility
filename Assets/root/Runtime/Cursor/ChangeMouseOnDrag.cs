using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeMouseOnDrag : MonoBehaviour, IChangeMouseSource
{
    public int Priority => 4;
    
    public CursorKey Drag;
    bool m_isPressed;

    private void OnMouseOver()
    {
        if (!m_isPressed && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CursorUtility.SetCursor(this, Drag);
            m_isPressed = true;
        }
    }

    private void Update()
    {
        if (m_isPressed && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            CursorUtility.ClearCursor(this);
            m_isPressed = false;
        }
    }
    
    private void OnDisable()
    {
        if (m_isPressed)
        {
            CursorUtility.ClearCursor(this);
            m_isPressed = false;
        }
    }
}