namespace RotasViagensApi.Models
{
    public class RotaViagem
    {
        public int Id { get; set; }
        public string? Origem { get; set; }
        public string? Destino { get; set; }
        public decimal Valor { get; set; }
        public List<Escala>? Escalas { get; set; }

        public string Imprimir()
        {
            var template = $"{this.Origem}#ESCALAS# - {this.Destino} ao custo de ${this.Valor}";

            var text = string.Empty;

            if (Escalas != null)
            {
                foreach (var escala in Escalas) { text += " - " + escala.Destino?.ToString(); }
            }

            return template.Replace("#ESCALAS#", text);
        }
    }
}
