using BanHang.Data;
using DevExpress.Web;
using DevExpress.Web.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachHangHoa : System.Web.UI.Page
    {
        dataHangHoa data = new dataHangHoa();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid(cmbSoLuongXem.Value.ToString());
            }
        }

        private void LoadGrid(string HienThi)
        {
            data = new dataHangHoa();
            gridHangHoa.DataSource = data.LayDanhSachHangHoa(HienThi);
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dataHangHoa();
            data.XoaHangHoa(ID);
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(cmbSoLuongXem.Value.ToString());
        }

        protected void gridHangHoa_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dataHangHoa();
            string MaHang = e.NewValues["MaHang"].ToString();
            DataTable dd = data.KiemTraHangHoa(MaHang);
            if (dd.Rows.Count == 0)
            {
                string IDNhomHang = e.NewValues["IDNhomHang"].ToString();
                string TenHangHoa = e.NewValues["TenHangHoa"].ToString();
                string IDDonViTinh = e.NewValues["IDDonViTinh"].ToString();
                float GiaMua = float.Parse(e.NewValues["GiaMua"].ToString());
                float GiaSi = float.Parse(e.NewValues["GiaSi"].ToString());
                float GiaLe = float.Parse(e.NewValues["GiaLe"].ToString());
                string GhiChu = e.NewValues["GhiChu"] != null ? e.NewValues["GhiChu"].ToString() : "";
                e.NewValues["HinhAnh"] = Session["UploadImages"];
                string HinhAnh = e.NewValues["HinhAnh"] != null ? e.NewValues["HinhAnh"].ToString() : "";
                object IDHangHoa = data.ThemHangHoa(IDNhomHang, MaHang, TenHangHoa, IDDonViTinh, GiaMua, GiaSi, GhiChu, HinhAnh, GiaLe);
                e.Cancel = true;
                gridHangHoa.CancelEdit();
                LoadGrid(cmbSoLuongXem.Value.ToString());

            }
            else Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại.'); </script>");
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            data = new dataHangHoa();
            string MaHang = e.NewValues["MaHang"].ToString();
            string IDNhomHang = e.NewValues["IDNhomHang"].ToString();
            string TenHangHoa = e.NewValues["TenHangHoa"].ToString();
            string IDDonViTinh = e.NewValues["IDDonViTinh"].ToString();
            float GiaMua = float.Parse(e.NewValues["GiaMua"].ToString());
            float GiaSi = float.Parse(e.NewValues["GiaSi"].ToString());
            float GiaLe = float.Parse(e.NewValues["GiaLe"].ToString());
            string GhiChu = e.NewValues["GhiChu"] != null ? e.NewValues["GhiChu"].ToString() : "";
            e.NewValues["HinhAnh"] = Session["UploadImages"];
            string HinhAnh = e.NewValues["HinhAnh"] != null ? e.NewValues["HinhAnh"].ToString() : "";
            string ID = e.Keys[0].ToString();
            if (HinhAnh != "")
            {
                Session["UploadImages"] = "";
                data.SuaThongTinHangHoa(ID, IDNhomHang, MaHang, TenHangHoa, IDDonViTinh, GiaMua, GiaSi, GhiChu, HinhAnh, GiaLe);
            }
            else
            {
                data.SuaThongTinHangHoaKHinh(ID, IDNhomHang, MaHang, TenHangHoa, IDDonViTinh, GiaMua, GiaSi, GhiChu, GiaLe);
            }
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(cmbSoLuongXem.Value.ToString());
        }

        protected void gridHangHoa_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["MaHang"] = dataHangHoa.Dem_Max();
            e.NewValues["GiaMua"] = "0";
            e.NewValues["GiaSi"] = "0";
            e.NewValues["GiaLe"] = "0";
            //e.NewValues["IDDonViTinh"] = "BAO";
        }
        protected void UploadImages_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            string name = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + e.UploadedFile.FileName;
            string path = Page.MapPath("~/UploadImages/") + name;
            e.UploadedFile.SaveAs(path);
            Session["UploadImages"] = name;
        }
        protected void cmbSoLuongXem_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(cmbSoLuongXem.Value.ToString());
        }
    }
}