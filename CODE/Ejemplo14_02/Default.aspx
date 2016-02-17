<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.WebControls" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Demo LINQDataSource</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="style1" 
        style="font-family: Arial, Helvetica, sans-serif;font-size: large;font-weight: bold;color: #000000">
        <br />
        Demo LinqDataSource<br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" 
        DataMember="DefaultView" DataSourceID="LinqDataSource1" PageSize="15">
        <footerstyle backcolor="#F7DFB5" forecolor="#8C4510" />
        <rowstyle backcolor="#FFF7E7" forecolor="#8C4510" />
        <Columns>
            <asp:boundfield DataField="CodigoClub" HeaderText="Club" ReadOnly="True" 
                SortExpression="CodigoClub">
            <itemstyle horizontalalign="Center" width="70px" />
            </asp:boundfield>
            <asp:boundfield DataField="Nombre" HeaderText="Nombre" ReadOnly="True" 
                SortExpression="Nombre">
            <headerstyle horizontalalign="Left" />
            <itemstyle horizontalalign="Left" width="250px" />
            </asp:boundfield>
            <asp:boundfield DataField="FechaNacimiento" DataFormatString="{0:dd/MM/yyyy}" 
                HeaderText="Fecha Nac." ReadOnly="True" SortExpression="FechaNacimiento">
            <headerstyle horizontalalign="Center" />
            <itemstyle horizontalalign="Center" width="100px" />
            </asp:boundfield>
        </Columns>
        <pagerstyle forecolor="#8C4510" horizontalalign="Center" />
        <selectedrowstyle backcolor="#738A9C" font-bold="True" forecolor="White" />
        <headerstyle backcolor="#A55129" font-bold="True" forecolor="White" />
    </asp:GridView>
    </div>
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="FutbolDataContext" OrderBy="CodigoClub, Nombre" 
        Select="new (Nombre, FechaNacimiento, CodigoClub)" TableName="Futbolista" 
        Where="CodigoPaisNacimiento == @CodigoPaisNacimiento">
        <whereparameters>
            <asp:parameter DefaultValue="ES" Name="CodigoPaisNacimiento" Type="String" />
        </whereparameters>
    </asp:LinqDataSource>
    </form>
</body>
</html>
