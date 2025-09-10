# Todo API - C# Web API Server

**[English](README.md) | 繁體中文**

這是 Todo List 應用程式的後端 API，使用 .NET 8 Web API 和 Entity Framework Core 開發。

## 安裝與執行

### 先決條件

- .NET 8 SDK
- Visual Studio 2022 或 VS Code (可選)

### 安裝與啟動

```bash
# 恢復 NuGet 套件
dotnet restore

# 啟動開發服務器
dotnet run
```

API 將在以下端點啟動：
- HTTP: `http://localhost:7000`
- HTTPS: `https://localhost:7001`
- Swagger UI: `https://localhost:7001/swagger`

## 專案結構

```
server/
├── Controllers/         # API 控制器
├── Data/               # 資料庫上下文
├── DTOs/               # 資料傳輸物件
├── Models/             # 資料模型
├── Properties/         # 啟動設定
├── appsettings.json    # 應用程式設定
└── Program.cs          # 應用程式進入點
```

## API 端點

### Todo 管理

| 方法 | 端點 | 描述 |
|------|------|------|
| GET | `/api/todos` | 取得所有 Todo 項目 |
| GET | `/api/todos/{id}` | 取得特定 Todo 項目 |
| POST | `/api/todos` | 建立新的 Todo 項目 |
| PUT | `/api/todos/{id}` | 更新 Todo 項目 |
| DELETE | `/api/todos/{id}` | 刪除 Todo 項目 |

### 請求範例

#### 建立 Todo
```http
POST /api/todos
Content-Type: application/json

{
  "title": "學習 Ionic",
  "description": "完成 Ionic Angular 教學"
}
```

#### 更新 Todo
```http
PUT /api/todos/1
Content-Type: application/json

{
  "title": "更新的標題",
  "isCompleted": true
}
```

## 資料模型

### Todo 模型
```csharp
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

## 配置

### 資料庫設定

目前使用 In-Memory Database，適合開發和測試。如需使用實際資料庫，請修改 `Program.cs`：

```csharp
// 使用 SQL Server
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(connectionString));

// 使用 SQLite
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite(connectionString));
```

### CORS 設定

CORS 已配置為允許來自前端應用的請求：

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:8100", "http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
```

## 開發工具

### Swagger UI
訪問 `https://localhost:7001/swagger` 查看和測試 API。

### Entity Framework 工具

```bash
# 安裝 EF 工具
dotnet tool install --global dotnet-ef

# 建立遷移 (如使用實際資料庫)
dotnet ef migrations add InitialCreate

# 更新資料庫
dotnet ef database update
```

## 建置與部署

### 開發建置
```bash
dotnet build
```

### 發佈應用
```bash
# 發佈為自包含應用
dotnet publish -c Release --self-contained

# 發佈為框架相依應用
dotnet publish -c Release
```

### Docker 部署

建立 `Dockerfile`：
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY bin/Release/net8.0/publish/ ./
ENTRYPOINT ["dotnet", "TodoApi.dll"]
```

## 安全性考量

- 目前未實作驗證授權
- 生產環境請考慮加入：
  - JWT 驗證
  - API 金鑰
  - 速率限制
  - HTTPS 強制

## 疑難排解

### 常見問題

1. **CORS 錯誤**: 檢查 CORS 政策設定
2. **連接埠衝突**: 修改 `launchSettings.json` 中的連接埠
3. **SSL 憑證問題**: 執行 `dotnet dev-certs https --trust`

### 記錄設定

修改 `appsettings.json` 調整記錄等級：

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```
