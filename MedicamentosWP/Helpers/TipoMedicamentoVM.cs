﻿using System;
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

        private ObservableCollection<TipoMedicamentoVM> _tipoMedicamentoVM { get; set; }

        public TipoMedicamentoVM()
        {
            _tipoMedicamentoVM = new ObservableCollection<TipoMedicamentoVM>();

            _tipoMedicamentoVM.Add(new TipoMedicamentoVM(1, "Fitoterápico"));
            _tipoMedicamentoVM.Add(new TipoMedicamentoVM(2, "Alopático"));
            _tipoMedicamentoVM.Add(new TipoMedicamentoVM(3, "Homeopático"));
            _tipoMedicamentoVM.Add(new TipoMedicamentoVM(4, "Similar"));
            _tipoMedicamentoVM.Add(new TipoMedicamentoVM(5, "Genérico"));
            _tipoMedicamentoVM.Add(new TipoMedicamentoVM(6, "Referência"));
            _tipoMedicamentoVM.Add(new TipoMedicamentoVM(7, "Manipulado"));
            _tipoMedicamentoVM.Add(new TipoMedicamentoVM(8, "Outro"));
        }

        private TipoMedicamentoVM(int key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public ObservableCollection<TipoMedicamentoVM> TiposMedicamento()
        {
            return _tipoMedicamentoVM;
        }
    }
}
