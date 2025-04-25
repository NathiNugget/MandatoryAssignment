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
        
        

        
    }
}