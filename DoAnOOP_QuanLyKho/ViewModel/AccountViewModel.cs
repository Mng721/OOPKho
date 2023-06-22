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
    public class AccountViewModel : BaseViewModel
    {

        private ObservableCollection<TaiKhoan> _List;
        public ObservableCollection<TaiKhoan> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<NhanVien> _EmployeesList;
        public ObservableCollection<NhanVien> EmployeesList { get => _EmployeesList; set { _EmployeesList = value; OnPropertyChanged(); } }

        private ObservableCollection<ChucVu> _PositionsList;
        public ObservableCollection<ChucVu> PositionsList { get => _PositionsList; set { _PositionsList = value; OnPropertyChanged(); } }

        private TaiKhoan _SelectedItem;
        public TaiKhoan SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.TenDangNhap;
                    MatKhau = SelectedItem.MatKhau;
                    SelectedEmployee = SelectedItem.NhanVien;
                    SelectedPosition = SelectedItem.ChucVu;
                }
            }
        }

        private NhanVien _SelectedEmployee;
        public NhanVien SelectedEmployee
        {
            get => _SelectedEmployee;
            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private ChucVu _SelectedPosition;
        public ChucVu SelectedPosition
        {
            get => _SelectedPosition;
            set
            {
                _SelectedPosition = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }

        public AccountViewModel()
        {

            List = new ObservableCollection<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
            EmployeesList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);
            PositionsList = new ObservableCollection<ChucVu>(DataProvider.Ins.DB.ChucVus);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedEmployee == null || SelectedPosition == null)
                    return false;
                return true;
                
            }, (p) => {
                var unit = new TaiKhoan() { TenDangNhap = DisplayName, MatKhau = MatKhau , MaCV = SelectedPosition.MaCV, MaNV = SelectedEmployee.MaNV};
                DataProvider.Ins.DB.TaiKhoans.Add(unit);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(unit);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedEmployee == null || SelectedPosition == null || SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.TaiKhoans.Where(x => x.TenDangNhap == DisplayName);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                var unit = DataProvider.Ins.DB.TaiKhoans.Where(x => x.MaTK == SelectedItem.MaTK).SingleOrDefault();
                unit.TenDangNhap = DisplayName;
                unit.MatKhau = MatKhau;
                unit.MaNV = SelectedEmployee.MaNV;
                unit.MaCV = SelectedPosition.MaCV; 
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.TenDangNhap = DisplayName;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.TaiKhoans.Where(x => x.TenDangNhap == DisplayName);
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
                    DataProvider.Ins.DB.TaiKhoans.Attach(SelectedItem);
                    DataProvider.Ins.DB.TaiKhoans.Remove(SelectedItem);
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
