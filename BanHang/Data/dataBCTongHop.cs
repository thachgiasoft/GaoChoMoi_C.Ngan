using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dataBCTongHop
    {
        public static float strDoanhSoBanHang(string ngaybd, string ngaykt)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(KhachCanTra) as Tong FROM GPM_HoaDon WHERE NgayBan >= '" + ngaybd + "' AND NgayBan <= '" + ngaykt + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        return float.Parse(tb.Rows[0]["Tong"].ToString());
                    }
                    catch (Exception e)
                    {
                        return float.Parse("0");
                    }
                }
            }
        }

        public static float strHanBanBiTraLai(string ngaybd, string ngaykt)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(TongTienTra) as Tong FROM GPM_PhieuKhachHangTraHang WHERE NgayDoi >= '" + ngaybd + "' AND NgayDoi <= '" + ngaykt + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        return float.Parse(tb.Rows[0]["Tong"].ToString());
                    }
                    catch (Exception e)
                    {
                        return float.Parse("0");
                    }
                }
            }
        }

        public static float strGiaVonBanHang(string ngaybd, string ngaykt)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(GiaMua) as Tong FROM GPM_ChiTietHoaDon WHERE NgayBan >= '" + ngaybd + "' AND NgayBan <= '" + ngaykt + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        return float.Parse(tb.Rows[0]["Tong"].ToString());
                    }
                    catch (Exception e)
                    {
                        return float.Parse("0");
                    }
                }
            }
        }

        public static float strCacKhoanChi(string ngaybd, string ngaykt)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(TongChi) as Tong FROM GPM_ChiPhi WHERE NgayChi >= '" + ngaybd + "' AND NgayChi <= '" + ngaykt + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        return float.Parse(tb.Rows[0]["Tong"].ToString());
                    }
                    catch (Exception e)
                    {
                        return float.Parse("0");
                    }
                }
            }
        }
    }
}