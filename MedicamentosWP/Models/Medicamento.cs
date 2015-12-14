using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicamentosWP.Models
{
    class Medicamento
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public int Tipo { get; set; }
        public int Veiculo { get; set; }
        public double Dosagem { get; set; }
        // public double Posologia { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraTermino { get; set; }
        public DateTime DataHoraUsado { get; set; }
        public DateTime DataHoraProxima { get; set; }
        public string Observacao { get; set; }

        public Medicamento()
        {
            //construtor vazio
        }

        public Medicamento(string Nome, DateTime Inicio, double Dosagem, int Veiculo)
        {
            this.Nome = Nome;
            this.DataHoraInicio = Inicio;
            this.Dosagem = Dosagem;
            this.Veiculo = Veiculo;
        }
    }

    enum Tipo
    {
        Fitoterapico,
        Alopatico,
        Homeopatico,
        Similar,
        Generico,
        Referencia,
        Manipulado
    }

    enum Veiculo
    {
        Capsula,
        Comprimido,
        Gotas,
        Unica,
        IntraVenoso,
        IntraMuscular,
        Inalavel,
        Topico
    }
}
