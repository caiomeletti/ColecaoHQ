namespace ColecaoQuadrinhos
{
    internal class Livro
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string Valor { get; set; }
        public string Tipo { get; set; }
        public string Status { get; internal set; }
    }
}
