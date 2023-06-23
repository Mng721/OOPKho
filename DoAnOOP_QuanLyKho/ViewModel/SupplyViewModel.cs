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

        private ObservableCollection<DonViTinh> _UnitList;
        public ObservableCollection<DonViTinh> UnitList { get => _UnitList; set { _UnitList = value; OnPropertyChanged(); } }

        private ObservableCollection<LoaiSanPham> _TypeList;
        public ObservableCollection<LoaiSanPham> TypeList { get => _TypeList; set { _TypeList = value; OnPropertyChanged(); } }

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
                    SelectedType = SelectedItem.LoaiSanPham;
                    SelectedUnit = SelectedItem.DonViTinh;
                }
            }
        }

        private DonViTinh _SelectedUnit;
        public DonViTinh SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
            }
        }

        private LoaiSanPham _SelectedType;
        public LoaiSanPham SelectedType
        {
            get => _SelectedType;
            set
            {
                _SelectedType = value;
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
            UnitList = new ObservableCollection<DonViTinh>(DataProvider.Ins.DB.DonViTinhs);
            TypeList = new ObservableCollection<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedUnit == null || SelectedType == null)
                    return false;
                return true;

            }, (p) => {
                var supply = new SanPham() { TenSP = DisplayName, MaDV = SelectedUnit.MaDV, MaLoai = SelectedType.MaLoai };
                DataProvider.Ins.DB.SanPhams.Add(supply);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(supply);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedUnit == null || SelectedType == null || SelectedItem == null)
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
                var supply = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSP == SelectedItem.MaSP).SingleOrDefault();
                supply.TenSP = DisplayName;
                supply.MaLoai = SelectedType.MaLoai;
                supply.MaDV = SelectedUnit.MaDV;
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
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
