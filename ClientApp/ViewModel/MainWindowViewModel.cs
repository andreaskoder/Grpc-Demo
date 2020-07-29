using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using So.Demo.Common.Entities;
using So.Demo.Common.Requests;
using So.Demo.Common.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace So.GrpcDemo.ClientApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ICustomerServiceClient _customerService;
        private readonly Dispatcher _dispatcher;

        public MainWindowViewModel(ICustomerServiceClient customerService)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _customerService = customerService;
            Customers = new ObservableCollection<Customer>();
            RefreshCommand = new RelayCommand<string>(Refresh);
            Refresh(10);
        }

        public ObservableCollection<Customer> Customers { get; }

        public string ServiceName => _customerService.Name;

        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    RaisePropertyChanged();
                }
            }
        }
        private bool _isBusy;
        private string _duration;
        private string _status;
        private string _serverDuration;

        public RelayCommand<string> RefreshCommand { get; }

        private async void Refresh(string amountString)
        {
            if (int.TryParse(amountString, out var amount))
                await Refresh(amount);
        }

        private async Task Refresh(int amount)
        {
            IsBusy = true;
            Status = "Busy";
            try
            {
                Customers.Clear();
                var request = new CustomersRequest { CustomersCount = amount };
                var stopwatch = Stopwatch.StartNew();
                var response = await _customerService.GetCustomersAsync(request);
                stopwatch.Stop();
                response.Customers
                    .ForEach(Customers.Add);
                Duration = $"{stopwatch.ElapsedMilliseconds} ms";
                ServerDuration = $"{response.Duration} ms";
                Status = "OK";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                Status = ex.Message;
            }
            IsBusy = false;
        }

        public string Duration
        {
            get { return _duration; }
            private set
            {
                if (_duration != value)
                {
                    _duration = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string ServerDuration
        {
            get { return _serverDuration; }
            private set
            {
                if (_serverDuration != value)
                {
                    _serverDuration = value;
                    RaisePropertyChanged();
                }
            }
        }


        public string Status
        {
            get { return _status; }
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged();
                }
            }
        }


    }
}
