using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiApp.Services;
using SkiApp.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace SkiApp.ViewModels
{
    public class TipsViewModel : ViewModelBase
    {
        private SkiServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ICommand GetTipsCommand { get; }
        private ObservableCollection<TipInfo> tipCol;
        public ObservableCollection<TipInfo> TipCol 
        { get => tipCol;  
           set { tipCol = value; OnPropertyChanged(); } 
        }
        public async void GetAllTips()
        {
            List<TipInfo> tips = new List<TipInfo>();
            tips = await this.proxy.GetTips();
            if (tips!=null)
                TipCol = new ObservableCollection<TipInfo>(tips);
        }
        public TipsViewModel(SkiServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            TipCol = new ObservableCollection<TipInfo> ();
            GetAllTips();
            
        }

    }
}
