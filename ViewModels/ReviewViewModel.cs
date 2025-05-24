using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiApp.Views;
using SkiApp.Services;
using SkiApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
//using static AndroidX.ConstraintLayout.Core.Motion.Utils.HyperSpline;
//using Android.Util;

namespace SkiApp.ViewModels
{
    public class ReviewViewModel : ViewModelBase
    {
        private SkiServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;


        public bool Coach
        {
            get => pro.TypeId==1;
            
        }

        // Properties
        private string txt;
        public string Txt {
            get => txt;
            set
            {
                txt = value;
                OnPropertyChanged();

            }
        }
        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();

            }
        }
        private int star;
        public int Star
        {
            get => star;
            set
            {
                star = value;
                OnPropertyChanged();

            }
        }
        private double? rating;
        public double? Rating
        {
            get => rating;
            set
            {
                rating = value;
                OnPropertyChanged();

            }
        }
        private double raterNum;
        public double RaterNum
        {
            get => raterNum;
            set
            {
                raterNum = value;
                OnPropertyChanged();

            }
        }
        private int reviewerID;
        public int ReviewerID
        {
            get => reviewerID;
            set
            {
                reviewerID = value;
                OnPropertyChanged();

            }
        }
        private ProfessionalInfo pro;
        public ProfessionalInfo Pro
        {
            get => pro;
            set
            {
                pro = value;
                OnPropertyChanged();

                // Load reviews when coach is set
                _ = LoadReviews();
            }
        }

        private ObservableCollection<ReviewInfo> reviewCol;
        public ObservableCollection<ReviewInfo> ReviewCol
        {
            get => reviewCol;
            set { reviewCol = value; OnPropertyChanged(); }
        }

        
        private ObservableCollection<string>? photos;
        public ObservableCollection<string>? Photos
        {
            get { return photos; }
            set { photos = value; OnPropertyChanged(); }
        }

        // Command to refresh reviews
        public ICommand RefreshCommand { get; }
        public ICommand OnReviewCommand { get; }
        public ICommand UploadReviewCommand { get; }
        public ICommand ReturnCommand { get; }

        private void Return()
        {
            if(Coach)
                ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Coaches>());
            else
                ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<Resorts>());

        }


        private void OnReview()
        {
            // Get the UploadReview page
            var uploadReviewPage = serviceProvider.GetService<UploadReview>();

            // Set THIS viewmodel as the binding context for the upload page
            if (uploadReviewPage != null)
            {
                uploadReviewPage.BindingContext = this; // Pass the same viewmodel instance
            }

    ((App)Application.Current).MainPage.Navigation.PushAsync(uploadReviewPage);
        }
        private void AfterReview()
        {
            // Get the UploadReview page
            var ReviewPage = serviceProvider.GetService<Review>();

            // Set THIS viewmodel as the binding context for the upload page
            if (ReviewPage != null)
            {
                ReviewPage.BindingContext = this; // Pass the same viewmodel instance
            }

    ((App)Application.Current).MainPage.Navigation.PushAsync(ReviewPage);
        }
        // Constructor
        public ReviewViewModel(SkiServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            ReviewCol = new ObservableCollection<ReviewInfo>();

            // Initialize commands
            RefreshCommand = new Command(async () => await LoadReviews());
            OnReviewCommand = new Command(OnReview);
            AddPhotoCommand = new Command(AddPhoto);
            UploadReviewCommand = new Command(UploadReview);
            ReturnCommand = new Command(Return);
            VisitorInfo u = ((App)Application.Current).LoggedInUser;
            ReviewerID = u.UserID;
            this.photos = new ObservableCollection<string>();
           
        }

        // Load reviews for the selected coach
        private async Task LoadReviews()
        {
            if (Pro == null)
                return;

            try
            {
                InServerCall = true;

                // Call your API to get reviews for this coach
                // Assuming you have (or will add) a method to get reviews by coach ID
                List<ReviewInfo> proReviews = await proxy.GetReviewsByPro(Pro.UserId);

                if (proReviews != null)
                {
                    ReviewCol = new ObservableCollection<ReviewInfo>(proReviews);
                }
                InServerCall = false;
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error loading reviews: {ex.Message}");
            }

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
        private async void UploadReview()
        {

            ProfessionalInfo reciever = pro;
            RaterNum = (double) reciever.RaterNum;
            Rating = reciever.Rating;
            Rating = (Rating * RaterNum + Star + 1) / (RaterNum + 1);
            reciever.Rating = Rating;
            reciever.RaterNum++;
            InServerCall = true;
            await proxy.UpdatePro(reciever);
            ReviewInfo review = new ReviewInfo();
            review.Rating = Star + 1;
            review.Title = Title;
            review.Txt = Txt;
            review.SenderId = ReviewerID;
            review.RecieverId = reciever.UserId;
             ReviewInfo r = await proxy.UploadReview(review);

            if (r == null)
            {

                string errorMsg = "Review failed. Please try again.";
                await Shell.Current.DisplayAlert("upload Review", errorMsg, "ok");
                InServerCall = false;

            }
            else
            {

                ReviewInfo? t = r;

                if (t != null)
                {
                    int fail = 0;
                    foreach (string path in this.Photos)
                    {
                        t = await proxy.UploadReviewImage(path, t.ReviewId);

                        if (t == null) ++fail;
                    }

                    if (fail > 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"Review was uploaded but {fail} images failed to be uploaded", "ok");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Success!", "Review was added successfully", "ok");


                    }
                    AfterReview();

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
