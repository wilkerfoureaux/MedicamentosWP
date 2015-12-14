using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicamentosWP.Models
{
    interface IDataRepository
    {
        // Create
        Task Add(Medicamento medicamento);

        // Retrieve
        Task<ObservableCollection<Medicamento>> Load();

        // Update
        Task Update(Medicamento medicamento);

        // Delete
        Task Remove(Medicamento medicamento);
    }
}
