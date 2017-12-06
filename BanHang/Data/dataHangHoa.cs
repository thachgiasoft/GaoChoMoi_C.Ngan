﻿using BanHang.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dataHangHoa
    {
        public static string Dem_Max()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                int STTV = 0;
                string So;
                string GPM = "0000";
                string cmdText = "SELECT * FROM [GPM_HANGHOA]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    STTV = tb.Rows.Count + 1;
                    int DoDaiHT = STTV.ToString().Length;
                    string DoDaiGPM = GPM.Substring(0, 4 - DoDaiHT);
                    So = DoDaiGPM + STTV;
                    return So;
                }
            }
        }
        public static bool KiemTraBarcode(string Barcode)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_HangHoa_Barcode] WHERE Barcode = N'" + Barcode + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return false;
                    }
                    else return true;
                }
            }
        }
        public void ThemHangVaoTonKho(object IDKho, int IDHangHoa, int SoLuongCon)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoaTonKho] ([IDKho],[IDHangHoa],[SoLuongCon],[NgayCapNhat]) VALUES (@IDKho,@IDHangHoa,@SoLuongCon,getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public DataTable LayDanhSachCuaHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_Kho] WHERE [DAXOA] = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void SuaDanhSachBarCode(object ID, List<string> BarCode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "DELETE FROM [GPM_HangHoa_Barcode] WHERE [IDHangHoa] = @IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", ID);
                        myCommand.ExecuteNonQuery();
                    }
                    int KT = 0;
                    foreach (string barCode in BarCode)
                    {
                        if (KiemTraBarcode(barCode) == false)
                        {
                            KT = 1;
                            throw new Exception("Lỗi:Barcode đã tồn tại !!");
                        }
                    }
                    if (KT == 0)
                    {
                        ThemDanhSachBarCode(ID, BarCode);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void SuaThongTinHangHoa(string ID, string IDNhomHang, string MaHang, string TenHangHoa, string IDDonViTinh, float GiaMua, float GiaSi, string GhiChu, string HinhAnh, float GiaLe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HANGHOA] SET [GiaLe] = @GiaLe,[IDNhomHang] = @IDNhomHang, [MaHang] = @MaHang, [TenHangHoa] = @TenHangHoa, [IDDonViTinh] = @IDDonViTinh, [GiaMua] = @GiaMua, [GiaSi] = @GiaSi, [GhiChu] = @GhiChu, [HinhAnh] = @HinhAnh, [NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@GiaLe", GiaLe);
                        myCommand.Parameters.AddWithValue("@IDNhomHang", IDNhomHang);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@GiaMua", GiaMua);
                        myCommand.Parameters.AddWithValue("@GiaSi", GiaSi);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@HinhAnh", HinhAnh);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void SuaThongTinHangHoaKHinh(string ID, string IDNhomHang, string MaHang, string TenHangHoa, string IDDonViTinh, float GiaMua, float GiaSi, string GhiChu, float GiaLe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HANGHOA] SET [GiaLe] = @GiaLe,[IDNhomHang] = @IDNhomHang, [MaHang] = @MaHang, [TenHangHoa] = @TenHangHoa, [IDDonViTinh] = @IDDonViTinh, [GiaMua] = @GiaMua, [GiaSi] = @GiaSi, [GhiChu] = @GhiChu, [NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@GiaLe", GiaLe);
                        myCommand.Parameters.AddWithValue("@IDNhomHang", IDNhomHang);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@GiaMua", GiaMua);
                        myCommand.Parameters.AddWithValue("@GiaSi", GiaSi);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void ThemDanhSachBarCode(object IDHangHoa, List<string> ListBarCode)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HangHoa_Barcode] ([IDHangHoa],[Barcode],[NgayCapNhat])" +
                             " VALUES(@IDHangHoa, @BarCode, getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@BarCode", "");
                        foreach (string barCode in ListBarCode)
                        {
                            myCommand.Parameters["@BarCode"].Value = barCode;
                            myCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public object ThemHangHoa(string IDNhomHang, string MaHang, string TenHangHoa, string IDDonViTinh, float GiaMua, float GiaSi, string GhiChu, string HinhAnh, float GiaLe)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object IDHangHoa = null;
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_HANGHOA] ([IDNhomHang], [MaHang], [TenHangHoa], [IDDonViTinh], [GiaMua], [GiaSi], [GhiChu], [HinhAnh], [NgayCapNhat],[GiaLe])" +
                                     " OUTPUT INSERTED.ID" +
                                                       " VALUES (@IDNhomHang, @MaHang, @TenHangHoa, @IDDonViTinh, @GiaMua, @GiaSi, @GhiChu, @HinhAnh, getdate(),@GiaLe)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNhomHang", IDNhomHang);
                        myCommand.Parameters.AddWithValue("@GiaLe", GiaLe);
                        myCommand.Parameters.AddWithValue("@MaHang", MaHang);
                        myCommand.Parameters.AddWithValue("@TenHangHoa", TenHangHoa);
                        myCommand.Parameters.AddWithValue("@IDDonViTinh", IDDonViTinh);
                        myCommand.Parameters.AddWithValue("@GiaMua", GiaMua);
                        myCommand.Parameters.AddWithValue("@GiaSi", GiaSi);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@HinhAnh", HinhAnh);
                        IDHangHoa = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();

                    return IDHangHoa;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public DataTable KiemTraHangHoa(string MaHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT ID FROM [GPM_HangHoa] WHERE [MaHang] = '" + MaHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        
        
        
        public void XoaHangHoa(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HANGHOA] SET [DAXOA] = 1 WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                    strSQL = "UPDATE [GPM_HangHoaTonKho] SET [DAXOA] = 1 WHERE [IDHangHoa] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                    strSQL = "UPDATE [GPM_HangHoa_Barcode] SET [DAXOA] = 1 WHERE [IDHangHoa] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public DataTable LayDanhSachHangHoa(string HienThi)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TOP " + HienThi + " [GPM_HANGHOA].* FROM [GPM_HANGHOA] WHERE GPM_HANGHOA.[DAXOA] = 0 ORDER BY [GPM_HANGHOA].TenHangHoa ASC ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public string LayTenHangHoa(string IDHH)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TenHangHoa FROM GPM_HANGHOA WHERE ID = '" + IDHH + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if(tb.Rows.Count != 0)
                        return tb.Rows[0]["TenHangHoa"].ToString();
                    return "";
                }
            }
        }

        public DataTable LayDanhSachHangHoa_Ten()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID, TenHangHoa FROM GPM_HANGHOA WHERE DAXOA = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable LayDanhSachHangHoa_IDNhom(string IdNhom)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID, TenHangHoa FROM GPM_HANGHOA WHERE DAXOA = 0 AND IDNhomHang = '" + IdNhom + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
    }
}