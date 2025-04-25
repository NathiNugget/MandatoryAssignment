using MandatoryAssignment.Interfaces;
using MandatoryAssignment.TracingClasses;
using System.Diagnostics;
using System.Numerics;

namespace MandatoryAssignment
{
    public class Creature : AbstractCreature, IDisposable
    {
        

        /// <summary>
        /// The Creature uses Observer-pattern due to being instantiated with a reference the World, <br/>which notifies all creatures when a given round has passed, buffing their items
        /// </summary>
        /// <param name="name">Name of the Creature</param>
        /// <param name="hitPoint">How much damage the Creature can take before dying.</param>
        /// <param name="difficulty">Which difficulty the create is initially set at.</param>

        public Creature(string name, int hitPoint, World world) : base(name, hitPoint, world)
        {
            LoggerHelper.GetInstance().TraceEvent(TraceEventType.Information, (int)IDs.Instantiation, $"{GetType().Name} is spawned: {ToString()}");   
        }

        #region Properties
        /// <summary>
        /// Difficulty of the game, affecting damage of the creature as well as directly adapting items throughout the game. 
        /// </summary>
        public int DifficultyMultiplier { get => (int) _world.GameDifficulty; }

        /// <summary>
        /// Hit-delegate is sealed as to not allow further changes to the Hit-behavior, further-more there is no set-method. 
        /// </summary>
        public sealed override Func<int> Hit
        {
            get {
                return HitPoint < InitialHp * 0.2 ? HitWeak : HitRegular;
            }
        }

        
        #endregion



        #region Operator Overload
        /// <summary>
        /// The singular overload for Creature using reflection to find out the runtime-type of the Item passed as argument.
        /// </summary>
        /// <param name="creature">The creature to which a certain item is added</param>
        /// <param name="item">Item to be added to a creature</param>
        /// <returns>Creature with item added to their equipment</returns>
        public static Creature operator +(Creature creature, IItem item)
        {
            switch (item)
            {
                case IAttackItem:
                    creature.AttackItems.Add((IAttackItem)item);
                    break;
                case IDefenceItem:
                    creature.DefensiveItems.Add((IDefenceItem)item);
                    break;
                default:
                    break;
            }
            LoggerHelper.GetInstance().TraceEvent(TraceEventType.Information, (int)IDs.PickupItem, $"{creature.Name} picked up {item.Name}");
            return creature;
        }




        #endregion


        #region Methods
        /// <summary>
        /// Calculate damage to deal - damage scales with difficulty of the game.
        /// </summary>
        /// <returns>The numerical damage dealt.</returns>
        public int HitRegular()
        {
            int dmg = AttackItems.Sum(item => item.Hit);
            LoggerHelper.GetInstance().TraceEvent(TraceEventType.Information, (int)IDs.HitEnemy, $"{Name} -> Dealing damage: {dmg}");
            return dmg * DifficultyMultiplier;
        }

        public int HitWeak()
        {
            int dmg = (int)(AttackItems.Sum(item => item.Hit) * 0.5 * DifficultyMultiplier);
            LoggerHelper.GetInstance().TraceEvent(TraceEventType.Information, (int)IDs.HitEnemy, $"{Name} -> Dealing damage using weak attack: {dmg}");
            return dmg;
        }

        /// <summary>
        /// Take damage, hit is reduced by defensive items the creature holds. 
        /// </summary>
        /// <param name="hit">The base damage to take</param>
        public void ReceiveHit(int hit)
        {
            int copy = hit;
            int defend = DefensiveItems.Sum(i => i.ReduceHitPoint);
            hit -= defend;
            LoggerHelper.GetInstance().TraceEvent(TraceEventType.Information, (int)IDs.ReceiveHit, $"{Name} <- base damage taken: {copy}, actual damage taken: {hit}");
            if (hit > 0) HitPoint -= hit;
            if (HitPoint <= 0)
            {
                Die();
            }
        }


