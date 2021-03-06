﻿using MedicamentosWP.Common;
using MedicamentosWP.Helpers;
using MedicamentosWP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MedicamentosWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateDelete_Medicamentos : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private IDataRepository _data = new MedicamentosDataRepository();
        private MedicamentosVM _medicamentosVM;

        public UpdateDelete_Medicamentos()
        {
            this.InitializeComponent();

            _medicamentosVM = new MedicamentosVM(_data);
            _medicamentosVM.Initialize();

            // ComboBox de Doses
            DosesVM dosesVM = new DosesVM();

            cbx_Dose.DisplayMemberPath = "value";
            cbx_Dose.ItemsSource = dosesVM.Doses();

            // ComboBox de Tipos
            TipoMedicamentoVM tipoMedicamentoVM = new TipoMedicamentoVM();

            cbx_Tipo.DisplayMemberPath = "value";
            cbx_Tipo.ItemsSource = tipoMedicamentoVM.TiposMedicamento();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            this.DataContext = (Medicamento)e.Parameter;

            var medicamento = (Medicamento)e.Parameter;

            this.Title.Text = medicamento.Nome;

            cbx_Dose.SelectedIndex = medicamento.Dose - 1;
            cbx_Tipo.SelectedIndex = medicamento.Tipo - 1;
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            Medicamento medicamento = (Medicamento)DataContext;

            medicamento.Nome = tbx_Nome.Text;
            medicamento.Marca = tbx_Marca.Text;
            TipoMedicamentoVM tmm = (TipoMedicamentoVM)cbx_Tipo.SelectedItem;
            medicamento.Tipo = tmm.key;
            medicamento.Dosagem = Double.Parse(tbx_Dosagem.Text);
            DosesVM dvm = (DosesVM) cbx_Dose.SelectedItem;
            medicamento.Dose = dvm.key;
            medicamento.DataHoraInicio = dtp_DataInicio.Date.DateTime;
            medicamento.DataHoraProxima = dtp_DataProximo.Date.DateTime;
            medicamento.DataHoraTermino = dtp_DataTermino.Date.DateTime;
            medicamento.DataHoraUsado = dtp_DataUsado.Date.DateTime;
            medicamento.Observacao = tbx_Observacao.Text;

            _medicamentosVM.UpdateMedicamento(medicamento);

            // lista o medicamento assim que salva
            Frame.Navigate(typeof(MainPage));
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            _medicamentosVM.DeleteMedicamento((Medicamento)DataContext);
            
            // lista o medicamento assim que salva
            Frame.Navigate(typeof(MainPage));
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
