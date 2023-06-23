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
    public class OutputInfoViewModel : BaseViewModel
    {
        private ObservableCollection<SanPham> _SuppliesList;
        public ObservableCollection<SanPham> SuppliesList { get => _SuppliesList; set { _SuppliesList = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPhieuXuat> _List;
        public ObservableCollection<ChiTietPhieuXuat> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPhieuNhap> _InputsList;
        public ObservableCollection<ChiTietPhieuNhap> InputsList { get => _InputsList; set { _InputsList = value; OnPropertyChanged(); } }

        private ChiTietPhieuXuat _SelectedItem;
        public ChiTietPhieuXuat SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedSupply = SelectedItem.SanPham;
                    DonGiaXuat = SelectedItem.DonGiaBan;
                }
            }
        }

        private SanPham _SelectedSupply;
        public SanPham SelectedSupply
        {
            get => _SelectedSupply;
            set
            {
                _SelectedSupply = value;
                OnPropertyChanged();
            }
        }

        private ChiTietPhieuXuat _SelectedInputInfo;
        public ChiTietPhieuXuat SelectedInputInfo
        {
            get => _SelectedInputInfo;
            set
            {
                _SelectedInputInfo = value;
                OnPropertyChanged();
            }
        }
        private string _TenSanPham;
        public string TenSanPham { get => _TenSanPham; set { _TenSanPham = value; OnPropertyChanged(); } }

        private int _MaPhieuXuat;
        public int MaPhieuXuat { get => _MaPhieuXuat; set { _MaPhieuXuat = value; OnPropertyChanged(); } }

        private int _SoLuongXuat;
        public int SoLuongXuat { get => _SoLuongXuat; set { _SoLuongXuat = value; OnPropertyChanged(); } }

        private decimal _DonGiaXuat;
        public decimal DonGiaXuat { get => _DonGiaXuat; set { _DonGiaXuat = value; OnPropertyChanged(); } }

        private DateTime _ngaynhap;
        public DateTime ngaynhap { get => _ngaynhap; set { _ngaynhap = value; OnPropertyChanged(); } }

        private DateTime _HSD;
        public DateTime HSD { get => _HSD; set { _HSD = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public OutputInfoViewModel()
        {
            MaPhieuXuat = OutputViewModel.MAPHIEUXUAT;

            InputsList = new ObservableCollection<ChiTietPhieuNhap>(DataProvider.Ins.DB.ChiTietPhieuNhaps);
            foreach (var item in InputsList)
            {
                item.GiaSanPham = item.DonGiaXuat / item.SoLuongNhap;
            }

            if (MaPhieuXuat == 0)
            {
                List = new ObservableCollection<ChiTietPhieuXuat>(DataProvider.Ins.DB.ChiTietPhieuXuats);

            }
            else
            {
                List = new ObservableCollection<ChiTietPhieuXuat>(DataProvider.Ins.DB.ChiTietPhieuXuats.Where(x => x.MaPX == MaPhieuXuat));
            }
            foreach (var item in List)
            {
                item.DonGiaBan = item.ChiTietPhieuNhap.GiaSanPham * item.SLXuat;
            }
            SuppliesList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenSanPham) || MaPhieuXuat == 0)
                    return false;
                return true;

            }, (p) =>
            {
                var inputInfo = new ChiTietPhieuXuat() { MaSP = SelectedSupply.MaSP, SLXuat = SoLuongXuat};
                DataProvider.Ins.DB.ChiTietPhieuXuats.Add(inputInfo);
                try
                {
                    DataProvider.Ins.DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                List.Add(inputInfo);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedSupply == null || MaPhieuXuat == 0)
                {
                    return false;
                }
                var displaylist = DataProvider.Ins.DB.ChiTietPhieuXuats.Where(x => x.MaSP == SelectedItem.MaSP);
                if (displaylist != null && displaylist.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) =>
            {
                var inputInfo = DataProvider.Ins.DB.ChiTietPhieuXuats.Where(x => x.MaCTPXuat == SelectedItem.MaCTPXuat).SingleOrDefault();
                inputInfo.MaPX = MaPhieuXuat;
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
                if (SelectedItem == null || MaPhieuXuat == 0)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.ChiTietPhieuXuats.Where(x => x.MaCTPXuat == SelectedItem.MaCTPXuat);
                if (displayList != null && displayList.Count() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) =>
            {
                try
                {
                    DataProvider.Ins.DB.ChiTietPhieuXuats.Attach(SelectedItem);
                    DataProvider.Ins.DB.ChiTietPhieuXuats.Remove(SelectedItem);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Remove(SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });
        }
    }
}
