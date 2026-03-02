import { type ChatMessageResponse } from "../../types/api"
import { ChatMessage, MessageContent } from "./ChatMessage"
import { TypingAnimation } from "./TypingAnimation"

interface ChatMessagesProps{
    messages: ChatMessageResponse[]
    isSendingMessage: boolean
}

export const ChatMessages = ( { messages, isSendingMessage }: ChatMessagesProps) => 
{
  return (
    <div className="h-full overflow-auto pb-32 flex flex-col">
      {messages.map((message: ChatMessageResponse) => 
      {
        return(
          <ChatMessage key={message.createdAt + message.role} role={message.role}>
            <MessageContent>{message.content}</MessageContent>
          </ChatMessage>
        )
      })}
      {isSendingMessage && (
        <ChatMessage role="ASSISTANT">
          <MessageContent>
            <TypingAnimation />
          </MessageContent>
        </ChatMessage>
      )}
    </div>
  )
}