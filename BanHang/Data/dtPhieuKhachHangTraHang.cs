﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtPhieuKhachHangTraHang
    {
        public DataTable DanhSachSoDonHang(string IDKhachHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HoaDon] WHERE IDKhachHang = '" + IDKhachHang + "' AND TrangThaiCongNoKhachHang = 0 AND TrangThai = 0 AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachPhieuKhachHangTraHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GPM_PhieuKhachHangTraHang.* FROM GPM_PhieuKhachHangTraHang ORDER BY GPM_PhieuKhachHangTraHang.ID DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable ChiTietPhieuKhachHangTraHang_Temp(string IDPhhieuTraHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GPM_HangHoa.MaHang,GPM_HangHoa.IDDonViTinh,GPM_PhieuKhachHangTraHang_ChiTiet_Temp.* FROM GPM_PhieuKhachHangTraHang_ChiTiet_Temp, GPM_HangHoa WHERE GPM_HangHoa.ID = GPM_PhieuKhachHangTraHang_ChiTiet_Temp.IDHangHoa AND GPM_PhieuKhachHangTraHang_ChiTiet_Temp.IDPhieuKhachHangTraHang = '" + IDPhhieuTraHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable ChiTietPhieuKhachHangTraHang(string IDPhhieuTraHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GPM_HangHoa.MaHang,GPM_HangHoa.IDDonViTinh,GPM_PhieuKhachHangTraHang_ChiTiet.* FROM GPM_PhieuKhachHangTraHang_ChiTiet, GPM_HangHoa WHERE GPM_HangHoa.ID = GPM_PhieuKhachHangTraHang_ChiTiet.IDHangHoa AND GPM_PhieuKhachHangTraHang_ChiTiet.IDPhieuKhachHangTraHang = '" + IDPhhieuTraHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable HoaDon_ID(string IDHoaDon)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select * from GPM_HoaDon where ID = '" + IDHoaDon + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public DataTable DanhSachHangHoa_HoaDon(string IDHoaDon)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_HangHoa.TenHangHoa, GPM_HangHoa.MaHang, GPM_DonViTinh.TenDonViTinh, GPM_ChiTietHoaDon.* from GPM_DonViTinh,GPM_HangHoa,GPM_ChiTietHoaDon where GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh and GPM_ChiTietHoaDon.IDHangHoa = GPM_HangHoa.ID and GPM_ChiTietHoaDon.IDHoaDon = '" + IDHoaDon + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public void XoaPhieu_(string IDPhieu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "delete from GPM_PhieuKhachHangTraHang where ID = '" + IDPhieu + "'";
                using (SqlCommand myCommand = new SqlCommand(cmdText, con))
                {
                    myCommand.ExecuteNonQuery();
                }
            }
        }

        public void XoaChiTiet_Temp(string IDPhieu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "delete from GPM_PhieuKhachHangTraHang_ChiTiet_Temp where IDPhieuKhachHangTraHang = '" + IDPhieu + "'";
                using (SqlCommand myCommand = new SqlCommand(cmdText, con))
                {
                    myCommand.ExecuteNonQuery();
                }
            }
        }

        public void XoaChiTiet_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "delete from GPM_PhieuKhachHangTraHang_ChiTiet_Temp where ID = '" + ID + "'";
                using (SqlCommand myCommand = new SqlCommand(cmdText, con))
                {
                    myCommand.ExecuteNonQuery();
                }
            }
        }

        public DataTable ChiTietHangHoa_ID(string IDHangHoa, string IDPhieu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select * from GPM_PhieuKhachHangTraHang_ChiTiet_Temp where IDHangHoa = '" + IDHangHoa + "' and IDPhieuKhachHangTraHang = '" + IDPhieu + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable ChiTietTongSoLuongHangHoa(string IDPhieu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select IDPhieuKhachHangTraHang, SUM(SoLuong) as TongSoLuong, SUM(ThanhTien) as TongTien from GPM_PhieuKhachHangTraHang_ChiTiet_Temp where IDPhieuKhachHangTraHang = '" + IDPhieu + "' group by IDPhieuKhachHangTraHang";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable ChiTietHangHoa(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_HangHoa.IDDonViTinh, GPM_ChiTietHoaDon.* from GPM_HangHoa,GPM_ChiTietHoaDon where GPM_ChiTietHoaDon.IDHangHoa = GPM_HangHoa.ID and GPM_ChiTietHoaDon.IDHangHoa = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachChiTietHoaDon(string IDHangHoa, string IDHoaDon)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_HangHoa.IDDonViTinh, GPM_ChiTietHoaDon.* from GPM_HangHoa,GPM_ChiTietHoaDon where GPM_ChiTietHoaDon.IDHangHoa = GPM_HangHoa.ID and GPM_ChiTietHoaDon.IDHoaDon = '" + IDHoaDon + "' AND GPM_ChiTietHoaDon.IDHangHoa = '" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayPhieuTraHang_Null(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select * from GPM_PhieuKhachHangTraHang where IDKho = '" + IDKho + "' and IDHoaDon is null and IDNhanVien is null and IDKhachHang is null";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }

        public object ThemPhieuKhachHangTraHang(string MaHoaDon, string IDNhanVien, string IDKhachHang, string TongTienTra, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuChuyenKho = null;
                    string cmdText = "INSERT INTO [GPM_PhieuKhachHangTraHang] ([MaHoaDon],[IDNhanVien],[IDKhachHang],[NgayDoi],[TongTienTra],[GhiChu]) OUTPUT INSERTED.ID VALUES (@MaHoaDon,@IDNhanVien,@IDKhachHang,getdate(),@TongTienTra,@GhiChu)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                        myCommand.Parameters.AddWithValue("@TongTienTra", TongTienTra);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        IDPhieuChuyenKho = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuChuyenKho;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public object ThemChiTietPhieuKhachHangTraHang_Temp(string IDPhieuKhachHangTraHang, string IDHangHoa, string GiaBan, string SoLuong, string ThanhTien, string LyDoDoi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuChuyenKho = null;
                    string cmdText = "INSERT INTO [GPM_PhieuKhachHangTraHang_ChiTiet_Temp] ([IDPhieuKhachHangTraHang],[IDHangHoa],[GiaBan],[SoLuong],[ThanhTien],[LyDoDoi]) OUTPUT INSERTED.ID VALUES (@IDPhieuKhachHangTraHang,@IDHangHoa,@GiaBan,@SoLuong,@ThanhTien,@LyDoDoi)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuKhachHangTraHang", IDPhieuKhachHangTraHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        myCommand.Parameters.AddWithValue("@LyDoDoi", LyDoDoi);
                        IDPhieuChuyenKho = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuChuyenKho;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public object ThemChiTietPhieuKhachHangTraHang(object IDPhieuKhachHangTraHang, string IDHangHoa, string GiaBan, string SoLuong, string ThanhTien, string LyDoDoi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    object IDPhieuChuyenKho = null;
                    string cmdText = "INSERT INTO [GPM_PhieuKhachHangTraHang_ChiTiet] ([IDPhieuKhachHangTraHang],[IDHangHoa],[GiaBan],[SoLuong],[ThanhTien],[LyDoDoi]) OUTPUT INSERTED.ID VALUES (@IDPhieuKhachHangTraHang,@IDHangHoa,@GiaBan,@SoLuong,@ThanhTien,@LyDoDoi)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuKhachHangTraHang", IDPhieuKhachHangTraHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        myCommand.Parameters.AddWithValue("@LyDoDoi", LyDoDoi);
                        IDPhieuChuyenKho = myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                    return IDPhieuChuyenKho;
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public void CapNhatChiTietPhieuKhachHangTraHang_Temp(string IDPhieuKhachHangTraHang, string IDHangHoa, string GiaBan, string SoLuong, string ThanhTien, string LyDoDoi)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuKhachHangTraHang_ChiTiet_Temp set GiaBan = @GiaBan, SoLuong = @SoLuong, ThanhTien = @ThanhTien, LyDoDoi = @LyDoDoi where IDPhieuKhachHangTraHang = @IDPhieuKhachHangTraHang and IDHangHoa =@IDHangHoa";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDPhieuKhachHangTraHang", IDPhieuKhachHangTraHang);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        myCommand.Parameters.AddWithValue("@LyDoDoi", LyDoDoi);
                        myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }

        public void CapNhatPhieuKhachHangTraHang(string ID, string IDHoaDon, string IDNhanVien, string IDKhachHang, string TongHangHoaDoi, string TongTienTra, string GhiChu)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "update GPM_PhieuKhachHangTraHang set IDHoaDon = '" + IDHoaDon + "', IDNhanVien = '" + IDNhanVien + "', IDKhachHang = '" + IDKhachHang + "', TongHangHoaDoi = '" + TongHangHoaDoi + "', TongTienTra = '" + TongTienTra + "', GhiChu = '" + GhiChu + "' where ID = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteScalar();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
    }
}