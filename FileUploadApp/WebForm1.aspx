<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FileUploadApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:FileUpload ID="FileUpload1" runat="server" style="margin-left: 360px" />
        <p style="margin-left: 440px">
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" 
                Width="70px" />
        </p>

    </form>
</body>
</html>
