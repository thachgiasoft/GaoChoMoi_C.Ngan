using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachChiPhi : System.Web.UI.Page
    {
        dtChiPhi data = new dtChiPhi();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtChiPhi();
            gridDanhSach.DataSource = data.DanhSach();
            gridDanhSach.DataBind();
        }

        protected void gridDanhSach_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["NgayChi"] = DateTime.Today.ToString("dd/MM/yyyy");
            e.NewValues["TongChi"] = "0";
            e.NewValues["DaChi"] = "0";
        }

        protected void gridDanhSach_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtChiPhi();
            data.Xoa(ID);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhSach_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string TenKhachHang = e.NewValues["TenKhachHang"].ToString();
            DateTime NgayChi = DateTime.Parse(e.NewValues["NgayChi"].ToString());
            double TongChi = double.Parse(e.NewValues["TongChi"].ToString());
            double DaChi = double.Parse(e.NewValues["DaChi"].ToString());
            double ConLai = TongChi - DaChi;
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            string TrangThai = e.NewValues["TrangThai"].ToString();
            data = new dtChiPhi();
            data.ThemMoi(TenKhachHang, NgayChi, TongChi, DaChi, ConLai, GhiChu, TrangThai);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhSach_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string TenKhachHang = e.NewValues["TenKhachHang"].ToString();
            DateTime NgayChi = DateTime.Parse(e.NewValues["NgayChi"].ToString());
            double TongChi = double.Parse(e.NewValues["TongChi"].ToString());
            double DaChi = double.Parse(e.NewValues["DaChi"].ToString());
            double ConLai = TongChi - DaChi;
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            string TrangThai = e.NewValues["TrangThai"].ToString();
            data = new dtChiPhi();
            data.CapNhatThongTin(ID, TenKhachHang, NgayChi, TongChi, DaChi, ConLai, GhiChu, TrangThai);
            e.Cancel = true;
            gridDanhSach.CancelEdit();
            LoadGrid();
        }
    }
}