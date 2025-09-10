# 📱 Mobile Build Quick Start

**Fast track to build your Todo app for Android and iOS**

## 🤖 Android APK

```bash
# From project root
cd client

# Install dependencies
npm install @capacitor/android

# Initialize (if not done)
npx cap init TodoApp com.example.todoapp --web-dir=dist

# Build and add Android
ionic build --prod
npx cap add android

# Build APK
cd android && ./gradlew assembleDebug
```

**Result**: `client/android/app/build/outputs/apk/debug/app-debug.apk`

## 🍎 iOS App

```bash
# From client directory
npm install @capacitor/ios

# Build web app
ionic build --prod

# Add iOS platform
npx cap add ios

# Sync and open Xcode
npx cap sync ios
npx cap open ios
```

**Result**: Xcode opens - click ▶️ to run on simulator or device

## 📲 Installation

### Android
```bash
# Install via ADB
adb install app-debug.apk

# Or transfer APK to phone and install
```

### iOS
- **Simulator**: Run directly from Xcode
- **Device**: Requires Apple Developer account
- **TestFlight**: For beta testing

## 🔄 Update Workflow

After making app changes:

```bash
# 1. Build web
ionic build --prod

# 2. Sync platforms
npx cap sync android
npx cap sync ios

# 3. Rebuild
# Android: cd android && ./gradlew assembleDebug
# iOS: Rebuild in Xcode
```

## 📚 Detailed Guides

- **Android**: [client/BUILD_ANDROID.md](client/BUILD_ANDROID.md) ([中文](client/BUILD_ANDROID_zh.md))
- **iOS**: [client/BUILD_IOS.md](client/BUILD_IOS.md) ([中文](client/BUILD_IOS_zh.md))

## 🛠️ Prerequisites

### Android
- Node.js and npm
- Android Studio (optional)
- Java JDK 11+

### iOS
- **macOS required**
- Xcode 14+
- Apple Developer account (for device testing)

---

**🎯 Result**: Native mobile apps for both Android and iOS!