        /// <summary>
        /// Buff offensive items and nerf defensive items. I use actual iteration instead of LINQ, due to LINQ being slow, suck it.<br/>Observer-pattern is used here. 
        /// 
        /// </summary>
        public void AlterItems()
        {

            for (int i = 0; i < AttackItems.Count; i++)
            {
                AttackItems[i].WrapItem(new AttackItem($"Upgraded {AttackItems[i].Name}", 5, 2));
            }

            for (int i = 0; i < DefensiveItems.Count; i++)
            {
                DefensiveItems[i].ReduceHitPoint -= 1;
            }
        }

        /// <summary>
        /// The creature dies and their items are cleared, freeing up memory. 
        /// </summary>
        private void Die()
        {
             

            LoggerHelper.GetInstance().TraceEvent(TraceEventType.Information, 1, $"{ToString()} was killed");
            LoggerHelper.GetInstance().TraceEvent(TraceEventType.Verbose, (int)IDs.SetToNull, $"Defensive items for {Name} was set to null");
            LoggerHelper.GetInstance().TraceEvent(TraceEventType.Verbose, (int)IDs.SetToNull, $"Attack items for {Name} was set to null");
            Dispose();
        }


        /// <summary>
        /// This method Loots items from a WorldObject if it contains any
        /// 
        /// 
        /// </summary>
        /// <param name="worldobject">object to be looted from</param>
        public override void LootMultipleItems(IWorldObject worldobject)
        {
            if (worldobject != null && worldobject.Lootable)
            {
                if (worldobject.DefenceChest != null)
                {
                    foreach (IDefenceItem di in worldobject.DefenceChest)
                    {
                        DefensiveItems.Add(di);
                    }
                    worldobject.DefenceChest = null;
                    LoggerHelper.GetInstance().TraceEvent(TraceEventType.Verbose, IDs.SetToNull, $"DefenseChest set to null");

                }

                if (worldobject.AttackChest != null)
                {
                    foreach (IAttackItem ai in worldobject.AttackChest)
                    {
                        AttackItems.Add(ai);
                    }
                    worldobject.AttackChest = null;
                    LoggerHelper.GetInstance().TraceEvent(TraceEventType.Verbose, IDs.SetToNull, $"AttackChest set to null");
                }
                worldobject.Lootable = false;
                worldobject = null!;

            }

        }

        public void Loot(IWorldObject worldobject)
        {
            if (DifficultyMultiplier != (int) Difficulty.Trained)
            {
                LootMultipleItems(worldobject);

            }
            else LootSingleItem(worldobject);
        }


        public override void LootSingleItem(IWorldObject worldobject)
        {
            if (worldobject != null && worldobject.Lootable)
            {
                if (worldobject.DefenceChest != null)
                {
                    IDefenceItem item = worldobject.DefenceChest.First();
                    DefensiveItems.Add(item);
                    worldobject.DefenceChest.Remove(item);
                    LoggerHelper.GetInstance().TraceEvent(TraceEventType.Information, (int)IDs.Loot, $"Looted item: {item.ToString()}");
                    if (worldobject.DefenceChest.Count == 0) worldobject.DefenceChest = null;

                }

                else if (worldobject.AttackChest != null)
                {
                    IAttackItem item = worldobject.AttackChest.First();

                    worldobject.AttackChest = null;
                }

                else if (worldobject.AttackChest == null && worldobject.DefenceChest == null)
                {
                    worldobject.Lootable = false;
                    worldobject = null!;

                }
            }
        }

        public void Dispose()
        {
            IsDead = true;
            DefensiveItems.Clear();
            DefensiveItems = null!;
            AttackItems.Clear();
            AttackItems = null!;




        }

        public override string ToString()
        {
            return $"{{Creature {nameof(Name)}={Name}, {nameof(HitPoint)}={HitPoint.ToString()}}}";
        }









        #endregion
    }
}