namespace RotasViagensFront.Models
{
    public class RotaViagem
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Valor { get; set; }
        public List<Escala> Escalas { get; set; }
    }
}
