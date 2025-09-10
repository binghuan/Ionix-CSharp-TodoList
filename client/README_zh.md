# Todo Client - Ionic Angular App

**[English](README.en.md) | 繁體中文**

這是 Todo List 應用程式的前端部分，使用 Ionic Angular 框架開發。

## 安裝與執行

### 先決條件

- Node.js (版本 18+)
- npm 或 yarn
- Ionic CLI

### 安裝 Ionic CLI

```bash
npm install -g @ionic/cli
```

### 安裝依賴並啟動

```bash
# 安裝依賴
npm install

# 啟動開發服務器
ionic serve
```

應用程式將在 `http://localhost:8100` 啟動。

## 專案結構

```
src/
├── app/
│   ├── models/          # 資料模型
│   ├── services/        # API 服務
│   ├── pages/
│   │   └── todo/        # Todo 頁面元件
│   └── app.module.ts    # 主模組
├── environments/        # 環境配置
├── theme/              # 主題樣式
└── assets/             # 靜態資源
```

## 主要功能

- **Todo 管理**: 新增、編輯、刪除 Todo 項目
- **狀態切換**: 標記項目為完成/未完成
- **下拉刷新**: 重新載入資料
- **滑動操作**: 滑動刪除項目
- **響應式設計**: 支援手機和桌面裝置

## 配置

### API 端點配置

修改 `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7001/api'  // 修改為您的 API URL
};
```

### 主題自定義

修改 `src/theme/variables.css` 來自定義顏色主題。

## 建置與部署

### 開發建置
```bash
ionic build
```

### 生產建置
```bash
ionic build --prod
```

### 行動裝置建置

安裝 Capacitor 並建置原生應用：

```bash
# 安裝 Capacitor
npm install @capacitor/core @capacitor/cli

# 初始化 Capacitor
npx cap init

# 新增平台
npx cap add ios
npx cap add android

# 建置並同步
ionic build
npx cap sync
```

## 開發工具

- **Chrome DevTools**: 網頁除錯
- **Ionic DevApp**: 在真實裝置上測試
- **VSCode**: 建議的程式碼編輯器

## 疑難排解

### 常見問題

1. **CORS 錯誤**: 確保後端 API 已正確配置 CORS
2. **API 連線失敗**: 檢查 `environment.ts` 中的 API URL
3. **依賴問題**: 執行 `npm ci` 重新安裝依賴

### 除錯技巧

```bash
# 清除快取
npm run clean

# 重新安裝依賴
rm -rf node_modules package-lock.json
npm install

# 檢查 Ionic 資訊
ionic info
```
