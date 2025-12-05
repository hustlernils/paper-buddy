import type React from "react";

export interface LayoutProps{
    children: React.ReactNode[] | React.ReactNode
}

const Layout = ({ children } : LayoutProps) =>{
    return (
        <div className='flex justify-center mt-5'> 
            <div className='md:container md:mx-auto px-20'>
                {children}
            </div>
        </div>
    )
}

export default Layout;