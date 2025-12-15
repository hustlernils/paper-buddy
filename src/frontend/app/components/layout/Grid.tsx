import type React from "react";

// sm	40rem (640px)	@media (width >= 40rem)
// md	48rem (768px)	@media (width >= 48rem)
// lg	64rem (1024px)	@media (width >= 64rem)
// xl	80rem (1280px)	@media (width >= 80rem)
// 2xl	96rem (1536px)	@media (width >= 96rem)

export interface GridProps{
    children: React.ReactNode[] | React.ReactNode
}

const Grid = ( { children }: GridProps) => 
{
  return(
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-6 2xl:grid-cols-6  gap-2">
      {children}
    </div>
  ) 
} 

export default Grid;