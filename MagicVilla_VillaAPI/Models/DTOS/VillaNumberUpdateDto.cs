using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    public class VillaNumberUpdateDto
    {
        public int VillaNo { get; set; }
        public int VillaID { get; set; }
        public string SpecialDetails {  get; set; }    
        public DateTime CreatedDate { get; set; }   
        public DateTime UpdatedDate { get; set;}
    }
}
