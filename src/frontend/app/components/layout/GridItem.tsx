import type React from "react"

export interface GridItemProps{
    children: React.ReactNode
    className: string
}

export const GridItem = ({ children, className } :GridItemProps) => 
{
  return (
    <div className={className}>
      {children}
    </div>
  )
}