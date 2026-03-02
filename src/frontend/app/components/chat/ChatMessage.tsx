import { type ReactNode } from "react"

export interface ChatMessageProps {
    role: "USER" | "SYSTEM"
    children: ReactNode
}

export const ChatMessage = ({ role, children }: ChatMessageProps ) => 
{
    const className = role === "USER" 
        ? "self-end" 
        : "self-start"

    return(
        <div className={`m-1 inline-block max-w-[70%] rounded-xl p-2 bg-secondary text-secondary-foreground ${className}`}>
            {children}
        </div>
    )
}

export const MessageContent = ({ children }: { children: ReactNode }) => 
{
    return <div>{children}</div>
}