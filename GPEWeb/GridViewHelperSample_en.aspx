<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GridViewHelperSample.aspx.cs" Inherits="GridViewHelperSample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<META http-equiv="Page-Enter" content="blendTrans(Duration=0.2)">
<META http-equiv="Page-Exit" content="blendTrans(Duration=0.2)">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>GridViewHelper: Creating groups and summaries with 2 lines of code</title>
    <style type="text/css">
    body, a, span, td, input, select, textarea { font-size: 12px; font-family: Tahoma,Arial,Geneva,Verdana,sans-serif; }
    body {
	margin:5px 5px 5px 5px;
	overflow: auto;
        }
    </style>
    <script type="text/javascript">
    var IE = (navigator.userAgent.indexOf('MSIE') != -1);
    function no_scrollbar()
    {
	    if(!IE) return;
	    // no scrollbars no IE
	    var root = document.all[1]; // IE >= 4
	    var firstCall = (root.style.overflow != 'auto');
	    document.body.style.width = root.clientWidth + 'px';

	    if(firstCall)
		    root.style.overflow = 'auto';
    }
    onload = no_scrollbar;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="mainDiv">
        <table cellspacing="5" align="center">
            <tr>
                <td valign="top" width="690"><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="northwindDS" EnableViewState="False" DataKeyNames="OrderId" CellPadding="3">
            <Columns>
                <asp:BoundField DataField="ShipRegion" HeaderText="ShipRegion" SortExpression="ShipRegion" />
                <asp:BoundField DataField="ShipName" HeaderText="ShipName" SortExpression="ShipName" />
                <asp:BoundField DataField="OrderId" HeaderText="OrderId" InsertVisible="False" ReadOnly="True"
                    SortExpression="OrderId" />
                <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="UnitPrice" DataFormatString="{0:c}" HeaderText="UnitPrice"
                    HtmlEncode="False" SortExpression="UnitPrice" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="ItemTotal" DataFormatString="{0:c}" HeaderText="ItemTotal"
                    HtmlEncode="False" ReadOnly="True" SortExpression="ItemTotal" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </td>
                <td valign="top" align="left" width="328">
                                <fieldset>
                <LEGEND>Sample</LEGEND>
        <asp:RadioButtonList ID="rdBtnList" runat="server" AutoPostBack="True">
            <asp:ListItem Value="0" Selected="True">Normal</asp:ListItem>
            <asp:ListItem Value="1">Total</asp:ListItem>
            <asp:ListItem Value="2">Custom Operation : GetMinQuantity</asp:ListItem>
            <asp:ListItem Value="3">Total and CustomOperation</asp:ListItem>
            <asp:ListItem Value="13">Non automatic summary</asp:ListItem>
            <asp:ListItem Value="4">Group</asp:ListItem>
            <asp:ListItem Value="5">Group (don't hide group column)</asp:ListItem>
            <asp:ListItem Value="6">Group with Group Summary </asp:ListItem>
            <asp:ListItem Value="7">Group with Group Summary and General Summary</asp:ListItem>
            <asp:ListItem Value="8">Suppress group (hide non summarized columns)</asp:ListItem>
            <asp:ListItem Value="9">Suppress group (don't hide)</asp:ListItem>
            <asp:ListItem Value="10">Two Groups</asp:ListItem>
            <asp:ListItem Value="11">Composite group</asp:ListItem>
            <asp:ListItem Value="12">Not supported: Summary of inner groups</asp:ListItem>
        </asp:RadioButtonList>
        </fieldset>
        <br />
        <fieldset id="fsGroupBy" runat="server">
                <LEGEND>Group by</LEGEND>
        <asp:RadioButtonList ID="rdBtnLstGroup" runat="server" AutoPostBack="True">
            <asp:ListItem Selected="True">ShipRegion</asp:ListItem>
            <asp:ListItem>ShipName</asp:ListItem>
            <asp:ListItem>OrderId</asp:ListItem>
        </asp:RadioButtonList>
        </fieldset>
        <br />
        <fieldset id="fsUseFooter" runat="server">
        <LEGEND>Other options</LEGEND>
                    <asp:CheckBox ID="cbxUseFooter" runat="server" AutoPostBack="True" Text="Use footer for general summaries" />
                    </fieldset>
                    </td>
            </tr>
        </table>

        </div>

        <asp:SqlDataSource ID="northwindDS" runat="server" ConnectionString="Data Source=.;Initial Catalog=northwind;Persist Security Info=True;User ID=northwindreader;Password=northwindreader"
            ProviderName="System.Data.SqlClient" SelectCommand="select ShipRegion, ShipName, Orders.OrderId, ProductName, Quantity, Products.UnitPrice, (Quantity * Products.UnitPrice) as ItemTotal&#13;&#10; from Orders, [Order Details], Products&#13;&#10; where Orders.OrderId = [Order Details].OrderId and [Order Details].ProductId = Products.ProductId&#13;&#10;  and ShipRegion is not null and ShipCountry in ('Brazil') and year(OrderDate) = 1998 and month(OrderDate) = 3 ">
        </asp:SqlDataSource>
    </form>
    <script type="text/javascript">
	var util = document.documentElement.clientWidth;
	if ( util == 0 ) util = document.body.clientWidth;
	if ( util < screen.width - 50 )
    {
	  window.moveTo(0,0);
	  window.resizeTo(screen.width,screen.height - 25);
	}
    </script>
</body>
</html>
