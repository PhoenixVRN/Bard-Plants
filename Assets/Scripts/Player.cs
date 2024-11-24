using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnCollisionStay2D(Collision2D other)
    {
        if (_harvesting || _musicOn) return;
        Debug.Log($"Plaer OnCollisionStay {other.gameObject.name}");
        if (other.gameObject.GetComponent<Grydka>())
        {
            if (other.gameObject.GetComponent<Grydka>().ripe && _dynamicJoystick.Vertical == 0 &&
                _dynamicJoystick.Horizontal == 0)
            {
                _harvesting = true;
                StartCoroutine(SowHarvesting(other.gameObject.GetComponent<Grydka>()));
            }

            if (other.gameObject.GetComponent<Grydka>().needMusic && _dynamicJoystick.Vertical == 0 &&
                _dynamicJoystick.Horizontal == 0)
            {
                _musicOn = true;
                StartCoroutine(SowPlayMusic(other.gameObject.GetComponent<Grydka>()));
            }
        }
    }

    IEnumerator SowHarvesting(Grydka grydka)
    {
        arbaiten.SetActive(true);
        yield return new WaitForSeconds(3f);
        grydka.Harvesting();
        _harvesting = false;
        arbaiten.SetActive(false);
    }

    IEnumerator SowPlayMusic(Grydka grydka)
    {
        arbaiten.SetActive(true);
        yield return new WaitForSeconds(2f);
        grydka.PlayMusic();
        _musicOn = false;
        arbaiten.SetActive(false);
    }

    // public void OnCollisionEnter2D(Collision2D collision)
    // {
    //     Debug.Log($"Plaer OnCollisionEnter2D {collision.gameObject.name}");
    // }
    void FixedUpdate()
    {
        if (_harvesting || _musicOn) return;

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

        _rb.linearVelocity = new Vector2(_dynamicJoystick.Horizontal * _moveSpeed,
            _dynamicJoystick.Vertical * _moveSpeed);
    }
}