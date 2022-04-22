<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script src="<%=ENETSDOMAIN%>/GW2/js/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="<%=ENETSDOMAIN%>/GW2/pluginpages/env.jsp"></script>
    <script type="text/javascript" src="<%=ENETSDOMAIN%>/GW2/js/apps.js"></script>
    <style>
        #anotherSection, #anotherSection fieldset {
            border: none !important;
        }
    </style>
        <br />
        <input type="hidden" id="txnReq" name="txnReq" runat="server" clientidmode="static">
        <input type="hidden" id="keyId" name="keyId" runat="server" clientidmode="static">
        <input type="hidden" id="hmac" name="hmac" runat="server" clientidmode="static">
        <input type="hidden" id="returntoken" name="returntoken" runat="server" clientidmode="static">
        <div id="anotherSection">
            <fieldset>
                <div id="ajaxResponse"></div>
            </fieldset>
        </div>
        <asp:Panel ID="pnlWait" runat="server" Visible="false">
            <h3 style="text-align: center; width: 100%;">Processing... Please wait.</h3>
        </asp:Panel>
    <script>
        window.onload = function () {
            var txnReq = document.forms[0].txnReq.value;
            var keyId = document.forms[0].keyId.value;
            var hmac = document.forms[0].hmac.value;
            if (txnReq && txnReq != "") {
                sendPayLoad(txnReq, hmac, keyId);
            }
        };
    </script>

</asp:Content>
