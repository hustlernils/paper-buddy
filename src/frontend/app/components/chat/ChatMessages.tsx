import { type ChatMessageResponse } from "../../pages/ProjectDetails"

interface ChatMessagesProps{
    messages: ChatMessageResponse[],
}

export const ChatMessages = ( { messages }: ChatMessagesProps) => 
{

  return (
    <div className="h-96 overflow-auto">
      {messages.map((message: ChatMessageResponse, index: number) => 
      {
        return(
          <h1 key={index}>{message.content}</h1>
        )
      })}
    </div>
  )
}