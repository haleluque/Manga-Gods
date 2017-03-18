<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutComplete.aspx.cs" Inherits="MangaGods.Checkout.CheckoutComplete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h1>Checkout Complete</h1>
    <p></p>
    <h3>Payment Transaction ID:</h3>
    <asp:Label ID="lblIdTransaccion" runat="server"></asp:Label>
    <p></p>
    <h3>Thank You!</h3>
    <p></p>
    <hr />
    <asp:Button ID="bntContinuar" runat="server" Text="Continue Shopping" OnClick="bntContinuar_Click" />
</asp:Content>
