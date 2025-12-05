import { Routes, Route } from 'react-router-dom'
import { SidebarInset } from './components/ui/sidebar'
import Papers from './pages/Papers'
import Projects from './pages/Projects'

function App() {
  return (
      <SidebarInset>
        {/* main layout, TODO: create component instead  */}
        <div className='flex justify-center mt-5'> 
            {/* <div className='mx-auto md:max-w-7xl sm:max-w-sm xl:max-w-10xl'> */}
            <div className='md:container md:mx-auto px-20'>
                <Routes>
                    <Route path="/" element={<Papers />} />
                    <Route path="/papers" element={<Papers />} />
                    <Route path="/projects" element={<Projects />} />
                    <Route path="*" element={<div>Page not found</div>} />
                </Routes>
            </div>
        </div>
      </SidebarInset>
  )
}

export default App
