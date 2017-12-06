using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietCongNo : System.Web.UI.Page
    {
        dtCongNo data = new dtCongNo();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            data = new dtCongNo();
            gridDanhSach.DataSource = data.DanhSachChiTietCongNo();
            gridDanhSach.DataBind();
        }

        protected void ASPxFormLayout1_E2_Click(object sender, EventArgs e)
        {
            popup.ShowOnPageLoad = true;

        }
        protected void dateNgayThanhToan_Init(object sender, EventArgs e)
        {
            dateNgayThanhToan.Date = DateTime.Today;
        }

        protected void cmbNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "")
            {
                txtTienThanhToan.Text = "";
                txtNhapSoHoaDon.Enabled = true;
                txtNoHienTai.Text = dtNhaCungCap.LayCongNo_IDNCC(cmbNhaCungCap.Value.ToString()) + "";
            }
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            cmbNhaCungCap.Text = "";
            txtNoHienTai.Text = "";
            txtTienThanhToan.Text = "";
            txtNhapSoHoaDon.Text = "";
            txtNoiDung.Text = "";
            txtTienThanhToan.Enabled = false;
            txtNhapSoHoaDon.Enabled = false;
            popup.ShowOnPageLoad = false;
        }
        
       
        protected void btnCapNhatThanhToan_Click(object sender, EventArgs e)
        {
            dtCongNo dt = new dtCongNo();
            if (cmbNhaCungCap.Text != "" && txtTienThanhToan.Text != "")
            {
                string IDNhaCungCap = cmbNhaCungCap.Value.ToString();
                string SoHoaDon = txtNhapSoHoaDon.Text == null ? "" : txtNhapSoHoaDon.Text;

                float SoTienThanhToan = float.Parse(txtTienThanhToan.Text);
                string NoiDung = txtNoiDung.Text == null ? "" : txtNoiDung.Text;
                DateTime NgayThanhToan = DateTime.Parse(dateNgayThanhToan.Text);
                object ID = dt.ThemChiTietCongNo(SoHoaDon, IDNhaCungCap, "", SoTienThanhToan, NoiDung, NgayThanhToan);
                if (ID != null)
                {
                    dt.CapNhatCongNo(IDNhaCungCap, SoTienThanhToan);
                    data = new dtCongNo();
                    DataTable db = data.DanhSachSoDonHang(cmbNhaCungCap.Value.ToString());
                    if (db.Rows.Count != 0)
                    {
                        foreach (DataRow dr in db.Rows)
                        {
                            float TienMaPhieu = float.Parse(dr["TongTien"].ToString());
                            string IDDonHang = dr["ID"].ToString();
                            if (SoTienThanhToan > TienMaPhieu)
                            {
                                dt = new dtCongNo();
                                dt.CapNhatTinhTrang(IDDonHang);
                                SoTienThanhToan = SoTienThanhToan - TienMaPhieu;
                            }
                            else if (SoTienThanhToan > 0)
                            {
                                dt = new dtCongNo();
                                dt.CapNhatTinhTrang(IDDonHang);
                                SoTienThanhToan = 0;
                                Response.Redirect("ChiTietCongNo.aspx");
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập đủ thông tin.'); </script>");
            }

           // dtLichSuTruyCap.ThemLichSu(Session["IDChiNhanh"].ToString(), Session["IDNhom"].ToString(), Session["IDNhanVien"].ToString(), "Cập nhật công nợ nhà cung cấp", "Thanh toán công nợ.");
        }
    }
}