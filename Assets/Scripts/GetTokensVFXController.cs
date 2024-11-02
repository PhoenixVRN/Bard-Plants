using DG.Tweening;
using TMPro;
using UnityEngine;

public class GetTokensVFXController : MonoBehaviour
{
    // [SerializeField] private GameObject _tokenPrefab;
    // [SerializeField] private Transform _spawnPoint;
    // [SerializeField] private TextMeshProUGUI _text;
    
    public Transform _target;

    public void ShowGetTokensVFX(int countToken, Vector3 startPos, Vector3 endPos, GameObject tokenPrefab)
    {
        for (int i = 0; i < countToken; i++)
        {
          InstCoins(startPos, endPos, tokenPrefab);
        }
        // Invoke("AddCoin", 2);
        
    }

    private void InstCoins(Vector3 startPos, Vector3 endPos, GameObject tokenPrefab)
    {
        GameObject token = Instantiate(tokenPrefab,
            startPos + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0),
            Quaternion.identity);
        token.transform.DOMove(endPos, 1f).OnComplete(() => Hide(token));
    }
    private void Hide(GameObject token)
    {
        token.transform.DOScale(new Vector3(0, 0, 0), 0.3f).OnComplete(() => BoundeAndDestroy(token));
    }

    private void BoundeAndDestroy(GameObject token)
    {
        // GameManager.Instance.Coins += 10;
        Destroy(token);
    }

    private void AddCoin()
    {
        GameManager.instance.coin.Value += 100;
    }
}