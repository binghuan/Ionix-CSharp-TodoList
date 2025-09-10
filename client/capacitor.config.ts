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
  }
};

export default config;
