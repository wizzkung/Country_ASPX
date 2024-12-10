<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Country.aspx.cs" Inherits="Country_ASPX.Country" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Country Management</title>
    <link href="Content\bootstrap.min.css" rel="stylesheet" />
        <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f9;
        }
        .container {
            margin: 20px auto;
            padding: 10px;
            max-width: 1200px;
            background: #fff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }
        .block {
            margin-bottom: 20px;
            padding: 15px;
            background-color: #e9ecef;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .grid {
     width: 100%; /* Растягивает GridView на всю ширину */
    border-collapse: collapse; /* Убирает лишние отступы между ячейками */
}

.grid th, .grid td {
    border: 1px solid #ddd; /* Рамка вокруг ячеек */
    padding: 8px; /* Отступы внутри ячеек */
    text-align: left; /* Выравнивание текста в ячейках */
}

.grid th {
    background-color: #f4f4f4; /* Фон для заголовков */
    font-weight: bold; /* Полужирный шрифт для заголовков */
}

.grid td {
    word-wrap: break-word; /* Разрыв длинных слов (если есть) */
}
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Поиск 1 -->
            <div class="block">
                
                <h3>Поиск по названию города</h3>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button runat="server" Text="Поиск" ID="btSearchName" OnClick="btSearchName_Click"></asp:Button>
            </div>
            <div class="block">
                 <h3>Добавить город</h3>
                <asp:TextBox ID="tbCityName" runat="server"></asp:TextBox>
                <asp:TextBox ID="tbPopulation" runat="server"></asp:TextBox><asp:TextBox ID="tbid" runat="server"></asp:TextBox><asp:Button runat="server" Text="Добавить" ID="CityAdd" OnClick="CityAdd_Click"></asp:Button><asp:Button runat="server" Text="Изменить" ID="btUpdate" OnClick="btUpdate_Click"></asp:Button><asp:DropDownList ID="ddlCountry" runat="server" DataTextField="Name" DataValueField="Id" />

                
            </div>

            <!-- Поиск 2 -->
            <div class="block">
                <h3>Поиск по стране</h3>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><asp:Button runat="server" Text="Поиск" ID="btSearchCountry" OnClick="btSearchCountry_Click"></asp:Button>
            </div>
            
            <div class="block">
                <h3>Добавить страну</h3>
                <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                <asp:TextBox ID="tbSize" runat="server"></asp:TextBox>
                <asp:TextBox ID="tbCityID" runat="server"></asp:TextBox>
                <asp:Button ID="btAddCountry" runat="server" Text="Добавить" />

            </div>

            <!-- Поиск 3 -->
            <div class="block" id="btEXCEL">
                <h3>Операции</h3>
                <asp:TextBox runat="server" ID="tbdelupd"></asp:TextBox>
                <asp:Button runat="server" Text="Удалить" ID="btDelete" OnClick="btDelete_Click"></asp:Button>
                <asp:Button runat="server" Text="Показать все" ID="btAll" OnClick="btAll_Click"></asp:Button><asp:Button runat="server" Text="CSV" ID="btCSV" OnClick="btCSV_Click"></asp:Button><asp:Button runat="server" Text="EXCEL" ID="btEXCEL" OnClick="btEXCEL_Click"></asp:Button>            
            </div>

            <!-- Грид -->
            <div class="grid" id="gv">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" CssClass="grid" />
            </div>
        </div>
    </form>
</body>
</html>
