using MandatoryAssignment.Interfaces;

namespace MandatoryAssignment
{
    public class WorldObject : IWorldObject
    {
        public WorldObject(string name, bool removeable, int y, int x, List<IAttackItem>? attackChest = null, List<IDefenceItem>? defenceChest = null)
        {
            Name = name;
            Lootable = attackChest != null || defenceChest != null;
            Removeable = removeable;
            AttackChest = attackChest;
            DefenceChest = defenceChest;
            Y = y;
            X = x;
        }

        #region Properties

        public string Name { get; init; }
        public bool Lootable { get; set; }
        public bool Removeable { get; set; }
        public int Y { get; set; }
        public int X { get; set; }

        public List<IAttackItem>? AttackChest { get; set; }
        public List<IDefenceItem>? DefenceChest { get; set; }
        #endregion




    }
}
