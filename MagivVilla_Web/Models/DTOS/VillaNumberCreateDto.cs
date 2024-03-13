using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_Web.Models
{
    public class VillaNumberCreateDto
    {
        public int VillaNo { get; set; }
        public int VillaID { get; set; }
        public string SpecialDetails {  get; set; }    
        public DateTime CreatedDate { get; set; }   
        
    }
}
