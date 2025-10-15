using Unity.EditorCoroutines.Editor;
using UnityEditor;

namespace Editor.Scripts
{
    [InitializeOnLoad]
    public static class ExclusiveCoroutineEditor
    {
        static ExclusiveCoroutineEditor()
        {
            ExclusiveCoroutine.EditorCoroutineInjected += (c, h) =>
            {
                EditorCoroutineUtility.StartCoroutine(c, h);
                return true;
            };
        }
    }
}