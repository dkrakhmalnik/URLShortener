using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using URLShortener.Models;
using URLShortener.Services;

namespace URLShortener.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private IUrlShortener _urlShortener;
        private string _fullUrl;
        public ICommand AddShortUrlCommand { get; }
        public ICommand OpenLinkCommand { get; }
        public ObservableCollection<UrlData> Urls { get;} = new ObservableCollection<UrlData>();
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel(IUrlShortener urlShortener)
        {
            _urlShortener = urlShortener;
            AddShortUrlCommand = new Command(async () => await AddShortUrl());
            OpenLinkCommand = new Command<string>(async (url) => await OpenLink(url));
        }

        public string FullUrl
        {
            get { return _fullUrl; }
            set
            {
                _fullUrl = value;
                OnPropertyChanged();
            }
        }

        private async Task GetUrls()
        {
            Urls.Clear();
            (await _urlShortener.GetUrls()).ToList().ForEach(Urls.Add);
        }

        private async Task AddShortUrl()
        {
            try
            {
                await _urlShortener.GetShortUrl(FullUrl);
                FullUrl = String.Empty;
                await GetUrls();
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private async Task OpenLink(string url)
        {
            await Browser.Default.OpenAsync(url);
        }

        public async Task Init()
        {
            await GetUrls();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
