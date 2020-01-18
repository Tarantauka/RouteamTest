using System.ComponentModel.DataAnnotations.Schema;

namespace RouteamTest.Models.Interfaces
{
    public interface IBaseLink
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
