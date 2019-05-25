namespace App.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Services;
    using Xamarin.Forms;
    using Models;
    using app.Views;

    public class NotesViewModel : BaseViewModel
    {
        #region Attributes
        string nota;
       
        bool isrunning;
        bool isenabled;
        ApiService apiService;
        #endregion

        #region Properties
        public string Notas
        {
            get
            {
                return this.Notas;
            }
            set
            {
                SetValue(ref this.nota, value);
            }
        }
       
        public bool IsRunning
        {
            get
            {
                return this.isrunning;
            }
            set
            {
                SetValue(ref this.isrunning, value);
            }

        }
        public bool IsEnabled
        {
            get
            {
                return this.isenabled;
            }
            set
            {
                SetValue(ref this.isenabled, value);
            }
        }
        #endregion

        #region Commands
        public ICommand RegistroCommand
        {
            get
            {
                return new RelayCommand(cmdRegistro);
            }
        }

        private async void cmdRegistro()
        {
            if (String.IsNullOrEmpty(nota))
            {
                await App.Current.MainPage.DisplayAlert("Email empty",
                                "Please enter your email",
                                "Accept");
                return;
            }
          

            IsRunning = true;
            IsEnabled = false;

            var conexion = await this.apiService.CheckConnection();
            if (!conexion.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                   "ERROR",
                   conexion.Message,
                   "Accept");
                return;
            }

            Response rsp2 = await this.apiService.Post<Response>(
                "https://notesplc.azurewebsites.net/",
             
                );
                 


            
        }
        #endregion

        public NotesViewModel()
        {

        }
    }
}