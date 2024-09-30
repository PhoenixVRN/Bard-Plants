public class GameModel 
{
    public SubscriptionField<int> StarchNut { get; }
    public SubscriptionField<int> MysticalMushroom { get; }
    public SubscriptionField<int> CrystalNut { get; }
    public SubscriptionField<int> CountCustomerInGame { get; }
    public SubscriptionField<int> LevelGame { get; }
    public SubscriptionField<eTypeAnimation> AnimationPlayer { get; }
    public SubscriptionField<eTypeAnimation> AnimationCollectorGnome { get; }
    public SubscriptionField<eTypeAnimation> AnimationMusicHelpers { get; }
    public SubscriptionField<eTypeAnimation> AnimationGardenGnome { get; }
    
    public SubscriptionField<bool> BagInteractive { get; }
    
   
    public GameModel()
    {
        StarchNut = new SubscriptionField<int>();
        MysticalMushroom = new SubscriptionField<int>();
        CrystalNut = new SubscriptionField<int>();
        
        CountCustomerInGame = new SubscriptionField<int>();
        LevelGame = new SubscriptionField<int>() {Value = 1};
        AnimationPlayer = new SubscriptionField<eTypeAnimation>();
        AnimationCollectorGnome = new SubscriptionField<eTypeAnimation>();
        AnimationGardenGnome = new SubscriptionField<eTypeAnimation>();
        AnimationMusicHelpers = new SubscriptionField<eTypeAnimation>();
        BagInteractive = new SubscriptionField<bool>() {Value = true};
    }
}
