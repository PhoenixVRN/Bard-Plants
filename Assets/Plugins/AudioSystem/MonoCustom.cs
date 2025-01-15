using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class MonoCustom : MonoBehaviour
{
    public static MonoCustom Instance;
    public Dictionary<string, Coroutine> _repeatingBase = new Dictionary<string, Coroutine>();

    private void Awake()
    {
        if (Instance) Destroy(gameObject); else Instance = this;
    }
    public void StartCoroutineWithArg<T>(Action<T> currentAction, float timeToActionDo, T arg) => StartCoroutine(CustomInvoke(currentAction, timeToActionDo, arg));
    public void StartCoroutineWithOUTArg(Action currentAction, float timeToActionDo) => StartCoroutine(CustomInvoke(currentAction, timeToActionDo));
    /// <summary>
    /// Повторение каких-то методов
    /// </summary>
    /// <param name="currentAction">прокидываем, что будет повторяться</param>
    /// <param name="timeToActionDo">прокидываем время повторов</param>
    /// <param name="repeatingNameMethod">прокидываем название этого повторятора, чтобы потом его можно было остановить</param>
    public void StartRepeatingCoroutineWithOUTArg(Action currentAction, float timeToActionDo, string repeatingNameMethod) { if (!_repeatingBase.ContainsKey(repeatingNameMethod)) { _repeatingBase.Add(repeatingNameMethod, StartCoroutine(CustomInvokeRepeating(currentAction, timeToActionDo))); } }
    /// <summary>
    /// Остановить повторение метода
    /// </summary>
    /// <param name="repeatingNameMethod">Как его остановить, название повторятора</param>
    public void StopRepeatingCoroutineWithOUTArg(string repeatingNameMethod) { if (_repeatingBase.ContainsKey(repeatingNameMethod)) { StopCoroutine(_repeatingBase[repeatingNameMethod]); _repeatingBase.Remove(repeatingNameMethod); } }

    public Vector3 GetRange(Transform transformValue)
    {
        return transformValue.TransformPoint(new(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
    }

    private IEnumerator CustomInvokeRepeating(Action currentAction, float timeToActionDo) // customReInvoke with actions 
    {
        while (true)
        {
            currentAction?.Invoke();
            yield return MonoCustomHelpers.GetWait(timeToActionDo);
        }
    }

    private IEnumerator CustomInvoke(Action currentAction, float timeToActionDo) // customInvoke with actions 
    {
        yield return MonoCustomHelpers.GetWait(timeToActionDo);
        currentAction?.Invoke();
    }

    private IEnumerator CustomInvoke<T>(Action<T> currentAction, float timeToActionDo, T arg) // customInvoke with actions 
    {
        yield return MonoCustomHelpers.GetWait(timeToActionDo);
        currentAction?.Invoke(arg);
    }

}

public static class MonoCustomHelpers
{
    private static Camera _camera;
    public static Camera Camera
    {
        get
        {
            if (!_camera) _camera = Camera.main;
            return _camera;
        }
    }


    private static Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float value)
    {
        if (WaitDictionary.TryGetValue(value, out var wait)) return wait;
        WaitDictionary[value] = new WaitForSeconds(value);
        return WaitDictionary[value];
    }


    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;
    public static bool IsOverUI()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }
}