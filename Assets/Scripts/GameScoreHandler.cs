using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameScoreHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreCountText;
    private int _oldCoins = 0;

    private void Start()
    {
        // GameManager.instance.coin.Subscribe(ChangeValueScore);
        // _scoreCountText.text = GameManager.instance.coin.Value.ToString();
        // _oldCoins = GameManager.instance.coin.Value;
        _oldCoins = 10;
    }

    private void OnDisable()
    {
        GameManager.instance.coin.UnSubscribe(ChangeValueScore);
    }

    private void ChangeValueScore(int value)
    {
        var newinc = value - _oldCoins;
        StartCoroutine(CoroutineValueScore(newinc));
        _scoreCountText.transform.DOScale(1.5f, 0.2f);
    }

    IEnumerator CoroutineValueScore(int newinc)
    {
        for (int i = 0; i < newinc; i++)
        {
            _scoreCountText.text = (_oldCoins + 1).ToString();
            _oldCoins++;
            yield return null;
        }

        _scoreCountText.transform.DOScale(1.0f, 0.2f);
    }
}