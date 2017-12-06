using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using BanHang.Data;
using System.Data;

namespace BanHang
{
    public partial class BanHangLe1 : System.Web.UI.Page
    {
        public List<HoaDon> DanhSachHoaDon
        {
            get
            {
                if (ViewState["DanhSachHoaDon"] == null)
                    return new List<HoaDon>();
                else
                    return (List<HoaDon>)ViewState["DanhSachHoaDon"];
            }
            set
            {
                ViewState["DanhSachHoaDon"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTBanLe"] == "GPMBanLe")
            {
                //if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                //{
                    if (!IsPostBack)
                    {
                        DanhSachHoaDon = new List<HoaDon>();
                        ThemHoaDonMoi();
                        btnNhanVien.Text = Session["TenThuNgan"].ToString();
                        txtBarcode.Focus();
                        if (Session["IDNhom"].ToString() != "1")
                        {
                            gridChiTietHoaDon.Columns["dongia1"].Visible = false;
                            gridChiTietHoaDon.Columns["dongia2"].Visible = true;
                        }
                        else
                        {
                            gridChiTietHoaDon.Columns["dongia1"].Visible = true;
                            gridChiTietHoaDon.Columns["dongia2"].Visible = false;
                        }
                    }
                    DanhSachKhachHang();
                //}
                //else
                //{
                //    Response.Redirect("DangNhap.aspx");
                //}
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        public void BindGridChiTietHoaDon()
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            UpdateSTT(MaHoaDon);
            gridChiTietHoaDon.DataSource = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon;
            gridChiTietHoaDon.DataBind();
            formLayoutThanhToan.DataSource = DanhSachHoaDon[MaHoaDon];
            ccbKhachHang.SelectedIndex = DanhSachHoaDon[MaHoaDon].IDKhachHang;
            formLayoutThanhToan.DataBind();
            cmbChonGia.SelectedIndex = 0;
        }

        public void ThemHoaDonMoi()
        {
             dtKhachHang dtkh = new dtKhachHang();
             DataTable da = dtkh.LayDanhSachKhachHang();
             HoaDon hd = new HoaDon(da.Rows.Count);
            DanhSachHoaDon.Add(hd);
            Tab tabHoaDonNew = new Tab();
            int SoHoaDon = DanhSachHoaDon.Count;
            tabHoaDonNew.Name = SoHoaDon.ToString();
            tabHoaDonNew.Text = "Hóa đơn " + SoHoaDon.ToString();
            tabHoaDonNew.Index = SoHoaDon - 1;
            tabControlSoHoaDon.Tabs.Add(tabHoaDonNew);
            tabControlSoHoaDon.ActiveTabIndex = SoHoaDon - 1;
            BindGridChiTietHoaDon();
            txtTienThua.Value = 0;
        }
        public void HuyHoaDon()
        {
            int indexTabActive = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon.RemoveAt(indexTabActive);
            tabControlSoHoaDon.Tabs.RemoveAt(indexTabActive);
            for (int i = 0; i < tabControlSoHoaDon.Tabs.Count; i++)
            {
                tabControlSoHoaDon.Tabs[i].Text = "Hóa đơn " + (i + 1).ToString();
            }
            if (DanhSachHoaDon.Count == 0)
            {
                ThemHoaDonMoi();
            }
            else
            {
                BindGridChiTietHoaDon();
            }
        }
        public void ThemHangVaoChiTietHoaDon(DataTable tbThongTin)
        {
            txtKhachThanhToan.Text = "0";
            string MaHang = tbThongTin.Rows[0]["MaHang"].ToString();
            int IDHangHoa = Int32.Parse(tbThongTin.Rows[0]["ID"].ToString());
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.IDHangHoa == IDHangHoa && item.TrangThaiGia == Int32.Parse(cmbChonGia.Value.ToString()));
            if (exitHang != null)
            {
                // kiểm tra đơn giá 
                int SoLuong = exitHang.SoLuong + int.Parse(txtSoLuong.Text);
                double ThanhTienOld = exitHang.ThanhTien;
                exitHang.SoLuong = SoLuong;
                exitHang.HinhAnh = tbThongTin.Rows[0]["HinhAnh"].ToString();
                if (cmbChonGia.Value.ToString() == "0")
                {
                    // giá lẻ
                    exitHang.DonGia = double.Parse(tbThongTin.Rows[0]["GiaLe"].ToString());
                    exitHang.TrangThaiGia = 0;
                }
                else
                {
                    // giá sỉ
                    exitHang.DonGia = double.Parse(tbThongTin.Rows[0]["GiaSi"].ToString());
                    exitHang.TrangThaiGia = 1;
                }
                exitHang.TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), Session["IDKho"].ToString());
                exitHang.ThanhTien = SoLuong * exitHang.DonGia;
                DanhSachHoaDon[MaHoaDon].TongTien += SoLuong * exitHang.DonGia - ThanhTienOld;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;

            }
            else
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.IDHangHoa = IDHangHoa;
                cthd.MaHang = MaHang;
                cthd.TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), Session["IDKho"].ToString());
                cthd.TenHang = tbThongTin.Rows[0]["TenHangHoa"].ToString();
                cthd.SoLuong = int.Parse(txtSoLuong.Text);
                cthd.DonViTinh = tbThongTin.Rows[0]["TenDonViTinh"].ToString();
                if (cmbChonGia.Value.ToString() == "0")
                {
                    // giá lẻ
                    cthd.DonGia = double.Parse(tbThongTin.Rows[0]["GiaLe"].ToString());
                    cthd.TrangThaiGia = 0;
                }
                else
                {
                    // giá sỉ
                    cthd.DonGia = double.Parse(tbThongTin.Rows[0]["GiaSi"].ToString());
                    cthd.TrangThaiGia = 1;
                }
                cthd.GiaMua = double.Parse(tbThongTin.Rows[0]["GiaMua"].ToString());
                cthd.HinhAnh = tbThongTin.Rows[0]["HinhAnh"].ToString();
                cthd.ThanhTien = int.Parse(txtSoLuong.Text) * double.Parse(cthd.DonGia.ToString());
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Add(cthd);
                DanhSachHoaDon[MaHoaDon].SoLuongHang++;
                DanhSachHoaDon[MaHoaDon].TongTien += cthd.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
            }
        }
        protected void btnInsertHang_Click(object sender, EventArgs e)
        {
            try
            {
                dtBanHangLe dt = new dtBanHangLe();
                if (txtBarcode.Text.Trim() != "")
                {
                    DataTable tbThongTin;
                    if (txtBarcode.Value == null)
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Text.ToString());
                    }
                    else
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Value.ToString());
                    }

                    if (tbThongTin.Rows.Count > 0)
                    {
                        ThemHangVaoChiTietHoaDon(tbThongTin);
                        BindGridChiTietHoaDon();
                    }
                    else
                        HienThiThongBao("Mã hàng không tồn tại!!");
                }
                txtBarcode.Focus();
                txtBarcode.Text = "";
                txtBarcode.Value = "";
                txtSoLuong.Text = "1";
            }
            catch (Exception ex)
            {
                HienThiThongBao("Error: " + ex);
            }
        }
        public void HienThiThongBao(string thongbao)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + thongbao + "');", true);
        }
        protected void btnUpdateGridHang_Click(object sender, EventArgs e)
        {
            BatchUpdate();
            BindGridChiTietHoaDon();
        }
        private void BatchUpdate()
        {
            txtKhachThanhToan.Text = "0";
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            string IDKho = Session["IDKho"].ToString();
            for (int i = 0; i < gridChiTietHoaDon.VisibleRowCount; i++)
            {
                object SoLuong = gridChiTietHoaDon.GetRowValues(i, "SoLuong");
                object DonGiaCu = gridChiTietHoaDon.GetRowValues(i, "DonGia");
                ASPxSpinEdit spineditSoLuong = gridChiTietHoaDon.FindRowCellTemplateControl(i, (GridViewDataColumn)gridChiTietHoaDon.Columns["SoLuong"], "txtSoLuongChange") as ASPxSpinEdit;
                ASPxSpinEdit spineditDonGia;
                object GiaMoi = 0;
                object SoLuongMoi = spineditSoLuong.Value;
                if (Session["IDNhom"].ToString() == "1")
                {
                    spineditDonGia = gridChiTietHoaDon.FindRowCellTemplateControl(i, (GridViewDataColumn)gridChiTietHoaDon.Columns["DonGia"], "txtDonGia") as ASPxSpinEdit;
                    GiaMoi = spineditDonGia.Value;
                }
                else
                {
                    GiaMoi = DonGiaCu;
                }
                if (SoLuong != SoLuongMoi || DonGiaCu != GiaMoi)
                {
                    int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                    var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                    int SoLuongOld = exitHang.SoLuong;
                    double ThanhTienOld = exitHang.ThanhTien;
                    exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                    exitHang.DonGia = double.Parse(GiaMoi.ToString());
                    exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                    DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                    DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                }
            }
        }

        protected void txtKhachThanhToan_TextChanged(object sender, EventArgs e)
        {

            float TienKhachThanhToan;
            bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
            if (!isNumeric)
            {
                txtKhachThanhToan.Text = "";
                txtKhachThanhToan.Focus();
                HienThiThongBao("Nhập không đúng số tiền !!"); return;
            }
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon[MaHoaDon].KhachThanhToan = TienKhachThanhToan;
            DanhSachHoaDon[MaHoaDon].TienThua = TienKhachThanhToan - DanhSachHoaDon[MaHoaDon].KhachCanTra;
            if (txtKhachThanhToan.Text != "0")
            {

                txtTienThua.Text = DanhSachHoaDon[MaHoaDon].TienThua.ToString();
            }
        }

        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            try
            {
                int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                int STT = Convert.ToInt32(((ASPxButton)sender).CommandArgument);
                var itemToRemove =  DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Single(r => r.STT == STT);
                DanhSachHoaDon[MaHoaDon].SoLuongHang--;
                DanhSachHoaDon[MaHoaDon].TongTien = DanhSachHoaDon[MaHoaDon].TongTien - itemToRemove.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien -  DanhSachHoaDon[MaHoaDon].GiamGia;
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Remove(itemToRemove);
                BindGridChiTietHoaDon();
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        protected void UpdateSTT(int MaHoaDon)
        {
            for (int i = 1; i <= DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count; i++)
            {
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon[i - 1].STT = i;
            }
        }
        protected void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            HuyHoaDon();
        }

        protected void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            ThemHoaDonMoi();
        }

        protected void tabControlSoHoaDon_ActiveTabChanged(object source, TabControlEventArgs e)
        {
            BindGridChiTietHoaDon();
        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            if (DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count > 0)
            {
                float TienKhachThanhToan;
                bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
                if (!isNumeric)
                {
                    HienThiThongBao("Nhập không đúng số tiền !!"); return;
                }
                DanhSachHoaDon[MaHoaDon].KhachThanhToan = TienKhachThanhToan;
                dtBanHangLe dt = new dtBanHangLe();
                string IDKho = Session["IDKho"].ToString();
                string IDNhanVien = Session["IDThuNgan"].ToString();
                int IDKhachHang = 1;
                if (ccbKhachHang.Value != null)
                    IDKhachHang = Int32.Parse(ccbKhachHang.Value.ToString());
                if (IDKhachHang == 1)// khách lẻ
                {
                    if (TienKhachThanhToan < DanhSachHoaDon[MaHoaDon].KhachCanTra)
                    {
                        txtKhachThanhToan.Text = "";
                        txtKhachThanhToan.Focus();
                        HienThiThongBao("Thanh toán chưa đủ số tiền !!"); return;
                    }

                    object IDHoaDon = dt.InsertHoaDon(IDKho, IDNhanVien, IDKhachHang.ToString(), DanhSachHoaDon[MaHoaDon], "0", "0", "0", "0", "0", "0");
                    HuyHoaDon();
                    ccbKhachHang.Text = "";
                    chitietbuilInLai.ContentUrl = "~/InPhieuGiaoHang.aspx?IDHoaDon=" + IDHoaDon + "&KT=" + 1;
                    chitietbuilInLai.ShowOnPageLoad = true;
                    txtBarcode.Focus();
                }
                else// khách sỉ
                {
                    // tính chiết khấu khách sỉ
                    int TyLeChietKhauKhachHang = dtKhachHang.TyLeChietKhauKhachHang(IDKhachHang.ToString());
                    // nếu tiền chiết khấu lưu trong hóa đơn, tổng tiền còn lại thì cập nhật vào công nợ khách hàng
                    double CongNoCu = dtKhachHang.LayCongNoCuKhachHang(IDKhachHang.ToString());
                    double TongTienKhachHang = DanhSachHoaDon[MaHoaDon].KhachThanhToan - DanhSachHoaDon[MaHoaDon].KhachCanTra;//
                    double ChietKhauKhachHang = DanhSachHoaDon[MaHoaDon].TongTien * (TyLeChietKhauKhachHang / (float)100);
                    double CongNoMoi = CongNoCu + (TongTienKhachHang * -1);
                    object IDHoaDon = dt.InsertHoaDon(IDKho, IDNhanVien, IDKhachHang.ToString(), DanhSachHoaDon[MaHoaDon], ChietKhauKhachHang.ToString(), (TongTienKhachHang * -1).ToString(), TyLeChietKhauKhachHang.ToString(), "0", CongNoCu.ToString(), CongNoMoi.ToString());
                    HuyHoaDon();
                    ccbKhachHang.Text = "";
                    chitietbuilInLai.ContentUrl = "~/InPhieuGiaoHang.aspx?IDHoaDon=" + IDHoaDon + "&KT=" + 0;
                    chitietbuilInLai.ShowOnPageLoad = true;

                    txtBarcode.Focus();
                }
            }
            else
            {
                HienThiThongBao("Danh sách hàng hóa trống !!!");
                txtBarcode.Focus();
            }
        }
      
        protected void btnHuyKhachHang_Click(object sender, EventArgs e)
        {
            popupThemKhachHang.ShowOnPageLoad = false;
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            txtTenKhachHang.Text = "";
            cmbNhomKhachHang.Text = "";
            txtSoDienThoai.Text = "";
            txtDiaChi.Text = "";
            cmbChietKhau.Text = "";
            popupThemKhachHang.ShowOnPageLoad = true;
        }

        protected void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            if (cmbNhomKhachHang.Text != "" && txtTenKhachHang.Text != "" && cmbChietKhau.Text !="")
            {
                int IDNhom = Int32.Parse(cmbNhomKhachHang.Value.ToString());
                string TenKH = txtTenKhachHang.Text;
                string SDT = txtSoDienThoai.Text == null ? "" : txtSoDienThoai.Text;
                string DC = txtDiaChi.Text == null ? "" : txtDiaChi.Text;
                string IDChietKhau = cmbChietKhau.Value.ToString();
                dtKhachHang dtkh = new dtKhachHang();
                string MaKh = "";
                string Barcode = "";
                object ID = dtkh.ThemKhachHang(IDNhom, MaKh, TenKH, DateTime.Now, "", DC, SDT, "", Barcode, "", Session["IDKho"].ToString(), IDChietKhau);
                if (ID != null)
                {
                    dtkh = new dtKhachHang();
                    dtkh.CapNhatMaKhachHang(ID, (Session["IDKho"].ToString() + "." + ID).ToString(), (Session["IDKho"].ToString() + "." + ID).Replace(".", ""));
                }
                DanhSachKhachHang();
                txtTenKhachHang.Text = "";
                cmbNhomKhachHang.Text = "";
                txtSoDienThoai.Text = "";
                txtDiaChi.Text = "";
                cmbChietKhau.Text = "";
                HienThiThongBao("Thêm khách hàng thành công !!");
                popupThemKhachHang.ShowOnPageLoad = false; return;
            }
            else
            {
                HienThiThongBao("Vui lòng nhập thông tin đầy đủ (*) !!"); return;
            }
        }
        public void DanhSachKhachHang()
        {
            dtKhachHang dtkh = new dtKhachHang();
            ccbKhachHang.DataSource = dtkh.LayDanhSachKhachHangBanHang();
            ccbKhachHang.TextField = "TenKhachHang";
            ccbKhachHang.ValueField = "ID";
            ccbKhachHang.DataBind();
        }

        protected void txtBarcode_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaSi], [GiaLe] , [TenDonViTinh],[HinhAnh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang,GPM_HangHoa.HinhAnh, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaSi,GPM_HangHoa.GiaLe, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang) OR GPM_HangHoa.TenHangHoa LIKE @TenHang)  AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex ORDER BY TenHangHoa ASC";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void txtBarcode_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang,GPM_HangHoa.HinhAnh, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaSi ,GPM_HangHoa.GiaLe, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID) ORDER BY TenHangHoa ASC";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void ccbKhachHang_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            sqlKhachHang.SelectCommand = @"SELECT ID,TenKhachHang,DienThoai,DiaChi
                                        FROM (
	                                            select ID,TenKhachHang, DienThoai,DiaChi, row_number()over(order by MaKhachHang) as [rn] 
	                                            FROM GPM_KhachHang
	                                            WHERE ((TenKhachHang LIKE @TenKhachHang OR DienThoai LIKE @DienThoai OR DiaChi LIKE @DiaChi) AND (DaXoa = 0))
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex ORDER BY TenKhachHang ASC";
            sqlKhachHang.SelectParameters.Clear();
            sqlKhachHang.SelectParameters.Add("TenKhachHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("DienThoai", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("DiaChi", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            sqlKhachHang.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = sqlKhachHang;
            comboBox.DataBind();
        }
        

        protected void ccbKhachHang_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            sqlKhachHang.SelectCommand = @"SELECT ID,TenKhachHang,DienThoai,DiaChi
                                        FROM GPM_KhachHang
                                        WHERE (GPM_KhachHang.ID = @ID)  ORDER BY TenKhachHang ASC";
            sqlKhachHang.SelectParameters.Clear();
            sqlKhachHang.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = sqlKhachHang;
            comboBox.DataBind();
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "")
            {
                string TuKhoa = txtTimKiem.Text.ToString();
                dtBanHangLe dt = new dtBanHangLe();
                DataTable db = dt.LayThongHoaDon(TuKhoa);
                if (db.Rows.Count > 0)
                {
                    string IDKH = 1 + "";
                    if (Int32.Parse(db.Rows[0]["IDKhachHang"].ToString()) != 1)
                        IDKH = 0 + "";

                    chitietbuilInLai.ContentUrl = "~/InPhieuGiaoHang.aspx?IDHoaDon=" + db.Rows[0]["ID"].ToString() + "&KT=" + IDKH;
                    chitietbuilInLai.ShowOnPageLoad = true;
                }
                else
                {
                    txtTimKiem.Focus();
                    HienThiThongBao("Không tìm thấy dữ liệu cần tìm?");
                }
            }
            else
            {
                txtTimKiem.Focus();
                HienThiThongBao("Vui lòng nhập thông tin cần tìm?");
            }
        }
    }
    [Serializable]
    public class HoaDon
    {
        public int IDHoaDon { get; set; }
        public int IDKhachHang { get; set; }
        public int SoLuongHang { get; set; }
        public double TongTien { get; set; }
        public double GiamGia { get; set; }
        public double KhachCanTra { get; set; }
        public double KhachThanhToan { get; set; }
        public double TienThua { get; set; }
        public List<ChiTietHoaDon> ListChiTietHoaDon { get; set; }
        public HoaDon(int idkh)
        {
            SoLuongHang = 0;
            TongTien = 0;
            GiamGia = 0;
            KhachCanTra = 0;
            KhachThanhToan = 0;
            TienThua = 0;
            IDKhachHang = idkh;
            ListChiTietHoaDon = new List<ChiTietHoaDon>();
        }
    }
    [Serializable]
    public class ChiTietHoaDon
    {
        public int STT { get; set; }
        public string MaHang { get; set; }
        public int IDHangHoa { get; set; }
        public string TenHang { get; set; }
        public int MaDonViTinh { get; set; }
        public int TonKho { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public double DonGia { get; set; }
        public double GiaMua { get; set; }
        public double ThanhTien { get; set; }
        public int TrangThaiGia { get; set; }
        public string HinhAnh { get; set; }
        public ChiTietHoaDon()
        {
            TrangThaiGia = 0;
            TonKho = 0;
            SoLuong = 0;
            DonGia = 0;
            ThanhTien = 0;
        }
    }
}