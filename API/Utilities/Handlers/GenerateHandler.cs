using API.DTOs.TransactionEvents;
using System;
using System.Globalization;
using System.Xml.Linq;

namespace API.Utilities.Handler;

public class GenerateHandler // Class untuk menghandle generate NIK baru
{
    public static string Nik(string? nik = null)
    {
        if (nik is null) // Jika NIK terakhir belum ada, maka NIK baru akan dimulai dari 111111
            return "111111";

        var generateNik = int.Parse(nik) + 1; // Jika NIK terakhir sudah ada, maka NIK baru akan ditambahkan 1

        return generateNik.ToString(); // Mengembalikan NIK baru dalam bentuk string
    }

    public static string Invoice(string? invoice = null)
    {
        if (invoice is null)
        {
            int currentYear = DateTime.Now.Year;
            return $"TRS-{currentYear}-0001";
        }
        string getYear = invoice.Substring(4, 4);
        string getNumber = invoice.Substring(9, 4);
        int numberInt = int.Parse(getNumber);
        numberInt++;
        string newNumberString = numberInt.ToString("D4");
        return $"TRS-{getYear}-{newNumberString}";
    }

    public static string EmailTransactionTemplate(TransactionDetailDto data, string msg)
    {
        string formattedDate = data.EventDate.ToString("dd/MM/yyyy");
        var price = Convert.ToInt32(data.Price).ToString();
        return @"
<!doctype html>
<html>
  <head>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>
    <meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
    <style>
      img {
        border: none;
        -ms-interpolation-mode: bicubic;
        max-width: 100%; 
      }

      body {
        background-color: #f6f6f6;
        font-family: sans-serif;
        -webkit-font-smoothing: antialiased;
        font-size: 14px;
        line-height: 1.4;
        margin: 0;
        padding: 0;
        -ms-text-size-adjust: 100%;
        -webkit-text-size-adjust: 100%; 
      }

      table {
        border-collapse: separate;
        mso-table-lspace: 0pt;
        mso-table-rspace: 0pt;
        width: 100%; }
        table td {
          font-family: sans-serif;
          font-size: 14px;
          vertical-align: top; 
      }
      .body {
        background-color: #f6f6f6;
        width: 100%; 
      }
       .container {
        display: block;
        margin: 0 auto !important;
        /* makes it centered */
        max-width: 580px;
        padding: 10px;
        width: 580px; 
      }
      .content {
        box-sizing: border-box;
        display: block;
        margin: 0 auto;
        max-width: 580px;
        padding: 10px; 
      }
      .main {
        background: #ffffff;
        border-radius: 3px;
        width: 100%; 
      }

      .wrapper {
        box-sizing: border-box;
        padding: 20px; 
      }

      .content-block {
        padding-bottom: 10px;
        padding-top: 10px;
      }

      .footer {
        clear: both;
        margin-top: 10px;
        text-align: center;
        width: 100%; 
      }
        .footer td,
        .footer p,
        .footer span,
        .footer a {
          color: #999999;
          font-size: 12px;
          text-align: center; 
      }
      h1,
      h2,
      h3,
      h4 {
        color: #000000;
        font-family: sans-serif;
        font-weight: 400;
        line-height: 1.4;
        margin: 0;
        margin-bottom: 30px; 
      }

      h1 {
        font-size: 35px;
        font-weight: 300;
        text-align: center;
        text-transform: capitalize; 
      }

      p,
      ul,
      ol {
        font-family: sans-serif;
        font-size: 14px;
        font-weight: normal;
        margin: 0;
        margin-bottom: 15px; 
      }
        p li,
        ul li,
        ol li {
          list-style-position: inside;
          margin-left: 5px; 
      }

      a {
        color: #3498db;
        text-decoration: underline; 
      }
      .last {
        margin-bottom: 0; 
      }

      .first {
        margin-top: 0; 
      }

      .align-center {
        text-align: center; 
      }

      .align-right {
        text-align: right; 
      }

      .align-left {
        text-align: left; 
      }

      .clear {
        clear: both; 
      }

      .mt0 {
        margin-top: 0; 
      }

      .mb0 {
        margin-bottom: 0; 
      }

      .preheader {
        color: transparent;
        display: none;
        height: 0;
        max-height: 0;
        max-width: 0;
        opacity: 0;
        overflow: hidden;
        mso-hide: all;
        visibility: hidden;
        width: 0; 
      }

      .powered-by a {
        text-decoration: none; 
      }

      hr {
        border: 0;
        border-bottom: 1px solid #f6f6f6;
        margin: 20px 0; 
      }
      @media all {
        .ExternalClass {
          width: 100%; 
        }
        .ExternalClass,
        .ExternalClass p,
        .ExternalClass span,
        .ExternalClass font,
        .ExternalClass td,
        .ExternalClass div {
          line-height: 100%; 
        }
        .apple-link a {
          color: inherit !important;
          font-family: inherit !important;
          font-size: inherit !important;
          font-weight: inherit !important;
          line-height: inherit !important;
          text-decoration: none !important; 
        }
        #MessageViewBody a {
          color: inherit;
          text-decoration: none;
          font-size: inherit;
          font-family: inherit;
          font-weight: inherit;
          line-height: inherit;
        }
      }

    </style>
  </head>
  <body>
    <span class=""preheader"">This is preheader text. Some clients will show this text as a preview.</span>
    <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"" class=""body"">
      <tr>
        <td>&nbsp;</td>
        <td class=""container"">
          <div class=""content"">

            <!-- START CENTERED WHITE CONTAINER -->
            <table role=""presentation"" class=""main"">
              <tr>
                <td class=""wrapper"">
                  <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                    <tr>
                      <td>
                        <p>Dear "+ data.FirstName + " " + data.LastName + @",</p>
                        <p>"+ msg + @"</p>
                        <p>Invoice Number: " + data.Invoice + @"</p>
                        <p>Package Name : "+ data.Package + @"</p>
                        <p>Email : "+ data.Email + @"</p>
                        <p>Event Date : "+ formattedDate + @"</p>
                        <p>Total Price : Rp. "+ price + @"</p><br/>
                        <p>--</p>
                        <p>Evora</p>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </table>
            <!-- END CENTERED WHITE CONTAINER -->

            <!-- START FOOTER -->
            <div class=""footer"">
              <table role=""presentation"" border=""0"" cellpadding=""0"" cellspacing=""0"">
                <tr>
                  <td class=""content-block"">
                    <span class=""apple-link"">Evora, Unit H1, The Mansion, Jakarta</span>
                    <br> Don't like these emails? <a href=""http://i.imgur.com/CScmqnj.gif"">Unsubscribe</a>.
                  </td>
                </tr>
                <tr>
                  <td class=""content-block powered-by"">
                    Powered by <a href=""http://htmlemail.io"">HTMLemail</a>.
                  </td>
                </tr>
              </table>
            </div>
          </div>
        </td>
        <td>&nbsp;</td>
      </tr>
    </table>
  </body>
</html>
        ";
    }
}