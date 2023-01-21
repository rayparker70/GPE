<%@ Page Title="Model" Language="C#" MasterPageFile="~/Site.Master" smartNavigation="true" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Model.aspx.cs" Inherits="GPEWeb.Model" %>
<%@ MasterType VirtualPath="~/Site.Master" %> 
                

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 
    
            <div class="jumbotron-fluid">
                <h1>Create a Model</h1>
                <p class="lead">Create a Pricing <strong>Model</strong> that contains user defined <strong>Inputs, Revenue, Expenses, Rates</strong> and <strong>Groups</strong>.  The Model can then be used to create a costing Estimate for a Customer.</p>
           </div>

    

            
                <asp:GridView ID="gvModel" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="ModelID"
                    ShowHeaderWhenEmpty="True" Width ="100%"

                    OnRowCommand="gvModel_RowCommand" OnRowEditing="gvModel_RowEditing" OnRowCancelingEdit="gvModel_RowCancelingEdit"
                    OnRowUpdating="gvModel_RowUpdating" OnRowDeleting="gvModel_RowDeleting" OnSelectedIndexChanged ="gvModel_SelectedIndexChanged" OnRowDataBound="gvModel_RowDataBound" 

                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" CssClass="grid">
                                    
                    <Columns>
                        <asp:TemplateField HeaderText="Model #" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Button ButtonType="Link" Text='<%# Eval("ModelID") %>' runat="server" CommandName="Select" Width="100%" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Model Name" >
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("Name") %>'  runat="server"  />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtModelName" Text='<%# Eval("Name") %>'  Width="100%" runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtModelName"  Width="100%" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/process.png" runat="server" CommandName="Calculate" ToolTip="Calculate" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/duplicate.png" runat="server" CommandName="Duplicate" ToolTip="Duplicate" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true" OnClientClick="return confirm('Are you sure you want to delete?');" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="25px" Height="25px"/>
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
     
        
            <table style="width: 100%" >
              <tr>
                <td>
                  <asp:Button Text="Input" BorderStyle="None" ID="InputTab" CssClass="Initial" runat="server" OnClick="Tab1_Click" />
                  <asp:Button Text="Revenue" BorderStyle="None" ID="RevenueItemsTab" CssClass="Initial" runat="server" OnClick="Tab2_Click" />
                  <asp:Button Text="Expenses" BorderStyle="None" ID="ExpenseItemsTab" CssClass="Initial" runat="server" OnClick="Tab3_Click" />
                  <asp:Button Text="Constants" BorderStyle="None" ID="RatesTab" CssClass="Initial" runat="server" OnClick="Tab4_Click" />
                  <asp:Button Text="Groups" BorderStyle="None" ID="GroupsTab" CssClass="Initial" runat="server" OnClick="Tab5_Click" />
                  
                  <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                      <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                          <td >--%>

                            <asp:GridView ID="gvModelInput" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="InputID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black"
                                OnRowEditing="gvModelInput_RowEditing" OnRowCancelingEdit="gvModelInput_RowCancelingEdit" OnRowUpdating="gvModelInput_RowUpdating" OnRowCommand="gvModelInput_RowCommand" OnRowDeleting="gvModelInput_RowDeleting" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Input Name">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtInputName" Text='<%# Eval("Name") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtInputName"  Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Input Key">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInputKey" Text='<%# Eval("InputKey") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtInputKey" Text='<%# Eval("InputKey") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtInputKey"  Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value"  ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInputValue" Text='<%# Eval("Value") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtInputValue" Text='<%# Eval("Value") %>' Width="100%" runat="server" />
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtInputValue" Width="100%" runat="server" />
                                        </FooterTemplate> 
                                    </asp:TemplateField>                        
                                    <asp:TemplateField ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true" OnClientClick="return confirm('Are you sure you want to delete?');"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="25px" Height="25px"/>
                                        </FooterTemplate>                            
                                    </asp:TemplateField>
                                </Columns>
                    
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="#FFFFCC" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                                 
                          <%--</td>
                        </tr>
                      </table>--%>
                    </asp:View>
                      <asp:View ID="View2" runat="server">
                      <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                          <td>--%>

                            <asp:GridView ID="gvModelRevenueItem" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="ItemID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" 
                                OnRowCancelingEdit="gvModelRevenueItem_RowCancelingEdit" OnRowEditing="gvModelRevenueItem_RowEditing" OnRowDataBound="gvModelRevenueItem_RowDataBound" OnRowUpdating="gvModelRevenueItem_RowUpdating" OnRowCommand="gvModelRevenueItem_RowCommand" OnRowDeleting="gvModelRevenueItem_RowDeleting" >
                                 
                                <Columns>
                                    <asp:TemplateField HeaderText="Group" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupName" Text='<%# Eval("GroupName") %>' runat="server" Font-Bold="True" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlGroupName" Width="100%" Height="26px" runat="server" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGroupName" Width="100%" Height="26px" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtItemName" Text='<%# Eval("Name") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtItemName"  Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Key">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemKey" Text='<%# Eval("ItemKey") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtItemKey" Text='<%# Eval("ItemKey") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtItemKey" Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Formula" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFormula" Text='<%# Eval("Formula") %>' runat="server" Font-Size="Smaller" Font-Italic="True" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFormula" Text='<%# Eval("Formula") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFormula" Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Rate" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" Text='<%# Eval("Rate") %>' runat="server"  />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRate" Text='<%# Eval("Rate") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRate" Text="0" Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" Text='<%# Eval("Amount","{0:0.00}") %>' runat="server"  />
                                        </ItemTemplate>                                                               
                                    </asp:TemplateField>                                         
                                    <asp:TemplateField ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true" OnClientClick="return confirm('Are you sure you want to delete?');"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="25px" Height="25px"/>
                                        </FooterTemplate>                            
                                    </asp:TemplateField>
                                </Columns>                                                                        
                                    
                                
                    
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="#FFFFCC" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>

                          <%--</td>
                        </tr>
                      </table>--%>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                      
                            
                            
                            <asp:GridView ID="gvModelExpenseItem" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="ItemID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" 
                                OnRowCancelingEdit="gvModelExpenseItem_RowCancelingEdit" OnRowEditing="gvModelExpenseItem_RowEditing" OnRowDataBound="gvModelExpenseItem_RowDataBound" OnRowUpdating="gvModelExpenseItem_RowUpdating" OnRowCommand="gvModelExpenseItem_RowCommand" OnRowDeleting="gvModelExpenseItem_RowDeleting" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Group" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupName" Text='<%# Eval("GroupName") %>' runat="server" Font-Bold="True" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlGroupName" Width="100%" Height="26px" runat="server" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGroupName" Width="100%" Height="26px" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtItemName" Text='<%# Eval("Name") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtItemName"  Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Key">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemKey" Text='<%# Eval("ItemKey") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtItemKey" Text='<%# Eval("ItemKey") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtItemKey" Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Formula" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFormula" Text='<%# Eval("Formula") %>' runat="server" Font-Size="Smaller" Font-Italic="True" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFormula" Text='<%# Eval("Formula") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtFormula" Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Rate" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" Text='<%# Eval("Rate") %>' runat="server"  />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRate" Text='<%# Eval("Rate") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRate" Text="0" Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" Text='<%# Eval("Amount","{0:0.00}") %>' runat="server"  />
                                        </ItemTemplate>                                                               
                                    </asp:TemplateField>                                         
                                    <asp:TemplateField ItemStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true" OnClientClick="return confirm('Are you sure you want to delete?');"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="25px" Height="25px"/>
                                        </FooterTemplate>                            
                                    </asp:TemplateField>
                                </Columns>
                    
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="#FFFFCC" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                           
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                      
                            <asp:GridView ID="gvModelRate" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="RateID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" 
                                OnRowUpdating="gvModelRate_RowUpdating" OnRowEditing="gvModelRate_RowEditing" OnRowCancelingEdit="gvModelRate_RowCancelingEdit" OnRowCommand="gvModelRate_RowCommand" OnRowDataBound="gvModelRate_RowDataBound" OnRowDeleting ="gvModelRate_RowDeleting">
                                <Columns>
                                   <asp:TemplateField HeaderText="Group">
                                        <ItemTemplate>
                                            <b><asp:Label ID="lblGroupName" Text='<%# Eval("GroupName") %>' Width="100%" Height="26px" runat="server" /></b>
                                        </ItemTemplate>
                                       <EditItemTemplate>
                                            <asp:DropDownList ID="ddlGroupName" Width="100%" Height="26px" runat="server" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlGroupName" Width="100%" Height="26px" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRateName" Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRateName" Text='<%# Eval("Name") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRateName"  Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate Key">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRateKey" Text='<%# Eval("RateKey") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRateKey" Text='<%# Eval("RateKey") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRateKey" Width="100%"  runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRateValue" Text='<%# Eval("Rate") %>'  runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRateValue" Text='<%# Eval("Rate") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRateValue" Width="100%"  runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>                        
                                    <asp:TemplateField ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true" OnClientClick="return confirm('Are you sure you want to delete?');"/>
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
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="#FFFFCC" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                         
                    </asp:View>
                    <asp:View ID="View5" runat="server">
                      
                            <asp:GridView ID="gvModelGroup" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="GroupID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" 
                                OnRowUpdating="gvModelGroup_RowUpdating" OnRowEditing="gvModelGroup_RowEditing" OnRowCancelingEdit="gvModelGroup_RowCancelingEdit" OnRowDeleting="gvModelGroup_RowDeleting" OnRowCommand="gvModelGroup_RowCommand">
                                <Columns>
                                   
                                    <asp:TemplateField HeaderText="Group Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupName" Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGroupName" Text='<%# Eval("Name") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtGroupName"  Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Key">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupKey" Text='<%# Eval("GroupKey") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGroupKey" Text='<%# Eval("GroupKey") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtGroupKey"  Width="100%" runat="server" />
                                        </FooterTemplate>
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Group Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccountingType" Text='<%# Eval("AccountingTypeName") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlAccountingType" SelectedValue='<%# Eval("AccountingType") %>' Width="100%" Height="26px" runat="server" >
                                                    <asp:listitem text="Revenue" value="R"></asp:listitem>
                                                    <asp:listitem text="Expense" value="E"></asp:listitem>
                                                    <asp:listitem text="Rate" value="X"></asp:listitem>
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlAccountingType" Width="100%" Height="26px" runat="server" >
                                                    <asp:listitem text="Revenue" value="R"></asp:listitem>
                                                    <asp:listitem text="Expense" value="E"></asp:listitem>
                                                    <asp:listitem text="Rate" value="X"></asp:listitem>
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupTotal" Text='<%# Eval("Total") %>' runat="server" />
                                        </ItemTemplate>                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true"  OnClientClick="return confirm('Are you sure you want to delete?');"/>
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
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="#FFFFCC" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                         
                    </asp:View>
                  </asp:MultiView>
                </td>
              </tr>
            </table>
            
            
            

            
                
        

</asp:Content>

