<%@ Page Title="Estimate" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Estimate.aspx.cs" Inherits="GPEWeb.Quote" %>
<%@ MasterType VirtualPath="~/Site.Master" %> 

                

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

 
    
            <div class="jumbotron-fluid">
                <h1>Build a Pricing Estimate</h1>
                <p class="lead">Create a Pricing <strong>Estimate</strong> for a <strong>Customer</strong> based on various pricing <strong>Models</strong> that contain predetermined <strong>Inputs, Revenue, Expenses</strong> and <strong>Rates</strong>.  Use the default Rates from the Model or change them for your Estimate.</p>
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
            

            
              <div style="width: 100%; max-height: 500px; overflow-x: hidden; overflow-y: hidden; overflow-y: scroll;">  
                <asp:GridView ID="gvQuote" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="QuoteID"
                    ShowHeaderWhenEmpty="True" Width ="100%" AllowSorting="true" OnSorting="OnSorting"

                    OnRowCommand="gvQuote_RowCommand" OnRowEditing="gvQuote_RowEditing" OnRowCancelingEdit="gvQuote_RowCancelingEdit"
                    OnRowUpdating="gvQuote_RowUpdating" OnRowDeleting="gvQuote_RowDeleting" OnSelectedIndexChanged ="gvQuote_SelectedIndexChanged" OnRowDataBound="gvQuote_RowDataBound"

                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" CssClass="grid">
                                    
                    <Columns>
                        <asp:TemplateField HeaderText="Estimate #" SortExpression="QuoteID">
                            <ItemTemplate>
                                <asp:Button ButtonType="Link" Text='<%# Eval("QuoteID") %>' runat="server" CommandName="Select" Width="100%" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="280px" SortExpression="QuoteDesc">
                            <ItemTemplate>
                                <asp:Label ID="txtQuoteDesc" Text='<%# Eval("QuoteDesc") %>'  runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQuoteDesc" Text='<%# Eval("QuoteDesc") %>'  Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtQuoteDesc"  Width="100%" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer" SortExpression="CustomerName">
                            <ItemTemplate>
                                <asp:Label ID="lblCustomer" Text='<%# Eval("CustomerName") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCustomer" Width="100%" Height="26px" runat="server" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlCustomer" Width="100%" Height="26px" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pricing Model">  
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("ModelName") %>'  runat="server" />
                            </ItemTemplate>                             
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlModel" Width="100%" Height="25px"  runat="server" />
                            </FooterTemplate>  
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblAccountingType" Text='<%# Eval("StatusName") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlStatus" SelectedValue='<%# Eval("Status") %>' Width="100%" Height="26px" runat="server" >
                                        <asp:listitem text="In Progress" value="0"></asp:listitem>
                                        <asp:listitem text="Completed" value="1"></asp:listitem>
                                </asp:DropDownList>
                            </EditItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Revenue Per Month">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("RevenuePerMonth","{0:#,##0}") %>' runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expense Per Month">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("ExpensePerMonth","{0:#,##0}") %>' runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Margin Per Month">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("NetMarginPerMonth","{0:#,##0}") %>' runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Net Margin Percentage">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("PercentNetMargin","{0:0.0}") %>' runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="180px">
                            <ItemTemplate>
                                <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/process.png" runat="server" CommandName="Calculate" ToolTip="Calculate" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/comments.png" runat="server" CommandName="Comment" ToolTip='<%# Eval("Comment") %>' Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/report.png" runat="server" CommandName="Report" ToolTip="Show Report" Width="20px" Height="20px" AutoPostBack="false" Target="_blank" OnClientClick = <%# "window.open('" + ConfigurationManager.AppSettings["ReportServerURL"]  + "QuoteDetails&QuoteID=" + Eval("QuoteID") + "','_blank'); return false;" %> />
                                <asp:ImageButton ImageUrl="~/Images/invoice.png" runat="server" CommandName="Invoice" ToolTip="Show Invoice" Width="20px" Height="20px" AutoPostBack="false" Target="_blank" OnClientClick = <%# "window.open('" + ConfigurationManager.AppSettings["ReportServerURL"]  + "QuoteInvoice&QuoteID=" + Eval("QuoteID") + "','_blank'); return false;" %> />
                                <asp:ImageButton ImageUrl="~/Images/duplicate.png" runat="server" CommandName="Duplicate" ToolTip="Duplicate" Width="20px" Height="20px" AutoPostBack="true"/>
                                <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true" OnClientClick="return confirm('Are you sure you want to delete?');"/>
                                <%--<asp:ImageButton ImageUrl="~/Images/report.png" runat="server" CommandName="Report" ToolTip="Show Report" Width="20px" Height="20px" AutoPostBack="false" Target="_blank" OnClientClick = <%#"window.open('http://rparker-pc:8888/ReportServer/Pages/ReportViewer.aspx?/QuoteDetails&QuoteID=" +  Eval("QuoteID") + "','_blank'); return false;" %> />--%>
                                <%--<asp:ImageButton ImageUrl="~/Images/report.png" runat="server" CommandName="Report" ToolTip="Show Report" Width="20px" Height="20px" AutoPostBack="true" Target="_blank" />--%>
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
             </div>   
             <br />

            <table style="width: 100%" >
              <tr>
                <td>
                    <table>
                        <tr>
                            <td><asp:Button Text="Customer Data" BorderStyle="None" ID="InputTab" CssClass="Initial" runat="server" OnClick="Tab1_Click" /></td>
                            <td><asp:Button Text="Revenue" BorderStyle="None" ID="RevenueItemsTab" CssClass="Initial" runat="server" OnClick="Tab2_Click" /></td>
                            <td><asp:Button Text="Expenses" BorderStyle="None" ID="ExpenseItemsTab" CssClass="Initial" runat="server" OnClick="Tab3_Click" /></td>
                            <td><asp:Button Text="Constants" BorderStyle="None" ID="RatesTab" CssClass="Initial" runat="server" OnClick="Tab4_Click" /></td>
                            <td style="width: 90%">
                                <table style="width: 100%; text-align: center">
                                    <tr>
                                        <td><asp:Label ID="lblQuoteTitle" Text="" Font-Bold="True" font-size="18pt" font-italic="True" runat="server" /></td>
                                       </tr>           
                                </table>
                            </td>
                        </tr>           
                    </table>
                </td>
             </tr>
             <tr>
                <td>
                  <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                      <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                          <td >--%>

                            <asp:GridView ID="gvQuoteInput" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="QInputID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" 
                                OnRowEditing="gvQuoteInput_RowEditing" OnRowCancelingEdit="gvQuoteInput_RowCancelingEdit" OnRowUpdating="gvQuoteInput_RowUpdating" OnRowCommand="gvQuoteInput_RowCommand" OnSelectedIndexChanged = "OnSelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInputName" Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value"  ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Value","{0:#,##0.00}") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtInputValue" Text='<%# Eval("Value") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();"/>
                                        </EditItemTemplate>                           
                                    </asp:TemplateField>                        
                                    <asp:TemplateField ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px"/>
                                            <asp:ImageButton ID="btnComment" ImageUrl="~/Images/comments.png" runat="server" CommandName="Comment" ToolTip='<%# Eval("Comment") %>' Width="20px" Height="20px" AutoPostBack="true"/>
                                            
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px"/>
                                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                                        </EditItemTemplate>                            
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

                            <asp:GridView ID="gvQuoteRevenueItem" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="QItemID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" OnRowEditing="gvQuoteRevenueItem_RowEditing" OnRowCancelingEdit="gvQuoteRevenueItem_RowCancelingEdit"  OnRowUpdating="gvQuoteRevenueItem_RowUpdating" OnRowCommand="gvQuoteRevenueItem_RowCommand">
                                 
                                <Columns>
                                    <asp:TemplateField HeaderText="Group" >
                                        <ItemTemplate>
                                            <b><asp:Label ID="lblGroupName" Text='<%# Eval("GroupName") %>' runat="server" /></b>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Charge" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" Text='<%# Eval("Rate") %>' runat="server"  />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRate" Text='<%# Eval("Rate") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>                                         
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Charge Per Month" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPerMonth" Text='<%# Eval("AmountPerMonth","{0:#,##0}") %>' runat="server" />
                                        </ItemTemplate>                                                                  
                                    </asp:TemplateField>     
                                    <%--<asp:TemplateField HeaderText="Amount Per Year" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPerYear" Text='<%# Eval("AmountPerYear","{0:0.00}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount Per Week" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPerWeek" Text='<%# Eval("AmountPerWeek","{0:0.00}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount Per Day" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPerDay" Text='<%# Eval("AmountPerDay","{0:0.00}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField >--%>
                                    
                                                                             
                                    <asp:TemplateField ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/comments.png" runat="server" CommandName="Comment" ToolTip='<%# Eval("Comment") %>' Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/formula.png" runat="server" CommandName="Formula" Visible='<%# Eval("Formula").ToString() != "" %>' ToolTip='<%# Eval("Formula") %>' Width="20px" Height="20px" AutoPostBack="true"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                                        </EditItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="25px" Height="25px"/>
                                        </FooterTemplate>--%>                            
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
                      <%--<table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                          <td>--%>

                            <asp:GridView ID="gvQuoteExpenseItem" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="QItemID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" OnRowEditing="gvQuoteExpenseItem_RowEditing" OnRowCancelingEdit="gvQuoteExpenseItem_RowCancelingEdit"  OnRowUpdating="gvQuoteExpenseItem_RowUpdating"  OnRowCommand="gvQuoteExpenseItem_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Group" >
                                        <ItemTemplate>
                                            <b><asp:Label ID="lblGroupName" Text='<%# Eval("GroupName") %>' runat="server" /></b>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Cost" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" Text='<%# Eval("Rate") %>' runat="server"  />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRate" Text='<%# Eval("Rate") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>                                         
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cost Per Month" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPerMonth" Text='<%# Eval("AmountPerMonth","{0:#,##0}") %>' runat="server" />
                                        </ItemTemplate>                                                                 
                                    </asp:TemplateField>     
                                    <%--<asp:TemplateField HeaderText="Amount Per Year" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPerYear" Text='<%# Eval("AmountPerYear","{0:0.00}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount Per Week" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPerWeek" Text='<%# Eval("AmountPerWeek","{0:0.00}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount Per Day" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPerDay" Text='<%# Eval("AmountPerDay","{0:0.00}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                                                             
                                    <asp:TemplateField ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" AutoPostBack="true"/>                                            
                                            <asp:ImageButton ID="btnComment" ImageUrl="~/Images/comments.png" runat="server" CommandName="Comment" ToolTip='<%# Eval("Comment") %>' Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/formula.png" runat="server" CommandName="Formula" Visible='<%# Eval("Formula").ToString() != "" %>' ToolTip='<%# Eval("Formula") %>' Width="20px" Height="20px" AutoPostBack="true"/>
                                            <%--<asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" AutoPostBack="true" OnClientClick="return confirm('Are you sure you want to delete?');"/>--%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" AutoPostBack="true"/>
                                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                                        </EditItemTemplate>
                                        <%--<FooterTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="25px" Height="25px"/>
                                        </FooterTemplate>--%>                            
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
                    <asp:View ID="View4" runat="server">
                     <%-- <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                          <td>--%>
                            <asp:GridView ID="gvQuoteRate" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="QRateID"
                                ShowHeaderWhenEmpty="True" Width ="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" 
                                OnRowUpdating="gvQuoteRate_RowUpdating" OnRowEditing="gvQuoteRate_RowEditing" OnRowCancelingEdit="gvQuoteRate_RowCancelingEdit" OnRowCommand="gvQuoteRate_RowCommand">
                                <Columns>
                                   <asp:TemplateField HeaderText="Group">
                                        <ItemTemplate>
                                            <b><asp:Label ID="lblGroupName" Text='<%# Eval("GroupName") %>' runat="server" /></b>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRateName" Text='<%# Eval("Name") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("Rate") %>' runat="server" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRate" Text='<%# Eval("Rate") %>' Width="100%" runat="server" onfocus="Javascript:this.focus();this.select();" />
                                        </EditItemTemplate>                           
                                    </asp:TemplateField>                        
                                    <asp:TemplateField ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px"/>
                                            <asp:ImageButton ImageUrl="~/Images/comments.png" runat="server" CommandName="Comment" ToolTip='<%# Eval("Comment") %>' Width="20px" Height="20px" AutoPostBack="true"/>
                                            </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px"/>
                                            <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px"/>
                                        </EditItemTemplate>                            
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
                  </asp:MultiView>
                </td>
              </tr>
            </table>

            <!-- Modal Popup -->
            <div id="MyPopup" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                            <h4 class="modal-title">
                            </h4>
                        </div>
                        <div class="modal-body">
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
            


                
        

</asp:Content>

        