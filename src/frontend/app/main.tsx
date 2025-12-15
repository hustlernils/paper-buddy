import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import './index.css'
import App from './App.tsx'
import { ThemeProvider} from "./components/ThemeProvider.tsx";
import { SidebarProvider } from "./components/ui/Sidebar.tsx"
import { AppSidebar } from "./components/AddSidebar.tsx"

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
        <SidebarProvider>
          <AppSidebar />
          <App />
        </SidebarProvider>
      </ThemeProvider>
    </BrowserRouter>
  </StrictMode>,
)
