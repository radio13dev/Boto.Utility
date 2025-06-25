using System.Collections;
using System.Collections.Generic;
using System.Text;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public interface IChangeMouseSource
{
    public int Priority { get; }
}

public class CursorUtility : MonoBehaviour
{
    private static CursorUtility instance;
    private static LinkedList<IChangeMouseSource> _sourceStack = new();
    private static LinkedList<CursorKey> _keyStack = new();
    
    public SerializedDictionary<CursorKey, Sprite> cursors = new();
    static CursorKey currentCursor = CursorKey.Default;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SetCursor(CursorKey.Default);
    }
    
    public static void SetCursor(CursorKey cursor)
    {
        if (cursor == currentCursor)
            return;
        if (cursor != CursorKey.Default && instance.cursors.TryGetValue(cursor, out var cursorTexture))
        {
            //Pivot is done on the bottom-left but hotspot needs to be from the top-left.
            var pivot = cursorTexture.pivot;
            var hotspot = new Vector2(pivot.x, cursorTexture.texture.height - pivot.y);
            currentCursor = cursor;
            Cursor.SetCursor(cursorTexture.texture, hotspot, CursorMode.Auto);
        }
        else
        {
            currentCursor = CursorKey.Default;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
    public static void SetCursor(IChangeMouseSource source, CursorKey cursor)
    {
        _sourceStack.AddLast(source);
        _keyStack.AddLast(cursor);
        RefreshCursor();
    }

    public static void ClearCursor(IChangeMouseSource source)
    {
        var it = _sourceStack.Last;
        var it2 = _keyStack.Last;
        while (it != null && it2 != null)
        {
            if (it.Value == source)
            {
                it.List.Remove(it);
                it2.List.Remove(it2);
                break;
            }
            it = it.Previous;
            it2 = it2.Previous;
        }
        RefreshCursor();
    }
    
    static void RefreshCursor()
    {
        var it = _sourceStack.First;
        var it2 = _keyStack.First;
        var bestIt = it;
        var bestIt2 = it2;
        while (it != null && it2 != null)
        {
            if (it.Value.Priority > bestIt.Value.Priority)
            {
                bestIt = it;
                bestIt2 = it2;
                break;
            }
            it = it.Next;
            it2 = it2.Next;
        }
        
        SetCursor(bestIt2 != null ? bestIt2.Value : CursorKey.Default);
    }

#if UNITY_EDITOR
    [EditorButton]
    public void CheckCursorTextures()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var cursorKvp in cursors)
        {
            sb.AppendLine(CheckCursorTexture(cursorKvp.Key, cursorKvp.Value));
        }

        Debug.LogError(sb.ToString());
    }


    public string CheckCursorTexture(CursorKey key, Sprite CursorTexture)
    {
        if (CursorTexture == null)
            return $"<color=red>INVALID</color> {key}:Cursor Texture not assigned.";


        if (CursorTexture.texture == null)
            return $"<color=red>INVALID</color> {key}: Cursor Texture not assigned.";


        var tex = CursorTexture.texture;


        if (!tex.isReadable)
            return $"<color=red>INVALID</color> {key}: Cursor Texture must be read/write enabled.";


        if (tex.mipmapCount != 1)
            return $"<color=red>INVALID</color> {key}: Cursor Texture must have mip maps off.";


        if (tex.format != TextureFormat.RGBA32)
            return $"<color=red>INVALID</color> {key}: Cursor Texture format must be RGBA32.";


        if (!tex.alphaIsTransparency)
            return $"<color=red>INVALID</color> {key}: Cursor Texture must have \"Alpha is transparency\" set to true.";


        if (tex.width != tex.height)
            return $"<color=red>INVALID</color> {key}: I think the texture should be square.";

        if (tex.width != 32 || tex.height != 32)
            return $"<color=red>INVALID</color> {key}: Cursor Texture should be 32x32 or it will be giant on Macs. Is currently {tex.width}x{tex.height}. Change Max Size in import settings.";
        
        return $"<color=#00ff2fff>Valid</color> {key}: If the cursor position is wrong, try changing the sprite's pivot.";
    }

#endif
}