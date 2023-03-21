using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcSeguridadDoctores.Models {
	[Table("ENFERMO")]
	public class Enfermo {

		[Key] [Column("inscripcion")]
		public int Inscripcion { get; set; }
		
		[Column("apellido")]
		public string Apellido { get; set;}
		
		[Column("direccion")]
		public string Direccion { get; set;}
		
		[Column("fecha_nac")]
		public DateTime FechaNac { get; set;}
		
		[Column("s")]
		public string Sexo { get; set;}
		
		[Column("nss")]
		public int NSS { get; set;}

	}
}

