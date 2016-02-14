using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GridLikeViewWithDynamicColumns.Model;

namespace GridLikeViewWithDynamicColumns.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        private string _name;
        private int _cost;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public int Cost
        {
            get { return _cost; }
            set
            {
                _cost = value;
                RaisePropertyChanged();
                Messenger.Default.Send(new ItemCostUpdatedMessage());
            }
        }
    }
}