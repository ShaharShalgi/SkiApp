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
    public class CoachesViewModel : ViewModelBase
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
        public async void GetAllCoaches()
        {
            List<ProfessionalInfo> posts = new List<ProfessionalInfo>();
            posts = await this.proxy.GetCoaches();
            if (posts != null)
                PostCol = new ObservableCollection<ProfessionalInfo>(posts);
        }
        
        public CoachesViewModel(SkiServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            PostCol = new ObservableCollection<ProfessionalInfo>();
            GetAllCoaches();
            //SortTipsCommand = new Command(SortTips);


        }
    }
}
