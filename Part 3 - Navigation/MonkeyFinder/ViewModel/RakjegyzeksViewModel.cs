using MonkeyFinder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyFinder.ViewModel
{
    public partial class RakjegyzeksViewModel:BaseViewModel
    {
        public ObservableCollection<Rakjegyzek> Rakjegyzeks { get; } = new();
        public string AktRakjegy { get; set; }
        readonly RakjegyzekService rakjegyzekService;
        public Command LoadItemsCommand { get; }
        public Command PutItemToRaklapCommand { get; }
        public RakjegyzeksViewModel(RakjegyzekService rakjegyzekService)
        {
            Title = "Rakjegyzek lista";
            this.rakjegyzekService = rakjegyzekService;
            LoadItemsCommand = new Command(async () => await GetRakjegyzeksAsync());
            PutItemToRaklapCommand = new Command(OnPutRaklap);
        }

        private async void OnPutRaklap()
        {
            Debug.WriteLine("OnPutRaklap()");
            //Items.First(i => i.Rakjegy == AktRakjegy).Felrakva = true;
            int? id = Rakjegyzeks.FirstOrDefault(i => i.Rakjegy == AktRakjegy)?.Id ?? null;
            if (id is null)
            {
                Debug.WriteLine("Hibás rakjegyzek!");
                return;
            }
            await rakjegyzekService.PutItemToFelrakAsync((int)id);
            //await rakjegyzekService.PutItemToFelrakAsync(AktRakjegy);
            Debug.WriteLine($"{AktRakjegy} felrakva.");//await DataStore.GetItemsAsync();//SelectedItem.Felrakva = true;
            //await GetRakjegyzeksAsync();
            await Shell.Current.DisplayPromptAsync("Sikeres felrakás!", $"{AktRakjegy} felrakva.", "OK");
            await GetRakjegyzeksAsync(); 
            //invoke OnAppearing//await Shell.Current.GoToAsync(nameof(View.MainPage));
            //throw new NotImplementedException();
        }

        async public void OnAppearing()
        {
            IsBusy = false;
            //SelectedItem = null;
            await GetRakjegyzeksAsync();
        }

        [RelayCommand]
        async Task GetRakjegyzeksAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var rakjegyzeks = await rakjegyzekService.GetRakjegyzeks();

                if (Rakjegyzeks.Count != 0)
                    Rakjegyzeks.Clear();
                int i = 0;
                foreach (var rakjegyzek in rakjegyzeks)
                {
                    //if (++i > 5)
                      //  break;
                    Rakjegyzeks.Add(rakjegyzek);
                }
                //OnPropertyChanged(nameof(Rakjegyzeks));

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
