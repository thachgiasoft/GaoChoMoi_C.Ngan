using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtChiPhi
    {
        public DataTable DanhSach()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ChiPhi] WHERE [DAXOA] = 0 ORDER BY ID DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatThongTin(string ID, string TenKhachHang, DateTime NgayChi, double TongChi, double DaChi, double ConLai, string GhiChu, string TrangThai)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_ChiPhi] SET [TrangThai] =@TrangThai,[GhiChu] = @GhiChu,[ConLai] =@ConLai,[DaChi] = @DaChi,[TenKhachHang] = @TenKhachHang,[NgayChi] = @NgayChi,[TongChi] = @TongChi, [NgayCapNhat] = getdate() WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@TenKhachHang", TenKhachHang);
                        myCommand.Parameters.AddWithValue("@NgayChi", NgayChi);
                        myCommand.Parameters.AddWithValue("@TongChi", TongChi);
                        myCommand.Parameters.AddWithValue("@DaChi", DaChi);
                        myCommand.Parameters.AddWithValue("@ConLai", ConLai);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public void ThemMoi(string TenKhachHang, DateTime NgayChi, double TongChi, double DaChi, double ConLai, string GhiChu, string TrangThai)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_ChiPhi] ([TenKhachHang],[NgayChi],[NgayCapNhat],[TongChi],[GhiChu],[TrangThai],[DaChi],[ConLai]) VALUES (@TenKhachHang,@NgayChi, getdate(),@TongChi,@GhiChu,@TrangThai,@DaChi,@ConLai)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@TenKhachHang", TenKhachHang);
                        myCommand.Parameters.AddWithValue("@NgayChi", NgayChi);
                        myCommand.Parameters.AddWithValue("@TongChi", TongChi);
                        myCommand.Parameters.AddWithValue("@GhiChu", GhiChu);
                        myCommand.Parameters.AddWithValue("@TrangThai", TrangThai);
                        myCommand.Parameters.AddWithValue("@DaChi", DaChi);
                        myCommand.Parameters.AddWithValue("@ConLai", ConLai);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void Xoa(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_ChiPhi] SET [DAXOA] = 1 WHERE [ID] = @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
    }

}