using System.Numerics;

namespace MandatoryAssignment.Interfaces
{
    public interface IAttackItem : IItem
    {
        /// <summary>
        /// How much damage the item inflicts
        /// </summary>
        int Hit { get; init; }
        /// <summary>
        /// Possible inner IAttackItem
        /// </summary>
        IAttackItem? InnerItem { get; set; }
        /// <summary>
        /// Range of the given weapon
        /// </summary>
        int Range { get; init; }
        /// <summary>
        /// Method to wrap the item <para/>for example when 10 rounds has passed, items can be passively upgraded for intensity purposes. 
        /// 
        /// 
        /// </summary>
        /// <param name="ai"></param>
        /// <returns></returns>
        IAttackItem WrapItem(IAttackItem ai);

        
    }
}