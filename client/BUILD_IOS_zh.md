# iOS 應用建置指南

**繁體中文 | [English](BUILD_IOS.md)**

本指南說明如何使用 Capacitor 將 Ionic Angular Todo 應用打包成 iOS 應用程式。

## 環境需求

- ✅ **macOS** (iOS 開發必需)
- ✅ **Xcode 14+** (從 Mac App Store 下載)
- ✅ **Node.js 和 npm** 已安裝
- ✅ **Ionic CLI** 已安裝 (`npm install -g @ionic/cli`)
- ✅ **iOS 模擬器** 或實體 iOS 設備
- ✅ **Apple 開發者帳號** (用於設備測試和 App Store)

## 詳細步驟

### 1. 安裝 Capacitor iOS

```bash
cd client
npm install @capacitor/ios
```

### 2. 建置 Web 應用程式

```bash
ionic build --prod
```

### 3. 添加 iOS 平台

```bash
npx cap add ios
```

### 4. 同步代碼到 iOS

```bash
npx cap sync ios
```

### 5. 在 Xcode 中打開

```bash
npx cap open ios
```

這會在 Xcode 中打開 `App.xcworkspace` 文件。

## 在 Xcode 中建置

### 使用 iOS 模擬器

1. **選擇模擬器**: 從設備選擇器中選擇 iOS 模擬器（如 iPhone 15 Pro）
2. **建置並運行**: 點擊播放按鈕 (▶️) 或按 `Cmd + R`
3. **等待**: Xcode 會編譯並在模擬器中啟動您的應用

### 使用實體設備

1. **連接設備**: 通過 USB 連接 iPhone/iPad
2. **信任設備**: 按照提示在設備上信任您的 Mac
3. **選擇設備**: 從設備選擇器中選擇已連接的設備
4. **配置簽名**:
   - 在導航器中選擇項目
   - 轉到「Signing & Capabilities」標籤
   - 選擇您的 Apple 開發者帳號
   - Xcode 會自動管理配置
5. **建置並運行**: 點擊播放按鈕或按 `Cmd + R`

## App Store 發佈

### 創建存檔

1. **選擇設備**: 從設備選擇器選擇「Any iOS Device (arm64)」
2. **存檔**: 選擇 `Product` → `Archive`
3. **等待**: Xcode 會建置優化版本

### 發佈應用

1. **打開管理器**: 存檔後，管理器視窗會打開
2. **發佈應用**: 點擊「Distribute App」
3. **選擇方式**:
   - **App Store Connect**: 用於 App Store 提交
   - **Ad Hoc**: 用於在註冊設備上測試
   - **Enterprise**: 用於內部發佈
   - **Development**: 用於開發團隊測試

## 開發流程

修改應用程式後的更新流程：

```bash
# 1. 修改 src/ 目錄中的代碼
# 2. 建置 web 應用
ionic build --prod

# 3. 同步到 iOS
npx cap sync ios

# 4. 在 Xcode 中: 建置並運行 (Cmd + R)
```

## 應用程式配置

### 應用資訊

編輯 `ios/App/App/Info.plist`：
- `CFBundleDisplayName`: 主畫面顯示的應用名稱
- `CFBundleIdentifier`: Bundle ID (如 com.example.todoapp)
- `CFBundleVersion`: 建置編號
- `CFBundleShortVersionString`: 版本號

### 應用圖示和啟動畫面

1. **應用圖示**: 替換 `ios/App/App/Assets.xcassets/AppIcon.appiconset/` 中的圖示
2. **啟動畫面**: 編輯 `ios/App/App/Base.lproj/LaunchScreen.storyboard`
3. **使用資源生成器**: 考慮使用 `@capacitor/assets` 自動生成

### 權限

在 `ios/App/App/Info.plist` 中添加所需權限：

```xml
<!-- 範例: 相機權限 -->
<key>NSCameraUsageDescription</key>
<string>此應用使用相機掃描 QR 碼</string>

<!-- 範例: 位置權限 -->
<key>NSLocationWhenInUseUsageDescription</key>
<string>此應用使用位置尋找附近項目</string>
```

## 疑難排解

### 常見問題

1. **建置失敗 - 簽名**:
   - 確保在 Xcode 中登入了 Apple ID
   - 在項目設定中勾選「Automatically manage signing」
   - 驗證 bundle identifier 是唯一的

2. **找不到模組**:
   - 清除建置資料夾: `Product` → `Clean Build Folder`
   - 刪除衍生數據: `Xcode` → `Preferences` → `Locations` → `Derived Data`

3. **CocoaPods 問題**:
   ```bash
   cd ios/App
   pod repo update
   pod install --clean-install
   ```

4. **設備無法識別**:
   - 檢查設備是否受信任
   - 確保設備處於開發者模式
   - 更新到最新的 Xcode

### 除錯技巧

```bash
# 查看 iOS 日誌
npx cap run ios --open

# 清除 Capacitor 快取
npx cap clean ios
npx cap sync ios

# 更新 Capacitor
npm install @capacitor/core@latest @capacitor/cli@latest @capacitor/ios@latest
```

## 性能優化

- **啟用 bitcode** 進行 App Store 優化
- **優化圖片** 在 Assets.xcassets 中
- **使用延遲載入** 路由和模組
- **最小化打包大小** 使用 `ionic build --prod`
- **在真實設備上測試** 以獲得準確的性能表現

## Xcode 專案結構

```
ios/App/
├── App/                          # 主要應用目標
│   ├── App/                      # 源文件
│   │   ├── public/              # Web 資源
│   │   ├── capacitor.config.json
│   │   └── Info.plist           # 應用配置
│   ├── Assets.xcassets/         # 應用圖示和圖片
│   └── Base.lproj/              # Storyboards 和 xibs
├── App.xcodeproj/               # Xcode 專案
├── App.xcworkspace/             # 建議打開此文件
├── Podfile                      # CocoaPods 依賴
└── Pods/                        # 已安裝的 pods
```

## 實用指令

```bash
# 檢查 Capacitor 設定
npx cap doctor ios

# 在特定模擬器上運行
npx cap run ios --target="iPhone 15 Pro"

# 不使用 Xcode 打開 iOS 模擬器
xcrun simctl boot "iPhone 15 Pro"
open -a Simulator

# 列出可用模擬器
xcrun simctl list devices available
```

## 下一步

- **在多個 iOS 版本和設備尺寸上測試**
- **實作 iOS 特定功能** 如 Face ID、Apple Pay
- **使用 `@capacitor/push-notifications` 添加推播通知**
- **針對不同螢幕尺寸優化** (iPhone、iPad)
- **透過 App Store Connect 提交到 App Store**

## 參考資源

- [Capacitor iOS 文檔](https://capacitorjs.com/docs/ios)
- [Ionic iOS 開發](https://ionicframework.com/docs/developing/ios)
- [Apple 開發者文檔](https://developer.apple.com/documentation/)
- [App Store 審查指南](https://developer.apple.com/app-store/review/guidelines/)

---

**🎯 結果**: 您現在擁有 Todo List 應用程式的原生 iOS 應用！
