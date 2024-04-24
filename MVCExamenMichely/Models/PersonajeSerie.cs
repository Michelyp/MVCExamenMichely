using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCExamenMichely.Models
{
    public class PersonajeSerie
    {
        public int IdPersonaje { set; get; }
        public string Nombre { set; get; }

        public string Imagen { set; get; }

        public string Serie{ set; get; }

    }
}
