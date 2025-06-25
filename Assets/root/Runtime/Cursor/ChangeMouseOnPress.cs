using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeMouseOnPress : MonoBehaviour, IChangeMouseSource
{
    public int Priority => 5;
    
    public CursorKey Click;
    bool m_isPressed;

    private void OnMouseOver()
    {
        if (m_isPressed && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            CursorUtility.ClearCursor(this);
            m_isPressed = false;
        }
        if (!m_isPressed && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CursorUtility.SetCursor(this, Click);
            m_isPressed = true;
        }
    }

    private void OnMouseExit()
    {
        if (m_isPressed)
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