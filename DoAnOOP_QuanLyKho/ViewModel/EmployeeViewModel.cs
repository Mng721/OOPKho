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
    public class EmployeeViewModel : BaseViewModel
    {
        private ObservableCollection<GioiTinh> _GendersList;
        public ObservableCollection<GioiTinh> GendersList { get => _GendersList; set { _GendersList = value; OnPropertyChanged(); } }

        private ObservableCollection<NhanVien> _List;
        public ObservableCollection<NhanVien> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<ChucVu> _PositionsList;
        public ObservableCollection<ChucVu> PositionsList { get => _PositionsList; set { _PositionsList = value; OnPropertyChanged(); } }

        private NhanVien _SelectedItem;
        public NhanVien SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.TenNV;
                    SDT = SelectedItem.SDT;
                    SelectedGender = SelectedItem.GioiTinh1;
                    CMND = SelectedItem.CMND;
                    NgaySinh = SelectedItem.NgaySinh;
                    SelectedPosition = SelectedItem.ChucVu;
                }
            }
        }

        private GioiTinh _SelectedGender;
        public GioiTinh SelectedGender
        {
            get => _SelectedGender;
            set
            {
                _SelectedGender = value;
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

        private string _SDT;
        public string SDT { get => _SDT; set { _SDT = value.Trim() ; OnPropertyChanged(); } }

        private string _CMND;
        public string CMND { get => _CMND; set { _CMND = value.Trim(); OnPropertyChanged(); } }

        private DateTime _NgaySinh;
        public DateTime NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }
        public EmployeeViewModel()
        {
            List = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);
            GendersList = new ObservableCollection<GioiTinh>(DataProvider.Ins.DB.GioiTinhs);
            PositionsList = new ObservableCollection<ChucVu>(DataProvider.Ins.DB.ChucVus);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedPosition == null)
                    return false;
                return true;

            }, (p) => {
                var Employee = new NhanVien() { TenNV = DisplayName, SDT = SDT, GioiTinh = SelectedGender.MaGT, CMND = CMND, NgaySinh = NgaySinh, MaCV = SelectedPosition.MaCV, };
                DataProvider.Ins.DB.NhanViens.Add(Employee);
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                List.Add(Employee);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedItem == null || SelectedGender == null || SelectedPosition == null)
                {
                    return false; 
                }
                var displayList = DataProvider.Ins.DB.NhanViens.Where(x => x.MaNV == SelectedItem.MaNV);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                var Employee = DataProvider.Ins.DB.NhanViens.Where(x => x.MaNV == SelectedItem.MaNV).SingleOrDefault();
                Employee.TenNV = DisplayName;
                if (SDT.Length == 10)
                {
                    Employee.SDT = SDT;
                }
                else
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                }
                if (CMND.Length == 12 || CMND.Length == 9)
                {
                    Employee.CMND = CMND;
                }
                else
                {
                    MessageBox.Show("Chứng minh nhân dân không hợp lệ");
                }
                Employee.GioiTinh = SelectedGender.MaGT;
                Employee.NgaySinh = NgaySinh;
                Employee.MaCV = SelectedPosition.MaCV;
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                SelectedItem.TenNV = DisplayName;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.NhanViens.Where(x => x.MaNV == SelectedItem.MaNV);
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
                    DataProvider.Ins.DB.NhanViens.Attach(SelectedItem);
                    DataProvider.Ins.DB.NhanViens.Remove(SelectedItem);
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
