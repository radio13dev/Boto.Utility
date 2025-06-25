using UnityEngine;
using UnityEngine.InputSystem;

public class PointerUtility : MonoBehaviour
{
    static PointerUtility Instance;
    
    static int DefaultRaycastLayers;
    static RaycastHit? m_lastHit;
    public static RaycastHit LastHit
    {
        get
        {
            if (!m_lastHit.HasValue)
            {
                if (Physics.Raycast(MouseRay, out var hit, layerMask: DefaultRaycastLayers, maxDistance: 100))
                {
                    m_lastHit = hit;
                }
                else
                {
                    m_lastHit = default(RaycastHit);
                }
            }
            return m_lastHit.Value;
        }
    }
    public static Vector2 MousePosition => Mouse.current.position.ReadValue();
    public static Ray MouseRay => Camera.main.ScreenPointToRay(MousePosition);

    private void Awake()
    {
        Instance = this;
        DefaultRaycastLayers = LayerMask.GetMask("Default") | LayerMask.GetMask("UI");
    }

    private void Update()
    {
        m_lastHit = null;
    }
}