<%@ Page Title="Model" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="GPEWeb.Customer" %>
<%@ MasterType VirtualPath="~/Site.Master" %> 
                

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 
    
            <div class="jumbotron-fluid">
                <h1>Create a Customer</h1>
                <p class="lead">Create a <strong>Customer</strong> that can be references on a <strong>Estimate</strong>.</p>
           </div>

            
           
            <table style="width: 100%" >
                <tr>
                <td></td>
                <td style="text-align:right">
                    <asp:Label ID="lblStatus" Text="" runat="server"/>                        
                </td>
                </tr>
            </table>
            <br /> 
            

            
                
                <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="CustomerID"
                    ShowHeaderWhenEmpty="True" Width ="100%" AllowSorting="true" OnSorting="OnSorting"

                    OnRowCommand="gvCustomer_RowCommand" OnRowEditing="gvCustomer_RowEditing" OnRowCancelingEdit="gvCustomer_RowCancelingEdit"
                    OnRowUpdating="gvCustomer_RowUpdating" OnRowDeleting="gvCustomer_RowDeleting" OnSelectedIndexChanged ="gvCustomer_SelectedIndexChanged"  

                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" CssClass="grid">
                                    
                    <Columns>
                        <asp:TemplateField HeaderText="Customer #" ItemStyle-Width="200px" SortExpression="CustomerID">
                            <ItemTemplate>
                                <asp:Button ButtonType="Link" Text='<%# Eval("CustomerID") %>' runat="server" CommandName="Select" Width="100%" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name" SortExpression="Name">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("Name") %>'  runat="server"  />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCustomerName" Text='<%# Eval("Name") %>'  Width="100%" runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtCustomerName"  Width="100%" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Name" >
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("ContactName") %>'  runat="server"  />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtContactName" Text='<%# Eval("ContactName") %>'  Width="100%" runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtContactName"  Width="100%" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Email" >
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("ContactEmail") %>'  runat="server"  />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtContactEmail" Text='<%# Eval("ContactEmail") %>'  Width="100%" runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtContactEmail"  Width="100%" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contact Phone" >
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("ContactPhone") %>'  runat="server"  />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtContactPhone" Text='<%# Eval("ContactPhone") %>'  Width="100%" runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtContactPhone"  Width="100%" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#FFFFCC" Font-Bold="True"  />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
                
             <br />


           

</asp:Content>

