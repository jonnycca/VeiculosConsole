using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculos
{
    class Veiculo
    {//atributos do veiculo que serão recebidos do arquivo txt
        private string nomeVeiculo;
        private string nomeFabricante;
        private int anoFabricacao;
        private int anoModelo;
        private string motor;
        private string cor;
        private DateTime dataLancamentoMercado;

        public string NomeVeiculo { get => nomeVeiculo; set => nomeVeiculo = value; }
        public string NomeFabricante { get => nomeFabricante; set => nomeFabricante = value; }
        public int AnoFabricacao { get => anoFabricacao; set => anoFabricacao = value; }
        public int AnoModelo { get => anoModelo; set => anoModelo = value; }
        public string Motor { get => motor; set => motor = value; }
        public string Cor { get => cor; set => cor = value; }
        public DateTime DataLancamentoMercado { get => dataLancamentoMercado; set => dataLancamentoMercado = value; }


        public Veiculo(string nomeCarro, string nomeFabricante, int ano, int modelo, string motor, string cor, DateTime data)
        {
            if (ConfereDadosDoVeiculo(nomeCarro, nomeFabricante, ano, modelo, motor, cor, data))//metodo construtor para construir o veiculo
            {//atribuindo os atributos do veiculo
                NomeVeiculo = nomeCarro;
                NomeFabricante = nomeFabricante;
                AnoFabricacao = ano;
                AnoModelo = modelo;
                Motor = motor;
                Cor = cor;
                DataLancamentoMercado = data;
            }
        }

        //metodo para verificar se os dados do veiculo esta no tamanho certo
        private bool ConfereDadosDoVeiculo(string nomeCarro, string nomeFabricante, int ano, int modelo, string motor, string cor, DateTime data)
        {
            //verificando se o tamanho dos dados estao corretos
            if ((nomeCarro.Length <= 30) && (nomeFabricante.Length <= 20) && (ano.ToString().Length == 4) && (modelo.ToString().Length == 4) && (motor.Length <= 15) && (cor.Length <= 15) && (data.ToString("dd/mm/yyyy").Length <= 10))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //metodo que verifica se a data esta em um periodo válido
        private bool VerificaData(DateTime data)
        {
            if (data.Day > 0 && data.Day <= 32 && data.Year > 1800 && data.Year < 2030 && data.Month > 0 && data.Month <= 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
