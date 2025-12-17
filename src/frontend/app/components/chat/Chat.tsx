import { type ChatMessageResponse } from "../../pages/ProjectDetails"
import { ChatInput } from "./ChatInput"
import { ChatMessages } from "./ChatMessages"

interface ChatProps{
    id: string,
    messages: ChatMessageResponse[],
    onSubmit: (content: string) => void
}

export const Chat = ( { messages, onSubmit }: ChatProps) => 
{

  return (
    <div className="relative h-[calc(100vh-8rem)]">
      <ChatMessages messages={messages}/>
      <ChatInput onSubmit={onSubmit} />
    </div>
  )
}