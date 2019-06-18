using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Veiculos
{
    class ManipuladorDeARquivo
    {
        public void CarregaArquivo()
        {
            Veiculos veiculos = new Veiculos();
            if (File.Exists("Veiculos.txt"))//verifica se arquivo a ser lido existe
            {
                try
                {
                    using (StreamReader sr = new StreamReader("Veiculos.txt"))//cria uma instancia para ler o arquivo "Veiculos.txt"
                    {
                        string linha;//variavel que vai pegar as linhas do arquivo
                        string[] texto = null;//variavel que vai pegar os dados de cada linha do arquivo

                        while ((linha = sr.ReadLine()) != null)//enquanto existir linhas
                        {
                            texto = linha.Split(';');//pega os dados que estao entre os ponto e virgula
                            Veiculo v = new Veiculo(//objeto para obter os dados 
                            texto[0],
                            texto[1],
                            Convert.ToInt32(texto[2]),
                            Convert.ToInt32(texto[3]),
                            texto[4],
                            texto[5],
                            Convert.ToDateTime(texto[6])
                            );
                            if (v != null)
                            {
                                veiculos.ListVeiculos.Add(v);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Arquivo Veiculos.txt nao encontrado!");
            }
        }
    }
}
