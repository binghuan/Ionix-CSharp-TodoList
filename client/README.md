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
├── app/
│   ├── models/          # Data models
│   ├── services/        # API services
│   ├── pages/
│   │   └── todo/        # Todo page components
│   └── app.module.ts    # Main module
├── environments/        # Environment configuration
├── theme/              # Theme styles
└── assets/             # Static resources
```

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
npm install @capacitor/core @capacitor/cli

# Initialize Capacitor
npx cap init

# Add platforms
npx cap add ios
npx cap add android

# Build and sync
ionic build
npx cap sync
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
