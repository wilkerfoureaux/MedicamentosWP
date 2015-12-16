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
        public int Dose { get; set; }
        public double Dosagem { get; set; }
        // public double Posologia { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraUsado { get; set; }
        public DateTime DataHoraTermino { get; set; }
        public DateTime DataHoraProxima { get; set; }
        public string Observacao { get; set; }

        public Medicamento()
        {
            //construtor vazio
        }

        public Medicamento(string Nome, DateTime Inicio, double Dosagem, int Dose)
        {
            this.Nome = Nome;
            this.DataHoraInicio = Inicio;
            this.DataHoraProxima = Inicio;
            this.DataHoraUsado = DateTime.MinValue;
            this.DataHoraTermino = DateTime.MaxValue;
            this.Dosagem = Dosagem;
            this.Dose = Dose;
        }

    }
}
