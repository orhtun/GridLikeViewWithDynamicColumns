using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GridLikeViewWithDynamicColumns.Model;
using System.Collections.ObjectModel;

namespace GridLikeViewWithDynamicColumns.ViewModel
{
    public class SupplierListViewModel : ViewModelBase
    {
        public ObservableCollection<SupplierViewModel> Suppliers
        {
            get { return _suppliers; }
        }

        public RelayCommand InsertSupplierCommand { get; }

        private ObservableCollection<SupplierViewModel> _suppliers = new ObservableCollection<SupplierViewModel>();

        public SupplierListViewModel()
        {
            InsertSupplierCommand = new RelayCommand(InsertSupplierCommandExecute, InsertSupplierCommandCanExecute);
        }

        public bool InsertSupplierCommandCanExecute()
        {
            return true;
        }

        public void InsertSupplierCommandExecute()
        {
            var supplier = new SupplierViewModel() {Name = string.Format("Supplier {0}", Suppliers.Count+1)};

            var r = new Random();

            var itemCnt = r.Next(5, 10);
            var testSkipItem = r.Next(1, itemCnt);

            for (int j = 1; j <= itemCnt; j++)
            {
                if (j == testSkipItem)

                    continue;

                var item = new ItemViewModel() { Name = string.Format("Item {0}", j), Cost = r.Next(1, 100) };

                supplier.Items.Add(item);
            }

            Suppliers.Add(supplier);
            Messenger.Default.Send(new SupplierAddedMessage());
        }
    }
}