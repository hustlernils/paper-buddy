export interface ChatMessageProps {
    content: string,
    role: string
}

export const ChatMessage = ({ content, role }: ChatMessageProps ) => {
    const className = role === "USER" 
        ? "self-end" 
        : "self-start"

    return(
        <div className={`m-1 inline-block max-w-[70%] rounded-xl p-2 bg-secondary text-secondary-foreground ${className}`}>
            <h1>{content}</h1>
        </div>
    )
}