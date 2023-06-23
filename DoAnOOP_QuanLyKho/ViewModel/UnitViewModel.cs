using DoAnOOP_QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DoAnOOP_QuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<DonViTinh> _List;
        public ObservableCollection<DonViTinh> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private DonViTinh _SelectedItem;
        public DonViTinh SelectedItem { 
            get=>_SelectedItem; 
            set { 
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.TenDV;
                }
            } 
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
        public UnitViewModel()
        {
            List = new ObservableCollection<DonViTinh>(DataProvider.Ins.DB.DonViTinhs);

            AddCommand = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.DonViTinhs.Where(x => x.TenDV == DisplayName);
                if (displayList != null && displayList.Count() != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) => {
                var unit = new DonViTinh() { TenDV = DisplayName };
                DataProvider.Ins.DB.DonViTinhs.Add(unit);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(unit);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.DonViTinhs.Where(x => x.TenDV == DisplayName);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                var unit = DataProvider.Ins.DB.DonViTinhs.Where(x => x.MaDV == SelectedItem.MaDV).SingleOrDefault();
                unit.TenDV = DisplayName;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.TenDV = DisplayName; 
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.DonViTinhs.Where(x => x.TenDV == DisplayName);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                try
                {
                    DataProvider.Ins.DB.DonViTinhs.Attach(SelectedItem);
                    DataProvider.Ins.DB.DonViTinhs.Remove(SelectedItem);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
