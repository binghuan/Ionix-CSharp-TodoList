import type { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.example.todoapp',
  appName: 'TodoApp',
  webDir: 'dist',
  server: {
    // Force use of HTTP instead of HTTPS
    androidScheme: 'http',
    iosScheme: 'http',
    // Allow mixed content and navigation
    allowNavigation: [
      'http://10.0.2.2:8080/*',
      'http://192.168.1.91:8080/*',
      'http://localhost:8080/*'
    ],
    // Allow cleartext traffic
    cleartext: true
  },
  plugins: {
    CapacitorHttp: {
      enabled: true
    }
  },
  ios: {
    contentInset: 'automatic',
    preferences: {
      'NSAppTransportSecurity': {
        'NSAllowsArbitraryLoads': true,
        'NSAllowsArbitraryLoadsInWebContent': true,
        'NSAllowsLocalNetworking': true,
        'NSExceptionDomains': {
          '192.168.1.91': {
            'NSExceptionAllowsInsecureHTTPLoads': true,
            'NSIncludesSubdomains': true
          },
          'localhost': {
            'NSExceptionAllowsInsecureHTTPLoads': true,
            'NSIncludesSubdomains': true
          },
          '127.0.0.1': {
            'NSExceptionAllowsInsecureHTTPLoads': true,
            'NSIncludesSubdomains': true
          }
        }
      },
      'NSLocalNetworkUsageDescription': 'This app needs to access local network to connect to development server.'
    }
  },
  android: {
    allowMixedContent: true,
    captureInput: true,
    webContentsDebuggingEnabled: true,
    preferences: {
      'android.usesCleartextTraffic': 'true'
    }
  }
};

export default config;
