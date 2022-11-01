namespace DnDCharacter.Models
{
    public class Abilities
    {
        public int? Id { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        public Abilities(int id, int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
        {
            Id = id;
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
        }

        public Abilities()
        {
            return;
        }
    }

}
