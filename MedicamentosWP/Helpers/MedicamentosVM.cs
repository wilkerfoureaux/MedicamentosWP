using MedicamentosWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MedicamentosWP.Helpers
{
    class MedicamentosVM : INotifyPropertyChanged
    {
        IDataRepository _data;

        // Lista de Medicamentos
        private ObservableCollection<Medicamento> _medicamentos;

        public ObservableCollection<Medicamento> Medicamentos
        {
            get
            {
                return _medicamentos;
            }

            set
            {
                if (value != _medicamentos)
                {
                    _medicamentos = value;
                    RaisePropertyChanged();
                }
            }
        }

        // Medicamento selecionado
        private Medicamento _itemSelecionado;

        public Medicamento MedicamentoSelecionado
        {
            get
            {
                return _itemSelecionado;
            }

            set
            {
                _itemSelecionado = value;
                RaisePropertyChanged();
            }
        }

        // Construtor ViewModel
        public MedicamentosVM(IDataRepository data)
        {
            _data = data;
        }

        // Carrega todos os atuais itens ao inicializar
        async public void Initialize()
        {
            Medicamentos = await _data.Load();
        }

        // Manipuladores do conjunto de dados
        internal void AddMedicamento(Medicamento medicamento)
        {
            _data.Add(medicamento);
            RaisePropertyChanged("Medicamentos");
        }

        internal void DeleteMedicamento(Medicamento medicamento)
        {
            _data.Remove(medicamento);
            RaisePropertyChanged("Medicamentos");
        }

        internal void UpdateMedicamento(Medicamento medicamento)
        {
            _data.Update(medicamento);
            RaisePropertyChanged("Medicamentos");
        }

        internal void ProximaHora(Medicamento medicamento)
        {
            medicamento.DataHoraProxima = medicamento.DataHoraUsado.AddHours(medicamento.Dosagem);
            UpdateMedicamento(medicamento);
        }

        internal void Termino(Medicamento medicamento, DateTime termino)
        {
            medicamento.DataHoraTermino = termino;
            UpdateMedicamento(medicamento);
        }

        // Interface
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
