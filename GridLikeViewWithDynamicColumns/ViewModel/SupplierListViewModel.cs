using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace GridLikeViewWithDynamicColumns.ViewModel
{
    public class SupplierListViewModel : ViewModelBase
    {
        public ObservableCollection<SupplierViewModel> Suppliers
        {
            get { return _suppliers; }
        }

        public RelayCommand InsertSupplierCommand;
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
            Suppliers.Add(new SupplierViewModel() { Name = string.Format("Supplier {0}", Suppliers.Count) });
        }
    }
}