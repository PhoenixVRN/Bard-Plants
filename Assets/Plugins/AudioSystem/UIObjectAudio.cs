using System;
using AudioSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WebGame.AudioSystem;

public class UIObjectAudio : MonoBehaviour
{
    [SerializeField] private ESound soundType;

    private Button _button;
    private EventTrigger _eventTrigger;
    private EventTrigger.Entry entry;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _eventTrigger = GetComponent<EventTrigger>();
    }

    private void OnEnable()
    {
        if (_button != null)
        {
            _button.onClick.AddListener(PlayObjectSound);
        }

        if (_eventTrigger != null)
        {
            entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown }; // Указываем тип события
            entry.callback.AddListener(OnPointerDown); // Добавляем слушатель
            _eventTrigger.triggers.Add(entry); // Добавляем в список событий
        }
    }

    // private void Start()
    // {
    //     if (_button != null)
    //     {
    //         _button.onClick.AddListener(PlayObjectSound);
    //     }
    //
    //     if (_eventTrigger != null)
    //     {
    //         entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown }; // Указываем тип события
    //         entry.callback.AddListener(OnPointerDown); // Добавляем слушатель
    //         _eventTrigger.triggers.Add(entry); // Добавляем в список событий
    //     }
    // }
    
    private void OnPointerDown(BaseEventData eventData) =>  AudioManagerView.Instance.PlaySound(soundType);
    public void PlayObjectSound() => AudioManagerView.Instance.PlaySound(soundType);
    
   
    private void OnDisable()
    {
        if (_eventTrigger != null)
        {
            entry.callback.RemoveListener(OnPointerDown);
        }

        if (_button != null)
        {
            _button.onClick.RemoveListener(PlayObjectSound);
        }
    }
}