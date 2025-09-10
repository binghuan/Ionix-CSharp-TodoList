# Build Android App Guide

**[中文版本](BUILD_ANDROID_zh.md) | English**

This guide explains how to build your Ionic Angular Todo app as an Android application using Capacitor.

## Prerequisites

- ✅ Node.js and npm installed
- ✅ Ionic CLI installed (`npm install -g @ionic/cli`)
- ✅ Android Studio (recommended) or Android SDK
- ✅ Java Development Kit (JDK 11 or higher)

## Step-by-Step Instructions

### 1. Install Capacitor

```bash
cd client
npm install @capacitor/core @capacitor/cli @capacitor/android
```

### 2. Initialize Capacitor

```bash
npx cap init TodoApp com.example.todoapp --web-dir=dist
```

### 3. Build Web Application

```bash
ionic build --prod
```

### 4. Add Android Platform

```bash
npx cap add android
```

### 5. Sync Code to Android

```bash
npx cap sync android
```

### 6. Build APK

#### Method A: Using Android Studio (Recommended)

```bash
npx cap open android
```

Then in Android Studio:
1. Wait for Gradle sync to complete
2. Go to `Build` → `Build Bundle(s) / APK(s)` → `Build APK(s)`

#### Method B: Using Command Line

```bash
cd android
./gradlew assembleDebug
```

## Generated APK Location

- **Debug APK**: `android/app/build/outputs/apk/debug/app-debug.apk`
- **Release APK**: `android/app/build/outputs/apk/release/app-release.apk`

## Installation Methods

### Install via ADB
```bash
adb install path/to/app-debug.apk
```

### Install on Physical Device
1. Transfer APK file to your Android phone
2. Open file manager and locate the APK
3. Tap to install (may need to enable "Unknown sources")

### Install on Emulator
```bash
# With emulator running
adb install app-debug.apk
```

## Development Workflow

When making changes to your app:

```bash
# 1. Make your changes in src/
# 2. Build web app
ionic build --prod

# 3. Sync to Android
npx cap sync android

# 4. Rebuild APK
cd android && ./gradlew assembleDebug
```

## Production Build

For release builds (requires signing):

```bash
cd android
./gradlew assembleRelease
```

Or use Android Studio:
- `Build` → `Generate Signed Bundle / APK`
- Follow the signing wizard

## App Configuration

### Change App Name
Edit `client/capacitor.config.ts`:
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

### Change Package Name
1. Update `appId` in `capacitor.config.ts`
2. Run `npx cap sync android`

### App Icons and Splash Screen
1. Replace icons in `android/app/src/main/res/`
2. Use tools like `@capacitor/assets` for automatic generation

## Troubleshooting

### Common Issues

1. **Gradle Build Failed**: Check Java version and Android SDK installation
2. **App Crashes**: Check console logs in Chrome DevTools or Android Studio
3. **Network Requests Fail**: Update API URLs and check CORS settings

### Debug Tips

```bash
# View Android logs
adb logcat

# Check connected devices
adb devices

# Clear app data
adb shell pm clear com.example.todoapp
```

### Performance Optimization

- Enable ProGuard for release builds
- Optimize images and assets
- Use lazy loading for routes
- Minimize bundle size with `ionic build --prod`

## Android Studio Setup

1. Download Android Studio from https://developer.android.com/studio
2. Install Android SDK (API level 33+ recommended)
3. Configure environment variables:
   ```bash
   export ANDROID_HOME=$HOME/Android/Sdk
   export PATH=$PATH:$ANDROID_HOME/tools
   export PATH=$PATH:$ANDROID_HOME/platform-tools
   ```

## Useful Commands

```bash
# Check Capacitor info
npx cap doctor

# List available platforms
npx cap ls

# Clean and rebuild
npx cap clean android
npx cap sync android

# Update Capacitor
npm install @capacitor/core@latest @capacitor/cli@latest @capacitor/android@latest
```

## Next Steps

- Test on different Android versions and screen sizes
- Implement push notifications with `@capacitor/push-notifications`
- Add native features like camera, geolocation
- Publish to Google Play Store

## Resources

- [Capacitor Android Documentation](https://capacitorjs.com/docs/android)
- [Ionic Android Development](https://ionicframework.com/docs/developing/android)
- [Android Developer Guide](https://developer.android.com/guide)
