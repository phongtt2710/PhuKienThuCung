﻿using CSharp5.Models;
using Microsoft.EntityFrameworkCore;

namespace CSharp5.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiaChi>().HasOne(p => p.nguoiDung).WithMany(n => n.diaChis).HasForeignKey(p => p.Id_Nguoidung);
            modelBuilder.Entity<HoaDon>().HasOne(p => p.diaChi).WithMany(n => n.hoaDons).HasForeignKey(p => p.Id_diachi);
            modelBuilder.Entity<TrangThai>().HasOne(p => p.hoaDon).WithMany(n => n.trangThais).HasForeignKey(p => p.Id_hoadon);
            modelBuilder.Entity<TrangThai>().HasOne(p => p.quanLi).WithMany(n => n.trangThais).HasForeignKey(p => p.Id_quanli);
            modelBuilder.Entity<QuanLi>().HasOne(p => p.phanQuyen).WithMany(n => n.quanLis).HasForeignKey(p => p.Id_phanquyen);
            modelBuilder.Entity<HoaDon>().HasOne(p => p.sanPhamChiTiet).WithMany(n => n.hoaDons).HasForeignKey(p => p.Id_spct);
            modelBuilder.Entity<SanPhamChiTiet>().HasOne(p => p.sanPham).WithMany(n => n.sanPhamChiTiets).HasForeignKey(p => p.Id_SP);
            modelBuilder.Entity<SPCT_Mau>().HasKey(p => new { p.Id_mau, p.Id_spct });
            modelBuilder.Entity<SPCT_Mau>().HasOne(p => p.mau).WithMany(n => n.sPCT_Maus).HasForeignKey(p => p.Id_mau);
            modelBuilder.Entity<SPCT_Mau>().HasOne(p => p.sanPhamChiTiet).WithMany(n => n.sPCT_Maus).HasForeignKey(p => p.Id_spct);
            modelBuilder.Entity<HinhAnhSP>().HasOne(p => p.sanPhamChiTiet).WithMany(n => n.hinhAnhSPs).HasForeignKey(p => p.Id_SPCT);
            modelBuilder.Entity<Size>().HasOne(p => p.sanPhamChiTiet).WithMany(n => n.Sizes).HasForeignKey(p => p.Id_SPCT);
            modelBuilder.Entity<NhaCungCap>().HasOne(p => p.sanPhamChiTiet).WithMany(n => n.nhaCungCaps).HasForeignKey(p => p.Id_SPCT);
        }   

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<DiaChi> diaChis { get; set; }
        public DbSet<HinhAnhSP> hinhAnhSPs { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<NhaCungCap> nhaCungCaps { get; set; }
        public DbSet<PhanQuyen> phanQuyens { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }
        public DbSet<SanPhamChiTiet> sanPhamChiTiets { get; set; }
        public DbSet<Size> sizes { get; set; }
        public DbSet<TrangThai> trangThais { get; set; }
        public DbSet<QuanLi> quanLis { get; set; }
        public DbSet<Mau> maus { get; set; }
        public DbSet<SPCT_Mau> sPCT_Maus { get; set; }


    }
}