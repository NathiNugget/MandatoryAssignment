
namespace MandatoryAssignment
{
    public class World
    {

        public World(int maxX, int maxY, List<Creature> creatures, List<WorldObject> worldObjects)
        {
            MaxX = maxX;
            MaxY = maxY;
            Creatures = creatures;
            WorldObjects = worldObjects;
            RoundNumber = 0;
            GameDifficulty = (Difficulty) 1;
        }

        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public List<Creature> Creatures { get; set; }
        public List<WorldObject> WorldObjects { get; set; }
        public Difficulty GameDifficulty { get; set; }

        public int RoundNumber { get; set; }

        /// <summary>
        /// Spend a round, possibly raising difficulty of creatures
        /// </summary>
        public void NextRound()
        {
            RoundNumber++;
            if (RoundNumber == 10 || RoundNumber == 20) {
                for (int i = 0; i < Creatures.Count; i++)
                {
                    Creatures[i].AlterItems(); 
                }
            }
        }






    }
}
