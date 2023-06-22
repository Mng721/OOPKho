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
    public class SupplyViewModel : BaseViewModel
    {

        private ObservableCollection<SanPham> _List;
        public ObservableCollection<SanPham> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<NhanVien> _EmployeesList;
        public ObservableCollection<NhanVien> EmployeesList { get => _EmployeesList; set { _EmployeesList = value; OnPropertyChanged(); } }

        private ObservableCollection<ChucVu> _PositionsList;
        public ObservableCollection<ChucVu> PositionsList { get => _PositionsList; set { _PositionsList = value; OnPropertyChanged(); } }

        private SanPham _SelectedItem;
        public SanPham SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.TenSP;
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

        public SupplyViewModel()
        {

            List = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            EmployeesList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);
            PositionsList = new ObservableCollection<ChucVu>(DataProvider.Ins.DB.ChucVus);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedEmployee == null || SelectedPosition == null)
                    return false;
                return true;

            }, (p) => {
                var unit = new SanPham() {  };
                DataProvider.Ins.DB.SanPhams.Add(unit);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(unit);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedEmployee == null || SelectedPosition == null || SelectedItem == null)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSP == SelectedItem.MaSP);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                var unit = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSP == SelectedItem.MaSP).SingleOrDefault();
                unit.TenSP = DisplayName;

                SelectedItem.TenSP = DisplayName;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSP == DisplayName);
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
                    DataProvider.Ins.DB.SanPhams.Attach(SelectedItem);
                    DataProvider.Ins.DB.SanPhams.Remove(SelectedItem);
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
