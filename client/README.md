# Todo Client - Ionic Angular App

**English | [繁體中文](README_zh.md)**

This is the frontend part of the Todo List application, built with the Ionic Angular framework.

## Installation and Setup

### Prerequisites

- Node.js (version 18+)
- npm or yarn
- Ionic CLI

### Install Ionic CLI

```bash
npm install -g @ionic/cli
```

### Install Dependencies and Start

```bash
# Install dependencies
npm install

# Start development server
ionic serve
```

The application will start at `http://localhost:8100`.

## Project Structure

```
src/
├── main.ts                   # 🚀 Application entry point and bootstrap
├── polyfills.ts             # 🌐 Browser compatibility and Zone.js integration  
├── test.ts                  # 🧪 Test environment configuration
├── index.html               # 🚪 Main HTML container with mobile meta tags
├── global.scss              # 🌈 Application-wide styles and Ionic customization
├── app/
│   ├── app.module.ts        # 🏗️ Root module with dependency injection and service configuration
│   ├── app-routing.module.ts # 🗺️ Navigation and lazy loading configuration
│   ├── app.component.ts     # 🎭 Root component with basic layout structure
│   ├── app.component.html   # 🏗️ Root template with <ion-router-outlet>
│   ├── app.component.scss   # 🎭 Root component styling
│   ├── models/
│   │   └── todo.model.ts    # 📋 TypeScript interfaces (Todo, CreateTodoDto, UpdateTodoDto)
│   ├── services/
│   │   └── todo.service.ts  # 🌐 API communication with platform-aware URL configuration
│   └── pages/
│       └── todo/
│           ├── todo.page.ts        # 🎮 Todo page business logic and state management
│           ├── todo.page.html      # 📱 Todo UI with lists, forms, and mobile components
│           ├── todo.page.scss      # 📝 Page-specific styling and animations
│           ├── todo.module.ts      # 📦 Feature module with Ionic UI components
│           └── todo-routing.module.ts # 🔗 Page-level routing configuration
├── environments/
│   ├── environment.ts       # 🛠️ Development API URL (http://localhost:8080/api)
│   └── environment.prod.ts  # 🚀 Production configuration
├── theme/
│   └── variables.css        # 🎨 Color themes, dark mode, and platform-specific variables
└── assets/                  # 📁 Static resources (images, icons, etc.)
```

**📊 Architecture**: This structure follows **Angular best practices** and **Ionic framework conventions** with clear separation of concerns: **🔥 Core** (main.ts, app.module.ts), **🎨 UI** (templates, styles), **⚙️ Config** (environments, routing), and **🏗️ Structure** (models, services, pages).

## Main Features

- **Todo Management**: Add, edit, delete Todo items
- **State Toggle**: Mark items as completed/incomplete
- **Pull-to-Refresh**: Reload data
- **Swipe Actions**: Swipe to delete items
- **Responsive Design**: Support for mobile and desktop devices

## Configuration

### API Endpoint Configuration

Modify `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7001/api'  // Change to your API URL
};
```

### Theme Customization

Modify `src/theme/variables.css` to customize color themes.

## Build and Deployment

### Development Build
```bash
ionic build
```

### Production Build
```bash
ionic build --prod
```

### Mobile App Build

Install Capacitor and build native apps:

```bash
# Install Capacitor
npm install @capacitor/core @capacitor/cli @capacitor/android

# Initialize Capacitor
npx cap init TodoApp com.example.todoapp --web-dir=dist

# Build web app first
ionic build --prod

# Add Android platform
npx cap add android

# Sync to Android
npx cap sync android

# Build APK
cd android && ./gradlew assembleDebug
```

📱 **Detailed Mobile Build Guides**: 
- **Android**: [BUILD_ANDROID.md](BUILD_ANDROID.md)
- **iOS**: [BUILD_IOS.md](BUILD_IOS.md)

### iOS App Build

```bash
# Install iOS platform
npm install @capacitor/ios

# Build web app
ionic build --prod

# Add iOS platform
npx cap add ios

# Sync to iOS
npx cap sync ios

# Open in Xcode
npx cap open ios
```

## Development Tools

- **Chrome DevTools**: Web debugging
- **Ionic DevApp**: Test on real devices
- **VSCode**: Recommended code editor

## Troubleshooting

### Common Issues

1. **CORS Error**: Ensure backend API has CORS properly configured
2. **API Connection Failed**: Check API URL in `environment.ts`
3. **Dependency Issues**: Run `npm ci` to reinstall dependencies

### Debugging Tips

```bash
# Clear cache
npm run clean

# Reinstall dependencies
rm -rf node_modules package-lock.json
npm install

# Check Ionic information
ionic info
```
