using MedicamentosWP.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicamentosWP.Helpers
{
    class Database
    {
        SQLiteConnection dbConn;

        //Create Table 
        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<Medicamento>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Retrieve the specific contact from the database. 
        public Medicamento ReadMedicamento(int medicamentoId)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var medicamento = dbConn.Query<Medicamento>("select * from Medicamentos where Id =" + medicamentoId).FirstOrDefault();
                return medicamento;
            }
        }

        // Retrieve the all contact list from the database. 
        public ObservableCollection<Medicamento> ReadMedicamentos()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Medicamento> myCollection = dbConn.Table<Medicamento>().ToList<Medicamento>();
                ObservableCollection<Medicamento> MedicamentosList = new ObservableCollection<Medicamento>(myCollection);
                return MedicamentosList;
            }
        }

        //Update existing conatct 
        public void UpdateContact(Medicamento medicamento)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var medicamentoExistente = dbConn.Query<Medicamento>("select * from Contacts where Id =" + medicamento.Id).FirstOrDefault();
                if (medicamentoExistente != null)
                {
                    medicamentoExistente.Nome = medicamento.Nome;
                    medicamentoExistente.DataHoraInicio = medicamento.DataHoraInicio;
                    medicamentoExistente.Dosagem = medicamento.Dosagem;
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(medicamentoExistente);
                    });
                }
            }
        }
        // Insert the new contact in the Contacts table. 
        public void Insert(Medicamento medicamento)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(medicamento);
                });
            }
        }

        //Delete specific contact 
        public void DeleteContact(int Id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Medicamento>("select * from Medicamento where Id =" + Id).FirstOrDefault();
                if (existingconact != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Delete(existingconact);
                    });
                }
            }
        }
        //Delete all contactlist or delete Contacts table 
        public void DeleteAllContact()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() => 
                { 
                    dbConn.DropTable<Medicamento>();
                    dbConn.CreateTable<Medicamento>();
                    dbConn.Dispose();
                    dbConn.Close();
                }); 
            }
        }
    }
}
