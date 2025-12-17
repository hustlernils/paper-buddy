import { type ChatMessageResponse } from "../../pages/ProjectDetails"
import { ChatMessage } from "./ChatMessage"

interface ChatMessagesProps{
    messages: ChatMessageResponse[],
}

export const ChatMessages = ( { messages }: ChatMessagesProps) => 
{
  return (
    <div className="h-full overflow-auto pb-32 flex flex-col">
      {messages.map((message: ChatMessageResponse, index: number) => 
      {
        return(
          <ChatMessage key={index} content={message.content} role={message.role}/>
        )
      })}
    </div>
  )
}