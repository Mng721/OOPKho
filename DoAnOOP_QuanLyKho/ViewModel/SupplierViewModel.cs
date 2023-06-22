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
    public class SupplierViewModel : BaseViewModel
    {

        private ObservableCollection<NhaCungCap> _List;
        public ObservableCollection<NhaCungCap> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private NhaCungCap _SelectedItem;
        public NhaCungCap SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.TenNCC;
                    DiaChi = SelectedItem.DiaChi;
                    SDT = SelectedItem.SDT;
                    Email = SelectedItem.Email;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value.Trim(); OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value.Trim(); OnPropertyChanged(); } }

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value.Trim(); OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email ; set { _Email = value.Trim(); OnPropertyChanged(); } }

        public SupplierViewModel()
        {
            List = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) => {
                var supplier = new NhaCungCap()
                {
                    TenNCC = DisplayName,
                    DiaChi = DiaChi,
                    SDT = SDT,
                    Email = Email
                };
                DataProvider.Ins.DB.NhaCungCaps.Add(supplier);
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                    List.Add(supplier);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.MaNCC == SelectedItem.MaNCC);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                var supplier = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.MaNCC == SelectedItem.MaNCC).SingleOrDefault();
                supplier.TenNCC = DisplayName;
                supplier.DiaChi = DiaChi;
                if (SDT.Length == 10)
                {
                    supplier.SDT = SDT;
                }
                else
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                }
                supplier.Email = Email;
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.TenNCC = DisplayName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.NhaCungCaps.Where(x => x.TenNCC == DisplayName);
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
                    DataProvider.Ins.DB.NhaCungCaps.Attach(SelectedItem);
                    DataProvider.Ins.DB.NhaCungCaps.Remove(SelectedItem);
                    DataProvider.Ins.DB.SaveChanges();

                    List.Remove(SelectedItem);
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
