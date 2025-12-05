import type React from "react"
import { Separator } from "../ui/separator"

export interface ToolbarProps{
    children: React.ReactNode[] | React.ReactNode
} 

const Toolbar = ({ children } : ToolbarProps) => {

    return (
        <div className="mb-4">
            <div className='flex justify-center pb-4'>
                {children}
            </div>    
            <Separator/>
        </div>
    )    
}

export default Toolbar;