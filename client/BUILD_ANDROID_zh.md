# Android 應用建置指南

**繁體中文 | [English](BUILD_ANDROID.md)**

本指南說明如何使用 Capacitor 將 Ionic Angular Todo 應用打包成 Android 應用程式。

## 環境需求

- ✅ 已安裝 Node.js 和 npm
- ✅ 已安裝 Ionic CLI (`npm install -g @ionic/cli`)
- ✅ Android Studio (推薦) 或 Android SDK
- ✅ Java Development Kit (JDK 11 或更高版本)

## 詳細步驟

### 1. 安裝 Capacitor

```bash
cd client
npm install @capacitor/core @capacitor/cli @capacitor/android
```

### 2. 初始化 Capacitor

```bash
npx cap init TodoApp com.example.todoapp --web-dir=dist
```

### 3. 建置 Web 應用程式

```bash
ionic build --prod
```

### 4. 添加 Android 平台

```bash
npx cap add android
```

### 5. 同步代碼到 Android

```bash
npx cap sync android
```

### 6. 建置 APK

#### 方法 A: 使用 Android Studio (推薦)

```bash
npx cap open android
```

在 Android Studio 中：
1. 等待 Gradle 同步完成
2. 選擇 `Build` → `Build Bundle(s) / APK(s)` → `Build APK(s)`

#### 方法 B: 使用命令列

```bash
cd android
./gradlew assembleDebug
```

## APK 生成位置

- **調試版 APK**: `android/app/build/outputs/apk/debug/app-debug.apk`
- **發布版 APK**: `android/app/build/outputs/apk/release/app-release.apk`

## 安裝方法

### 使用 ADB 安裝
```bash
adb install path/to/app-debug.apk
```

### 安裝到實體設備
1. 將 APK 文件傳送到 Android 手機
2. 開啟文件管理器，找到 APK 文件
3. 點擊安裝（可能需要啟用「未知來源」）

### 安裝到模擬器
```bash
# 在模擬器運行時
adb install app-debug.apk
```

## 開發流程

修改應用程式後的更新流程：

```bash
# 1. 修改 src/ 目錄中的代碼
# 2. 建置 web 應用
ionic build --prod

# 3. 同步到 Android
npx cap sync android

# 4. 重新建置 APK
cd android && ./gradlew assembleDebug
```

## 生產版本建置

建置發布版本（需要簽名）：

```bash
cd android
./gradlew assembleRelease
```

或使用 Android Studio：
- `Build` → `Generate Signed Bundle / APK`
- 按照簽名精靈操作

## 應用程式配置

### 修改應用名稱
編輯 `client/capacitor.config.ts`：
```typescript
const config: CapacitorConfig = {
  appId: 'com.example.todoapp',
  appName: 'Todo App',
  webDir: 'dist',
  server: {
    androidScheme: 'https'
  }
};
```

### 修改包名稱
1. 更新 `capacitor.config.ts` 中的 `appId`
2. 執行 `npx cap sync android`

### 應用圖示和啟動畫面
1. 替換 `android/app/src/main/res/` 中的圖示
2. 使用 `@capacitor/assets` 工具自動生成

## 疑難排解

### 常見問題

1. **Gradle 建置失敗**: 檢查 Java 版本和 Android SDK 安裝
2. **應用程式崩潰**: 檢查 Chrome DevTools 或 Android Studio 中的控制台日誌
3. **網路請求失敗**: 更新 API URL 並檢查 CORS 設定

### 除錯技巧

```bash
# 查看 Android 日誌
adb logcat

# 檢查已連接的設備
adb devices

# 清除應用數據
adb shell pm clear com.example.todoapp
```

### 性能優化

- 為發布版本啟用 ProGuard
- 優化圖片和資源
- 使用路由延遲載入
- 使用 `ionic build --prod` 最小化打包大小

## Android Studio 設定

1. 從 https://developer.android.com/studio 下載 Android Studio
2. 安裝 Android SDK (推薦 API level 33+)
3. 配置環境變量：
   ```bash
   export ANDROID_HOME=$HOME/Android/Sdk
   export PATH=$PATH:$ANDROID_HOME/tools
   export PATH=$PATH:$ANDROID_HOME/platform-tools
   ```

## 實用指令

```bash
# 檢查 Capacitor 資訊
npx cap doctor

# 列出可用平台
npx cap ls

# 清理並重建
npx cap clean android
npx cap sync android

# 更新 Capacitor
npm install @capacitor/core@latest @capacitor/cli@latest @capacitor/android@latest
```

## 下一步

- 在不同 Android 版本和螢幕尺寸上測試
- 使用 `@capacitor/push-notifications` 實作推播通知
- 添加相機、地理位置等原生功能
- 發布到 Google Play 商店

## 參考資源

- [Capacitor Android 文檔](https://capacitorjs.com/docs/android)
- [Ionic Android 開發](https://ionicframework.com/docs/developing/android)
- [Android 開發者指南](https://developer.android.com/guide)
