using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using AnimationState = Spine.AnimationState;

public class Player : MonoBehaviour
{
    public GameObject arbaiten;

    [SerializeField] private List<Sprite> _spritesPlayer;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private DynamicJoystick _dynamicJoystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rb;
    private bool _harvesting;
    private bool _musicOn;
    private GameModel _gameModel;

    public SkeletonAnimation LeftPlayerAnimation;
    public AnimationState spineAnimationState;

    void Start()
    {
        spineAnimationState = LeftPlayerAnimation.AnimationState;
        spineAnimationState.SetAnimation(0, "Idle", true);
        _gameModel = Reference.GameModel;
        _gameModel.AnimationPlayer.Subscribe(Animation);
    }


    void FixedUpdate()
    {
        if (_dynamicJoystick.Horizontal > 0)
        {
            // _spriteRenderer.sprite = _spritesPlayer[1];
            Reference.GameModel.AnimationPlayer.Value = eTypeAnimation.Walk;
            LeftPlayerAnimation.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (_dynamicJoystick.Horizontal < 0)
        {
            Reference.GameModel.AnimationPlayer.Value = eTypeAnimation.Walk;
            LeftPlayerAnimation.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        if (_dynamicJoystick.Horizontal == 0)
        {
            Reference.GameModel.AnimationPlayer.Value = eTypeAnimation.Idle;
        }

        _rb.linearVelocity = new Vector2(_dynamicJoystick.Horizontal * _moveSpeed,
            _dynamicJoystick.Vertical * _moveSpeed);
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

            case eTypeAnimation.WalkPlaying:
                spineAnimationState.SetAnimation(0, "Walk_playing", true);
                break;
        }
    }
}