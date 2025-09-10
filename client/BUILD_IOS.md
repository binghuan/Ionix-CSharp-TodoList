# Build iOS App Guide

**[中文版本](BUILD_IOS_zh.md) | English**

This guide explains how to build your Ionic Angular Todo app as an iOS application using Capacitor.

## Prerequisites

- ✅ **macOS** (required for iOS development)
- ✅ **Xcode 14+** (from Mac App Store)
- ✅ **Node.js and npm** installed
- ✅ **Ionic CLI** installed (`npm install -g @ionic/cli`)
- ✅ **iOS Simulator** or physical iOS device
- ✅ **Apple Developer Account** (for device testing and App Store)

## Step-by-Step Instructions

### 1. Install Capacitor iOS

```bash
cd client
npm install @capacitor/ios
```

### 2. Build Web Application

```bash
ionic build --prod
```

### 3. Add iOS Platform

```bash
npx cap add ios
```

### 4. Sync Code to iOS

```bash
npx cap sync ios
```

### 5. Open in Xcode

```bash
npx cap open ios
```

This will open the `App.xcworkspace` file in Xcode.

## Building in Xcode

### For iOS Simulator

1. **Select Simulator**: Choose an iOS simulator (e.g., iPhone 15 Pro) from the device selector
2. **Build & Run**: Click the play button (▶️) or press `Cmd + R`
3. **Wait**: Xcode will compile and launch your app in the simulator

### For Physical Device

1. **Connect Device**: Connect your iPhone/iPad via USB
2. **Trust Device**: Follow prompts to trust your Mac on the device
3. **Select Device**: Choose your connected device from the device selector
4. **Configure Signing**:
   - Select your project in the navigator
   - Go to "Signing & Capabilities" tab
   - Choose your Apple Developer account
   - Xcode will automatically manage provisioning
5. **Build & Run**: Click play button or press `Cmd + R`

## App Store Distribution

### Create Archive

1. **Select Device**: Choose "Any iOS Device (arm64)" from device selector
2. **Archive**: Go to `Product` → `Archive`
3. **Wait**: Xcode will build an optimized version

### Distribute App

1. **Open Organizer**: After archiving, the Organizer window opens
2. **Distribute App**: Click "Distribute App"
3. **Choose Method**:
   - **App Store Connect**: For App Store submission
   - **Ad Hoc**: For testing on registered devices
   - **Enterprise**: For internal distribution
   - **Development**: For development team testing

## Development Workflow

When making changes to your app:

```bash
# 1. Make changes in src/
# 2. Build web app
ionic build --prod

# 3. Sync to iOS
npx cap sync ios

# 4. In Xcode: Build & Run (Cmd + R)
```

## App Configuration

### App Information

Edit `ios/App/App/Info.plist`:
- `CFBundleDisplayName`: App name shown on home screen
- `CFBundleIdentifier`: Bundle ID (e.g., com.example.todoapp)
- `CFBundleVersion`: Build number
- `CFBundleShortVersionString`: Version number

### App Icons and Launch Screen

1. **App Icons**: Replace icons in `ios/App/App/Assets.xcassets/AppIcon.appiconset/`
2. **Launch Screen**: Edit `ios/App/App/Base.lproj/LaunchScreen.storyboard`
3. **Use Asset Generator**: Consider `@capacitor/assets` for automatic generation

### Permissions

Add required permissions to `ios/App/App/Info.plist`:

```xml
<!-- Example: Camera permission -->
<key>NSCameraUsageDescription</key>
<string>This app uses camera to scan QR codes</string>

<!-- Example: Location permission -->
<key>NSLocationWhenInUseUsageDescription</key>
<string>This app uses location to find nearby items</string>
```

## Troubleshooting

### Common Issues

1. **Build Failed - Signing**:
   - Ensure you're signed into Xcode with your Apple ID
   - Check "Automatically manage signing" in project settings
   - Verify bundle identifier is unique

2. **Module Not Found**:
   - Clean build folder: `Product` → `Clean Build Folder`
   - Delete derived data: `Xcode` → `Preferences` → `Locations` → `Derived Data`

3. **CocoaPods Issues**:
   ```bash
   cd ios/App
   pod repo update
   pod install --clean-install
   ```

4. **Device Not Recognized**:
   - Check device is trusted
   - Ensure device is in developer mode
   - Update to latest Xcode

### Debug Tips

```bash
# View iOS logs
npx cap run ios --open

# Clean Capacitor cache
npx cap clean ios
npx cap sync ios

# Update Capacitor
npm install @capacitor/core@latest @capacitor/cli@latest @capacitor/ios@latest
```

## Performance Optimization

- **Enable bitcode** for App Store optimization
- **Optimize images** in Assets.xcassets
- **Use lazy loading** for routes and modules
- **Minimize bundle size** with `ionic build --prod`
- **Test on real devices** for accurate performance

## Xcode Project Structure

```
ios/App/
├── App/                          # Main app target
│   ├── App/                      # Source files
│   │   ├── public/              # Web assets
│   │   ├── capacitor.config.json
│   │   └── Info.plist           # App configuration
│   ├── Assets.xcassets/         # App icons and images
│   └── Base.lproj/              # Storyboards and xibs
├── App.xcodeproj/               # Xcode project
├── App.xcworkspace/             # Recommended to open
├── Podfile                      # CocoaPods dependencies
└── Pods/                        # Installed pods
```

## Useful Commands

```bash
# Check Capacitor setup
npx cap doctor ios

# Run on specific simulator
npx cap run ios --target="iPhone 15 Pro"

# Open iOS simulator without Xcode
xcrun simctl boot "iPhone 15 Pro"
open -a Simulator

# List available simulators
xcrun simctl list devices available
```

## Next Steps

- **Test on multiple iOS versions** and device sizes
- **Implement iOS-specific features** like Face ID, Apple Pay
- **Add push notifications** with `@capacitor/push-notifications`
- **Optimize for different screen sizes** (iPhone, iPad)
- **Submit to App Store** via App Store Connect

## Resources

- [Capacitor iOS Documentation](https://capacitorjs.com/docs/ios)
- [Ionic iOS Development](https://ionicframework.com/docs/developing/ios)
- [Apple Developer Documentation](https://developer.apple.com/documentation/)
- [App Store Review Guidelines](https://developer.apple.com/app-store/review/guidelines/)

---

**🎯 Result**: You now have a native iOS app of your Todo List application!
