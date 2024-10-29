using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSystem : MonoBehaviour
{
    public static CustomerSystem instance;

    public List<OrderCfg> orderCfgs;
    public List<Customer> allCustomerType;
    private int turn = 0;
    public float delayForSpawn = 15;
    private float lastTime;
    
    
    void Start()
    {
        if (instance == null) { 
            instance = this; 
        } else if(instance == this)
        { 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        lastTime = Time.time;
    }

   
    void Update()
    {
        if (lastTime + delayForSpawn < Time.time && Reference.GameModel.CountCustomerInGame.Value < 3 && orderCfgs.Count > turn)
        {
            lastTime = Time.time;
            Reference.GameModel.CountCustomerInGame.Value++;
            // Debug.Log($"orderCfgs[turn] {turn}");
            InitCustomer(orderCfgs[turn]);
            turn++;
        }
    }

    public void InitCustomer(OrderCfg orderCfg)
    {
        var customer = Instantiate(allCustomerType[orderCfg.typeCustomer], transform).GetComponent<Customer>();
        customer.reward = orderCfg.reward;
        // Debug.Log($"InitCustomer {customer.orders}");
        customer.orders.InitOrders(orderCfg);
    }
}
