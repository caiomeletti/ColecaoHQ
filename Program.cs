namespace ColecaoQuadrinhos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var content = Load("colecao_completa.txt");

            var livros = new List<Livro>();
            string fileName = "colecao_completa.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            string line;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);

                        if (!line.Equals("") && !line[..1].Equals(" ") && !line[..1].Equals("\t"))
                        {
                            //titulo
                            var titulo = line[..line.IndexOf("/")].Trim();

                            var editora = "";
                            if (line.IndexOf("Valor:") > 0)
                                editora = line.Substring(line.IndexOf("/") + 1, line.Substring(line.IndexOf("/") + 1).IndexOf("Valor:")).Trim();
                            else
                                editora = line.Substring(line.IndexOf("/") + 1, line.Substring(line.IndexOf("/") + 1).IndexOf("Quantidade:")).Trim();

                            var valor = line.IndexOf("R$ ") > 0 ? line.Substring(line.IndexOf("R$ ") + 3) : "";
                            valor = line.IndexOf("R$ ") > 0 ? valor.Substring(0, valor.IndexOf("\t")) : "";

                            var status = "Lido";
                            var qtde = line.Substring(line.IndexOf("Quantidade: ") + "Quantidade: ".Length);
                            int quantidade = int.Parse(qtde);
                            if (quantidade > 1)
                            {
                                var valorNum = !string.IsNullOrEmpty(valor) ? float.Parse(valor) / quantidade : 0;
                                for (int i = 1; i <= quantidade; i++)
                                {
                                    line = reader.ReadLine();
                                    var num = line.Substring(0, line.IndexOf("\t"));
                                    var tituloNum = line != null ? titulo + num : "";

                                    livros.Add(new Livro()
                                    {
                                        Titulo = tituloNum,
                                        Editora = editora,
                                        Valor = valorNum.ToString("0.00"),
                                        Status = status,
                                        Tipo = "Graphic novel"
                                    });
                                }
                            }
                            else
                            {
                                livros.Add(new Livro()
                                {
                                    Titulo = titulo,
                                    Editora = editora,
                                    Valor = valor,
                                    Status = status,
                                    Tipo = "Graphic novel"
                                });
                            }
                        }
                    }
                }

                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Livros.csv");
                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (StreamWriter outputFile = new StreamWriter(filePath))
                {
                    outputFile.WriteLine("Tipo;Name;Autor;Gênero;Editora;Valor;Status");

                    foreach (var livro in livros)
                        outputFile.WriteLine(livro.Tipo + ";" + livro.Titulo + ";;;" + livro.Editora + ";" + livro.Valor + ";" + livro.Status);
                }
            }
        }
    }
}