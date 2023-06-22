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
    public class CustomerViewModel : BaseViewModel
    {

        private ObservableCollection<KhachHang> _List;
        public ObservableCollection<KhachHang> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private KhachHang _SelectedItem;
        public KhachHang SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.TenKH;
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
        public string Email { get => _Email; set { _Email = value.Trim(); OnPropertyChanged(); } }

        public CustomerViewModel()
        {
            List = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH == DisplayName);
                if (displayList == null || displayList.Count() != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) => {
                var Customer = new KhachHang()
                {
                    TenKH = DisplayName,
                    SDT = SDT,
                    Email = Email
                };
                DataProvider.Ins.DB.KhachHangs.Add(Customer);
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                    List.Add(Customer);
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
                var displayList = DataProvider.Ins.DB.KhachHangs.Where(x => x.MaKH == SelectedItem.MaKH);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                var Customer = DataProvider.Ins.DB.KhachHangs.Where(x => x.MaKH == SelectedItem.MaKH).SingleOrDefault();
                Customer.TenKH = DisplayName;
                if (SDT.Length == 10)
                {
                    Customer.SDT = SDT;
                }
                else
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                }
                Customer.Email = Email;
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.TenKH = DisplayName;
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
                var displayList = DataProvider.Ins.DB.KhachHangs.Where(x => x.MaKH == SelectedItem.MaKH);
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
                    DataProvider.Ins.DB.KhachHangs.Attach(SelectedItem);
                    DataProvider.Ins.DB.KhachHangs.Remove(SelectedItem);
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
