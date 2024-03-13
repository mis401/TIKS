using System.ComponentModel.DataAnnotations;

namespace event_driven_backend.Models
{
    public class User
    {
        [Key]
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }

        public List<Community> CreatedCommunities { get; set; } = new List<Community>();
        //public List<Community> Communities { get; set; } M:N
        //lista zajednica m:n, lista svojih zajendica koje je kreirao
    }
}
