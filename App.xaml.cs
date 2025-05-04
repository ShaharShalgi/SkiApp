using SkiApp.Models;
using SkiApp.ViewModels;
using SkiApp.Views;
using SkiApp.Services;
using Windows.System;
namespace SkiApp
{
    public partial class App : Application
    {

        public VisitorInfo? LoggedInUser { get; set; }
        public List<VisitorInfo> Visitors = new List<VisitorInfo>();
        public List<PostPhotoInfo> PostPhotos = new List<PostPhotoInfo>();

        private SkiServiceWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, SkiServiceWebAPIProxy proxy)
        {
            this.proxy = proxy;
            InitializeComponent();
            Homepage? v = serviceProvider.GetService<Homepage>();
            //SignUpView? v = serviceProvider.GetService<SignUpView>();
            LoadBasicData();
            MainPage = new NavigationPage(v);
        }
     
        public async void LoadBasicData()
        {
            //this gets the data of the statuses and food types
            Visitors = await this.proxy.GetAllUsers();
            PostPhotos = await this.proxy.GetAllPostPhotos();
           
        }
    }
}
    

