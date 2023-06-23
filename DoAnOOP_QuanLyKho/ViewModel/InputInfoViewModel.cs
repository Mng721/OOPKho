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
    public class InputInfoViewModel : BaseViewModel
    {
        private ObservableCollection<SanPham> _SuppliesList;
        public ObservableCollection<SanPham> SuppliesList { get => _SuppliesList; set { _SuppliesList = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPhieuNhap> _List;
        public ObservableCollection<ChiTietPhieuNhap> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ChiTietPhieuNhap _SelectedItem;
        public ChiTietPhieuNhap SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedSupply = SelectedItem.SanPham;
                    SelectedInput = SelectedItem.PhieuNhap;
                    SoLuongNhap = SelectedItem.SoLuongNhap;
                    GiaNhap = SelectedItem.DonGiaNhap / SelectedItem.SoLuongNhap;
                    GiaXuat = SelectedItem.DonGiaXuat / SelectedItem.SoLuongNhap;
                    HSD = SelectedItem.HSD;
                    TenSanPham = SelectedSupply.TenSP;
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

        private PhieuNhap _SelectedInput;
        public PhieuNhap SelectedInput
        {
            get => _SelectedInput;
            set
            {
                _SelectedInput = value;
                OnPropertyChanged();
            }
        }
        private string _TenSanPham;
        public string TenSanPham { get => _TenSanPham; set { _TenSanPham = value; OnPropertyChanged(); } }

        private int _MaPhieuNhap;
        public int MaPhieuNhap { get => _MaPhieuNhap; set { _MaPhieuNhap = value; OnPropertyChanged(); } }

        private int _SoLuongNhap;
        public int SoLuongNhap { get => _SoLuongNhap; set { _SoLuongNhap = value; OnPropertyChanged(); } }

        private decimal _GiaNhap;
        public decimal GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

        private decimal _GiaXuat;
        public decimal GiaXuat { get => _GiaXuat; set { _GiaXuat = value; OnPropertyChanged(); } }

        private DateTime _ngaynhap;
        public DateTime ngaynhap { get => _ngaynhap; set { _ngaynhap = value; OnPropertyChanged(); } }

        private DateTime _HSD;
        public DateTime HSD { get => _HSD; set { _HSD = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public InputInfoViewModel()
        {
            MaPhieuNhap = InputViewModel.MAPHIEUNHAP;
            if (MaPhieuNhap == 0)
            {
                List = new ObservableCollection<ChiTietPhieuNhap>(DataProvider.Ins.DB.ChiTietPhieuNhaps);

            }
            else
            {
                List = new ObservableCollection<ChiTietPhieuNhap>(DataProvider.Ins.DB.ChiTietPhieuNhaps.Where(x => x.MaPN == MaPhieuNhap));
            }
            foreach (var item in List)
            {
                item.GiaSanPham = item.DonGiaXuat / item.SoLuongNhap;
            }
            SuppliesList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenSanPham) || MaPhieuNhap == 0)
                    return false;
                return true;

            }, (p) =>
            {
                var inputInfo = new ChiTietPhieuNhap() { MaSP = SelectedSupply.MaSP, SoLuongNhap = SoLuongNhap, GiaSanPham = GiaXuat, DonGiaNhap = GiaNhap * SoLuongNhap, DonGiaXuat = GiaXuat * SoLuongNhap, HSD = HSD, MaPN = MaPhieuNhap };
                DataProvider.Ins.DB.ChiTietPhieuNhaps.Add(inputInfo);
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
                if (SelectedItem == null)
                {
                    return false;
                }
                var displaylist = DataProvider.Ins.DB.ChiTietPhieuNhaps.Where(x => x.MaSP == SelectedItem.MaSP);
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
                var inputInfo = DataProvider.Ins.DB.ChiTietPhieuNhaps.Where(x => x.MaCTPNhap == SelectedItem.MaCTPNhap).SingleOrDefault();
                inputInfo.MaPN = MaPhieuNhap;
                inputInfo.MaSP = SelectedItem.MaSP;
                inputInfo.SoLuongNhap = SoLuongNhap;
                inputInfo.HSD = HSD;
                inputInfo.GiaSanPham = GiaXuat;
                inputInfo.DonGiaNhap = GiaNhap * SoLuongNhap;
                inputInfo.DonGiaXuat = GiaXuat * SoLuongNhap;
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
                if (SelectedItem == null || MaPhieuNhap == 0)
                {
                    return false;
                }
                var displayList = DataProvider.Ins.DB.ChiTietPhieuNhaps.Where(x => x.MaCTPNhap == SelectedItem.MaCTPNhap);
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
                    DataProvider.Ins.DB.ChiTietPhieuNhaps.Attach(SelectedItem);
                    DataProvider.Ins.DB.ChiTietPhieuNhaps.Remove(SelectedItem);
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
