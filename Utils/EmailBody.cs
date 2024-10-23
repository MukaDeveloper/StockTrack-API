using System.Text;

namespace StockTrack_API
{
    public class EmailBody
    {
        public static string ConfirmationEmail(string name, string url)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"pt-BR\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine(
                "    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">"
            );
            sb.AppendLine("    <title>Confirmação de Conta - StockTrack</title>");
            sb.AppendLine("    <style>");
            sb.AppendLine("        body {");
            sb.AppendLine("            font-family: Arial, sans-serif;");
            sb.AppendLine("            background-color: #f4f4f4;");
            sb.AppendLine("            margin: 0;");
            sb.AppendLine("            padding: 20px;");
            sb.AppendLine("        }");
            sb.AppendLine("        .container {");
            sb.AppendLine("            background-color: #ffffff;");
            sb.AppendLine("            border-radius: 5px;");
            sb.AppendLine("            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);");
            sb.AppendLine("            padding: 20px;");
            sb.AppendLine("            max-width: 600px;");
            sb.AppendLine("            margin: auto;");
            sb.AppendLine("        }");
            sb.AppendLine("        .header {");
            sb.AppendLine("            text-align: center;");
            sb.AppendLine("            padding: 10px 0;");
            sb.AppendLine("        }");
            sb.AppendLine("        .header h1 {");
            sb.AppendLine("            color: #2c3e50;");
            sb.AppendLine("        }");
            sb.AppendLine("        .content {");
            sb.AppendLine("            font-size: 16px;");
            sb.AppendLine("            color: #34495e;");
            sb.AppendLine("        }");
            sb.AppendLine("        .button {");
            sb.AppendLine("            display: inline-block;");
            sb.AppendLine("            background-color: #2980b9;");
            sb.AppendLine("            color: white;");
            sb.AppendLine("            padding: 10px 20px;");
            sb.AppendLine("            text-decoration: none;");
            sb.AppendLine("            border-radius: 5px;");
            sb.AppendLine("            margin-top: 20px;");
            sb.AppendLine("        }");
            sb.AppendLine("        .footer {");
            sb.AppendLine("            text-align: center;");
            sb.AppendLine("            font-size: 12px;");
            sb.AppendLine("            color: #7f8c8d;");
            sb.AppendLine("            margin-top: 20px;");
            sb.AppendLine("        }");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("    <div class=\"container\">");
            sb.AppendLine("        <div class=\"header\">");
            sb.AppendLine("            <h1>Bem-vindo à StockTrack!</h1>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"content\">");
            sb.AppendLine($"            <p>Olá {name},</p>");
            sb.AppendLine(
                "            <p>Obrigado por se registrar no StockTrack. Para ativar sua conta, clique no botão abaixo:</p>"
            );
            sb.AppendLine(
                $"            <a href=\"{url}\" class=\"button\">Confirmar Conta</a>"
            );
            sb.AppendLine("            <p>Se você não se registrou, pode ignorar este e-mail.</p>");
            sb.AppendLine("        </div>");
            sb.AppendLine("        <div class=\"footer\">");
            sb.AppendLine("            <p>Atenciosamente,<br>Equipe StockTrack</p>");
            sb.AppendLine("        </div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            // Para retornar o HTML como uma string
            return sb.ToString();;
        }
    }
}
