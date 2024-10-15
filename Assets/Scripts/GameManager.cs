using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public List<UpgradeGrydkaCfg> upgradeGrydkaCfgs;
    public static GameManager instance; 
    public List<Plant> allPlants;
    public List<Grydka> allGrydka;
    public List<Texture> spriteTexturesPlant;
    public TextMeshProUGUI textCoin;
    public SubscriptionField<int> coin;
    public GameObject PoPUpUpgrade;
    public Transform ParentGrydka;

   
    // public List<Customer> customers;

    void Start()
    {
        coin = new SubscriptionField<int>();
        coin.Value = 0;
        coin.Subscribe(ChangeCoins);
        if (instance == null) { 
            instance = this; 
        } else if(instance == this)
        { 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        CustomersSpawn();
        // InitializeManager();
    }

    private void CustomersSpawn()
    {
        // customers[0].gameObject.SetActive(true);
    }

    private void ChangeCoins(int newValue)
    {
        textCoin.text = newValue.ToString();
    }
    void Update()
    {
        
    }
}
