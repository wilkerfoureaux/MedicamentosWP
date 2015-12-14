using MedicamentosWP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using SQLite;

namespace MedicamentosWP.Helpers
{
    class MedicamentosDataRepository : IDataRepository
    {
        private static readonly string _dbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MedicamentosWP.sqlite");
        private ObservableCollection<Medicamento> _medicamentos;

        public MedicamentosDataRepository()
        {
            Initialize();
        }

        private void Initialize()
        {
            // throw new NotImplementedException();

            using (var db = new SQLite.SQLiteConnection(_dbPath))
            {
                db.CreateTable<Medicamento>();

                if (db.ExecuteScalar<int>(
                    "select count(1) from Medicamento" ) == 0)
                {

                    db.RunInTransaction(() =>
                    {
                        db.Insert(new Medicamento()
                        {
                            Nome = "Paracetamol",
                            DataHoraInicio = DateTime.Now,
                            Dosagem = 10,
                            Veiculo = 1
                        });
                        db.Insert(new Medicamento()
                        {
                            Nome = "Neosaldina",
                            DataHoraInicio = DateTime.Now,
                            Dosagem = 9.99,
                            Veiculo = 2
                        });
                    });
                }
                else
                {
                    Load();
                }
            }
        }

        public Task Add(Medicamento medicamento)
        {
            // throw new NotImplementedException();

            _medicamentos.Add(medicamento);

            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.InsertAsync(medicamento);
        }

        public async Task<ObservableCollection<Medicamento>> Load()
        {
            // throw new NotImplementedException();

            var asyncConnection = new SQLiteAsyncConnection(_dbPath);

            _medicamentos = new ObservableCollection<Medicamento>(
                await asyncConnection.QueryAsync<Medicamento>(
                    "select * from Medicamento"
                    ));
            return _medicamentos;
        }

        public Task Remove(Medicamento medicamento)
        {
            // throw new NotImplementedException();

            _medicamentos.Remove(medicamento);

            var connection = new SQLiteAsyncConnection(_dbPath);
            return connection.DeleteAsync(medicamento);
        }

        public Task Update(Medicamento medicamento)
        {
            // throw new NotImplementedException();

            var oldMedicamento = _medicamentos.FirstOrDefault(
                m => m.Id == medicamento.Id );

            if (oldMedicamento == null)
            {
                throw new System.ArgumentException(
                    "Medicamento não encontrado!");
            }

            _medicamentos.Remove(oldMedicamento);
            _medicamentos.Add(medicamento);

            var connection = new SQLiteAsyncConnection(_dbPath);

            return connection.UpdateAsync(medicamento);
        }
    }
}
