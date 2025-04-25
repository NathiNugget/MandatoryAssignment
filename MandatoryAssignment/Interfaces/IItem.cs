namespace MandatoryAssignment.Interfaces
{
    public interface IItem
    {
        public string Name { get; init; }
        /// <summary>
        /// Method to wrap the item <para/>for example when 10 rounds has passed, items can be passively upgraded for intensity purposes. 
        /// 
        /// </summary>
        public IItem WrapItem(IItem item);
    }
}