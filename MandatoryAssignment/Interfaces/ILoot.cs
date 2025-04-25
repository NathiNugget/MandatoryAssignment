namespace MandatoryAssignment.Interfaces
{
    public interface ILoot
    {
        void LootSingleItem(IWorldObject worldobj); 

        void LootMultipleItems(IWorldObject worldobj);
    }
}