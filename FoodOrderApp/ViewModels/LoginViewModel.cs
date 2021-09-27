using System;
using System.Threading.Tasks;
using FoodOrderApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        private string _Username;
        public string Username
        {
            set
            {
                this._Username = value;
                OnPropertyChanged("Username");
            }
            get
            {
                return this._Username;
            }
        }
        private string _Password;
        public string Password
        {
            set
            {
                this._Password = value;
                OnPropertyChanged("Password");
            }
            get
            {
                return this._Password;
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                this._IsBusy = value;
                OnPropertyChanged("IsBusy");
            }
            get
            {
                return this._IsBusy;
            }
        }

        private bool _Result;
        public bool Result
        {
            set
            {
                this._Result = value;
                OnPropertyChanged("Result");
            }
            get
            {
                return this._Result;
            }
        }



        public Command LoginCommand { get; set; }
        public  Command RegisterCommand { get; set; }



       public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userServices = new UserService();
                Result = await userServices.RegisterUser(Username, Password);
                if (Result)
                {
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invlid Username or Password", "ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("error", ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private async Task LoginCommandAsync()
        {

            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userServices = new UserService();
                Result = await userServices.LoginUser(Username, Password);
                if (Result)
                {
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invlid Username or Password", "ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("error", ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
            
    }

    internal class ProductsView : Page
    {
    }
}
