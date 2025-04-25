using MandatoryAssignment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandatoryAssignment
{
    /// <summary>
    /// I made this class specifically for template-pattern
    /// </summary>
    public abstract class AbstractCreature : ILoot
    {
        protected World _world;
         

        public AbstractCreature(string name, int hitpoint, World world) {
            HitPoint = hitpoint;
            InitialHp = hitpoint;
            Hit = (() =>
            {
                return (int)(0.5*HitPoint);
            });
            IsDead = false;
            Name = name;
            _world = world;
            DefensiveItems = new();
            AttackItems = new();
        }

        /// <summary>
        /// Initial health to find out how if next attack should be weak depending on current HitPoint
        /// </summary>
        protected int InitialHp { get; init; }

        public int HitPoint { get; protected set; }

        /// <summary>
        /// Set the Hit-func to be equal to half that of the unit's hitpoints. 
        /// <br> Without this, any non-standard Creature will not be sure to have a set method when Hit is called </br>
        /// </summary>
        public virtual Func<int> Hit { get; protected set; }
        
        /// <summary>
        /// Find out if Creature has died
        /// </summary>
        public virtual bool IsDead { get; protected set; }



                public string Name { get; init; }

                /// <summary>
        /// List of defensive items, as long as they implement the interface.
        /// </summary>
        public List<IDefenceItem> DefensiveItems { get; set; }
        /// <summary>
        /// List of attack items, as long as they implement the interface.
        /// </summary>
        public List<IAttackItem> AttackItems { get; set; }

        public abstract void LootSingleItem(IWorldObject worldobj);

        public abstract void LootMultipleItems(IWorldObject worldobj);
    }
}
