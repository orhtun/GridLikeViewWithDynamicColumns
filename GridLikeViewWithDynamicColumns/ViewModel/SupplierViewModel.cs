using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GridLikeViewWithDynamicColumns.Model;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GridLikeViewWithDynamicColumns.ViewModel
{
    public class SupplierViewModel : ViewModelBase
    {
        public ObservableCollection<ItemViewModel> Items
        {
            get { return _items; }
        }

        public RelayCommand InsertItemCommand;

        private string _name;
        private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public int ItemsTotalCost
        {
            get { return Items.Sum(p => p.Cost); }
        }

        public SupplierViewModel()
        {
            InsertItemCommand = new RelayCommand(InsertItemCommandExecute, InsertItemCommandCanExecute);

            Messenger.Default.Register<ItemCostUpdatedMessage>(
     this,
     m => RaisePropertyChanged("ItemsTotalCost"));
        }

        public bool InsertItemCommandCanExecute()
        {
            return true;
        }

        public void InsertItemCommandExecute()
        {
            Items.Add(new ItemViewModel() { Name = string.Format("Item {0}", Items.Count) });
        }

        public List<ItemViewModel> ItemsForGrid
        {
            get
            {
                //This calculation can be moved outside for better performance, do only when items change
                var mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
                var distinctItemNames = mainViewModel.DistinctItemNames;

                List<ItemViewModel> itemsForGrid = new List<ItemViewModel>();
                foreach (var name in distinctItemNames)
                {
                    var item = Items.FirstOrDefault(p => p.Name.Equals(name));
                    itemsForGrid.Add(item ?? new ItemViewModelFake() { Name = "" });
                }

                return itemsForGrid;
            }
        }
    }
}