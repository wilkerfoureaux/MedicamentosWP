using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicamentosWP.Helpers
{
    class DosesVM
    {
        public int key { get; set; }
        public string value { get; set; }

        private ObservableCollection<DosesVM> _doses { get; set; }

        public DosesVM()
        {
            _doses = new ObservableCollection<DosesVM>();

            _doses.Add(new DosesVM(1, "Única"));
            _doses.Add(new DosesVM(2, "Cápsula"));
            _doses.Add(new DosesVM(3, "Comprimido"));
            _doses.Add(new DosesVM(4, "Gotas"));
            _doses.Add(new DosesVM(5, "Intravenoso"));
            _doses.Add(new DosesVM(6, "Intramuscular"));
            _doses.Add(new DosesVM(7, "Inalável"));
            _doses.Add(new DosesVM(8, "Tópico"));
            _doses.Add(new DosesVM(9, "Outro"));
        }

        private DosesVM(int key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public ObservableCollection<DosesVM> Doses()
        {
            return _doses;
        }
    }
}
