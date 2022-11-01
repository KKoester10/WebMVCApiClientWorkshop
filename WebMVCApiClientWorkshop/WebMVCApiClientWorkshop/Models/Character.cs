namespace DnDCharacter.Models
{
    public class Character
    {
        public int? Id { get; set; }
        public string? PlayerName { get; set; } 
        public string? Name { get; set; }
        public string? Class { get; set; }
        public int Level { get; set; }
        public string? Race { get; set; }
        public string? Allignment { get; set; }
        public string? Background { get; set; }
        public int ProficiencyBonus { get; set; }
        public int Experiance { get; set; }
        public int ArmorClass { get; set; }
        public int Initiative { get; set; }
        public int HitPoints { get; set; }
        public int Speed { get; set; }
        public int? PartyId { get; set; }
        public virtual Party? Party { get; set; }
        public int? AbilitiesId { get; set; }
        public virtual Abilities? Abilities { get; set; }
        public int? InventoryId { get; set; }
        public virtual CharacterInventory? Inventory { get; set; }

        public Character()
        {
            return;
        }

        public Character(int id, string? playerName, string? @class, int level, string? race, string? allignment, string? background, int proficiencyBonus, int experiance, int armorClass, int initiative, int hitPoints, int speed, int? partyId, Party? party, int? abilitiesId, Abilities? abilities, int? inventoryId, CharacterInventory? inventory)
        {
            Id = id;
            PlayerName = playerName;
            Class = @class;
            Level = level;
            Race = race;
            Allignment = allignment;
            Background = background;
            ProficiencyBonus = proficiencyBonus;
            Experiance = experiance;
            ArmorClass = armorClass;
            Initiative = initiative;
            HitPoints = hitPoints;
            Speed = speed;
            PartyId = partyId;
            Party = party;
            AbilitiesId = abilitiesId;
            Abilities = abilities;
            InventoryId = inventoryId;
            Inventory = inventory;
        }
    }
}
