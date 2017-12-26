using BanHang.Data;
using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class InBaoCaoTongHop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NgayBD = Request.QueryString["ngayBD"];
            string NgayKT = Request.QueryString["NgayKT"];

            string strNgay = DateTime.Parse(NgayBD).ToString("dd-MM-yyyy") + " - " + DateTime.Parse(NgayKT).ToString("dd-MM-yyyy");
            rpBaoCaoTongHop rp = new rpBaoCaoTongHop();
            rp.Parameters["strNgay"].Value = strNgay;
            rp.Parameters["strNgay"].Visible = false;

            float strDoanhSoBanHang = dataBCTongHop.strDoanhSoBanHang(NgayBD, NgayKT);
            float strHangBanBiTraLai = dataBCTongHop.strHanBanBiTraLai(NgayBD, NgayKT);
            float strGiaVonBanHang = dataBCTongHop.strGiaVonBanHang(NgayBD, NgayKT);
            float strCacKhoanChi = dataBCTongHop.strCacKhoanChi(NgayBD, NgayKT);

            rp.Parameters["strDoanhSoBanHang"].Value = strDoanhSoBanHang;
            rp.Parameters["strDoanhSoBanHang"].Visible = false;
            rp.Parameters["strHangBanBiTraLai"].Value = strHangBanBiTraLai;
            rp.Parameters["strHangBanBiTraLai"].Visible = false;
            rp.Parameters["strDoanhThuSauTru"].Value = strDoanhSoBanHang - strHangBanBiTraLai;
            rp.Parameters["strDoanhThuSauTru"].Visible = false;

            rp.Parameters["strGiaVonBanHang"].Value = strGiaVonBanHang;
            rp.Parameters["strGiaVonBanHang"].Visible = false;

            rp.Parameters["strDoanhThuThuan"].Value = strDoanhSoBanHang - strHangBanBiTraLai - strGiaVonBanHang;
            rp.Parameters["strDoanhThuThuan"].Visible = false;

            rp.Parameters["strCacKhoanChi"].Value = strCacKhoanChi;
            rp.Parameters["strCacKhoanChi"].Visible = false;

            rp.Parameters["strLai"].Value = strDoanhSoBanHang - strHangBanBiTraLai - strGiaVonBanHang - strCacKhoanChi;
            rp.Parameters["strLai"].Visible = false;

            rp.Parameters["strCongNoKH"].Value = dataBCTongHop.strCongNoKH();
            rp.Parameters["strCongNoKH"].Visible = false;
            rp.Parameters["strCongNoNCC"].Value = dataBCTongHop.strCongNoNCC();
            rp.Parameters["strCongNoNCC"].Visible = false;

            viewerReport.Report = rp;
        }
    }
}