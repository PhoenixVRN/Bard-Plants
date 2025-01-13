using System.Collections;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;

public class GardenGnome : MonoBehaviour
{
    public Vector2 _target;
    public Transform idlePoint;
    public bool MoveToGrydka;
    public bool WePlant;
    public SkeletonAnimation LeftGnomeAnimation;
    public Spine.AnimationState spineAnimationState;
    public float speedMove;
    private GameModel _gameModel;
    

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
                    Vector3.MoveTowards(transform.position, idlePoint.position, speedMove * Time.deltaTime);
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
        yield return new WaitForSeconds(3f);
       
        GameManager.instance.PlantAplant(_target);
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
        transform.position = Vector3.MoveTowards(transform.position, _target, speedMove * Time.deltaTime);
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