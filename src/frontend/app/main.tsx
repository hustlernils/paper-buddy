import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { ThemeProvider} from "./components/theme-provider.tsx";
import { SidebarProvider } from "./components/ui/sidebar"
import { AppSidebar } from "./components/app-sidebar"

createRoot(document.getElementById('root')!).render(
  <StrictMode>
      <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
          <SidebarProvider>
              <AppSidebar />
              <App />
          </SidebarProvider>
      </ThemeProvider>
  </StrictMode>,
)
