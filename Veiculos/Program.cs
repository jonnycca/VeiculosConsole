using System;
using System.Collections.Generic;

namespace Veiculos
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcao = 0;
            do
            {
                try
                {
                    Console.WriteLine("\n\n\t////////////////////////////////\n\n");
                    Console.WriteLine("1 - Carregar arquivo txt");
                    Console.WriteLine("2 - Inserir veiculos no banco de dados");
                    Console.WriteLine("3 - Ver veiculos que estao no banco de dados");
                    Console.WriteLine("4 - Remover veiculo do banco de dados");
                    Console.WriteLine("5 - Pesquisa veiculo pelo nome");
                    Console.WriteLine("6 - Adiciona veiculo");
                    Console.Write("Escola uma opcao:");
                    opcao = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Menu(opcao);
                }
                catch (Exception)
                {
                    Console.WriteLine("Numero digitado invalido!");
                }
            } while (opcao != 99);
        }

        private static void Menu(int op)
        {
            VeiculosDAO veiculosDAO = new VeiculosDAO();
            ManipuladorDeARquivo manipulador = new ManipuladorDeARquivo();
            string nomeVeiculo;
            switch (op)
            {
                case 1:
                    manipulador.CarregaArquivo();
                    Console.WriteLine("Fim da operação!");
                    break;
                case 2:
                    veiculosDAO.InserirVeiculos();
                    break;
                case 3:
                    List<Veiculo> listVeiculo = new List<Veiculo>();
                    listVeiculo = veiculosDAO.GetVeiculos();
                    foreach (Veiculo v in listVeiculo)
                    {
                        Console.WriteLine(v.NomeVeiculo);
                    }
                    break;
                case 4:
                    Console.Write("Digite o nome do veiculo que deseja remover:");
                    nomeVeiculo = Console.ReadLine();
                    int veiculosAfetados = veiculosDAO.ExcluirVeiculo(nomeVeiculo);
                    Console.WriteLine("{0} veiculo removido com esse nome", veiculosAfetados);
                    break;
                case 5:
                    Console.Write("Digite o nome do veiculo que deseja pesquisar:");
                    nomeVeiculo = Console.ReadLine();
                    veiculosDAO.GetVeiculo(nomeVeiculo);
                    listVeiculo = null;
                    listVeiculo = veiculosDAO.GetVeiculo(nomeVeiculo);
                    if (listVeiculo.Count >= 1)
                    {
                        foreach (Veiculo v in listVeiculo)
                        {
                            Console.WriteLine(v.NomeVeiculo);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum veiculo encontrado com esse nome!");
                    }
                    break;
                case 6:
                    Veiculo veiculo = CriarVeiculo();
                    if (veiculo != null)
                    {
                        veiculosDAO.InserirVeiculo(veiculo);
                        Console.WriteLine("Veiculo adicionado!");
                    }
                    else
                    {
                        Console.WriteLine("Nao foi possivel adicionar o veiculo!");
                    }
                    break;
                default:
                    Console.WriteLine("Opcao invalida!");
                    break;
            }
        }

        private static Veiculo CriarVeiculo()
        {
            Veiculo v = null;
            try
            {
                Console.Write("Digite o nome do veiculo:");
                string nomeVeiculo = Console.ReadLine();
                Console.Write("Digite o nome do fabricante:");
                string nomeFabricante = Console.ReadLine();
                Console.Write("Digite o ano de fabricacao do veiculo:");
                int anoFabricacao = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o ano do modelo:");
                int anoModelo = Convert.ToInt32(Console.ReadLine());
                Console.Write("Digite o motor do veiculo:");
                string motor = Console.ReadLine();
                Console.Write("Digite a cor do veiculo:");
                string cor = Console.ReadLine();
                Console.Write("Digite a data de lancamento do veiculo:");
                DateTime dataLancamentoMercado = Convert.ToDateTime(Console.ReadLine());
                v = new Veiculo(nomeVeiculo, nomeFabricante, anoFabricacao, anoModelo, motor, cor, dataLancamentoMercado);
            }
            catch (Exception)
            {
                Console.WriteLine("Dado informado invalido!");
            }
            return v;
        }
    }
}
