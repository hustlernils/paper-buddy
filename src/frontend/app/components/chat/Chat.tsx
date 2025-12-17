import { type ChatMessageResponse } from "../../pages/ProjectDetails"
import { ChatInput } from "./ChatInput"

interface ChatProps{
    id: string,
    messages: ChatMessageResponse[],
    onSubmit: (content: string) => void
}

export const Chat = ( { id, messages, onSubmit }: ChatProps) => {

    return (
        <>
        <div>{`This is the chat for id ${id}`}</div>
        {messages.map((message: ChatMessageResponse, index: number) => {
            return(
                <h1 key={index}>{message.content}</h1>
            )
        })}
        <ChatInput onSubmit={onSubmit} />
        </>
    )
}