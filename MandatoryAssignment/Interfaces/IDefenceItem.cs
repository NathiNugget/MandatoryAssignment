namespace MandatoryAssignment.Interfaces
{
    public interface IDefenceItem : IItem
    {
        IDefenceItem? InnerItem { get; set; }
        
        int ReduceHitPoint { get; set; }
        
        IDefenceItem WrapItem(IDefenceItem di);
    }
}