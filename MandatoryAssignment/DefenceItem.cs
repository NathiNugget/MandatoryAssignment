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



        public IItem WrapItem(IItem item)
        {
            DefenceItem di = (DefenceItem)item;
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