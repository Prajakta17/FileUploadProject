<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FileUploadApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
    <style type="text/css">

body
{
    align-self:center;
    text-align:center;
    padding: 0;
    background-color: #B7CAF1;
    font-family: Arial;
}
.div1
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
}
.center
{
    z-index: 1000;
    margin: 250px auto;
    margin-left:37%;
    padding: 1px;
    width: 0px;
    height:1px;
    background-color: Black;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
}
.center img
{
    height: 182px;
    width: 346px;
    margin-left: 0px;
}
</style>

   <script type="text/javascript">
       function showProgress() {
           var updateProgress = $get("<%= UpdateProgress1.ClientID %>");
            updateProgress.style.display = "block";
        }
   </script>
</head>
<body>
    <h1>File Upload Application</h1>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <br /><br />

                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" OnClientClick="showProgress()" Text="Upload" Width="70px" />
                <br />
        <br />
                <br />
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5">
                    <ProgressTemplate>
                        <div class="div1">
                     <div class="center">
                         <img src="please_wait.gif" /></div>
                        </div>
                      <br />
                    
                    </ProgressTemplate>
                </asp:UpdateProgress>
                
                <p>
                    &nbsp;</p>


    </form>
</body>
</html>
