import type React from "react"
import { Separator } from "../ui/Separator"
import Header from "../Header"

export interface ToolbarProps{
    title?: string
    children?: React.ReactNode[] | React.ReactNode
} 

const Toolbar = ({ title, children } : ToolbarProps) => 
{

  return (
    <div className="mb-4">
      <div className='flex justify-between pb-4 items-center'>
        {title && <Header label={title}/>}    
        {children && <div className="flex gap-2">{children}</div>}
      </div>    
      <Separator/>
    </div>
  )    
}

export default Toolbar;