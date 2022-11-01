namespace DnDCharacter.Models
{
    public class CharacterInventory
    {
        public int? Id { get; set; }
        public string? ItemName { get; set; }
        public int Amount { get; set; }


        public CharacterInventory(int? id, string? itemName, int amount)
        {
            Id = id;
            ItemName = itemName;
            Amount = amount;
        }

        public CharacterInventory()
        {
            return;
        }
    }
}
