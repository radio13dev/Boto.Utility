using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CoroutineHost : MonoBehaviour
{
    public static CoroutineHost Instance;

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public static class Methods
    {
        public static IEnumerator Lerp(Transform from, Transform to, float duration)
        {
            var startPosition = from.localPosition;
            var startRotation = from.localRotation;
            from.SetPositionAndRotation(to.position, to.rotation);
            var toPosition = from.localPosition;
            var toRotation = from.localRotation;
            from.SetLocalPositionAndRotation(startPosition, startRotation);

            float t = 0;
            while (t < duration && from && to)
            {
                t += Time.deltaTime;
                t = math.min(t, duration);

                float progress = t / duration;
                from.localPosition = Vector3.Lerp(startPosition, toPosition, progress);
                from.localRotation = Quaternion.Lerp(startRotation, toRotation, progress);
                yield return null;
            }
        }

        public static IEnumerator Lerp(Transform from, Vector3 to, float duration, bool local, bool smooth)
        {
            var startPosition = local ? from.localPosition : from.position;

            float t = 0;
            while (t < duration && from)
            {
                t += Time.deltaTime;
                t = math.min(t, duration);

                float progress = t / duration;
                if (smooth)
                    progress = EaseCubic(progress);

                if (local)
                    from.localPosition = Vector3.Lerp(startPosition, to, progress);
                else
                    from.position = Vector3.Lerp(startPosition, to, progress);
                yield return null;
            }
        }

        public static IEnumerator Lerp(Transform from, Quaternion to, float duration, bool local, bool smooth)
        {
            var startRotation = local ? from.localRotation : from.rotation;

            float t = 0;
            while (t < duration && from)
            {
                t += Time.deltaTime;
                t = math.min(t, duration);

                float progress = t / duration;
                if (smooth)
                    progress = EaseCubic(progress);

                if (local)
                    from.localRotation = Quaternion.Slerp(startRotation, to, progress);
                else
                    from.rotation = Quaternion.Lerp(startRotation, to, progress);
                yield return null;
            }
        }

        public static IEnumerator LerpSmooth(Transform from, Transform to, float duration)
        {
            var startPosition = from.localPosition;
            var startRotation = from.localRotation;
            var startScale = from.localScale;

            from.SetLocalPositionAndRotation(to.localPosition, to.localRotation);
            from.localScale = to.localScale;

            var toPosition = from.localPosition;
            var toRotation = from.localRotation;
            var toScale = from.localScale;

            from.localScale = startScale;
            from.SetLocalPositionAndRotation(startPosition, startRotation);

            float t = 0;
            while (t < duration && from && to)
            {
                t += Time.deltaTime;
                t = math.min(t, duration);

                // Smooth the progress so it eases in and out
                float progress = t / duration;
                progress = EaseCubic(progress);

                from.localPosition = Vector3.Lerp(startPosition, toPosition, progress);
                from.localRotation = Quaternion.Lerp(startRotation, toRotation, progress);
                from.localScale = Vector3.Lerp(startScale, toScale, progress);
                yield return null;
            }
        }

        public static float EaseCubic(float progress)
        {
            return progress < 0.5 ? 4 * progress * progress * progress : 1 - math.pow(-2 * progress + 2, 3) / 2;
        }

        public static IEnumerator Combine(IEnumerable<IEnumerator> cos)
        {
            var set = new List<IEnumerator>(cos);
            while (set.Count > 0)
            {
                yield return null;
                for (int i = 0; i < set.Count; i++)
                {
                    if (!set[i].MoveNext())
                    {
                        set.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    public static void FixOnValidateError(MonoBehaviour host, Action action)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && ExclusiveCoroutine.EditorCoroutineInjected?.Invoke(DelayedAction(() =>
            {
                if (!Application.isPlaying) action();
            }), host) == true)
        {
            return;
        }
#endif
    }

    private static IEnumerator DelayedAction(Action action)
    {
        yield return null;
        action();
    }
}

public struct ExclusiveCoroutine
{
    Coroutine co;

#if UNITY_EDITOR
    public static Func<IEnumerator, MonoBehaviour, bool> EditorCoroutineInjected;
#endif

    public void StartCoroutine(MonoBehaviour host, IEnumerator coroutine)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && EditorCoroutineInjected?.Invoke(coroutine, host) == true)
        {
            return;
        }
#endif

        if (co != null) host.StopCoroutine(co);
        co = host.StartCoroutine(coroutine);
    }
    
    public void StopCoroutine(MonoBehaviour host)
    {
        if (co != null) host.StopCoroutine(co);
    }
}