import { type ChatResponse } from "../../types/api"
import {
  Item,
  ItemContent,
  ItemDescription,
  ItemTitle,
  ItemGroup
} from "../ui/item"

interface ChatItemProps {
  chat: ChatResponse
  onClick: () => void
}

const ChatItem = ( {chat, onClick}: ChatItemProps) => {
  return (
  <div className="flex w-full flex-col gap-6" onClick={onClick}>
        <Item variant="outline">
          <ItemContent>
            <ItemTitle>{`There is no title yet, so here is an id: ${chat.id}`}</ItemTitle>
            <ItemDescription>
              A dummy description
            </ItemDescription>
          </ItemContent>
        </Item>
    </div>
  )  
}

export interface ChatListProps {
  chats: ChatResponse[],
  openChat: (index: number)  => void
}

export const ChatList = ( { chats, openChat }: ChatListProps ) => {
    return (
      <ItemGroup>
        {chats.map((chat, index) => (
            <ChatItem key={`chat-${index}`} chat={chat} onClick={() => openChat(index)}/>
          ))}
      </ItemGroup>      
    )
}

