# Todo Client - Ionic Angular App

**[English](README.md) | 繁體中文**

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
├── main.ts                   # 🚀 應用程式進入點與啟動
├── polyfills.ts             # 🌐 瀏覽器相容性與 Zone.js 整合  
├── test.ts                  # 🧪 測試環境配置
├── index.html               # 🚪 主要 HTML 容器與行動裝置標籤
├── global.scss              # 🌈 全應用程式樣式與 Ionic 自定義
├── app/
│   ├── app.module.ts        # 🏗️ 根模組與依賴注入配置
│   ├── app-routing.module.ts # 🗺️ 路由與延遲載入配置
│   ├── app.component.ts     # 🎭 根元件與基本布局結構
│   ├── app.component.html   # 🏗️ 根模板與 <ion-router-outlet>
│   ├── app.component.scss   # 🎭 根元件樣式
│   ├── models/
│   │   └── todo.model.ts    # 📋 TypeScript 介面 (Todo, CreateTodoDto, UpdateTodoDto)
│   ├── services/
│   │   └── todo.service.ts  # 🌐 API 通訊與平台感知 URL 配置
│   └── pages/
│       └── todo/
│           ├── todo.page.ts        # 🎮 Todo 頁面業務邏輯與狀態管理
│           ├── todo.page.html      # 📱 Todo UI 與清單、表單、行動元件
│           ├── todo.page.scss      # 📝 頁面專用樣式與動畫
│           ├── todo.module.ts      # 📦 功能模組與 Ionic UI 元件
│           └── todo-routing.module.ts # 🔗 頁面級路由配置
├── environments/
│   ├── environment.ts       # 🛠️ 開發環境 API URL (http://localhost:8080/api)
│   └── environment.prod.ts  # 🚀 生產環境配置
├── theme/
│   └── variables.css        # 🎨 色彩主題、深色模式與平台專用變數
└── assets/                  # 📁 靜態資源 (圖片、圖示等)
```

**📊 架構概覽**: 此結構遵循 **Angular 最佳實踐** 與 **Ionic 框架慣例**，清楚分離關注點：**🔥 核心** (main.ts, app.module.ts)、**🎨 UI** (模板、樣式)、**⚙️ 配置** (環境、路由)、**🏗️ 結構** (模型、服務、頁面)。

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
  apiUrl: 'http://localhost:8080/api'  // 修改為您的 API URL
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
