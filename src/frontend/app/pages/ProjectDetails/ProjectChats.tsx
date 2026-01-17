import { Chat } from "../../components/chat/Chat";
import { ChatList } from "../../components/chat/ChatList";
import { useChats } from "../../hooks/useChats";

export interface ProjectChatsProps {
  projectId: string | undefined
}

export const ProjectChats = ( { projectId }: ProjectChatsProps) => {
  const { chats, activeChat, chatMessages, createChat, setActiveChat, sendChatMessage } = useChats(projectId, 'Project');

  const openChat = (index: number) => 
  {
    const id = chats[index].id;
    setActiveChat(id)
  }

    return (
      <div>
        {activeChat
          ? <Chat id={activeChat} messages={chatMessages} onSubmit={sendChatMessage}></Chat>
          : <ChatList chats={chats} openChat={openChat}/>
        }
      </div>
    )
}