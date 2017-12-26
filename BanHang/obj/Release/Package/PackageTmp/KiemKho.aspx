<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="KiemKho.aspx.cs" Inherits="BanHang.KiemKho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="FormLayout1" runat="server" ColCount="4" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Thông tin phiếu kiểm kho" ColCount="4" ColSpan="4">
                <Items>
                    <dx:LayoutItem Caption="Người Điều Chỉnh" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txtNguoiLapPhieu" runat="server" Width="100%" Enabled="False">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ngày Điều Chỉnh">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxDateEdit ID="txtNgayLapPhieu" runat="server" OnInit="txtNgayLapPhieu_Init" DisplayFormatString="dd/MM/yyyy" Enabled="False" Width="100%">
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Trạng Thái">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server" Width="100%">
                                <dx:ASPxTextBox ID="FormLayout1_E2" runat="server" Enabled="False" Text="Phiếu tạm" Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ghi Chú" ColSpan="4">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Hàng Hóa" ColCount="4" ColSpan="4">
                <Items>
                    <dx:LayoutItem Caption="Chọn Hàng Hóa" ColSpan="3">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="txtBarcode" runat="server" ValueType="System.String" 
                                        DropDownWidth="600" 
                                        ValueField="ID" 
                                        NullText="Nhập tên hàng hóa..." Width="100%" TextFormatString="{1}"
                                        EnableCallbackMode="true" CallbackPageSize="10" DataSourceID="dsHangHoa" 
                                        >                                    
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="MaHang" Width="80px" Caption="Mã Hàng" />
                                            <dx:ListBoxColumn FieldName="TenHangHoa" Width="250px" Caption="Tên Hàng Hóa"/>
                                            <dx:ListBoxColumn FieldName="TenDonViTinh" Width="100px" Caption="Đơn Vị Tính"/>
                                        </Columns>
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="dsHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [GPM_HangHoa].[ID], [GPM_HangHoa].[MaHang], [GPM_HangHoa].[TenHangHoa], [GPM_HangHoa].[IDDonViTinh],[GPM_DonViTinh].TenDonViTinh FROM [GPM_HangHoa],[GPM_DonViTinh] WHERE ([GPM_HangHoa].[DaXoa] = @DaXoa AND [GPM_HangHoa].IDDonViTinh = [GPM_DonViTinh].ID)" >                                       
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnThemTemp" runat="server" OnClick="btnThemTemp_Click" Text="Thêm">
                                    <Image IconID="actions_add_32x32">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
             <dx:LayoutGroup ColSpan="4" Caption="Danh sách hàng hóa" ColCount="4">
                <Items>
                    <dx:LayoutItem Caption="" ColSpan="4">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                
                                <dx:ASPxGridView ID="gridDanhSachHangHoa_Temp" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" OnRowDeleting="gridDanhSachHangHoa_Temp_RowDeleting" Width="100%" OnRowUpdating="gridDanhSachHangHoa_Temp_RowUpdating">
                                    <SettingsPager Mode="ShowAllRecords">
                                    </SettingsPager>
                                    <SettingsEditing Mode="Batch">
                                    </SettingsEditing>
                                    <SettingsBehavior ProcessSelectionChangedOnServer="True" ConfirmDelete="True" />
                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton ButtonType="Image">
                                        </ShowAdaptiveDetailButton>
                                        <HideAdaptiveDetailButton ButtonType="Image">
                                        </HideAdaptiveDetailButton>
                                        <DeleteButton>
                                            <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                                            </Image>
                                        </DeleteButton>
                                    </SettingsCommandButton>
                                    <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn có chắc chắn muốn xóa hàng hóa khỏi phiếu kiểm này không??" CommandBatchEditCancel="Hủy thay đổi" CommandBatchEditUpdate="Lưu tất cả" EmptyDataRow="Danh sách hàng hóa trống." />
                                    <Columns>
                                        <dx:GridViewDataComboBoxColumn Caption="Tên Hàng" FieldName="IDHangHoa" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True">
                                            <PropertiesComboBox DataSourceID="sqlHangHoa" TextField="TenHangHoa" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="6">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Tồn Kho" FieldName="TonKho" ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="True">
<PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Chênh Lệch" FieldName="ChenhLech" ShowInCustomizationForm="True" VisibleIndex="5" ReadOnly="True">
<PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Thực Tế" FieldName="ThucTe" ShowInCustomizationForm="True" VisibleIndex="4">
<PropertiesSpinEdit DisplayFormatString="g" MinValue="0"></PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2">
                                            <PropertiesComboBox DataSourceID="SqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                    </Columns>
                                    <Styles>
                                        <Header Font-Bold="True" HorizontalAlign="Center">
                                        </Header>
                                        <AlternatingRow Enabled="True">
                                        </AlternatingRow>
                                        <TitlePanel Font-Bold="True" HorizontalAlign="Left">
                                        </TitlePanel>
                                    </Styles>
                                </dx:ASPxGridView>
                                                
                                <asp:SqlDataSource ID="SqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                                
                                <asp:SqlDataSource ID="sqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_HangHoa] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                                
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" HorizontalAlign="Right" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                <dx:ASPxButton ID="btnLuu" runat="server" Text="Lưu" OnClick="btnLuu_Click">
                                    <Image IconID="save_saveto_32x32">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                <dx:ASPxButton ID="btnHuy" runat="server" Text="Hủy" OnClick="btnHuy_Click">
                                    <Image IconID="save_saveandclose_32x32">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    <asp:HiddenField ID="IDPhieuKiemKho_Temp" runat="server" />
</asp:Content>
