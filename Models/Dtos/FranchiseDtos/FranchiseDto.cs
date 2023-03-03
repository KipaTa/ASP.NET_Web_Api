using System.ComponentModel.DataAnnotations;

namespace MovieCharactersAPI.Models.Dtos.FranchiseDtos
{
    public class FranchiseDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string> Movies { get; set; }

    }
}
