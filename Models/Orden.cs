using MessagePack;

namespace apprueba.Models
{
    public class Orden
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public bool ConSalsa { get; set; }
        public bool ConEnsalada { get; set; }
        public bool PapasExtra { get; set; }
    }
}
