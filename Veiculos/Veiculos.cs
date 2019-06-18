using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculos
{
    class Veiculos
    {
        //lista de veiculos para armazenar os veiculos
        private static List<Veiculo> listVeiculos = new List<Veiculo>();

        public List<Veiculo> ListVeiculos { get => listVeiculos; set => listVeiculos = value; }
    }
}
