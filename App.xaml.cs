using SkiApp.Models;
using SkiApp.ViewModels;
using SkiApp.Views;
using SkiApp.Services;
//using Windows.System;
namespace SkiApp
{
    public partial class App : Application
    {

        public VisitorInfo? LoggedInUser { get; set; }
        public List<VisitorInfo> Visitors = new List<VisitorInfo>();
        public List<PostPhotoInfo> PostPhotos = new List<PostPhotoInfo>();
        public List<ReviewPhotoInfo> ReviewPhotos = new List<ReviewPhotoInfo>();
        public async void GetPostPhotoPath()
        {
            foreach(PostPhotoInfo p in PostPhotos)
            {
                p.PhotoUrlPath = await this.proxy.GetPathByPhotoID(p.PhotoId);
            }
        }
        public async void GetReviewPhotoPath()
        {
            foreach (ReviewPhotoInfo p in ReviewPhotos)
            {
                p.PhotoUrlPath = await this.proxy.GetReviewPathByPhotoID(p.PhotoId);
            }
        }

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
            ReviewPhotos = await this.proxy.GetAllReviewPhotos();
            GetPostPhotoPath();
            GetReviewPhotoPath();


        }
        public async Task RefreshAppData()
        {
            try
            {
                // Refresh visitors and post photos data
                Visitors = await this.proxy.GetAllUsers();
                PostPhotos = await this.proxy.GetAllPostPhotos();
                ReviewPhotos = await this.proxy.GetAllReviewPhotos();

                // Update photo paths
                foreach (PostPhotoInfo p in PostPhotos)
                {
                    p.PhotoUrlPath = await this.proxy.GetPathByPhotoID(p.PhotoId);
                }
                foreach (ReviewPhotoInfo p in ReviewPhotos)
                {
                    p.PhotoUrlPath = await this.proxy.GetReviewPathByPhotoID(p.PhotoId);
                }
            }
            catch (Exception ex)
            {
                // Consider logging or displaying an error message
                Console.WriteLine($"Error refreshing app data: {ex.Message}");
            }
        }
    }
}
    

