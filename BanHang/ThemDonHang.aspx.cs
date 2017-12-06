using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ThemDonHang : System.Web.UI.Page
    {
        dtThemDonHangKho data = new dtThemDonHangKho();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    txtBarcode.Focus();
                    IDThuMuaDatHang_Temp.Value = Session["IDNhanVien"].ToString();
                    txtNguoiLap.Text = Session["TenDangNhap"].ToString();
                    txtSoDonHang.Text = DateTime.Now.ToString("ddMMyyyy-hhmmss");
                }
                LoadGrid(IDThuMuaDatHang_Temp.Value.ToString());
            }
        }
        private void LoadGrid(string p)
        {
            data = new dtThemDonHangKho();
            gridDanhSachHangHoa.DataSource = data.DanhSachDonDatHang_Temp(p);
            gridDanhSachHangHoa.DataBind();
        }
        public double TinhTongTien()
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            DataTable dt = data.DanhSachDonDatHang_Temp(IDThuMuaDatHang);
            if (dt.Rows.Count > 0)
            {
                double TongTien = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    double ThanhTien = double.Parse(dr["ThanhTien"].ToString());
                    TongTien = TongTien + ThanhTien;
                }
                return TongTien;
            }
            else
                return 0;
        }
        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "")
            {
                string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
                data = new dtThemDonHangKho();
                DataTable dt = data.DanhSachDonDatHang_Temp(IDThuMuaDatHang);
                if (dt.Rows.Count != 0)
                {
                    string SoDonHang = txtSoDonHang.Text.Trim();
                    string IDNguoiLap = Session["IDNhanVien"].ToString();
                    DateTime NgayLap = DateTime.Parse(txtNgayLap.Text);
                    string TongTien = TinhTongTien().ToString();
                    string IDChiNhanh = Session["IDKho"].ToString();
                    string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                    string IDNhaCungCap = cmbNhaCungCap.Text == "" ? "" : cmbNhaCungCap.Value.ToString();
                    int TrangThai = 0;
                    if (ckThanhToan.Checked == true)
                    {
                        TrangThai = 1;
                    }
                    if (cmbNhaCungCap.Text != "" && ckThanhToan.Checked == false)
                    {
                        data = new dtThemDonHangKho();
                        data.CongCongNoNCC(IDNhaCungCap, TongTien);

                    }
                    data = new dtThemDonHangKho();
                    object ID = data.ThemPhieuDatHang();
                    if (ID != null)
                    {
                        data.CapNhatDonDatHang(ID, SoDonHang, IDNguoiLap, NgayLap, TongTien, GhiChu, IDNhaCungCap, TrangThai);
                        foreach (DataRow dr in dt.Rows)
                        {
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            string MaHangHoa = dr["MaHangHoa"].ToString();
                            string IDDonViTinh = dr["IDDonViTinh"].ToString();
                            string SoLuong = dr["SoLuong"].ToString();
                            string DonGia = dr["DonGia"].ToString();
                            string ThanhTien = dr["ThanhTien"].ToString();
                            data = new dtThemDonHangKho();
                            dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDChiNhanh); // cộng kho không qua bước duyệt
                            // ghi lịch sử
                            data.ThemChiTietDonHang(ID, IDHangHoa, MaHangHoa, IDDonViTinh, SoLuong, DonGia, ThanhTien);
                        }
                        data = new dtThemDonHangKho();
                        data.XoaChiTietDonHang_Nhap(IDThuMuaDatHang);
                        Response.Redirect("DanhSachPhieuDatHang.aspx");
                    }
                }
                else
                {
                    txtBarcode.Focus();
                    Response.Write("<script language='JavaScript'> alert('Danh sách nguyên liệu rỗng.'); </script>");
                }
            }
            else
            {
                cmbNhaCungCap.Focus();
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn nhà cung cấp.'); </script>");
            }
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            data.XoaChiTietDonHang_Nhap(IDThuMuaDatHang);
            Response.Redirect("DanhSachPhieuDatHang.aspx");
        }
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            txtBarcode.Focus();
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            string IDThuMuaDatHang = IDThuMuaDatHang_Temp.Value.ToString();
            data = new dtThemDonHangKho();
            data.XoaChiTietDonHang_Temp_ID(ID);
            LoadGrid(IDThuMuaDatHang);
            txtTongTien.Text = TinhTongTien().ToString();
        }

        protected void txtNgayLap_Init(object sender, EventArgs e)
        {
            txtNgayLap.Date = DateTime.Today;
        }
        protected void cmbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "")
            {
                ckThanhToan.Enabled = true;
            }
        } 
        protected void gridDanhSachHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["SoLuong"] != null && e.NewValues["DonGia"] != null)
            {
                string ID = e.Keys[0].ToString();
                int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
                double DonGia = double.Parse(e.NewValues["DonGia"].ToString());
                data.CapNhatChiTietDonHang_temp2(IDThuMuaDatHang_Temp.Value.ToString(), ID, SoLuong, DonGia);
                e.Cancel = true;
                gridDanhSachHangHoa.CancelEdit();
                LoadGrid(IDThuMuaDatHang_Temp.Value.ToString());
            }
            else
                throw new Exception("Lỗi: Không được bỏ trống số lượng và giá mua !!!");
        }

        protected void txtTraTruoc_Init(object sender, EventArgs e)
        {
            txtTraTruoc.Text = "0";
        }

        protected void txtTongTien_Init(object sender, EventArgs e)
        {
            txtTongTien.Text = "0";
        }

        protected void btnThemTam_Click(object sender, EventArgs e)
        {
            dtBanHangLe dt = new dtBanHangLe();
            DataTable tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Value.ToString(), Session["IDKho"].ToString());
            if (tbThongTin.Rows.Count > 0)
            {
                string IDKho = Session["IDKho"].ToString();
                string IDDonHang = IDThuMuaDatHang_Temp.Value.ToString();
                string IDHangHoa = tbThongTin.Rows[0]["ID"].ToString();
                string MaHangHoa = tbThongTin.Rows[0]["MaHang"].ToString();
                string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                string HinhAnh = tbThongTin.Rows[0]["HinhAnh"].ToString();
                int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
                double DonGia = double.Parse(tbThongTin.Rows[0]["GiaMua"].ToString());
                DataTable db = dtThemDonHangKho.KTChiTietDonHang_Temp(IDHangHoa, IDDonHang);// kiểm tra hàng hóa
                if (db.Rows.Count == 0)
                {
                    data.ThemChiTietDonHang_Temp(IDDonHang, IDHangHoa, MaHangHoa, IDDonViTinh, SoLuong, DonGia, HinhAnh);
                    txtTongTien.Text = TinhTongTien().ToString();
                }
                else
                {
                    data.CapNhatChiTietDonHang_temp(IDDonHang, IDHangHoa, SoLuong, DonGia);
                    txtTongTien.Text = TinhTongTien().ToString();
                }
                LoadGrid(IDDonHang);
            }
            else
            {
                txtBarcode.Focus();
                Response.Write("<script language='JavaScript'> alert('Mã hàng không tồn tại !!!'); </script>");
            }
        }
    }
}