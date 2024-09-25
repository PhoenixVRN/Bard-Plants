public class GameModel 
{
    public SubscriptionField<int> StarchNut { get; }
    public SubscriptionField<int> MysticalMushroom { get; }
    public SubscriptionField<int> CrystalNut { get; }
    
    
    public SubscriptionField<int> CountCustomerInGame { get; }
    
    public SubscriptionField<int> LevelGame { get; }
    public GameModel()
    {
        StarchNut = new SubscriptionField<int>();
        MysticalMushroom = new SubscriptionField<int>();
        CrystalNut = new SubscriptionField<int>();
        
        CountCustomerInGame = new SubscriptionField<int>();
        LevelGame = new SubscriptionField<int>() {Value = 1};
    }
}
