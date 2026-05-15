using StudentTrackerClient.Services;
using StudentTrackerClient.ViewModels.Basics;
using StudentTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using StudentTrackerLib.Exceptions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StudentTrackerClient.Windows;

namespace StudentTrackerClient.ViewModels
{
    internal class AuthenticationWindowViewModel : BaseViewModel
    {
        private ServerApi _serverApi;
        private AuthenticationWindow _window;
        
        public string Name { get; set; }
        public string Password { get; set; }
        public Teacher? AuthenticatedTeacher { get; set; }

        public ICommand OkCommand { get; set; }
        public AuthenticationWindowViewModel() 
        {
            Name = string.Empty;
            Password = string.Empty;
            AuthenticatedTeacher = null;
            OkCommand = new Command<string>(SendAuthenticationRequest);
        }
        public AuthenticationWindowViewModel(ServerApi serverApi, AuthenticationWindow window) : this()
        {
            _serverApi = serverApi;
            _window = window;
        }

        private async void SendAuthenticationRequest(string _)
        {
            try
            {
                AuthenticatedTeacher = await _serverApi.AuthenticateTeacher
                    (new Teacher()
                    {
                        Name = Name,
                        PasswordHash = Convert.ToHexString(SHA256.HashData
                            (Encoding.UTF8.GetBytes(Password)))
                    },
                    CancellationToken.None);

            }
            catch (AuthenticationException)
            {
                MessageBox.Show("ФИО или пароль неверны.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch
            {
                MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            //if (AuthenticatedTeacher == null)
            //{
            //    MessageBox.Show("Получить данные с сервера не удалось :(", "Ошибка!",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //else
            //{
                _window.DialogResult = true;
                _window.Close();
                return;
            //}
        }
        internal void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox pb)
            {
                Password = pb.Password;
            }
        }
    }
}
