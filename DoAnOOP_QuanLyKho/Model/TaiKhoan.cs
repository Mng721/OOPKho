//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnOOP_QuanLyKho.Model
{
    using DoAnOOP_QuanLyKho.ViewModel;
    using System;
    using System.Collections.Generic;

    public partial class TaiKhoan : BaseViewModel
    {
        public int MaTK { get; set; }
        public int MaNV { get; set; }

        private string _TenDangNhap;
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }

        private string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }

        public int MaCV { get; set; }

        private NhanVien _NhanVien;
        public virtual NhanVien NhanVien
        {
            get => _NhanVien;
            set
            {
                _NhanVien = value;
                OnPropertyChanged();
            }
        }

        private ChucVu _ChucVu;
        public virtual ChucVu ChucVu
        {
            get => _ChucVu;
            set
            {
                _ChucVu = value;
                OnPropertyChanged();
            }
        }
    }
}
