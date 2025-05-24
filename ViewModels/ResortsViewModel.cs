using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SkiApp.Models;
using SkiApp.Services;
using SkiApp.Views;

namespace SkiApp.ViewModels
{
    public class ResortsViewModel : ViewModelBase
    {
        
        private SkiServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        //public ICommand SortTipsCommand { get; }
        public ICommand AdaptCommand { get; }
        private ObservableCollection<ProfessionalInfo> postCol;
        public ObservableCollection<ProfessionalInfo> PostCol
        {
            get => postCol;
            set { postCol = value; OnPropertyChanged(); }
        }
        public async Task GetAllResorts()
        {
            try
            {
                InServerCall = true;
                // First refresh the app data
                if (Application.Current is App app)
                {
                    await app.RefreshAppData();
                }

                List<ProfessionalInfo> posts = await this.proxy.GetResorts();
                if (posts != null)
                    PostCol = new ObservableCollection<ProfessionalInfo>(posts);

                InServerCall = false;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error getting coaches: {ex.Message}");
            }
        }
        private int priceRange;
        public int PriceRange
        {
            get => priceRange;
            set { priceRange = value; OnPropertyChanged(); }
        }
        private int ratingRange;
        public int RatingRange
        {
            get => ratingRange;
            set { ratingRange = value; OnPropertyChanged(); }
        }
        public async Task Sort()
        {
            try
            {
                // First refresh the app data
                if (Application.Current is App app)
                {
                    await app.RefreshAppData();
                }

                bool priceAscending = priceRange != 0;
                bool ratingAscending = ratingRange != 0;

                List<ProfessionalInfo> posts2 = await this.proxy.SortResorts(priceAscending, ratingAscending);

                if (posts2 != null)
                    PostCol = new ObservableCollection<ProfessionalInfo>(posts2);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error sorting Resorts: {ex.Message}");
            }
        }
        public ICommand SortCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ViewReviewsCommand { get; }

        private async Task NavigateToReviews(ProfessionalInfo professional)
        {
            if (professional == null)
                return;

            // Get the ReviewsPage from the service provider
            var reviewsPage = serviceProvider.GetService<Review>();

            // Set the selected coach as the binding context for the reviews page
            if (reviewsPage != null && reviewsPage.BindingContext is ReviewViewModel viewModel)
            {
                viewModel.Pro = professional;
                await Shell.Current.Navigation.PushAsync(reviewsPage);
            }
        }
        public ResortsViewModel(SkiServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            PostCol = new ObservableCollection<ProfessionalInfo>();
            SortCommand = new Command(() => { _ = Sort(); });
            RefreshCommand = new Command(() => { _ = GetAllResorts(); });
            ViewReviewsCommand = new Command<ProfessionalInfo>(async (professional) => await NavigateToReviews(professional));
            _ = GetAllResorts();



        }
    }
}
