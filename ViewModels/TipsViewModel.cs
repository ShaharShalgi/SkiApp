using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiApp.Services;
using SkiApp.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using AndroidX.RecyclerView.Widget;

namespace SkiApp.ViewModels
{
    public class TipsViewModel : ViewModelBase
    {
        private SkiServiceWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public ICommand SortTipsCommand { get; }
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
        //public async Task<List<TipInfo>> GetTipsByDifficulty(int diff)
        //{
        //        List<TipInfo> list = await this.proxy.SortTips(diff);
        //        return list;

        //}
        private int diff;
        public int Diff
        {
            get => diff;
            set { diff = value; OnPropertyChanged(); }
        }
        public async void SortTips()
        {
            List<TipInfo> tips = new List<TipInfo>();
            tips = await this.proxy.SortTips(Diff);
            if (tips != null)
                TipCol = new ObservableCollection<TipInfo>(tips);
        }

        public TipsViewModel(SkiServiceWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            TipCol = new ObservableCollection<TipInfo> ();
            GetAllTips();
            SortTipsCommand = new Command(SortTips);
            
        }

    }
}
