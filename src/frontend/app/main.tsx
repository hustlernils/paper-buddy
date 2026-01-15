import { Profiler, StrictMode, type ProfilerOnRenderCallback } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import './index.css'
import App from './App.tsx'
import { ThemeProvider} from "./components/ThemeProvider.tsx";
import { SidebarProvider } from "./components/ui/Sidebar.tsx"
import { AppSidebar } from "./components/AddSidebar.tsx"

const onRender: ProfilerOnRenderCallback = (id, phase, actualDuration) => {

  const color =
    actualDuration > 16 ? "#ef4444" : // red (slow)
    actualDuration > 8  ? "#f59e0b" : // amber
                         "#22c55e";  // green (fast)
  
    const log = actualDuration > 50 ? console.warn : console.log;

    log(
    `%c[ReactProfiler]%c[id: ${id}]%c[phase: ${phase}]%c took ${actualDuration.toFixed()}ms`,
    "color:#61dafb;font-weight:bold",          // Profiler
    "color:white;font-weight:bold",            // Component id
    "color:#a855f7;font-style:italic",         // Phase
    `color:${color};font-weight:bold`          // Duration
  );
}


createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <ThemeProvider defaultTheme="dark" storageKey="vite-ui-theme">
        <Profiler id="main-app" onRender={onRender}>
          <SidebarProvider>
            <AppSidebar />
            <App />
          </SidebarProvider>
        </Profiler>        
      </ThemeProvider>
    </BrowserRouter>
  </StrictMode>,
)
