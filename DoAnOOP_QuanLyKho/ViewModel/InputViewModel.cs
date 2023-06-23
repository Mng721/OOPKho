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
    public class InputViewModel : BaseViewModel
    {
        public static int MAPHIEUNHAP;
        private ObservableCollection<NhaCungCap> _SuppliersList;
        public ObservableCollection<NhaCungCap> SuppliersList { get => _SuppliersList; set { _SuppliersList = value; OnPropertyChanged(); } }

        private ObservableCollection<PhieuNhap> _List;
        public ObservableCollection<PhieuNhap> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<NhanVien> _EmployeesList;
        public ObservableCollection<NhanVien> EmployeesList { get => _EmployeesList; set { _EmployeesList = value; OnPropertyChanged(); } }

        private PhieuNhap _SelectedItem;
        public PhieuNhap SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedEmployee = SelectedItem.NhanVien;
                    SelectedSupplier = SelectedItem.NhaCungCap;
                    NgayNhap = SelectedItem.NgayNhap;
                    MAPHIEUNHAP = SelectedItem.MaPN;
                } else
                {
                    MAPHIEUNHAP = 0;
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

        private NhaCungCap _SelectedSupplier;
        public NhaCungCap SelectedSupplier
        {
            get => _SelectedSupplier;
            set
            {
                _SelectedSupplier = value;
                OnPropertyChanged();
            }
        }

        private int _MaPhieuNhap;
        public int MaPhieuNhap { get => _MaPhieuNhap; set { _MaPhieuNhap = value; OnPropertyChanged(); } }

        private DateTime _NgayNhap;
        public DateTime NgayNhap { get => _NgayNhap; set { _NgayNhap = value; OnPropertyChanged(); } }

        public ICommand InfoCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public InputViewModel()
        {
            List = new ObservableCollection<PhieuNhap>(DataProvider.Ins.DB.PhieuNhaps);
            SuppliersList = new ObservableCollection<NhaCungCap>(DataProvider.Ins.DB.NhaCungCaps);
            EmployeesList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens.Where(x => x.ChucVu.TenCV == "Nhân viên nhập hàng"));


            InfoCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                InputInfosWindow wd = new InputInfosWindow();
                wd.ShowDialog();
            });

            AddCommand = new RelayCommand<object>((p) =>
            {
            if (SelectedEmployee == null || SelectedSupplier == null)
                    return false;
                return true;

            }, (p) => {
                var Input = new PhieuNhap() { MaNCC = SelectedSupplier.MaNCC, MaNV = SelectedEmployee.MaNV, NgayNhap = NgayNhap };
                DataProvider.Ins.DB.PhieuNhaps.Add(Input);
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
                if (SelectedItem == null || SelectedEmployee == null || SelectedSupplier == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.PhieuNhaps.Where(x => x.MaPN == SelectedItem.MaPN);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                var Input = DataProvider.Ins.DB.PhieuNhaps.Where(x => x.MaPN == SelectedItem.MaPN).SingleOrDefault();
                Input.MaNCC = SelectedSupplier.MaNCC;
                Input.MaNV = SelectedEmployee.MaNV;
                Input.NgayNhap = NgayNhap;
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
                var displayList = DataProvider.Ins.DB.PhieuNhaps.Where(x => x.MaPN == SelectedItem.MaPN);
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
                    DataProvider.Ins.DB.PhieuNhaps.Attach(SelectedItem);
                    DataProvider.Ins.DB.PhieuNhaps.Remove(SelectedItem);
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
