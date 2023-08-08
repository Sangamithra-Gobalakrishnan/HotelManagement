using System.ComponentModel.DataAnnotations;

namespace UserAuthentication.Models.DTO
{
    public class StatusDTO
    {
        [Required]
        public int AgentID { get; set; }
    }
}
