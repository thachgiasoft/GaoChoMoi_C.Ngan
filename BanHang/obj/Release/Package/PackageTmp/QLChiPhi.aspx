<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="QLChiPhi.aspx.cs" Inherits="BanHang.QLChiPhi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript">
        function OnMoreInfoClick(element, key) {
            popup.SetContentUrl("InPhieuThanhToan2.aspx?ID=" + key);
            popup.ShowAtElement();
            // alert(key);
        }

    </script>     
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Loại chi phí">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxGridView ID="gridChiPhi" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowDeleting="gridChiPhi_RowDeleting" OnRowInserting="gridChiPhi_RowInserting" OnRowUpdating="gridChiPhi_RowUpdating">
                                <SettingsEditing Mode="PopupEditForm">
                                </SettingsEditing>
                                <Settings AutoFilterCondition="Contains" ShowFilterRow="True" ShowTitlePanel="True" />
                                <SettingsBehavior ConfirmDelete="True" />
                                <SettingsCommandButton RenderMode="Image">
                                    <ShowAdaptiveDetailButton ButtonType="Image">
                                    </ShowAdaptiveDetailButton>
                                    <HideAdaptiveDetailButton ButtonType="Image">
                                    </HideAdaptiveDetailButton>
                                    <NewButton>
                                        <Image IconID="actions_add_16x16" ToolTip="Thêm">
                                        </Image>
                                    </NewButton>
                                    <UpdateButton ButtonType="Image" RenderMode="Image">
                                        <Image IconID="save_save_32x32office2013" ToolTip="Lưu">
                                        </Image>
                                    </UpdateButton>
                                    <CancelButton ButtonType="Image" RenderMode="Image">
                                        <Image IconID="actions_close_32x32" ToolTip="Hủy thao tác">
                                        </Image>
                                    </CancelButton>
                                    <EditButton ButtonType="Image" RenderMode="Image">
                                        <Image IconID="actions_edit_16x16devav" ToolTip="Sửa">
                                        </Image>
                                    </EditButton>
                                    <DeleteButton ButtonType="Image" RenderMode="Image">
                                        <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                                        </Image>
                                    </DeleteButton>
                                </SettingsCommandButton>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                                </SettingsPopup>
                                <SettingsSearchPanel Visible="True" />
                                <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin đơn vị tính" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
                                 <EditFormLayoutProperties>
                                     <Items>
                                         <dx:GridViewColumnLayoutItem Caption="Loại chi phí" ColumnName="Trạng Thái" Name="TrangThai">
                                         </dx:GridViewColumnLayoutItem>
                                         <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                                         </dx:EditModeCommandLayoutItem>
                                     </Items>
                                </EditFormLayoutProperties>
                                 <Columns>
                                     <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" VisibleIndex="2" Visible="False">
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="Trạng Thái" FieldName="TrangThai" VisibleIndex="3">
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewCommandColumn Caption="Chức năng" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="4">
                                     </dx:GridViewCommandColumn>
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
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
    
    <%--popup chi tiet don hang--%>
</asp:Content>
