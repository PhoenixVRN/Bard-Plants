using System.Collections;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

public class GardenGnome : MonoBehaviour
{
    public Vector2 _target;
    public Transform idlePoint;
    public bool MoveToGrydka;
    public bool WePlant;
    public SkeletonAnimation LeftGnomeAnimation;
    public AnimationState spineAnimationState;
    public float speedMove;
    public float jobTime;
    public float RecreationTime;
    private GameModel _gameModel;

    public float SpeedCharacteristicsIndex;
    public float JobCharacteristicsIndex;
    public float RecreationCharacteristicsIndex;

    private float finalSpeedValue;
    private float finaljobTime;
    private float finalRecreationTime;

    void Start()
    {
        spineAnimationState = LeftGnomeAnimation.AnimationState;
        spineAnimationState.SetAnimation(0, "Idle", true);
        _gameModel = Reference.GameModel;
        _gameModel.AnimationGardenGnome.Subscribe(Animation);
    }
  
    
    void Update()
    {
        if (WePlant) return;
        finalSpeedValue = speedMove * (1 +_gameModel.GardenGnomeLevel.Value.lvlSpeed * SpeedCharacteristicsIndex);
        // Debug.Log($"finalSpeedValue {finalSpeedValue}");
        if (GameManager.instance.currentGrydka.Count >= Reference.GameModel.MaxNumberPlants.Value)
        {
            if (Vector2.Distance(transform.position, idlePoint.position) < 0.1)
            {
                _gameModel.AnimationGardenGnome.Value = eTypeAnimation.Idle;
            }
            else
            {
                DirectAnim(idlePoint.position);
                transform.position =
                    Vector3.MoveTowards(transform.position, idlePoint.position,  finalSpeedValue * Time.deltaTime);
            }

            return;
        }

        if (!MoveToGrydka)
        {
            _target = GameManager.instance.SpawnPositionPlant();
            MoveToGrydka = true;
        }

        if (MoveToGrydka)
        {
            if (Vector2.Distance(transform.position, _target) < 0.1)
            {
                WePlant = true;
                StartCoroutine(WePlantPlant());
            }
            else
            {
                MoveToTarget();
            }
        }
    }

    IEnumerator WePlantPlant()
    {
        _gameModel.AnimationGardenGnome.Value = eTypeAnimation.ActionCicle;
        finaljobTime = jobTime/(1 + JobCharacteristicsIndex * _gameModel.GardenGnomeLevel.Value.lvlActions);
        // Debug.Log($"finaljobTime {finaljobTime}");
        yield return new WaitForSeconds(finaljobTime);
        GameManager.instance.PlantAplant(_target);
        //TODO реализовать визуал отдыха
        _gameModel.AnimationGardenGnome.Value = eTypeAnimation.Idle;
        finalRecreationTime = RecreationTime/(1 + RecreationCharacteristicsIndex * _gameModel.GardenGnomeLevel.Value.lvlStartAction);
        yield return new WaitForSeconds(finalRecreationTime);
        WePlant = false;
        MoveToGrydka = false;
    }

    public void MoveToTarget()
    {
        if (_gameModel.AnimationGardenGnome.Value != eTypeAnimation.Walk)
        {
            _gameModel.AnimationGardenGnome.Value = eTypeAnimation.Walk;
        }

        DirectAnim(_target);
        transform.position = Vector3.MoveTowards(transform.position, _target, finalSpeedValue * Time.deltaTime);
    }

    private void Animation(eTypeAnimation typeAnimation)
    {
        // Debug.Log($"Anim {typeAnimation.ToString()}");
        switch (typeAnimation)
        {
            case eTypeAnimation.Idle:
                spineAnimationState.SetAnimation(0, "Idle", true);
                break;

            case eTypeAnimation.Walk:
                // Debug.Log($"Walk Anim");
                spineAnimationState.SetAnimation(0, "Walk", true);
                break;

            case eTypeAnimation.ActionCicle:
                spineAnimationState.SetAnimation(0, "Action_cycle", true);
                break;

            case eTypeAnimation.ActionEnd:
                spineAnimationState.SetAnimation(0, "Action_end", true);
                break;

            case eTypeAnimation.ActionStart:
                spineAnimationState.SetAnimation(0, "Action_start", true);
                break;
        }
    }

    private void DirectAnim(Vector3 target)
    {
        if (target.x > transform.position.x)
        {
            LeftGnomeAnimation.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            LeftGnomeAnimation.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}