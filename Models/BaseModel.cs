using RouteamTest.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RouteamTest.Models
{
    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
