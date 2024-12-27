using System.Collections.Generic;

public class GameModel 
{
    public SubscriptionField<int> StarchNut { get; }
    public SubscriptionField<int> MysticalMushroom { get; }
    public SubscriptionField<int> CrystalNut { get; }
    public SubscriptionField<int> FlyEater { get; }
    public SubscriptionField<int> GutFlower { get; }
    public SubscriptionField<int> Mandrake { get; }
    public SubscriptionField<int> MiracleFruit { get; }
    public SubscriptionField<int> NeedleFlower { get; }
    public SubscriptionField<int> StaringFlower { get; }
    public SubscriptionField<int> ToxicMushroom  { get; }
    public SubscriptionField<int> BushTentacles { get; }
    public SubscriptionField<int> StarFruit { get; }

    // public List<SubscriptionField<int>> AllPlantsInModel;
    public SubscriptionField<int> CountCustomerInGame { get; }
    public SubscriptionField<int> LevelGame { get; }
    public SubscriptionField<int> NumberCompletedOrders { get; }
    public SubscriptionField<int> CloseCustomersInLevel { get; }
    public SubscriptionField<eTypeAnimation> AnimationPlayer { get; }
    public SubscriptionField<eTypeAnimation> AnimationCollectorGnome { get; }
    public SubscriptionField<eTypeAnimation> AnimationMusicHelpers { get; }
    public SubscriptionField<eTypeAnimation> AnimationGardenGnome { get; }
    
    public SubscriptionField<bool> BagInteractive { get; }
    
    public SubscriptionField<bool> GardenGnome { get; }
    public SubscriptionField<bool> CollectorGnome { get; }
    public SubscriptionField<bool> MusicHelpers { get; }
    
    public SubscriptionField<LvlAssistance> GardenGnomeLevel { get; }
    public SubscriptionField<LvlAssistance> CollectorGnomeLevel { get; }
    public SubscriptionField<LvlAssistance> MusicHelpersLevel { get; }
    public SubscriptionField<int> LevelMap { get; }
    public SubscriptionField<int> NumberClosedOrders { get; }
   
    public GameModel()
    {
        StarchNut = new SubscriptionField<int>();
        MysticalMushroom = new SubscriptionField<int>();
        CrystalNut = new SubscriptionField<int>();
        FlyEater = new SubscriptionField<int>();
        GutFlower = new SubscriptionField<int>();
        Mandrake = new SubscriptionField<int>();
        MiracleFruit = new SubscriptionField<int>();
        NeedleFlower = new SubscriptionField<int>();
        StaringFlower = new SubscriptionField<int>();
        ToxicMushroom = new SubscriptionField<int>();
        BushTentacles = new SubscriptionField<int>();
        StarFruit = new SubscriptionField<int>();
        
        CountCustomerInGame = new SubscriptionField<int>();
        LevelGame = new SubscriptionField<int>() {Value = 0};
        AnimationPlayer = new SubscriptionField<eTypeAnimation>();
        AnimationCollectorGnome = new SubscriptionField<eTypeAnimation>();
        AnimationGardenGnome = new SubscriptionField<eTypeAnimation>();
        AnimationMusicHelpers = new SubscriptionField<eTypeAnimation>();
        BagInteractive = new SubscriptionField<bool>() {Value = true};
        GardenGnome = new SubscriptionField<bool>() {Value = false};
        CollectorGnome = new SubscriptionField<bool>() {Value = false};
        MusicHelpers = new SubscriptionField<bool>() {Value = false};
        GardenGnomeLevel = new SubscriptionField<LvlAssistance>();
        GardenGnomeLevel.Value = new LvlAssistance();
        CollectorGnomeLevel = new SubscriptionField<LvlAssistance>();
        CollectorGnomeLevel.Value = new LvlAssistance();
        MusicHelpersLevel = new SubscriptionField<LvlAssistance>();
        MusicHelpersLevel.Value = new LvlAssistance();
        NumberCompletedOrders = new SubscriptionField<int>();
        CloseCustomersInLevel = new SubscriptionField<int>() {Value = 0};
        LevelMap = new SubscriptionField<int>() {Value = 0};
        NumberClosedOrders = new SubscriptionField<int>() {Value = 0};
        // AllPlantsInModel = new List<SubscriptionField<int>>()
        // {
        //     StarchNut,
        //     MysticalMushroom,
        //     CrystalNut,
        //     FlyEater,
        //     GutFlower,
        //     Mandrake,
        //     MiracleFruit,
        //     NeedleFlower,
        //     StaringFlower,
        //     ToxicMushroom,
        //     BushTentacles,
        //     StarFruit
        // };
    }
}
