
namespace MagicVilla_Web.Models.DTOS
{
    public class VillaNumberDto
    {        
        public int VillaNo { get; set; }
        public int VillaID { get; set; }
        public string SpecialDetails {  get; set; }    
        public DateTime CreatedDate { get; set; }   
        public DateTime UpdatedDate { get; set;}
        public VillaDto Villa { get; set; }
    }
}
