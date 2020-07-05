using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using So.Demo.Common.Entities;
using So.Demo.Grpc.Common.Requests;
using So.Demo.Grpc.Common.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace So.GrpcDemo.ClientApp.ViewModel
{
    public class MainWindowViewModel: ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMultiplyService _multiplyService;
        private readonly Dispatcher _dispatcher;

        public MainWindowViewModel(ICustomerService customerService, IMultiplyService multiplyService)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _customerService = customerService;
            _multiplyService = multiplyService;
            Customers = new ObservableCollection<Customer>();
            Refresh();
        }

        public ObservableCollection<Customer> Customers { get; }

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
        private string _result;

        public RelayCommand RefreshCommand { get; }
        private async Task Refresh()
        {
            IsBusy = true;
            try
            {
                var result = await _multiplyService.MultiplyAsync(new MultiplyRequest { X = 3, Y = 14 });
                Result = $"{result.Result}";
                
                Customers.Clear();
                var request = new CustomersRequest { CustomersCount = 10 };
                var customers = await _customerService.GetCustomersAsync(request);
                customers.Customers
                    .ForEach(Customers.Add);
                Result = $"{customers.Customers.Count}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            IsBusy = false;
        }

        public string Result
        {
            get { return _result; }
            private set
            {
                if (_result != value)
                {
                    _result = value;
                    RaisePropertyChanged();
                }
            }
        }

    }
}
