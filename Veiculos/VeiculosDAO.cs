using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Veiculos
{
    class VeiculosDAO
    {
        readonly string stringDeConexao = @"Server=" + Properties.Resources.Servidor.ToString() + ";DataBase=" + Properties.Resources.DataBase.ToString() +
                                 ";Uid=" + Properties.Resources.usuario.ToString() +
                                 ";Pwd=" + Properties.Resources.senha;

        public DbConnection GetConexao()//método auxiliar para retornar a conexao com o banco de dados
        {
            DbConnection conexao = null;
            try
            {
                conexao = new MySqlConnection(stringDeConexao);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ouve um erro com a string de conexao: " + e.Message);
            }
            return conexao;
        }

        public DbCommand GetComando(DbConnection conexao)//metodo auxiliar que retorna um comando criado na conexao com o banco
        {
            DbCommand comando = conexao.CreateCommand();
            return comando;
        }

        //metodo para inserir veiculo
        public void InserirVeiculos()
        {
            Veiculos veiculos = new Veiculos(); //instanciando a classe Veiculos para obter os veiculos existentes

            if (veiculos.ListVeiculos != null)//verifica se existe veiculos na lista
            {
                DbConnection connection = GetConexao();//cria conexao com banco de dados
                try
                {
                    connection.Open();
                    foreach (Veiculo v in veiculos.ListVeiculos)//roda um for para todos os veiculos serem adicionados
                    {
                        if ((VeiculoExiste(v.NomeVeiculo)) >= 1)//verifica se veiculo ja encotnra no banco de dados 
                        {
                            Console.WriteLine("Ja existe esse veiculo no banco de dados!");
                        }
                        else//veiculo ainda nao esta no banco de dados
                        {
                            try
                            {
                                if (connection != null)//se conseguiu criar a conexao
                                {
                                    DbCommand comando = GetComando(connection);//cria um comando dentro da conexao
                                    comando.CommandType = CommandType.Text;
                                    comando.CommandText = "INSERT INTO veiculos VALUES(@NomeVeiculo, @NomeFabricante, @AnoFabricacao, @AnoModelo, @Motor, @Cor, @DataLancamento)";//cria o comando e adiciona os paramentos
                                    comando.Parameters.Add(new MySqlParameter("NomeVeiculo", v.NomeVeiculo));
                                    comando.Parameters.Add(new MySqlParameter("NomeFabricante", v.NomeFabricante));
                                    comando.Parameters.Add(new MySqlParameter("AnoFabricacao", v.AnoFabricacao));
                                    comando.Parameters.Add(new MySqlParameter("AnoModelo", v.AnoModelo));
                                    comando.Parameters.Add(new MySqlParameter("Motor", v.Motor));
                                    comando.Parameters.Add(new MySqlParameter("Cor", v.Cor));
                                    comando.Parameters.Add(new MySqlParameter("DataLancamento", v.DataLancamentoMercado));
                                    comando.ExecuteNonQuery();//executa o comando
                                }
                                Console.WriteLine("Veiculo {0} adicionado", v.NomeVeiculo);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Ouve um erro ao inserir no banco de dados: " + e.Message);
                            }
                        }
                    }
                    connection.Close();//fecha a conexao
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erro ao se conectar com o banco de dados: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Nao ha veiculos para inserir!");
            }
        }

        //metodo para inserir um único veiculo
        public void InserirVeiculo(Veiculo v)
        {
            DbConnection conexao = GetConexao();
            if (conexao != null)
            {
                try
                {
                    conexao.Open();
                    DbCommand comando = GetComando(conexao);
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "INSERT INTO VEICULOS VALUES(@NomeVeiculo, @NomeFabricante, @AnoFabricacao, @AnoModelo, @Motor, @Cor, @DataLancamento)";
                    comando.Parameters.Add(new MySqlParameter("NomeVeiculo", v.NomeVeiculo));
                    comando.Parameters.Add(new MySqlParameter("NomeFabricante", v.NomeFabricante));
                    comando.Parameters.Add(new MySqlParameter("AnoFabricacao", v.AnoFabricacao));
                    comando.Parameters.Add(new MySqlParameter("AnoModelo", v.AnoModelo));
                    comando.Parameters.Add(new MySqlParameter("Motor", v.Motor));
                    comando.Parameters.Add(new MySqlParameter("Cor", v.Cor));
                    comando.Parameters.Add(new MySqlParameter("DataLancamento", v.DataLancamentoMercado));
                    comando.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ocorreu um erro ao inserir o veiculo no banco de dados: " + e.Message);
                }
            }
        }

        private int VeiculoExiste(string nomeVeiculo)//metodo usado para verificar se o veiculo ja existe, atraves do nome
        {
            int qntdVeiculos = 0;
            try
            {
                DbConnection conexao = GetConexao();//cria conexao com o banco
                if (conexao != null)
                {
                    conexao.Open();//abre a conexão
                    DbCommand comando = GetComando(conexao);//cria o comando
                    comando.CommandType = CommandType.Text;//cria o tipo do comando
                    comando.CommandText = "SELECT COUNT(*) FROM VEICULOS WHERE NomeVeiculo = @nomeVeiculo";//escreve o comando
                    comando.Parameters.Add(new MySqlParameter("nomeVeiculo", nomeVeiculo));//insere o parametro nomeVeiculo no comando
                    object resultado = comando.ExecuteScalar();//executa o comando obtendo o numero de veiculos
                    qntdVeiculos = Convert.ToInt32(resultado);
                    conexao.Close();
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erro com o comando SQL da verificacao se o veiculo ja existe no banco: " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro com a verificacao se o veiculo ja existe no banco: " + e.Message);
            }
            return qntdVeiculos;
        }

        //metodo para excluir veiculo pelo nome
        public int ExcluirVeiculo(string nomeVeiculo)
        {
            int numeroCarroAfetados = 0;
            DbConnection conexao = GetConexao();//cria conexao com o banco de dados
            if (conexao != null) //verifica se a conexão foi criada
            {
                try
                {
                    conexao.Open();//abre a conexao
                    DbCommand comando = GetComando(conexao);//cria o comando em cima da conexao
                    comando.CommandType = CommandType.Text;//comando do tipo texto
                    comando.CommandText = "DELETE FROM VEICULOS where NomeVeiculo = @nomeVeiculo"; //comando a ser executado
                    comando.Parameters.Add(new MySqlParameter("nomeVeiculo", nomeVeiculo));//adicionando o paramentro 
                    numeroCarroAfetados = comando.ExecuteNonQuery();//executando o comando
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao excluir veiculo do banco de dados: " + e.Message);
                }
            }
            return numeroCarroAfetados;
        }

        //metodo para listar os veiculos
        public List<Veiculo> GetVeiculos()
        {
            List<Veiculo> veiculosList = new List<Veiculo>();//instancia um lista de veiculos para armazenar os dados obtidos no select
            DbConnection conexao = GetConexao(); //cria conexao
            if (conexao != null)//se ocorreu a conexao
            {
                try
                {
                    conexao.Open();//abre conexao
                    DbCommand comando = GetComando(conexao);//cria comando
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "SELECT * FROM VEICULOS";
                    DbDataReader reader = comando.ExecuteReader();//cria o dataReader para obter os dados da execução do comando
                    while (reader.Read())//enquanto ainda tiver dados
                    {
                        Veiculo v = new Veiculo(
                            reader["NomeVeiculo"].ToString(),
                            reader["NomeFabricante"].ToString(),
                            Convert.ToInt32(reader["AnoFabricacao"].ToString()),
                            Convert.ToInt32(reader["AnoModelo"].ToString()),
                            reader["Motor"].ToString(),
                            reader["Cor"].ToString(),
                            Convert.ToDateTime(reader["DataLancamento"].ToString())
                        );//instanciando veiculo para obter os dados do select
                        veiculosList.Add(v);//adiciona o veiculo a lista
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ouve um erro ao obter os veiculos do banco de dados: " + e.Message);
                }
            }
            return veiculosList;
        }

        //metodo para obter o veiculo
        public List<Veiculo> GetVeiculo(string nomeVeiculo)
        {
            List<Veiculo> listVeiculos = new List<Veiculo>();

            DbConnection conexao = GetConexao();
            if (conexao != null)
            {
                try
                {
                    conexao.Open();
                    DbCommand comando = GetComando(conexao);
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "SELECT * FROM VEICULOS WHERE NomeVeiculo = @nomeVeiculo";
                    comando.Parameters.Add(new MySqlParameter("nomeVeiculo", nomeVeiculo));
                    DbDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        Veiculo v = new Veiculo(
                        reader["NomeVeiculo"].ToString(),
                        reader["NomeFabricante"].ToString(),
                        Convert.ToInt32(reader["AnoFabricacao"].ToString()),
                        Convert.ToInt32(reader["AnoModelo"].ToString()),
                        reader["Motor"].ToString(),
                        reader["Cor"].ToString(),
                        Convert.ToDateTime(reader["DataLancamento"].ToString())
                        );
                        if (v != null)
                        {
                            listVeiculos.Add(v);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao obter veiculo do banco de dados: " + e.Message);
                }
            }
            return listVeiculos;
        }
    }
}
