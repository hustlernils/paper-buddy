import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { useFetch } from "../hooks/useFetch";
import { Button } from "../components/ui/Button";
import { Toolbar } from "../components/layout/Toolbar";
import { useProjects } from "../hooks/useProjects";
import { Chat } from "../components/chat/Chat";

interface ChatResponse {
    id: string,
    createdAt: string
};

export interface ChatMessageResponse {
    role: string, 
    content: string,
    createdAt: string
}

interface CreateChatRequest {
    parentType: string,
    parentId: string
}

export const ProjectDetails = () => 
{
  const { api } = useFetch();
  const { projectId } = useParams<{projectId: string}>(); 
  const { projects } = useProjects();
  const [chats, setChats] = useState<ChatResponse[]>([]);
  const [activeChat, setActiveChat] = useState<string | null>(null)
  const [chatMessages, setChatMessages] = useState<ChatMessageResponse[]>([])

  const currentProject = projects.find(p => p.id === projectId)

  const fetchChats = async () => 
  {
    const chatsResponse = await api.get<ChatResponse[]>(`/chats/${projectId}?parentType=project`)
    if (chatsResponse)
    {
      setChats(chatsResponse)
    }
  }

  const fetchChatMessages = async (chatId : string) => 
  {
    const response = await api.get<ChatMessageResponse[]>(`/chats/${chatId}/messages`)
    if (response)
    {
      setChatMessages(response)
    }
  }

  const createChat = async () => 
  {
    await api.post("/chats", {
      parentType: "Project",
      parentId: projectId
    })
    fetchChats()
  }

  useEffect(() => 
  {
    fetchChats()
  }, [])

  useEffect(() => 
  {
    if (activeChat)
    {
      fetchChatMessages(activeChat)
    }
  }, [activeChat])

  const openChat = (index: number) => 
  {
    const id = chats[index].id;
    setActiveChat(id)
  }

  const sendMessage = async (content: string) =>
  {
    if(activeChat)
    {
      await api.post(`/chats/${activeChat}/messages`, { content: content})
      fetchChatMessages(activeChat)
    }

    setChatMessages(prev => [
      ...prev,
      {content, createdAt: "", role: "User"}])
  }

  return(
    <>
      <Toolbar title={currentProject?.title}>
        <Button onClick={() => createChat()}>New Chat</Button>
      </Toolbar>
      {activeChat 
        ? <Chat id={activeChat} messages={chatMessages} onSubmit={sendMessage}></Chat>
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