using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
//using Android.Media;
using SkiApp.Models;
using SkiApp.Services;
using SkiApp.Views;
//using static Java.Util.Jar.Attributes;

namespace SkiApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private SkiServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ProfileViewModel(SkiServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            VisitorInfo u = ((App)Application.Current).LoggedInUser;
            Username = u.Username;
            Gender = u.Gender;
            Email = u.Email;
            Password = u.Pass;
            IsProfessional = u.IsPro;
            Username2 = u.Username;
            Gender2 = u.Gender;
            Email2 = u.Email;
            UserID = u.UserID;
           
            if (IsProfessional)
            {
                ShowProff(u.UserID);
            }
            
            UpdateRequestCommand = new Command(RequestUpdate);
            SaveCommand = new Command(OnSave);
            UpgradeProCommand = new Command(Upgrade);
            OnUpgradeCommand = new Command(OnUpgrade);
            OnPostCommand = new Command(OnPost);
            NameError = "Name is required";
            UpdateRequest = false;
            EmailError = "Email is required";
            PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
            AddPhotoCommand = new Command(AddPhoto);
            UploadPostCommand = new Command(UploadPost);
            this.photos = new ObservableCollection<string>();

        }
        private bool isProfessional;
        public bool IsProfessional
        {
            get { return isProfessional; }
            set
            {
                isProfessional = value;
                OnPropertyChanged();
            
            }
        }


        
        public bool IsProfessional2
        {
            get { return !isProfessional; }
           
        }

        private async void ShowProff(int Id)
        {
            
            ProfessionalInfo p = new ProfessionalInfo();
            p = await proxy.GetPro(Id);
            Loc = p.Loc;
            Price = p.Price;
            Txt = p.Txt;
            Rating = p.Rating;
            TypeId = p.TypeId;
            if (TypeId == 1)
            {
                Type = "Coach";
            }
            else
            {
                Type = "Manager";
            }
        }

        private int userID;

        public int UserID
        {
            get => userID;
            set
            {
                userID = value;
              
                OnPropertyChanged("UserID");
            }
        }

        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                //ValidateName();
                OnPropertyChanged("Username");
            }
        }
        private string username2;

        public string Username2
        {
            get => username2;
            set
            {
                username2 = value;
                //ValidateName();
                OnPropertyChanged("Username2");
            }
        }
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }
        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }
        private void ValidateName()
        {
            this.ShowNameError = string.IsNullOrEmpty(Username);
        }

       



        private string gender;

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        private string gender2;

        public string Gender2
        {
            get => gender2;
            set
            {
                gender2 = value;
                OnPropertyChanged("Gender2");
            }
        }

        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                //ValidatePassword();
                OnPropertyChanged("Password");
            }
        }

        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }
        private string passwordError;

        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }
        private void ValidatePassword()
        {
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 4 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
        }
        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                //ValidateEmail();
                OnPropertyChanged("Email");
            }
        }
        private string email2;

        public string Email2
        {
            get => email2;
            set
            {
                email2 = value;
                //ValidateEmail();
                OnPropertyChanged("Email2");
            }
        }
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }
        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        private bool updateRequest;
        public bool UpdateRequest
        {
            get { return updateRequest; }
            set
            {
                updateRequest = value;
                OnPropertyChanged();
                ((Command)UpdateRequestCommand).ChangeCanExecute();
               
            }
        }
        public ICommand UpdateRequestCommand { get; }

        private async void RequestUpdate()
        {
            UpdateRequest = !UpdateRequest;

        }


        public ICommand SaveCommand { get; }


        private async void OnSave()
        {
            ValidateName();
            ValidateEmail();
            ValidatePassword();

           


            if (!ShowPasswordError && !ShowEmailError && !ShowNameError)
            {


                VisitorInfo theUser = ((App)App.Current).LoggedInUser;
                theUser.Username = Username;
                theUser.Gender = Gender;
                theUser.Email = Email;
                theUser.Pass = Password;
                
                InServerCall = true;
                
                bool success = await proxy.UpdateUser(theUser);

                if (success)
                {
                    InServerCall = false;
                    UpdateRequest = false;
                    Username2 = theUser.Username;
                    Gender2 = theUser.Gender;
                    Email2 = theUser.Email;
                    await Shell.Current.DisplayAlert("Save Profile", "Profile saved successfully", "ok");
                    
                }
                else
                {
                    InServerCall = false;
                    //If the registration failed, display an error message
                    string errorMsg = "Save Profile failed. Please try again.";
                    await Shell.Current.DisplayAlert("Save Profile", errorMsg, "ok");
                }

            }

           

        }

        private string loc;
        public string Loc
        {
            get => loc;
            set
            {
                loc = value;
                OnPropertyChanged("Loc");
            }
        }
        private int? typeId;
        public int? TypeId
        {
            get => typeId;
            set
            {
                typeId = value;
                OnPropertyChanged("TypeId");
            }
        }
        private double? price;
        public double? Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        private double? rating;
        public double? Rating
        {
            get => rating;
            set
            {
                rating = value;
                OnPropertyChanged("Rating");
            }
        }
        private string txt;
        public string Txt
        {
            get => txt;
            set
            {
                txt = value;
                OnPropertyChanged("Txt");
            }
        }
        private string type;
        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public ICommand UpgradeProCommand { get; }
        public ICommand OnUpgradeCommand { get; }
        public ICommand OnPostCommand { get; }

        private void OnPost()
        {
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<UploadPost>());
        }
        private void OnUpgrade()
        {

            // Navigate to the Upgrade View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<UpgradePro>());
        }
        public async void Upgrade()
        {

            VisitorInfo theUser = ((App)App.Current).LoggedInUser;
            theUser.IsPro = true;
           
            InServerCall = true;
            bool success = await proxy.UpdateUser(theUser);
            if (success) 
            {
                var newPro = new ProfessionalInfo
                {
                    Loc = this.Loc,
                    TypeId = this.TypeId + 1,
                    Price = this.Price,
                    UserId = theUser.UserID,
                    Txt = this.Txt,
                    Rating = 0,
                    RaterNum = 0
                };
                InServerCall = true;
                newPro = await proxy.SignUpPro(newPro);
                InServerCall = false;

                ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Profile>());

            }
            else
            {
                InServerCall = false;
                //If the registration failed, display an error message
                string errorMsg = "Upgrade failed. Please try again.";
                await Shell.Current.DisplayAlert("Upgrade Profile", errorMsg, "ok");
            }
           

        }
        private ObservableCollection<string>? photos;
        public ObservableCollection<string>? Photos
        {
            get { return photos; }
            set { photos = value; OnPropertyChanged(); }
        }
        public ICommand AddPhotoCommand { get; set; }


        private async void AddPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.Photos.Add(result.FullPath);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public ICommand UploadPostCommand {  get; set; }
        private async void UploadPost()
        {

            ProfessionalInfo theUser = await proxy.GetPro(UserID);
            VisitorInfo u = await proxy.GetUser(UserID);
            u.Username = Username;
                theUser.Price = Price;
                theUser.Loc = Loc;
                theUser.Txt = Txt;
              

                InServerCall = true;
            await proxy.UpdateUser(u);
           bool success = await proxy.UpdatePro(theUser);
            if (!success)
            {

                string errorMsg = "Post failed. Please try again.";
                await Shell.Current.DisplayAlert("upload Post", errorMsg, "ok");
                InServerCall = false;

            }
            else
            {

                VisitorInfo? t = await proxy.GetUser(UserID);

                if (t != null)
                {
                    int fail = 0;
                    foreach (string path in this.Photos)
                    {
                        t = await proxy.UploadPostImage(path, t.UserID);

                        if (t == null) ++fail;
                    }

                    if (fail > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"Post was uploaded but {fail} images fail to be uploaded", "ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Success!", "Post was added successfully", "ok");


                    }
                    ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Profile>());

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Something went wrong, try again later!", "ok");
                }



                InServerCall = false;
            }


        }
               
            



        }

    }

    
