using MandatoryAssignment.Interfaces;

namespace MandatoryAssignment
{
    public class DefenceItem : IDefenceItem
    {
        private int _reduceHitPoint;

        public DefenceItem(string name, int reduceHitPoint)
        {
            Name = name;
            ReduceHitPoint = reduceHitPoint;

        }

        /// <summary>
        /// This item gets wrapped inside the passed item using Decorator pattern
        /// </summary>
        /// <param name="di">Item to wrap this instance</param>
        /// <returns>new the passed item with this instance</returns>
        public IDefenceItem WrapItem(IDefenceItem di)
        {
            di.InnerItem = this;
            return di;
        }


        public string Name { get; init; }
        public int ReduceHitPoint
        {
            get { if (InnerItem == null) return _reduceHitPoint; return InnerItem.ReduceHitPoint + _reduceHitPoint; }

            set
            {
                _reduceHitPoint = value;
            }
        }

        public IDefenceItem? InnerItem { get; set; }
    }
}