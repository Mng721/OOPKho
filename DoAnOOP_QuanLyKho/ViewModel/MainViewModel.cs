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
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _TonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get => _TonKhoList; set { _TonKhoList = value; OnPropertyChanged(); } }

        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitCommand { get; set; }
        public ICommand SupplyCommand { get; set; }
        public ICommand SupplierCommand { get; set; }
        public ICommand EmployeeCommand { get; set; }
        public ICommand AccountCommand { get; set; }
        public ICommand InputCommand { get; set; }
        public ICommand OutputCommand { get; set; }
        public ICommand CustomerCommand { get; set; }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                IsLoaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin) {
                    p.Show();
                    LoadInventoryData();
                }
                else
                {
                    p.Close();
                }
                
            });

            UnitCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                UnitsWindow wd = new UnitsWindow();
                wd.ShowDialog();
            });

            SupplyCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                SuppliesWindow wd = new SuppliesWindow();
                wd.ShowDialog();
            });

            SupplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                SuppliersWindow wd = new SuppliersWindow();
                wd.ShowDialog();
            });

            EmployeeCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                EmployeesWindow wd = new EmployeesWindow();
                wd.ShowDialog();
            });

            AccountCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                AccountsWindow wd = new AccountsWindow();
                wd.ShowDialog();
            });

            InputCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                InputsWindow wd = new InputsWindow();
                wd.ShowDialog();
            });

            OutputCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                OutputsWindow wd = new OutputsWindow();
                wd.ShowDialog();
            });

            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CustomersWindow wd = new CustomersWindow();
                wd.ShowDialog();
            });

        }
        void LoadInventoryData()
        {
            TonKhoList = new ObservableCollection<TonKho>();
            var objectList = DataProvider.Ins.DB.SanPhams;
            int i = 1;
            foreach (var item in objectList)
            {
                var inputList = DataProvider.Ins.DB.ChiTietPhieuNhaps.Where(p => p.MaSP == item.MaSP);
                var outputList = DataProvider.Ins.DB.ChiTietPhieuXuats.Where(p => p.MaSP == item.MaSP);

                int sumInput = 0;
                int sumOutput = 0;

                if (inputList != null && inputList.Count() > 0)
                    sumInput = (int) inputList.Sum(p => p.SoLuongNhap);
                if (outputList != null&& outputList.Count() > 0)
                    sumOutput = (int) outputList.Sum(p => p.SLXuat);

                TonKho tonKho = new TonKho();
                tonKho.STT = i;
                tonKho.Count = sumInput - sumOutput;
                tonKho.Object = item;
                TonKhoList.Add(tonKho);
                i++;

            }    
       
        }
    }
}
