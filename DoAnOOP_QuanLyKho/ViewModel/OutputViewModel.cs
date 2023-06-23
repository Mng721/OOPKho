using DoAnOOP_QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DoAnOOP_QuanLyKho.ViewModel
{
    public class OutputViewModel : BaseViewModel
    {
        public static int MAPHIEUXUAT;
        private ObservableCollection<KhachHang> _CustomersList;
        public ObservableCollection<KhachHang> CustomersList { get => _CustomersList; set { _CustomersList = value; OnPropertyChanged(); } }

        private ObservableCollection<PhieuXuat> _List;
        public ObservableCollection<PhieuXuat> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<NhanVien> _EmployeesList;
        public ObservableCollection<NhanVien> EmployeesList { get => _EmployeesList; set { _EmployeesList = value; OnPropertyChanged(); } }

        private PhieuXuat _SelectedItem;
        public PhieuXuat SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedEmployee = SelectedItem.NhanVien;
                    SelectedCustomer = SelectedItem.KhachHang;
                    NgayXuat = SelectedItem.NgayXuat;
                    MAPHIEUXUAT = SelectedItem.MaPX;
                }
                else
                {
                    MAPHIEUXUAT = 0;
                }
            }
        }
        private NhanVien _SelectedEmployyee;
        public NhanVien SelectedEmployee
        {
            get => _SelectedEmployyee;
            set
            {
                _SelectedEmployyee = value;
                OnPropertyChanged();
            }
        }

        private KhachHang _SelectedCustomer;
        public KhachHang SelectedCustomer
        {
            get => _SelectedCustomer;
            set
            {
                _SelectedCustomer = value;
                OnPropertyChanged();
            }
        }

        private int _MaPhieuXuat;
        public int MaPhieuXuat { get => _MaPhieuXuat; set { _MaPhieuXuat = value; OnPropertyChanged(); } }

        private DateTime _NgayXuat;
        public DateTime NgayXuat { get => _NgayXuat; set { _NgayXuat = value; OnPropertyChanged(); } }

        public ICommand InfoCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public OutputViewModel()
        {
            List = new ObservableCollection<PhieuXuat>(DataProvider.Ins.DB.PhieuXuats);
            CustomersList = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);
            EmployeesList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens.Where(x => x.ChucVu.TenCV == "Nhân viên xuất hàng"));


            InfoCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                OutputInfosWindow wd = new OutputInfosWindow();
                wd.ShowDialog();
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedEmployee == null || SelectedCustomer == null)
                    return false;
                return true;

            }, (p) => {
                var Input = new PhieuXuat() { MaKH = SelectedCustomer.MaKH, MaNV = SelectedEmployee.MaNV, NgayXuat = NgayXuat };
                DataProvider.Ins.DB.PhieuXuats.Add(Input);
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                List.Add(Input);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedEmployee == null || SelectedCustomer == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.PhieuXuats.Where(x => x.MaPX == SelectedItem.MaPX);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                var Input = DataProvider.Ins.DB.PhieuXuats.Where(x => x.MaPX == SelectedItem.MaPX).SingleOrDefault();
                Input.MaKH = SelectedCustomer.MaKH;
                Input.MaNV = SelectedEmployee.MaNV;
                Input.NgayXuat = NgayXuat;
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.PhieuXuats.Where(x => x.MaPX == SelectedItem.MaPX);
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
                    DataProvider.Ins.DB.PhieuXuats.Attach(SelectedItem);
                    DataProvider.Ins.DB.PhieuXuats.Remove(SelectedItem);
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
