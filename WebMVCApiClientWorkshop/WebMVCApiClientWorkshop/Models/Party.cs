namespace DnDCharacter.Models
{
    public class Party
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public virtual List<Character>? Characters { get; set; }

        public Party(int id, string name,List<Character> characters)
        {
            Id = id;
            Name = name;
            Characters = characters;
        }

        public Party()
        {
            return;
        }
    }
}
