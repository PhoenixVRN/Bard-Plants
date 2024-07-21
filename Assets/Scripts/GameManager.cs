using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public List<Grydka> allGrydka;
    public List<Texture> sptiTexturesPlant;
    public TextMeshProUGUI textCoin;

    public GameObject PoPUpUpgrade;

    public int coin;
    // public List<Customer> customers;

    void Start()
    {
        coin = 0;
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
    void Update()
    {
        
    }
}
