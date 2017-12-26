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
                float tong = float.Parse("0");
                con.Open();
                string cmdText = "SELECT IDHangHoa, SUM(SoLuong) as SoLuong FROM GPM_ChiTietHoaDon WHERE NgayBan >= '" + ngaybd + "' AND NgayBan <= '" + ngaykt + "' GROUP BY IDHangHoa";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        for (int i = 0; i < tb.Rows.Count; i++)
                        {
                            DataRow dr = tb.Rows[i];
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            int SoLuong = Int32.Parse(dr["SoLuong"].ToString());
                            tong = tong + layGiaVonNhap(ngaybd, ngaykt, IDHangHoa) * SoLuong;
                        }

                        return tong;
                    }
                    catch (Exception e)
                    {
                        return tong;
                    }
                }
            }
        }

        public static float layGiaVonNhap(string ngaybd, string ngaykt, string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                float tong = float.Parse("0");
                con.Open();
                string cmdText = "SELECT SUM(GPM_DonDatHang_ChiTiet.SoLuong) as SoLuong, GPM_DonDatHang_ChiTiet.DonGia FROM GPM_DonDatHang, GPM_DonDatHang_ChiTiet WHERE GPM_DonDatHang.ID = GPM_DonDatHang_ChiTiet.IDDonHang AND GPM_DonDatHang_ChiTiet.IDHangHoa = " + IDHangHoa + " AND GPM_DonDatHang.NgayLap >= '" + ngaybd + "' AND GPM_DonDatHang.NgayLap <= '" + ngaykt + "' GROUP BY GPM_DonDatHang_ChiTiet.DonGia";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        int SL = 0;
                        float gia = 0;
                        for (int i = 0; i < tb.Rows.Count; i++)
                        {
                            DataRow dr = tb.Rows[i];
                            SL = SL + Int32.Parse(dr["SoLuong"].ToString());
                            gia = gia + float.Parse(dr["DonGia"].ToString()) * Int32.Parse(dr["SoLuong"].ToString());
                        }
                        return gia / SL;
                    }
                    catch (Exception e)
                    {
                        return tong;
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

        public static float strCongNoKH()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(CongNo) as Tong FROM GPM_KhachHang WHERE DaXoa = 0";
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

        public static float strCongNoNCC()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(CongNo) as Tong FROM GPM_NhaCungCap WHERE DaXoa = 0";
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