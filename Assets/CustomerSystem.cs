using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomerSystem : MonoBehaviour
{
    public static CustomerSystem instance;

    public List<OrderCfg> orderCfgs;
    public List<Customer> allWoodenCustomerType;
    public List<Customer> allGoldenCustomerType;
    // private int turn = 0;
    public float delayForSpawn = 15;
    private float lastTime;
    private int _quantityCustomersInLevel;
    private int _qq;
  
    
    // private int _currentCustomersInLevel;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (var customer in allWoodenCustomerType)
        {
            customer.IsUsed = false;
        }

        foreach (var customer in  allGoldenCustomerType)
        {
            customer.IsUsed = false;
        }
        
        lastTime = Time.time;
        // _quantityCustomersInLevel = Random.Range(1, 5); // количество покупателей для урвня
        _quantityCustomersInLevel = RandomQuantity();
        _qq = _quantityCustomersInLevel;// количество покупателей для урвня
        Reference.GameModel.CloseCustomersInLevel.Subscribe(CheclLevelUp);
    }


    void Update()
    {
        if (lastTime + delayForSpawn < Time.time && Reference.GameModel.CountCustomerInGame.Value < 3 &&
            _quantityCustomersInLevel > -1)
        {
            lastTime = Time.time;
            Reference.GameModel.CountCustomerInGame.Value++;
             // Debug.Log($"orderCfgs[turn] {turn}");
            InitCustomer();
        }
    }

    private int RandomQuantity()
    {
        return Random.Range(1, 4);
    }
    
    private void CheclLevelUp(int value)
    {
        // заполнение филда експой для уровня
        GameManager.instance.ShowAmoutExp(_qq,value);
        if (value > _qq)
        {
            Debug.Log($"LevelUp {value}/{_qq}");
            Reference.GameModel.CloseCustomersInLevel.Value = 0;
            Reference.GameModel.NumberCompletedOrders.Value++;
            _quantityCustomersInLevel = RandomQuantity();
            _qq = _quantityCustomersInLevel;
        }
    }
    

    public void InitCustomer()
    {
        // Debug.Log($"quantityCustomersInLevel {_quantityCustomersInLevel}");
        if (_quantityCustomersInLevel == 0)
        {
            //TODO спавним золотого и инитем его ордерами, не спавним больше покупателей пока не закроем всех созданных и не поднимем уровень
            Debug.Log($"Spawn Gold!");
            var goldenCustomer = allGoldenCustomerType[Random.Range(0, allGoldenCustomerType.Count)];
            goldenCustomer.IsUsed = true;
            var goldcustomer = Instantiate(goldenCustomer, transform);
            goldcustomer.orders.GoldOrdersInit();
            _quantityCustomersInLevel--;
            return;
        }

        var quantityOpenPlants = GameManager.instance.openPlants.Count;
        var quantityOrders = quantityOpenPlants < 4 ? Random.Range(1, quantityOpenPlants + 1) : Random.Range(1, 4);
        var nonUsedCustomer = allWoodenCustomerType.FindAll(c => c.IsUsed == false);
        var randomCustomer = nonUsedCustomer[Random.Range(0, nonUsedCustomer.Count)];
        randomCustomer.IsUsed = true;
        var customer = Instantiate(randomCustomer, transform);
       
        customer.orders.InitOrders(quantityOrders);
        _quantityCustomersInLevel--;
        // var customer = Instantiate(allWoodenCustomerType[orderCfg.typeCustomer], transform).GetComponent<Customer>();
        // TODO сделать расчеты о количестве ревара в зависимости от типа количества растений и уровня 
        // customer.reward = orderCfg.reward;
        // Debug.Log($"InitCustomer {customer.orders}");
        // customer.orders.InitOrders(orderCfg);
    }
}