using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using GridLikeViewWithDynamicColumns.Model;

namespace GridLikeViewWithDynamicColumns.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            GenerateTestData();

            Messenger.Default.Register<SupplierAddedMessage>(
    this,
    m => RaisePropertyChanged("SuppliersForGrid"));
        }

        private void GenerateTestData()
        {
            var supplierList = ServiceLocator.Current.GetInstance<SupplierListViewModel>();
            var r = new Random();

            for (int i = 1; i < 4; i++)
            {
                var supplier = new SupplierViewModel() { Name = $"Supplier {i}"};
                var itemCnt = r.Next(5, 10);
                var testSkipItem = r.Next(1, itemCnt);

                for (int j = 1; j <= itemCnt; j++)
                {
                    if (j == testSkipItem)

                        continue;

                    var item = new ItemViewModel() { Name = $"Item {j}", Cost = r.Next(1, 100) };

                    supplier.Items.Add(item);
                }

                supplierList.Suppliers.Add(supplier);
            }
        }

        public List<SupplierViewModel> SuppliersForGrid
        {
            get
            {
                //This calculation can be moved outside for better performance, do only when items change
                var supplierList = ServiceLocator.Current.GetInstance<SupplierListViewModel>();

                var suppliers = new List<SupplierViewModel>();
                var rowHeaderSupplierVM = new SupplierViewModel {Name = ""};

                foreach (var itemName in DistinctItemNames)
                {
                    rowHeaderSupplierVM.Items.Add(new ItemViewModelFake() { Name = itemName });
                }

                suppliers.Add(rowHeaderSupplierVM);
                suppliers.AddRange(supplierList.Suppliers);

                return suppliers;
            }
        }

        public List<string> DistinctItemNames
        {
            get
            {
                //This calculation can be moved outside for better performance, do only when items change
                var supplierList = ServiceLocator.Current.GetInstance<SupplierListViewModel>();

                var distinctItemNames =
                    supplierList.Suppliers.SelectMany(p => p.Items).Select(t => t.Name).Distinct().ToList();
                distinctItemNames.Sort();
                return distinctItemNames;
            }
        }
    }
}