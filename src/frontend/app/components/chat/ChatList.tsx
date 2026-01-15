import { type ChatResponse } from "../../types/api"

export interface ChatListProps {
  chats: ChatResponse[],
  openChat: (index: number)  => void
}

export const ChatList = ( { chats, openChat }: ChatListProps ) => {
    return (
      chats.map((chat, index) => 
        {
          return (
            <div key={index} onClick={() => 
            {
              openChat(index)
            }}>{chat.createdAt}</div>
          )
        }
    ))
}

