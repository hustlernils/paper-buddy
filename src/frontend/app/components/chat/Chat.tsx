import { type ChatMessageResponse } from "../../types/api"
import { ChatInput } from "./ChatInput" 
import { ChatMessages } from "./ChatMessages"

interface ChatProps {
    id: string,
    messages: ChatMessageResponse[],
    isSendingMessage: boolean,
    onSubmit: (content: string) => void
}

export const Chat = ( { messages, isSendingMessage, onSubmit }: ChatProps) => 
{

  return (
    <div className="relative h-[calc(100vh-8rem)]">
      <ChatMessages messages={messages} isSendingMessage={isSendingMessage}/>
      <ChatInput onSubmit={onSubmit} disabled={isSendingMessage} />
    </div>
  )
}