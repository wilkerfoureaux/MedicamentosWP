using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicamentosWP.Helpers
{
    class TipoMedicamentoVM
    {
        public int key { get; set; }
        public string value { get; set; }

        private ObservableCollection<TipoMedicamentoVM> _doses { get; set; }

        public TipoMedicamentoVM()
        {
            _doses = new ObservableCollection<TipoMedicamentoVM>();

            _doses.Add(new TipoMedicamentoVM(1, "Fitoterápico"));
            _doses.Add(new TipoMedicamentoVM(2, "Alopático"));
            _doses.Add(new TipoMedicamentoVM(3, "Homeopático"));
            _doses.Add(new TipoMedicamentoVM(4, "Similar"));
            _doses.Add(new TipoMedicamentoVM(5, "Genérico"));
            _doses.Add(new TipoMedicamentoVM(6, "Referência"));
            _doses.Add(new TipoMedicamentoVM(7, "Manipulado"));
            _doses.Add(new TipoMedicamentoVM(8, "Outro"));
        }

        private TipoMedicamentoVM(int key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public ObservableCollection<TipoMedicamentoVM> Doses()
        {
            return _doses;
        }
    }
}
