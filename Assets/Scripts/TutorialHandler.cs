using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    // public TextMeshProUGUI TutorialText;
    public LocalizeStringEvent tutorialText;
    public GameObject tutorialTextPanel;
    public GameObject BublTutor;
    public GameObject OwlAnimation;
    public GameObject GnomeAnimation;
    public GameObject Move;
    public GameObject Hand;
    public Transform UpAssistance;
    public Transform UpAssistanceBuy;
    public Transform UpAssistanceCloseButton;
    public Transform UpAssistanceRightButton;
    

    private GameModel _gameModel;

    void Start()
    {
        _gameModel = Reference.GameModel;
        _gameModel.StageTutorial.Subscribe(SwitchTutorial);
    }

    private void SwitchTutorial(int stage)
    {
        Debug.Log($"SwitchTutorial {stage}");
        switch (stage)
        {
            case 1:
                BublTutor.SetActive(true);
                OwlAnimation.SetActive(true);
                SetNewLocalizationKey("tutor_1");
                EventTrigger eventTrigger = BublTutor.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry =
                    eventTrigger.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry != null)
                {
                    clickEntry.callback.AddListener((data) =>
                    {
                        BublTutor.SetActive(false);
                        OwlAnimation.SetActive(false);
                        clickEntry.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 2;
                    });
                }

                break;

            case 2:
                Move.SetActive(true);
                EventTrigger eventTriggerMove = Move.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntryMove =
                    eventTriggerMove.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntryMove != null)
                {
                    clickEntryMove.callback.AddListener((data) =>
                    {
                        Move.SetActive(false);
                        clickEntryMove.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 3;
                    });
                }

                break;

            case 3:
                if (Reference.GameModel.IsTutorOrderСompleted.Value)
                {
                    TutorOrderCompleted(true);
                }
                else
                {
                    Reference.GameModel.IsTutorOrderСompleted.Subscribe(TutorOrderCompleted);
                }
                break;
            
            case 4:
                BublTutor.SetActive(true);
                OwlAnimation.SetActive(true);
                SetNewLocalizationKey("tutor_2");
                EventTrigger eventTrigger2 = BublTutor.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry2 =
                    eventTrigger2.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry2 != null)
                {
                    clickEntry2.callback.AddListener((data) =>
                    {
                        BublTutor.SetActive(false);
                        OwlAnimation.SetActive(false);
                        clickEntry2.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 5;
                    });
                }
                break;
            
            case 5:
                Hand.SetActive(true);
                Reference.GameModel.NumberClosedOrders.Subscribe(NumberClosedOrdersSubscrib);
                // UpAssistance.GetComponent<Button>().onClick.AddListener(CallBacUpAssistance);
                break;
            
            case 6:
                BublTutor.SetActive(true);
                OwlAnimation.SetActive(true);
                SetNewLocalizationKey("tutor_3");
                EventTrigger eventTrigger3 = BublTutor.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry3 =
                    eventTrigger3.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry3 != null)
                {
                    clickEntry3.callback.AddListener((data) =>
                    {
                        BublTutor.SetActive(false);
                        OwlAnimation.SetActive(false);
                        clickEntry3.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 7;
                    });
                }
                break;
            
            case 7:
                BublTutor.SetActive(true);
                OwlAnimation.SetActive(true);
                SetNewLocalizationKey("tutor_4");
                EventTrigger eventTrigger4 = BublTutor.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry4 =
                    eventTrigger4.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry4 != null)
                {
                    clickEntry4.callback.AddListener((data) =>
                    {
                        // Debug.Log($"tutor_4 - 6 to 7");
                        BublTutor.SetActive(false);
                        OwlAnimation.SetActive(false);
                        clickEntry4.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 8;
                    });
                }
                break;
            
            case 8:
                BublTutor.SetActive(true);
                OwlAnimation.SetActive(true);
                SetNewLocalizationKey("tutor_5");
                EventTrigger eventTrigger5 = BublTutor.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry5 =
                    eventTrigger5.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry5 != null)
                {
                    clickEntry5.callback.AddListener((data) =>
                    {
                        BublTutor.SetActive(false);
                        OwlAnimation.SetActive(false);
                        clickEntry5.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 9;
                    });
                }
                break;
            
            case 9:
                Hand.SetActive(true);
                Hand.gameObject.transform.position = UpAssistance.position;
                UpAssistance.GetComponent<Button>().onClick.AddListener(CallBacUpAssistance);
                break;
            
            case 10:
                Hand.SetActive(true);
                Hand.gameObject.transform.position = UpAssistanceBuy.position;
                UpAssistanceBuy.GetComponent<Button>().onClick.AddListener(CallBacUpAssistanceBuy);
                break;
            
            case 11:
                BublTutor.SetActive(true);
                GnomeAnimation.SetActive(true);
                SetNewLocalizationKey("tutor_6");
                EventTrigger eventTrigger10 = BublTutor.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry10 =
                    eventTrigger10.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry10 != null)
                {
                    clickEntry10.callback.AddListener((data) =>
                    {
                        BublTutor.SetActive(false);
                        GnomeAnimation.SetActive(false);
                        clickEntry10.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 12;
                    });
                }
                break;
            
            case 12:
                BublTutor.SetActive(true);
                GnomeAnimation.SetActive(true);
                SetNewLocalizationKey("tutor_7");
                EventTrigger eventTrigger11 = BublTutor.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry11 =
                    eventTrigger11.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry11 != null)
                {
                    clickEntry11.callback.AddListener((data) =>
                    {
                        BublTutor.SetActive(false);
                        GnomeAnimation.SetActive(false);
                        clickEntry11.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 13;
                    });
                }
                break;
            
            case 13:
                Hand.SetActive(true);
                Hand.gameObject.transform.position = UpAssistanceRightButton.position;
                EventTrigger eventTrigger12 = UpAssistanceRightButton.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry12 =
                    eventTrigger12.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                EventTrigger eventTrigger120 = UpAssistanceCloseButton.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry120 =
                    eventTrigger120.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry12 != null)
                {
                    clickEntry12.callback.AddListener((data) =>
                    {
                        Hand.SetActive(false);
                        clickEntry12.callback.RemoveAllListeners();
                        clickEntry120.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 14;
                    });
                }
                if (clickEntry120 != null)
                {
                    clickEntry120.callback.AddListener((data) =>
                    {
                        Hand.SetActive(false);
                        clickEntry12.callback.RemoveAllListeners();
                        clickEntry120.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 14;
                    });
                }
                break;
            
            case 14:
                Hand.SetActive(true);
                Hand.gameObject.transform.position = UpAssistanceCloseButton.position;
                EventTrigger eventTrigger13 = UpAssistanceCloseButton.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry13 =
                    eventTrigger13.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry13 != null)
                {
                    clickEntry13.callback.AddListener((data) =>
                    {
                        BublTutor.SetActive(false);
                        Hand.SetActive(false);
                        clickEntry13.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 15;
                    });
                }
                break;
            
            case 15:
                BublTutor.SetActive(true);
                OwlAnimation.SetActive(true);
                SetNewLocalizationKey("tutor_8");
                EventTrigger eventTrigger14 = BublTutor.gameObject.GetComponent<EventTrigger>();
                EventTrigger.Entry clickEntry14 =
                    eventTrigger14.triggers.Find(e => e.eventID == EventTriggerType.PointerClick);
                if (clickEntry14 != null)
                {
                    clickEntry14.callback.AddListener((data) =>
                    {
                        BublTutor.SetActive(false);
                        OwlAnimation.SetActive(false);
                        clickEntry14.callback.RemoveAllListeners();
                        _gameModel.StageTutorial.Value = 16;
                    });
                }
                break;
        }
    }

    private void CallBacUpAssistanceBuy()
    {
        
        Hand.SetActive(false);
        UpAssistance.GetComponent<Button>().onClick.RemoveListener(CallBacUpAssistanceBuy);
        _gameModel.StageTutorial.Value = 11;
    }
    private void CallBacUpAssistance()
    {
        Hand.SetActive(false);
        UpAssistance.GetComponent<Button>().onClick.RemoveListener(CallBacUpAssistance);
        _gameModel.StageTutorial.Value = 10;
    }
    private void TutorOrderCompleted(bool orderCompleted)
    {
        Reference.GameModel.IsTutorOrderСompleted.UnSubscribe(TutorOrderCompleted);
        _gameModel.StageTutorial.Value = 4;
        // Hand.SetActive(true);
        // Reference.GameModel.NumberClosedOrders.Subscribe(NumberClosedOrdersSubscrib);
    }

    private void NumberClosedOrdersSubscrib(int value)
    {
        // Debug.Log($"NumberClosedOrdersSubscrib {value}");
        Reference.GameModel.NumberClosedOrders.UnSubscribe(NumberClosedOrdersSubscrib);
        Hand.SetActive(false);
        _gameModel.StageTutorial.Value = 6;
    }

    private void ShowTutorial(GameObject tutorialObject, string tutorialKeyText)
    {
        tutorialObject.SetActive(true);
        SetNewLocalizationKey(tutorialKeyText);
    }

    private void SetNewLocalizationKey(string newKey)
    {
        tutorialText.StringReference.TableEntryReference = newKey;
        tutorialText.RefreshString();
    }
}