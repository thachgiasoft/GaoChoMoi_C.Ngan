﻿using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class KhachHang : System.Web.UI.Page
    {
        dtKhachHang data = new dtKhachHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
              
                LoadGrid();
            }
        }
        
        public void LoadGrid()
        {
            data = new dtKhachHang();
            gridKhachHang.DataSource = data.LayDanhSachKhachHang();
            gridKhachHang.DataBind();
        }

        protected void gridKhachHang_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            data = new dtKhachHang();
            int IDNhomKhachHang = Int32.Parse(e.NewValues["IDNhomKhachHang"].ToString());
            string TenKhachHang = e.NewValues["TenKhachHang"] == null ? "" : e.NewValues["TenKhachHang"].ToString();
            DateTime NgaySinh = DateTime.Parse(e.NewValues["NgaySinh"] == null ? DateTime.Today.ToString() : e.NewValues["NgaySinh"].ToString());
            string CMND = e.NewValues["CMND"] == null ? "" : e.NewValues["CMND"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            string IDChietKhau = "2";
            if (dtKhachHang.KT_SDT_KH_CapNhat(DienThoai.Trim(), ID) == -1)
            {
                if (dtKhachHang.KT_SDT_KH(DienThoai.Trim()) == 1)
                {
                    throw new Exception("Lỗi: Số điện thoại đã tồn tại?");
                }
            }
            else
            {

                data.SuaThongTinKhachHang(Int32.Parse(ID), IDNhomKhachHang, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, GhiChu, IDChietKhau);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Cập Nhật");   
            }
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            
        }

        protected void gridKhachHang_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            data = new dtKhachHang();
            int IDNhomKhachHang = Int32.Parse(e.NewValues["IDNhomKhachHang"].ToString());
            string TenKhachHang = e.NewValues["TenKhachHang"].ToString();
            DateTime NgaySinh = DateTime.Parse( e.NewValues["NgaySinh"] == null ? DateTime.Today.ToString() : e.NewValues["NgaySinh"].ToString());
            string CMND = e.NewValues["CMND"] == null ? "" : e.NewValues["CMND"].ToString();
            string DiaChi = e.NewValues["DiaChi"] == null ? "" : e.NewValues["DiaChi"].ToString();
            string DienThoai = e.NewValues["DienThoai"] == null ? "" : e.NewValues["DienThoai"].ToString();
            string Email = e.NewValues["Email"] == null ? "" : e.NewValues["Email"].ToString();
            string MaKh = "";
            string Barcode = "";
            object ID;
            string IDChietKhau = "2";
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (DienThoai != "")
            {
                if (dtKhachHang.KT_SDT_KH(DienThoai.Trim()) != -1)
                {
                    throw new Exception("Lỗi: Số điện thoại đã tồn tại?");
                }
                else
                {
                    ID = data.ThemKhachHang(IDNhomKhachHang, MaKh, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, Barcode, GhiChu, Session["IDKho"].ToString(), IDChietKhau);
                    if (ID != null)
                    {
                        if (e.NewValues["MaKhachHang"] == null)
                        {
                            data = new dtKhachHang();
                            data.CapNhatMaKhachHang(ID, (dtSetting.LayMaKho(Session["IDKho"].ToString()) + "." + (Int32.Parse(ID.ToString()) * 0.0001).ToString().Replace(".", "")).ToString(), (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001)).Replace(".", ""));
                            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                        }
                    }
                }
            }
            else
            {
                ID = data.ThemKhachHang(IDNhomKhachHang, MaKh, TenKhachHang, NgaySinh, CMND, DiaChi, DienThoai, Email, Barcode, GhiChu, Session["IDKho"].ToString(), IDChietKhau);
                if (ID != null)
                {
                    if (e.NewValues["MaKhachHang"] == null)
                    {
                        data = new dtKhachHang();
                        data.CapNhatMaKhachHang(ID, (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001).ToString().Replace(".", "")).ToString(), (Session["IDKho"].ToString() + "." + (Int32.Parse(ID.ToString()) * 0.0001)).Replace(".", ""));
                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + TenKhachHang, Session["IDKho"].ToString(), "Danh Mục", "Thêm");
                    }
                }
            }
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            
        }

        protected void gridKhachHang_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtKhachHang();
            data.XoaKhachHang(Int32.Parse(ID));
            e.Cancel = true;
            gridKhachHang.CancelEdit();
            LoadGrid();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Khách hàng:" + ID, Session["IDKho"].ToString(), "Danh Mục", "Xóa");  
        }
        
       

        protected void cmbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "")
            {
                txtTienThanhToan.Text = "";
                txtNhapSoHoaDon.Enabled = true;
                txtNoHienTai.Text = dtKhachHang.LayCongNoCuKhachHang(cmbKhachHang.Value.ToString()).ToString();
            }
        }
        
        protected void btnCongNo_Click(object sender, EventArgs e)
        {
            Clear();
            popup.ShowOnPageLoad = true;
        }
        public void Clear()
        {
            cmbKhachHang.Text = "";
            txtNoHienTai.Text = "";
            txtTienThanhToan.Text = "";
            txtNhapSoHoaDon.Text = "";
            txtNoiDung.Text = "";
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Clear();
            txtTienThanhToan.Enabled = false;
            txtNhapSoHoaDon.Enabled = false;
            popup.ShowOnPageLoad = false;
        }

        protected void btnCapNhatThanhToan_Click(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "" && txtTienThanhToan.Text != "")
            {
                data = new dtKhachHang();
                string IDKhachHang = cmbKhachHang.Value.ToString();
                string SoHoaDon = txtNhapSoHoaDon.Text == null ? "" : txtNhapSoHoaDon.Text;
                double SoTienThanhToan = double.Parse(txtTienThanhToan.Text);
                string NoiDung = txtNoiDung.Text == null ? "" : txtNoiDung.Text;
                DateTime NgayThanhToan = DateTime.Parse(dateNgayThanhToan.Text);
                object ID = data.ThemChiTietCongNo(SoHoaDon, IDKhachHang, "", "", SoTienThanhToan, NoiDung, NgayThanhToan);
                if (ID != null)
                {
                    data.CapNhatCongNo(IDKhachHang, SoTienThanhToan);
                    DataTable db = data.DanhSachSoDonHang(IDKhachHang);
                    if (db.Rows.Count != 0)
                    {
                        foreach (DataRow dr in db.Rows)
                        {
                            float TienMaPhieu = float.Parse(dr["TongTien"].ToString());
                            string IDHoaDon = dr["ID"].ToString();
                            if (SoTienThanhToan > TienMaPhieu)
                            {
                                data = new dtKhachHang();
                                data.CapNhatTinhTrang(IDHoaDon);
                                SoTienThanhToan = SoTienThanhToan - TienMaPhieu;
                            }
                            else if (SoTienThanhToan > 0)
                            {
                                data = new dtKhachHang();
                                data.CapNhatTinhTrang(IDHoaDon);
                                SoTienThanhToan = 0;
                            }
                            Response.Redirect("ChiTietCongNoKhachHang.aspx");
                        }
                    }
                    Response.Redirect("ChiTietCongNoKhachHang.aspx");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập đủ thông tin.'); </script>");
            }

           // dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Cập nhật công nợ nhà cung cấp", "Thanh toán công nợ.");
        
        }
        protected void dateNgayThanhToan_Init(object sender, EventArgs e)
        {
            dateNgayThanhToan.Date = DateTime.Now;
        }
    }
}