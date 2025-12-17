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
    <>
      <ChatMessages messages={messages}/>
      <ChatInput onSubmit={onSubmit} />
    </>
  )
}