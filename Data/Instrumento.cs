using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiInstrumentos.Data
{
    public class Instrumento
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public decimal preco { get; set; }
        public string imagem { get; set; }

    }
}
