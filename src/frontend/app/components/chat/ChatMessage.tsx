export interface ChatMessageProps {
    content: string,
    role: string
}

export const ChatMessage = ({ content, role }: ChatMessageProps ) => {
    const className = role === "USER" ? "self-end bg-neutral-700" : "self-start bg-blue-600" 

    return(
        <div className={`m-1 inline-block max-w-[70%] bg-slate-700 rounded-xl ${className}`}>
            <h1 className="p-2">{content}</h1>
        </div>
    )
}