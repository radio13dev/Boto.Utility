using System;
using UnityEngine;

[ExecuteAlways]
[DisallowMultipleComponent]
public class TransformChangeListener : MonoBehaviour
{
    public event Action OnTransformChanged;

    private void Update()
    {
        if (transform.hasChanged)
        {
            OnTransformChanged?.Invoke();
            var interfaces = GetComponents<Interface>();
            for (int i = 0; i < interfaces.Length; i++) interfaces[i].OnTransformChanged();
            transform.hasChanged = false;
        }
    }
        
    public interface Interface
    {
        void OnTransformChanged();
    }
}