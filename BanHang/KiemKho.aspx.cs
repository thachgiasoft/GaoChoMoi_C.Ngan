using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class KiemKho : System.Web.UI.Page
    {
        dtKiemKho data = new dtKiemKho();
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
                    txtNguoiLapPhieu.Text = Session["TenDangNhap"].ToString();
                    IDPhieuKiemKho_Temp.Value = Session["IDNhanVien"].ToString();
                }
                LoadGrid(IDPhieuKiemKho_Temp.Value.ToString());
            }
        }

        protected void gridDanhSachHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            data = new dtKiemKho();
            data.XoaPhieuKiemKho_Temp_ID(ID);
            e.Cancel = true;
            gridDanhSachHangHoa_Temp.CancelEdit();
            LoadGrid(IDPhieuKiemKho);
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data = new dtKiemKho();
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            data.XoaPhieuKiemKho_Temp_IDPhieuKiemKho(IDPhieuKiemKho);
            Response.Redirect("DanhSachKiemKho.aspx");
        }
        public void LoadGrid(string IDPhieuKiemKho)
        {
            data = new dtKiemKho();
            gridDanhSachHangHoa_Temp.DataSource = data.DanhSachKiemKhoTemp_IDPhieuKiemKho(IDPhieuKiemKho);
            gridDanhSachHangHoa_Temp.DataBind();
        }
        protected void gridDanhSachHangHoa_Temp_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            string ID = e.Keys[0].ToString();
            int ThucTe = Int32.Parse(e.NewValues["ThucTe"].ToString());
            if (ThucTe >= 0)
            {
                string IDHangHoa = e.NewValues["IDHangHoa"].ToString();
                int TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa,Session["IDKho"].ToString());
                data = new dtKiemKho();
                data.CapNhatPhieuKiemKho_Temp(ID, ThucTe, ThucTe - TonKho);
                e.Cancel = true;
                gridDanhSachHangHoa_Temp.CancelEdit();
                LoadGrid(IDPhieuKiemKho);
            }
            else
            {
                throw new Exception("Lỗi: Số lượng thực tế phải  >= 0");
            }
        }
        protected void txtNgayLapPhieu_Init(object sender, EventArgs e)
        {
            txtNgayLapPhieu.Date = DateTime.Today;
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            DataTable db = data.DanhSachKiemKhoTemp_IDPhieuKiemKho(IDPhieuKiemKho);
            if (db.Rows.Count > 0)
            {
                string IDNguoiDung = Session["IDNhanVien"].ToString();
                DateTime NgayKiemKho = DateTime.Parse(txtNgayLapPhieu.Text.ToString());
                string IDKho = Session["IDKho"].ToString();
                string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                data = new dtKiemKho();
                object ID1 = data.ThemPhieu_Temp();
                if (ID1 != null)
                {
                    data.CapNhatPhieuKiemKho(ID1, IDNguoiDung, NgayKiemKho, IDKho, GhiChu);
                    foreach (DataRow dr in db.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string TonKho = dr["TonKho"].ToString();
                        string ChenhLech = dr["ChenhLech"].ToString();
                        string ThucTe = dr["ThucTe"].ToString();
                        string MaHang = dr["MaHang"].ToString();
                        string IDDonViTinh = dr["IDDonViTinh"].ToString();
                        string IDKe = dr["IDKe"].ToString();
                        data = new dtKiemKho();
                        data.ThemPhieuKiemKho(ID1, IDHangHoa, TonKho, ChenhLech, ThucTe, MaHang, IDDonViTinh, IDKe);
                        if (Int32.Parse(ChenhLech) > 0)
                        {
                            object TheKho1 = dtTheKho.ThemTheKho("", "Kiểm kho: " + dtTheKho.LayTenKho_ID(IDKho), "0", "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho).ToString()) + ChenhLech).ToString(), Session["IDNhanVien"].ToString(), IDKho, IDHangHoa, "Nhập", "0", "0", ChenhLech.ToString());
                            if (TheKho1 != null)
                            {
                                dtCapNhatTonKho.CapNhatKho(IDHangHoa, ThucTe, IDKho);
                            }
                        }
                        else if (Int32.Parse(ChenhLech) < 0)
                        {
                            object TheKho2 = dtTheKho.ThemTheKho("", "Kiểm kho: " + dtTheKho.LayTenKho_ID(IDKho), "0", "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho).ToString()) + ChenhLech).ToString(), Session["IDNhanVien"].ToString(), IDKho, IDHangHoa, "Xuất", "0", "0", ChenhLech.ToString());
                            if (TheKho2 != null)
                            {
                                dtCapNhatTonKho.CapNhatKho(IDHangHoa, ThucTe, IDKho);
                            }
                        }
                    }
                    data = new dtKiemKho();
                    data.XoaPhieuKiemKho_Temp_IDPhieuKiemKho(IDPhieuKiemKho);
                    Response.Redirect("DanhSachKiemKho.aspx");
                    //dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Kiểm Kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");  
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Cập nhật không thành công.'); </script>");
                }
            }
            else
            {
                txtBarcode.Focus();
                Response.Write("<script language='JavaScript'> alert('Danh sách kiểm kho rỗng.'); </script>");
            }
        }

        protected void btnThemTemp_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text != "")
            {
                string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
                dtBanHangLe dt = new dtBanHangLe();
                DataTable tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Value.ToString());
                if (tbThongTin.Rows.Count > 0)
                {
                    string IDHangHoa = tbThongTin.Rows[0]["ID"].ToString();
                    string MaHangHoa = tbThongTin.Rows[0]["MaHang"].ToString();
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    int TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
                    int ChechLech = -TonKho;
                    DataTable dt1 = data.KTChiTietPhieuKiemKho_Temp(IDHangHoa, IDPhieuKiemKho);
                    if (dt1.Rows.Count == 0)
                    {
                        data = new dtKiemKho();
                        data.ThemPhieuKiemKho_Temp(IDPhieuKiemKho, IDHangHoa, TonKho, ChechLech, MaHangHoa, IDDonViTinh);
                    }
                    LoadGrid(IDPhieuKiemKho);
                }
                txtBarcode.Text = "";
                txtBarcode.Value = "";
                txtBarcode.Focus();
            }
        }
    }
}