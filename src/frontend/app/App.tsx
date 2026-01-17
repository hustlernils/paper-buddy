import { Routes, Route } from 'react-router-dom'
import { SidebarInset } from './components/ui/Sidebar'
import Papers from './pages/Papers'
import Projects from './pages/Projects'
import Layout from './components/layout/Layout'
import { ProjectDetails } from './pages/ProjectDetails/ProjectDetails'
import { ChatPage } from './pages/ChatPage'

function App() 
{
  return (
    <SidebarInset>
      <Layout>
        <Routes>
          <Route path="/" element={<Papers />} />
          <Route path="/papers" element={<Papers />} />
          <Route path="/projects" element={<Projects />} />
          <Route path="/projects/:projectId" element={<ProjectDetails/>}/>
          <Route path="/chats/:chatId" element={<ChatPage/>}/>
          <Route path="*" element={<div>Page not found</div>} />
        </Routes>
      </Layout>
    </SidebarInset>
  )
}

export default App
