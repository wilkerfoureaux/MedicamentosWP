using MedicamentosWP.Helpers;
using MedicamentosWP.Models;
using MedicamentosWP.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace MedicamentosWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private IDataRepository data = new MedicamentosDataRepository();

        private MedicamentosVM _medicamentosVM;

        public MainPage()
        {
            this.InitializeComponent();

            _medicamentosVM = new MedicamentosVM(data);
            _medicamentosVM.Initialize();
            DataContext = _medicamentosVM;

            this.NavigationCacheMode = NavigationCacheMode.Required;
    
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add_Medicamento), _medicamentosVM);
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Medicamentos.SelectedItem != null)
            {
                Frame.Navigate(typeof(UpdateDelete_Medicamentos), lst_Medicamentos.SelectedItem);
            }
            else
            {
                ShowMessage("Nenhum Medicamento Selecionado!");
            }
        }

        private async void ShowMessage(string message)
        {
            MessageDialog msgbox = new MessageDialog(message);
            //Calling the Show method of MessageDialog class  
            //which will show the MessageBox  
            await msgbox.ShowAsync();
        }

        private void btn_Finalize_Click(object sender, RoutedEventArgs e)
        {
            if (lst_Medicamentos.SelectedItem != null)
            {
                Medicamento med = (Medicamento)lst_Medicamentos.SelectedItem;
                _medicamentosVM.Termino(med, DateTime.Now);
            }
            else
            {
                ShowMessage("Nenhum Medicamento Selecionado!");
            }

            DataContext = null;
            DataContext = _medicamentosVM;
        }
    }
}
