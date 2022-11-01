namespace DnDCharacter.Models
{
    public class Party
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public virtual List<Character>? Characters { get; set; }

        public Party(int? id, string name)
        {
            Id = id;
            Name = name;
            
        }

        public Party()
        {
            return;
        }
    }
}
