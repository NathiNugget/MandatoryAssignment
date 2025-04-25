using MandatoryAssignment.Interfaces;

namespace MandatoryAssignment
{
    public class AttackItem : IAttackItem
    {
        private int _hit;


        public AttackItem(string name, int hit, int range)
        {
            Name = name;
            Hit = hit;
            Range = range;
        }

        /// <summary>
        /// This item gets wrapped inside the passed item using Decorator pattern
        /// </summary>
        /// <param name="di">Item to wrap this instance</param>
        /// <returns>new the passed item with this instance</returns>
        public IAttackItem WrapItem(IAttackItem ai)
        {

            ai.InnerItem = this;
            return ai;
        }

        public string Name { get; init; }
        public int Hit
        {
            get { if (InnerItem == null) return _hit; return InnerItem.Hit + _hit; }
            init { _hit = value; }
        }
        public int Range { get; init; }
        public IAttackItem? InnerItem { get; set; }

    }
}