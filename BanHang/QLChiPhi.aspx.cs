using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class QLChiPhi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            gridChiPhi.DataSource = dtQLChiPhi.DanhSach();
            gridChiPhi.DataBind();
        }

        protected void gridChiPhi_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dtQLChiPhi.Xoa(ID);
            e.Cancel = true;
            gridChiPhi.CancelEdit();
            LoadGrid();
        }

        protected void gridChiPhi_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            string LoaiChiPhi = e.NewValues["TrangThai"].ToString();
            dtQLChiPhi.ThemMoi(LoaiChiPhi);
            e.Cancel = true;
            gridChiPhi.CancelEdit();
            LoadGrid();
        }

        protected void gridChiPhi_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string LoaiChiPhi = e.NewValues["TrangThai"].ToString();
            dtQLChiPhi.CapNhat(ID,LoaiChiPhi);
            e.Cancel = true;
            gridChiPhi.CancelEdit();
            LoadGrid();
        }
    }
}