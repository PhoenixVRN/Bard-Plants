using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Sprite> _spritesPlayer;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private DynamicJoystick _dynamicJoystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rb;


    void FixedUpdate()
    {
        // if (_dynamicJoystick.Vertical != 0 || _dynamicJoystick.Horizontal != 0)
        // {
            if (_dynamicJoystick.Vertical > 0)
            {
                _spriteRenderer.sprite = _spritesPlayer[0];
            }
            else
            {
                if (_dynamicJoystick.Horizontal > 0)
                {
                    _spriteRenderer.sprite = _spritesPlayer[1];
                }
                else
                {
                    _spriteRenderer.sprite = _spritesPlayer[2];
                }
            }
        // }

        _rb.linearVelocity = new Vector2(_dynamicJoystick.Horizontal * _moveSpeed, _dynamicJoystick.Vertical * _moveSpeed);
    }
}