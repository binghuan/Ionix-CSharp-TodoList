#!/bin/bash

# Mobile Platform Configuration Setup Script
# This script applies necessary network configurations for iOS and Android

echo "🔧 Setting up mobile platform configurations..."

# Check if platforms exist
if [ ! -d "android" ] || [ ! -d "ios" ]; then
    echo "❌ Android or iOS platforms not found. Please run 'npx cap add android' and 'npx cap add ios' first."
    exit 1
fi

# Configure Android
echo "📱 Configuring Android..."

# 1. Update AndroidManifest.xml for cleartext traffic
ANDROID_MANIFEST="android/app/src/main/AndroidManifest.xml"
if [ -f "$ANDROID_MANIFEST" ]; then
    # Add usesCleartextTraffic and networkSecurityConfig
    sed -i '' 's/<application/<application android:usesCleartextTraffic="true" android:networkSecurityConfig="@xml\/network_security_config"/g' "$ANDROID_MANIFEST"
    
    # Add INTERNET permission if not exists
    if ! grep -q "android.permission.INTERNET" "$ANDROID_MANIFEST"; then
        sed -i '' 's|</manifest>|    <uses-permission android:name="android.permission.INTERNET" />\n</manifest>|g' "$ANDROID_MANIFEST"
    fi
    echo "✅ Android manifest updated"
fi

# 2. Create network security config
ANDROID_XML_DIR="android/app/src/main/res/xml"
NETWORK_CONFIG="$ANDROID_XML_DIR/network_security_config.xml"
mkdir -p "$ANDROID_XML_DIR"

cat > "$NETWORK_CONFIG" << 'EOF'
<?xml version="1.0" encoding="utf-8"?>
<network-security-config>
    <!-- Development environment: Allow all cleartext traffic -->
    <base-config cleartextTrafficPermitted="true">
        <trust-anchors>
            <certificates src="system"/>
            <certificates src="user"/>
        </trust-anchors>
    </base-config>

    <!-- Explicitly allow development server domains -->
    <domain-config cleartextTrafficPermitted="true">
        <domain includeSubdomains="true">10.0.2.2</domain>
        <domain includeSubdomains="true">192.168.1.91</domain>
        <domain includeSubdomains="true">localhost</domain>
        <domain includeSubdomains="true">127.0.0.1</domain>
        <domain includeSubdomains="true">192.168.0.0</domain>
        <domain includeSubdomains="true">192.168.1.0</domain>
        <domain includeSubdomains="true">10.0.0.0</domain>
    </domain-config>
</network-security-config>
EOF
echo "✅ Android network security config created"

# Configure iOS
echo "🍎 Configuring iOS..."

# Update Info.plist with network permissions
IOS_PLIST="ios/App/App/Info.plist"
if [ -f "$IOS_PLIST" ]; then
    # Use plutil to safely add network permissions
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity dict" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSAllowsArbitraryLoads bool true" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSAllowsArbitraryLoadsInWebContent bool true" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSAllowsLocalNetworking bool true" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSAllowsArbitraryLoadsForMedia bool true" "$IOS_PLIST" 2>/dev/null || true
    
    # Add exception domains
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains dict" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:localhost dict" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:localhost:NSExceptionAllowsInsecureHTTPLoads bool true" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:localhost:NSIncludesSubdomains bool true" "$IOS_PLIST" 2>/dev/null || true
    
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:127.0.0.1 dict" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:127.0.0.1:NSExceptionAllowsInsecureHTTPLoads bool true" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:127.0.0.1:NSIncludesSubdomains bool true" "$IOS_PLIST" 2>/dev/null || true
    
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:192.168.1.91 dict" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:192.168.1.91:NSExceptionAllowsInsecureHTTPLoads bool true" "$IOS_PLIST" 2>/dev/null || true
    /usr/libexec/PlistBuddy -c "Add :NSAppTransportSecurity:NSExceptionDomains:192.168.1.91:NSIncludesSubdomains bool true" "$IOS_PLIST" 2>/dev/null || true
    
    # Add local network description
    /usr/libexec/PlistBuddy -c "Add :NSLocalNetworkUsageDescription string 'This app needs to access local network to connect to development server.'" "$IOS_PLIST" 2>/dev/null || true
    
    echo "✅ iOS Info.plist updated using PlistBuddy"
fi

echo ""
echo "✨ Mobile configuration setup complete!"
echo ""
echo "📝 Next steps:"
echo "1. Run: npx cap sync"
echo "2. Build and test your apps"
echo ""
echo "🔍 Files modified:"
echo "   - $ANDROID_MANIFEST"
echo "   - $NETWORK_CONFIG" 
echo "   - $IOS_PLIST"
