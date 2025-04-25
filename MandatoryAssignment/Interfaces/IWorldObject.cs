namespace MandatoryAssignment.Interfaces
{
    public interface IWorldObject
    {
        List<IAttackItem>? AttackChest { get; set; }
        List<IDefenceItem>? DefenceChest { get; set; }
        bool Lootable { get; set; }
        string Name { get; init; }
        bool Removeable { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}