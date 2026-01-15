import { useParams } from "react-router-dom"
import { Button } from "../components/ui/Button";
import { Toolbar } from "../components/layout/Toolbar";
import { useProjects } from "../hooks/useProjects";
import { Chat } from "../components/chat/Chat";
import { useChats } from "../hooks/useChats";

export const ProjectDetails = () => 
{
  const { projectId } = useParams<{projectId: string}>(); 
  const { getCurrentProject } = useProjects();
  const { chats, activeChat, chatMessages, createChat, setActiveChat, sendChatMessage } = useChats(projectId, 'project');

  const currentProject = getCurrentProject(projectId)

  const openChat = (index: number) => 
  {
    const id = chats[index].id;
    setActiveChat(id)
  }

  const handleCreateChat = () => {
    if (projectId){
      createChat(projectId, 'project')
    }   
  }

  return(
    <>
      <Toolbar title={currentProject?.title}>
        <Button onClick={() => handleCreateChat()}>New Chat</Button>
      </Toolbar>
      {activeChat 
        ? <Chat id={activeChat} messages={chatMessages} onSubmit={sendChatMessage}></Chat>
        : chats.map((chat, index) => 
        {
          return (
            <div key={index} onClick={() => 
            {
              openChat(index)
            }}>{chat.createdAt}</div>
          )
        })
      }
    </>
  )
}